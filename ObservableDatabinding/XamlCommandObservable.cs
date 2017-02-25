using System;
using System.Reactive.Subjects;
using System.Windows.Input;

namespace ObservableDatabinding
{
    public class XamlCommandObservable<T> : ICommand
    {
        Subject<T> commandSubject = new Subject<T>();
        IObservable<bool> canExecuteObservable;
        IDisposable subscription;
        bool canExecute = true;
        public event EventHandler CanExecuteChanged;

        public IObservable<T> Command => commandSubject;
        public IObservable<bool> CanExecute
        {
            get { return canExecuteObservable; }
            set
            {
                subscription?.Dispose();
                canExecuteObservable = value;
                subscription = canExecuteObservable.Subscribe(t =>
                {
                    this.canExecute = t;
                    CanExecuteChanged?.Invoke(this, new EventArgs());
                });
            }
        }

        bool ICommand.CanExecute(object parameter) => canExecute;
        void ICommand.Execute(object parameter) => commandSubject.OnNext((T)parameter);
    }

}
