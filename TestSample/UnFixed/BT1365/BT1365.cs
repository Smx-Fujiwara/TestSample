using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if IJCAD
using GrxCAD.ApplicationServices;
using GrxCAD.Runtime;
using GrxCAD.Windows;
#else
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.Windows;
#endif

[assembly: CommandClass(typeof(TestSample.BT1365))]

namespace TestSample
{
	class BT1365
	{
		//ComponentManager がない
		[CommandMethod("BT1365")]
		public static void Command()
		{
#if !IJCAD
			var ribbon = ComponentManager.Ribbon;
			Application.ShowAlertDialog($"RibbonTab = {ribbon.Tabs.Count()}");
#endif
		}
	}
}
