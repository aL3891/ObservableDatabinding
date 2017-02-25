using System;
using System.ComponentModel;
using System.Reactive.Subjects;

namespace ObservableDatabinding
{
    public class XamlSubject<T> : INotifyPropertyChanged
    {
        Subject<T> subject = new Subject<T>();
        T value;
        public event PropertyChangedEventHandler PropertyChanged;
        public IObservable<T> Observable => subject;

        public T Value
        {
            get { return value; }
            set
            {
                this.value = value;
                subject.OnNext(value);
                PropertyChanged?.Invoke(this, Shared.ValueChanged);
            }
        }
    }

}
