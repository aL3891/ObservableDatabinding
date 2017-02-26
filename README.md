# Observable databinding

Typically when databinding in xaml apps ther are two options, Dependency properties and INotifyPropertyChanged. Both have advantages but they are both a little verbose or require base class helpers. Another disadvantage of these approaches is that they are hard to compose, that is, its hard to express dependencies between properties in the viewmodel. For example if a property on the view model is supposed to be based on the value of another property, this requires imperative code. This is file for simple scenarios (even if that also gets a little messy) but for complex scenarios like throttling based on a timeout, like for search box suggestions for example, it get a lot more complicated 

IObservables provide a very nice way to compose streams of events like that, and also has alot of buit in operators for dealing with things like throttling, buffering and filtering. However there is no built in support for binding to and from observables. That is what this library provides

Now you can have a view model that looks like this:

    public class ViewModel
    {
        public XamlObservable<string> MyProperty { get; set; } = new XamlObservable<string>();
    }
	
and bind to it like this:

    <TextBox Text="{x:Bind Model.MyProperty.Value, Mode=OneWay}"></TextBox>


The Observable of MyProperty can then be set to any observable and the binding will update for each value:

    OneWay.Observable = Observable.Interval(TimeSpan.FromSeconds(1)).Select(t => t.ToString());
