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

[assembly: CommandClass(typeof(TestSample.BT1397))]

namespace TestSample
{
	class BT1397
	{
		//コマンドグループ名.コマンド名の書式でコマンドが指定できない
		[CommandMethod("NetCommand", "BT1397", CommandFlags.Modal)]
		public void Command()
		{
			Application.ShowAlertDialog("BT1397");
		}		
	}
}
