using System;

namespace Code.Abstractions
{
    public interface ITimeModel
    {
        IObservable<int> GameTime { get; }
    }
}