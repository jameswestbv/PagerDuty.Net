using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PagerDuty.Net.TestForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnTrigger_Click(object sender, EventArgs e)
        {

            PagerDutyTriggerAPI client = new PagerDutyTriggerAPI();

            TriggerEventResponse resp = client.RaiseTrigger(txtServiceKey.Text,
                                                            txtIncidentKey.Text,
                                                            "Service Failed",
                                                            "", 
                                                            "",
                                                            "{data:some}");


            MessageBox.Show(resp.message);


        }
    }
}
