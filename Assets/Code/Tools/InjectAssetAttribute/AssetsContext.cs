using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Tools.InjectAssetAttribute
{
    [CreateAssetMenu(order = 3, menuName = "Config/AssetContext")]
    public class AssetsContext:ScriptableObject
    {
        [SerializeField] private Object[] _objects;

        public Object GetObjectOfType(Type targetType, string targetName = null)
        {
            for (int i = 0; i < _objects.Length; i++)
            {
                var obj = _objects[i];
                if (obj.GetType().IsAssignableFrom(targetType))
                {
                    if (targetName == null || obj.name == targetName)
                    {
                        Debug.Log(obj);
                        return obj;
                    }
                }
            }
            return null;
        }
    }
}