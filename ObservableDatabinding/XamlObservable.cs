using System;
using System.ComponentModel;

namespace ObservableDatabinding
{

    public class XamlObservable<T> : INotifyPropertyChanged
    {
        IDisposable subscription;
        IObservable<T> observable;
        public event PropertyChangedEventHandler PropertyChanged;
        public T Value { get; private set; }
        public IObservable<T> Observable
        {
            get { return observable; }
            set
            {
                subscription?.Dispose();
                observable = value;
                subscription = observable.Subscribe(t =>
                {
                    Value = t;
                    PropertyChanged?.Invoke(this, Shared.ValueChanged);
                });
            }
        }
    }

}
