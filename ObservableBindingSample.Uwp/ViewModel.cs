using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using Windows.UI.Xaml.Input;

namespace ObservableDatabinding
{
    public class ViewModel
    {
        public XamlObservable<string> OneWay { get; set; } = new XamlObservable<string>();
        public XamlSubject<string> TwoWay { get; set; } = new XamlSubject<string>();
        public XamlEventObservable<PointerRoutedEventArgs> MouseEvents { get; set; } = new XamlEventObservable<PointerRoutedEventArgs>();
        public XamlCommandObservable<string> Command { get; set; } = new XamlCommandObservable<string>();

        public ViewModel()
        {
            OneWay.Observable = TwoWay.Observable.Select(s => s.ToUpper()).Merge(MouseEvents.Observable.Select(m => m.GetCurrentPoint(null).Position.X.ToString()));
            Command.CanExecute = TwoWay.Observable.Select(s => s.Length > 5);
        }
    }
}
