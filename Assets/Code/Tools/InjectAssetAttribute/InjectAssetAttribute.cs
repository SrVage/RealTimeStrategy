using System;

namespace Code.Tools.InjectAssetAttribute
{
    [AttributeUsage(AttributeTargets.Field)]
    public class InjectAssetAttribute:Attribute
    {
        public readonly string AssetName;

        public InjectAssetAttribute(string name = null)
        {
            AssetName = name;
        }
    }
}