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

namespace HTMLBindingTest
{
    public partial class AwsomiumTest : Form
    {
        public AwsomiumTest()
        {
            InitializeComponent();
            var full_path_to_view = Path.GetFullPath(@"Views\Form.html");
            Uri uri = new Uri(full_path_to_view);
            webControl1.Source = uri;
        }
    }
}
