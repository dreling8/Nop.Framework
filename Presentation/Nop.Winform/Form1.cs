using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Nop.Core.Infrastructure;
using Nop.Services.Tests;

namespace Nop.Winform
{
    public partial class Form1 : Form, IRegistrarForm
    {
        private ITestService _testService;

        public Form1(
            ITestService testService
            )
        {
            InitializeComponent();
            _testService = testService;
            //如果不注入form可以使用EngineContext.Current.Resolve<ITestService>(); 得到实例 

        }

        private void button1_Click(object sender, EventArgs e)
        { 
            var tests = _testService.GetAllTests();
        }
    }
}
