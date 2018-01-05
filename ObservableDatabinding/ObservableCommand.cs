using System;
using System.Reactive.Linq;
using System.Windows.Input;

namespace ObservableDatabinding
{
	public class ObservableCommand<T> : ICommand
	{
		public ObservableProperty<T> Command { get; } = new ObservableProperty<T>();
		public ObservableProperty<bool> CanExecute { get; } = new ObservableProperty<bool>();
		public event EventHandler CanExecuteChanged;

		public ObservableCommand()
		{
			CanExecute.Observable.Subscribe(t => CanExecuteChanged?.Invoke(this, new EventArgs()));
		}

		bool ICommand.CanExecute(object parameter) => CanExecute.Value;
		void ICommand.Execute(object parameter) => Command.Value = (T)parameter;
	}

}
