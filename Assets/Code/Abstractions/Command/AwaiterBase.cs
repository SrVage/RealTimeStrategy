using System;

namespace Code.Abstractions.Command
{
    public abstract class AwaiterBase<T>:IAwaiter<T>
    {
        public bool IsCompleted => _isCompleted;
        protected event Action _continuation;
        protected bool _isCompleted;
        private T _result;
        
        public void OnCompleted(Action continuation)
        {
            if (_isCompleted)
                continuation?.Invoke();
            else
                _continuation = continuation;
        }
        
        protected void ONWaitFinish(T result)
        {
            _result = result;
            _isCompleted = true;
            _continuation?.Invoke();
        }

        public T GetResult() => _result;
    }
}