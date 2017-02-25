using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ObservableDatabinding
{
    internal static class Shared
    {
        internal static PropertyChangedEventArgs ValueChanged = new PropertyChangedEventArgs("Value");
    }
}
