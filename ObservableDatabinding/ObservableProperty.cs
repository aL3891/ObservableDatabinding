using System;
using System.ComponentModel;
using System.Reactive.Subjects;

namespace ObservableDatabinding
{
	public class ObservableProperty<T> : INotifyPropertyChanged
	{
		private Subject<IObservable<T>> observables;
		private IObservable<T> observable;
		Subject<T> valueSubject;
		private T value;
		private bool valueSubjectInUse;

		public event PropertyChangedEventHandler PropertyChanged;

		public ObservableProperty()
		{
			observables = new Subject<IObservable<T>>();
			observable = System.Reactive.Linq.Observable.Switch(observables);

			observable.Subscribe(t =>
			{
				value = t;
				PropertyChanged?.Invoke(this, Shared.ValueChanged);
			});
		}

		public T Value
		{
			get { return value; }
			set
			{
				if (valueSubject == null)
					valueSubject = new Subject<T>();

				if (!valueSubjectInUse)
				{
					observables.OnNext(valueSubject);
					valueSubjectInUse = true;
				}

				valueSubject.OnNext(value);
			}
		}

		public IObservable<T> Observable
		{
			get { return observable; }
			set
			{
				observables.OnNext(value);
				valueSubjectInUse = false;
			}
		}

		public void Handler(object sender, T e)
		{
			Value = e;
		}
	}

	public class ObservableProperty<TEventSender, T> : ObservableProperty<(TEventSender, T)>
	{
		public void Handler(TEventSender sender, T e)
		{
			Value = (sender, e);
		}
	}
}
