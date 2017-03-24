using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if IJCAD
using GrxCAD.ApplicationServices;
using GrxCAD.Runtime;
#else
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Runtime;
#endif

[assembly: CommandClass(typeof(TestSample.BT1486))]

namespace TestSample
{
	class BT1486
	{
		//SendStringToExecute()のコマンド中断は 0x03 でもできる
		[CommandMethod("BT1486")]
		public void Command()
		{
			var frm = new BT1486Form();
#if IJCAD
			Application.ShowModelessDialog(Application.MainWindow, frm);
#else
			Application.ShowModelessDialog(Application.MainWindow.Handle, frm);
#endif
		}
	}
}
