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
        public Action<int, string, object> OnNewValueCallback { get; set; }
        public void OnNewValue(int obj_id, string prop_name, object new_value)
        {
            OnNewValueCallback(obj_id, prop_name, new_value);
        }
    }
}
