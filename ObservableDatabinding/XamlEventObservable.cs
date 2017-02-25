using System;
using System.Reactive.Subjects;

namespace ObservableDatabinding
{
    public class XamlEventObservable<S, T>
    {
        Subject<Tuple<S, T>> subject = new Subject<Tuple<S, T>>();
        public IObservable<Tuple<S, T>> Observable { get { return subject; } }
        public void Handler(S sender, T e)
        {
            subject.OnNext(Tuple.Create(sender, e));
        }
    }

    public class XamlEventObservable<T>
    {
        Subject<T> subject = new Subject<T>();
        public IObservable<T> Observable { get { return subject; } }
        public void Handler(object sender, T e)
        {
            subject.OnNext(e);
        }
    }

    public class XamlEventObservable : XamlEventObservable<object> { }
}
