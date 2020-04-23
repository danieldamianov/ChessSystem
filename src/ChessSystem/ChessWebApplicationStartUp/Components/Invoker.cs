using System;
using Microsoft.JSInterop;

namespace ChessWebApplicationStartUp.Components
{
    public class Invoker
    {
        private Action _action;

        public Invoker(Action action)
        {
            _action = action;
        }

        [JSInvokable("BlazorSample")]
        public void UpdateMessageCaller()
        {
            _action.Invoke();
        }
    }
}
