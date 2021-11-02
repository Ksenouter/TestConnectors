using System;
using System.Collections.Generic;

namespace Events
{
    public class ParamEvent<T>
    {
        
        private readonly List<Action<T>> _callbacks = new List<Action<T>>();

        public void Subscribe(Action<T> callback)
        {
            _callbacks.Add(callback);
        }

        public void Publish(T value)
        {
            foreach (var callback in _callbacks)
                callback(value);
        }

        public void Unsubscribe(Action<T> callback)
        {
            _callbacks.Remove(callback);
        }
        
    }
}
