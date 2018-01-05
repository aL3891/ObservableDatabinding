using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using Windows.UI.Xaml.Input;

namespace ObservableDatabinding
{
    public class ViewModel
    {
        public ObservableProperty<string> OneWay { get; set; } = new ObservableProperty<string>();
        public ObservableProperty<string> TwoWay { get; set; } = new ObservableProperty<string>();
        public ObservableProperty<PointerRoutedEventArgs> MouseEvents { get; set; } = new ObservableProperty<PointerRoutedEventArgs>();
        public ObservableCommand<string> Command { get; set; } = new ObservableCommand<string>();
		public ObservableProperty<int> ClicksPerSec { get; set; } = new ObservableProperty<int>();


		public ViewModel()
        {
            OneWay.Observable = TwoWay.Observable.Select(s => s.ToUpper()).Merge(MouseEvents.Observable.Select(m => m.GetCurrentPoint(null).Position.X.ToString()));
            Command.CanExecute.Observable = TwoWay.Observable.Select(s => s.Length > 5);
			ClicksPerSec.Observable = Command.Command.Observable.Buffer(TimeSpan.FromSeconds(1)).Select(d => d.Count);
        }
    }
}
