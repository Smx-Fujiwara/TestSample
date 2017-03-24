using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

#if IJCAD
using CADApp = GrxCAD.ApplicationServices.Application;
#else
using CADApp = Autodesk.AutoCAD.ApplicationServices.Application;
#endif

namespace TestSample
{
	public partial class BT1486Form : Form
	{
		public BT1486Form()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			string command = "\x03_LINE 0,0 100,100  ";
			CADApp.DocumentManager.MdiActiveDocument.SendStringToExecute(command, true, false, true);
		}

		private void button2_Click(object sender, EventArgs e)
		{
			string command = "\x1b_CIRCLE 50,50 50 ";
			CADApp.DocumentManager.MdiActiveDocument.SendStringToExecute(command, true, false, true);
		}
	}
}
