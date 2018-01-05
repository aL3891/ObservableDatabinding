using System;
using System.ComponentModel;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace ObservableDatabinding
{
	public class ObservableProperty<T> : INotifyPropertyChanged
	{
		private Subject<IObservable<T>> observables = new Subject<IObservable<T>>();
		private IObservable<T> outputObservable;
		Subject<T> valueSubject = new Subject<T>();
		private T value;

		public event PropertyChangedEventHandler PropertyChanged;

		public ObservableProperty()
		{
			outputObservable = System.Reactive.Linq.Observable.Switch(observables).Merge(valueSubject);

			outputObservable.Subscribe(t =>
			{
				value = t;
				PropertyChanged?.Invoke(this, Shared.ValueChanged);
			});
		}

		public T Value
		{
			get { return value; }
			set { valueSubject.OnNext(value); }
		}

		public IObservable<T> Observable
		{
			get { return outputObservable; }
			set { observables.OnNext(value); }
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
