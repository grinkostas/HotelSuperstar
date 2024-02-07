using System;


public abstract partial class ABaseSignal<T>
{
    public class Callback
    {
        public T Handler { get; private set; }
        private int _repeatCount; 
        
        public int Priority { get; private set; }

        public void Handle()
        {
            if (_repeatCount > 0)
                _repeatCount--;

            if (IsAvailable() == false)
                return;
        }

        public bool IsAvailable()
        {
            return _repeatCount < 0 || _repeatCount > 0;
        }

        public Callback(T handler, int repeatCount = -1, int priority = 1)
        {
            _repeatCount = repeatCount;
            Priority = priority;
            Handler = handler;
        }
    }
}