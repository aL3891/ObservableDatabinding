using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using System.Windows.Input;

namespace ObservableDatabinding
{
    class ViewModel
    {
		public ObservableProperty<string> OneWay { get; set; } = new ObservableProperty<string>("hello");
		public ObservableProperty<string> TwoWay { get; set; } = new ObservableProperty<string>("world");
		public ObservableCommand<string> Command { get; set; } = new ObservableCommand<string>();
		public ObservableProperty<int> ClicksPerSec { get; set; } = new ObservableProperty<int>();

		public ViewModel()
        {
            OneWay.Observable = TwoWay.Observable.Select(s => s.ToUpper());
            Command.CanExecute.Observable = TwoWay.Observable.Select(s => s.Length > 5);
			ClicksPerSec.Observable = Command.Command.Observable.Buffer(TimeSpan.FromSeconds(0.3)).Select(d => d.Count).ObserveOnDispatcher();
		}
    }
}
