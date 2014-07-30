using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTMLBindingTest.Converters
{
    class ObservedWrapper
    {
        public System.ComponentModel.INotifyPropertyChanged ObservedObj { get; set; }

        public int ID { get; set; }
    }
}
