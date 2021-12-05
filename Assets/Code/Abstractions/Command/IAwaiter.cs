using System;
using System.Runtime.CompilerServices;

namespace Code.Abstractions.Command
{
    public interface IAwaiter<TAwaited>:INotifyCompletion
    {
        bool IsCompleted { get; }
        TAwaited GetResult();
    }
}