using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace HTMLBindingTest.ViewModels
{
    class Customer : Serialized.Cirrious.MvvmCross.ViewModels.MvxViewModel
    {

        private string _CustName;
        public string CustName
        {
            get { return _CustName; }
            set
            {
                if (_CustName != value)
                {
                    _CustName = value;
                    RaisePropertyChanged(() => CustName);
                }
            }
        }

        public ObservableCollection<Order> Orders { get; set; }

        private int _CustNum;
        public int CustNum
        {
            get { return _CustNum; }
            set
            {
                if (_CustNum != value)
                {
                    _CustNum = value;
                    RaisePropertyChanged(() => CustNum);
                }
            }
        }

    }
}
