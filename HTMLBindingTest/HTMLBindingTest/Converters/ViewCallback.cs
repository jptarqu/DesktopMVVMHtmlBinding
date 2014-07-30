using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTMLBindingTest.Converters
{
    [System.Runtime.InteropServices.ComVisible(true)]
    public class ViewCallback
    {
        public Action<int, object> OnNewValueCallback { get; set; }
        public void OnNewValue(int obj_id, object new_value)
        {
            OnNewValueCallback(obj_id, new_value);
        }
    }
}
