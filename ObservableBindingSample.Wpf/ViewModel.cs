using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using System.Windows.Input;

namespace ObservableDatabinding
{
    class ViewModel
    {
        public XamlObservable<string> OneWay { get; set; } = new XamlObservable<string>();
        public XamlSubject<string> TwoWay { get; set; } = new XamlSubject<string>();
        public XamlCommandObservable<string> Command { get; set; } = new XamlCommandObservable<string>();

        public ViewModel()
        {
            OneWay.Observable = TwoWay.Observable.Select(s => s.ToUpper());
            Command.CanExecute = TwoWay.Observable.Select(s => s.Length > 5);
        }
    }
}
