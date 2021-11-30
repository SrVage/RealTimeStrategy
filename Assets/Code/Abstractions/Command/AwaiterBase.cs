using System;

namespace Code.Abstractions.Command
{
    public abstract class AwaiterBase<T>:IAwaiter<T>
    {
        public bool IsCompleted => _isCompleted;
        protected event Action _continuation;
        protected bool _isCompleted;

        protected void StartEvent() => _continuation?.Invoke();

        public void OnCompleted(Action continuation)
        {
            if (_isCompleted)
                continuation?.Invoke();
            else
                _continuation = continuation;
        }

        public abstract T GetResult();
    }
}