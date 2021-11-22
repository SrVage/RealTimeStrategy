using System;
using System.Reflection;
using UnityEngine;

namespace Code.Tools.InjectAssetAttribute
{
    public static class AssetInjector
    {
        private static readonly Type _injectAssetAttributeType = typeof(InjectAssetAttribute);
        public static T Inject<T>(this AssetsContext assetsContext, T target)
        {
            Type type = target.GetType();
            FieldInfo[] allFields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            for (int i = 0; i < allFields.Length; i++)
            {
                FieldInfo field = allFields[i];
                InjectAssetAttribute injectAttributes = (InjectAssetAttribute)field.GetCustomAttribute(_injectAssetAttributeType);
                if (injectAttributes == null)
                    continue;
                var objectToInject = assetsContext.GetObjectOfType(field.FieldType, injectAttributes.AssetName);
                field.SetValue(target, objectToInject);
            }
            return target;
        }
    }
}