using UniRx;

namespace Code.Tools
{
    public static class CollectionExtensions
    {
        public static void RemoveAtIndex<T>(this ReactiveCollection<T> collection, int index)
        {
            for (int i = index; i < collection.Count-1; i++)
            {
                collection[i] = collection[i + 1];
            }
            collection.RemoveAt(collection.Count-1);
        }
    }
}