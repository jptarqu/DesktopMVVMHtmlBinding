using HTMLBindingTest.Converters;
using HTMLBindingTest.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestReportingFramework.ViewModels;

namespace HTMLBindingTest
{
    public partial class Form1 : Form
    {
        private NETToJS _converter;
        public Form1()
        {
            BrowserHelper.BrowserFeatureControl.SetBrowserFeatureControl();
            InitializeComponent();
            InitializeMvvmcross();
            var full_path_to_view = Path.GetFullPath(@"Views\Form.html");
            Uri uri = new Uri(full_path_to_view);
            webBrowser1.Navigate(uri);

            webBrowser1.DocumentCompleted += webBrowser1_DocumentCompleted;
            
        }
        public void InitializeMvvmcross()
        {
            var cross_setup = new MvvmCrossSetup();
            cross_setup.Setup();
        }
        void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            InjectModels();
        }

        private void InjectModels()
        {
            HtmlDocument doc = webBrowser1.Document;
            var customer = new Customer() { CustName ="Juan", CustNum = 2 };
            _converter = new NETToJS(doc, customer);
            webBrowser1.ObjectForScripting = new ViewCallback() { OnNewValueCallback = _converter.OnNewValue };
            customer.CustName = "cambiado";
//            HtmlElement head = doc.GetElementsByTagName("head")[0];
//            HtmlElement s = doc.CreateElement("script");
//            s.SetAttribute("text", @"function InitKO() {
//    var SimpleListModel = function (items) {
//        this.items = ko.observableArray(items);
//        this.itemToAdd = ko.observable('');
//        this.addItem = function () {
//            if (this.itemToAdd() != '') {
//                this.items.push(this.itemToAdd()); 
//                this.itemToAdd(''); 
//            }
//        }.bind(this);  
//    };
//
//    ko.applyBindings(new SimpleListModel(['Alpha', 'Beta', 'Gamma']));
//}");
//            head.AppendChild(s);
//            webBrowser1.Document.InvokeScript("InitKO");
        }
    }
}
