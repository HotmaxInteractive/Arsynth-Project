using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;

namespace U3D.KVO
{
    public class ReadOnlyListObserving<T>
    {
        protected List<T> m_value = new List<T>();
        public List<T> get
        {
            get
            {
                return m_value;
            }
        }    
        protected void SetValue(List<T> v)
        {
            m_value = v;
            Action<ReadOnlyCollection<T>>[] list = new Action<ReadOnlyCollection<T>>[m_observers.Count];
            m_observers.CopyTo(list, 0);
            NotifyObservers(list);
        }
        List<Action<ReadOnlyCollection<T>>> m_observers = new List<Action<ReadOnlyCollection<T>>>();
        public void RegisterObserver(Action<ReadOnlyCollection<T>> action)
        {
            m_observers.Add(action);
            action(m_value.AsReadOnly());
        }
        public void RemoveObserver(Action<ReadOnlyCollection<T>> action)
        {
            m_observers.Remove(action);
        }
        private void NotifyObservers(Action<ReadOnlyCollection<T>>[] list)
        {
            foreach (Action<ReadOnlyCollection<T>> a in list)
            {
                a(m_value.AsReadOnly());
            }
        }
    }
    
    public class ListObserving<T> : ReadOnlyListObserving<T>
    {
        public List<T> set
        {
            set
            {
                SetValue(value);
            }
        }
    }
}