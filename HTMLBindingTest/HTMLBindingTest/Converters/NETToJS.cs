using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utils;

namespace HTMLBindingTest.Converters
{
    class NETToJS
    {
        private Type[] _primitive_types = {
                                              typeof(Nullable<Int16>) 
            ,typeof(Nullable<int>) 
            ,typeof(Nullable<float>) 
            ,typeof(Nullable<double>) 
            ,typeof(Nullable<decimal>) 
            ,typeof(Nullable<DateTime>)

            ,typeof(bool)
            ,typeof(int) 
            ,typeof(Int16) 
            ,typeof(float) 
            ,typeof(double) 
            ,typeof(decimal) 
            ,typeof(DateTime)
                                    
            ,typeof(String)              
            ,typeof(string)    
                                          };
        private Type[] _js_literal_types = {
                                      

            typeof(bool)
            ,typeof(int) 
            ,typeof(Int16) 
            ,typeof(float) 
            ,typeof(double) 
            ,typeof(decimal) 
                           
                                          };

        private LinkedList<Type> _types_already_defined_in_js = new LinkedList<Type>();
        private LinkedList<ObservedWrapper> _observed_objs = new LinkedList<ObservedWrapper>();
        public HtmlElement HeadElement { get; set; }
        //public WebBrowser Browser { get; set; }
        public HtmlDocument Document { get; set; }


        public NETToJS(HtmlDocument doc, INotifyPropertyChanged root_viewmodel)
        {
            Document = doc;
            HeadElement = Document.GetElementsByTagName("head")[0];
            //EvalJS("var observed_objs = []");
            AddObservedObj(root_viewmodel);
            CallJS("InitBindings");
            //EvalJS("ko.applyBindings(observed_objs[0]);");

        }

        public void OnNewValue(int obj_id, object new_value)
        {

        }

        public void AddObservedObj(INotifyPropertyChanged to_convert)
        {
            //if (to_convert is INotifyPropertyChanged)
            //{
                var class_name = to_convert.GetType().Name;
                ObservedWrapper new_wrapped_obj = new ObservedWrapper() { ID = IDGenerator.GetID(), 
                    ObservedObj = to_convert };

                var obj_all_props = to_convert.GetType().GetProperties();
                var obj_primitive_props = obj_all_props.Where(p => _primitive_types.Contains(p.PropertyType));

                //if (!_types_already_defined_in_js.Contains(to_convert.GetType()))
                //{
                //    DefineClassInJS();
                //}

                CreateJSInstance(to_convert, class_name, new_wrapped_obj, obj_primitive_props);
            //}
            //else
            //{
            //    return "";
            //}

        }

        private void CreateJSInstance(INotifyPropertyChanged to_convert, string class_name, ObservedWrapper new_wrapped_obj, IEnumerable<System.Reflection.PropertyInfo> obj_primitive_props)
        {
            int new_obj_id = new_wrapped_obj.ID;
            string new_obj_id_str = new_wrapped_obj.ID.ToString();

            CallJS("Create_" + class_name, new_obj_id.ToString());

            foreach (var primitive_prop in obj_primitive_props)
            {
                //Register for prop change
                to_convert.PropertyChanged += (sender, evt)
                =>
                {
                    PropertyChangedOn(sender, evt, new_obj_id_str);
                };
                string init_value = GetValueOfProp(to_convert, primitive_prop.Name);
                var func_name = "Set_" + class_name + "_" + primitive_prop.Name;
                CallJS(func_name, new_obj_id_str, init_value);
            }
        }

        private void DefineClassInJS(Type net_class)
        {
            var obj_all_props =net_class.GetProperties();
            var obj_primitive_props = obj_all_props.Where(p => _primitive_types.Contains(p.PropertyType));
            DefineClassInJS(net_class.Name, obj_primitive_props);
        }
        private void DefineClassInJS(string class_name, IEnumerable<System.Reflection.PropertyInfo> obj_primitive_props)
        {
            StringBuilder js_class = new StringBuilder("\nvar ");
            js_class.Append(class_name);
            js_class.AppendLine(" = function () {");
            string prop_name;
            foreach (var primitive_prop in obj_primitive_props)
            {
                prop_name = primitive_prop.Name;
                //prop_value = (primitive_prop.GetValue(to_convert) ?? "").ToString();
                //if (!_js_literal_types.Contains(primitive_prop.PropertyType))
                //{
                //    prop_value = "'" + prop_value + "'";
                //}
                js_class.Append("\nthis.");
                js_class.Append(prop_name);
                js_class.AppendLine(" = ko.observable();");
                //js_class.Append(prop_value);
                //js_class.AppendLine(");");

            }
            js_class.AppendLine("};");
            InjectClassDefintion(js_class.ToString());
        }

        /// <summary>
        /// Fires when a property changes in .NET and needs to notify JS
        /// 
        /// 
        /// </summary>
        /// <param name="new_obj_id">must be the id of an object observed</param>
        /// <param name="sender"></param>
        /// <param name="evt"></param>
        private void PropertyChangedOn(object sender, PropertyChangedEventArgs evt, string obj_id_str)
        {
                string new_value = GetValueOfProp(sender, evt.PropertyName);
                var func_name = "Set_" + sender.GetType().Name + "_" + evt.PropertyName;
                CallJS(func_name, obj_id_str, new_value);
        }

        private string AssignValueToJSObj(int obj_id, string prop_name, string new_value)
        {
            return "observed_objs[" + obj_id.ToString() + "]." + prop_name + "(" + new_value + ");";
        }

        private void InjectClassDefintion(string class_definition)
        {
            HtmlElement s = Document.CreateElement("script");
            s.SetAttribute("text", class_definition);
            HeadElement.AppendChild(s);
        }

        private void CallJS(string js_func_name, params string[] js_parameter_values)
        {
            Document.InvokeScript(js_func_name, js_parameter_values);
        }

        private void EvalJS(string js_code)
        {
            Document.InvokeScript("eval", new [] {js_code});
        }
        /// <summary>
        /// Gets the value of a property of an object by name
        /// </summary>
        /// <param name="command_obj"></param>
        /// <param name="normal_name"></param>
        /// <returns></returns>
        private string GetValueOfProp(object command_obj, string normal_name)
        {
            string prop_value = "";
            var prop = command_obj.GetType().GetProperty(normal_name);
            if (prop != null)
            {
                prop_value = (prop.GetValue(command_obj) ?? "").ToString();
            }
       
            return prop_value;
        }

    }
}
