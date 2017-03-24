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

[assembly: CommandClass(typeof(TestSample.BT1312))]

namespace TestSample
{
	class BT1312
	{
		//ボタンを押したイベントハンドラで止められない
		[CommandMethod("BT1312")]
		public static void Command()
		{
			var frm = new BT1312Form();
			Application.ShowModalDialog(frm);
		}
	}
}
