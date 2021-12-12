using System;
using System.Reflection;
using Code.Tools.Scriptable;

namespace Code.Tools.InjectAssetAttribute
{
    public static class AssetInjector
    {
        private static readonly Type _injectAssetAttributeType = typeof(InjectAssetAttribute);
        public static T Inject<T>(this AssetsContext assetsContext, T target)
        {
            var targetType = target.GetType();
            while (targetType != null)
            {
                var allFields = targetType.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

                for (int i = 0; i < allFields.Length; i++)
                {
                    var fieldInfo = allFields[i];
                    var injectAssetAttribute = fieldInfo.GetCustomAttribute(_injectAssetAttributeType) as InjectAssetAttribute;
                    if (injectAssetAttribute == null)
                    {
                        continue;
                    }
                    var objectToInject = assetsContext.GetObjectOfType(fieldInfo.FieldType, injectAssetAttribute.AssetName);
                    fieldInfo.SetValue(target, objectToInject);
                }

                targetType = targetType.BaseType;
            }

            return target;
        }
    }
}