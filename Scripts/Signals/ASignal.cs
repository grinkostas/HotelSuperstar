using System;

public class ASignal : ABaseSignal<Action>
{
    public void Dispatch()
    {
        DispatchInternal((callback) => callback.Handler());
    }
}

public class ASignal<T> : ABaseSignal<Action<T>>
{
    public void Dispatch(T t)
    {
        DispatchInternal((callback) => callback.Handler(t));
    }
}

