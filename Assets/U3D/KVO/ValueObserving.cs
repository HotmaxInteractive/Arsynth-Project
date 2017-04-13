using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace U3D.KVO
{
    public class ReadOnlyValueObserving<T>
    {
        protected T m_value = default(T);
        public T get
        {
            get
            {
                return m_value;
            }
        }
        protected void SetValue(T v)
        {
            if (v == null && m_value == null) return;

            if ((m_value != null && v == null) || (m_value == null && v != null) || !m_value.Equals(v))
            {
                m_value = v;
                Action<T>[] list = new Action<T>[m_observers.Count];
                m_observers.CopyTo(list, 0);
                NotifyObservers(list);
            }
        }
        List<Action<T>> m_observers = new List<Action<T>>();
        public void RegisterObserver(Action<T> action, bool launchAction= true)
        {
            m_observers.Add(action);
            if(launchAction)
                action(m_value);
        }
        public void RemoveObserver(Action<T> action)
        {
            m_observers.Remove(action);
        }
        private void NotifyObservers(Action<T>[] list)
        {
            foreach (Action<T> a in list)
            {
                a(m_value);
            }
        }
    }
    
    public class ValueObserving<T> : ReadOnlyValueObserving<T>
    {
        public ValueObserving(T initialValue = default(T)) 
        {
            set = initialValue;
        }

        public T set
        {
            set
            {
                SetValue(value);
            }
        }
    }
}