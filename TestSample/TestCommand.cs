using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static TestSample.Utility;

#if IJCAD
using GrxCAD.ApplicationServices;
using GrxCAD.Runtime;
#else
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Runtime;
#endif

[assembly: CommandClass(typeof(TestSample.TestCommand))]

namespace TestSample
{
	public class TestCommand : IExtensionApplication
	{
		void IExtensionApplication.Initialize()
		{
			var ed = Application.DocumentManager.MdiActiveDocument.Editor;
			ed.WriteMessage("\n.NETアセンブリをロードしました");
		}

		void IExtensionApplication.Terminate()
		{
		}	

	}
}

/*
 * 
 * 
 * 
#if IJCAD
using GrxCAD.ApplicationServices;
using GrxCAD.Colors;
using GrxCAD.DatabaseServices;
using GrxCAD.EditorInput;
using GrxCAD.Export_Import;
using GrxCAD.Geometry;
using GrxCAD.GraphicsSystem;
using GrxCAD.LayerManager;
using GrxCAD.PlottingServices;
using GrxCAD.Publishing;
using GrxCAD.Runtime;
using GrxCAD.Windows;
using GrxCAD.Internal;
using CADGI = GrxCAD.GraphicsInterface;
using CADErrorStatus = GrxCAD.Runtime.ErrorStatus;
#else
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Colors;
using Autodesk.AutoCAD.ComponentModel;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.GraphicsSystem;
using Autodesk.AutoCAD.Internal;
using Autodesk.AutoCAD.LayerManager;
using Autodesk.AutoCAD.PlottingServices;
using Autodesk.AutoCAD.Publishing;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Windows;
using Autodesk.AutoCAD.Internal.Reactors;
using CADGI = Autodesk.AutoCAD.GraphicsInterface;
using CADErrorStatus = Autodesk.AutoCAD.Runtime.ErrorStatus;
#endif

[assembly: CommandClass(typeof(TestSample.))]
 * 
 * 
 * 
 * 
 * 
 */
