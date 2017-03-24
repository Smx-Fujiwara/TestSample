using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.IO;
using System.Diagnostics;

#if IJCAD
using GrxCAD.ApplicationServices;
using GrxCAD.DatabaseServices;
using GrxCAD.PlottingServices;
using GrxCAD.Runtime;
#else
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.PlottingServices;
using Autodesk.AutoCAD.Runtime;
#endif

[assembly: CommandClass(typeof(TestSample.BT1543))]

namespace TestSample
{
	class BT1543
	{
		//印刷できない？ BeginPage でアクセスバイオレーションエラー
		[CommandMethod("BT1543")]
		public void Command()
		{
			Document acDoc = Application.DocumentManager.MdiActiveDocument;
			Database acCurDb = acDoc.Database;
			Application.SetSystemVariable("BACKGROUNDPLOT", 0);
			string plotFile = Path.Combine(Utility.EXPORTXPath, "BT1543", Path.GetFileNameWithoutExtension(acDoc.Name) + ".pdf");
			using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
			{
				BlockTableRecord acLayBlk = acTrans.GetObject(acCurDb.CurrentSpaceId, OpenMode.ForRead) as BlockTableRecord;
				Layout acLayout = acTrans.GetObject(acLayBlk.LayoutId, OpenMode.ForWrite) as Layout;
				PlotSettingsValidator acPlSetVdr = PlotSettingsValidator.Current;
#if IJCAD
				acPlSetVdr.SetPlotType(acLayout, GrxCAD.DatabaseServices.PlotType.Extents);
#else
				acPlSetVdr.SetPlotType(acLayout, Autodesk.AutoCAD.DatabaseServices.PlotType.Extents);
#endif
				acPlSetVdr.SetUseStandardScale(acLayout, true);
				acPlSetVdr.SetStdScaleType(acLayout, StdScaleType.ScaleToFit);
				acPlSetVdr.SetPlotCentered(acLayout, true);
#if IJCAD
				// * IJCADではカスタム用紙名は同時に設定できません
				acPlSetVdr.SetPlotConfigurationName(acLayout, "DWG To PDF.pc3", "");
				// 利用可能な用紙名のリストを取得する
				StringCollection arrCanonicalMediaName = acPlSetVdr.GetCanonicalMediaNameList(acLayout);
				foreach (string strName in arrCanonicalMediaName)
				{
					//印刷設定に渡す用紙名 strName から PLOT[印刷]コマンドの表示用の用紙名 localMediaName を取得する
					string localMediaName = acPlSetVdr.GetLocaleMediaName(acLayout, strName);
					if (localMediaName == "ISO A4 (297.00 x 210.00 MM)")
					{
						acPlSetVdr.SetCanonicalMediaName(acLayout, strName);
						break;
					}
				}
#else
				acPlSetVdr.SetPlotConfigurationName(acLayout, "DWG To PDF.pc3", "ISO_A4_(210.00_x_297.00_MM)");
#endif
				acTrans.Commit();
			}

			using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
			{
				BlockTableRecord acLayBlk = acTrans.GetObject(acCurDb.CurrentSpaceId, OpenMode.ForRead) as BlockTableRecord;
				Layout acLayout = acTrans.GetObject(acLayBlk.LayoutId, OpenMode.ForRead) as Layout;
				using (PlotInfo acPlInfo = new PlotInfo())
				{
					acPlInfo.Layout = acLayout.ObjectId;
					acPlInfo.OverrideSettings = acLayout;
					using (PlotInfoValidator acPlInfoVdr = new PlotInfoValidator())
					{
						acPlInfoVdr.MediaMatchingPolicy = MatchingPolicy.MatchEnabled;
						acPlInfoVdr.Validate(acPlInfo);

						if (PlotFactory.ProcessPlotState == ProcessPlotState.NotPlotting)
						{
							using (PlotEngine acPlEng = PlotFactory.CreatePublishEngine())
							{
								using (PlotProgressDialog acPlProgDlg = new PlotProgressDialog(false, 1, true))
								{
									acPlProgDlg.set_PlotMsgString(PlotMessageIndex.DialogTitle, "Plot Progress");
									acPlProgDlg.set_PlotMsgString(PlotMessageIndex.CancelJobButtonMessage, "Cancel Job");
									acPlProgDlg.set_PlotMsgString(PlotMessageIndex.CancelSheetButtonMessage, "Cancel Sheet");
									acPlProgDlg.set_PlotMsgString(PlotMessageIndex.SheetSetProgressCaption, "Sheet Set Progress");
									acPlProgDlg.set_PlotMsgString(PlotMessageIndex.SheetProgressCaption, "Sheet Progress");
									acPlProgDlg.LowerPlotProgressRange = 0;
									acPlProgDlg.UpperPlotProgressRange = 100;
									acPlProgDlg.PlotProgressPos = 0;
									acPlProgDlg.OnBeginPlot();
									acPlProgDlg.IsVisible = true;
									acPlEng.BeginPlot(acPlProgDlg, null);
									acPlEng.BeginDocument(acPlInfo, acDoc.Name, null, 1, true, plotFile);
									acPlProgDlg.set_PlotMsgString(PlotMessageIndex.Status, "Plotting: " + acDoc.Name + " - " + acLayout.LayoutName);
									acPlProgDlg.OnBeginSheet();
									acPlProgDlg.LowerSheetProgressRange = 0;
									acPlProgDlg.UpperSheetProgressRange = 100;
									acPlProgDlg.SheetProgressPos = 0;

									//* BT1543確認用　ここで例外発生
									using (var acPlPageInfo = new PlotPageInfo())
										acPlEng.BeginPage(acPlPageInfo, acPlInfo, true, null);

									//* ここでも例外発生
									acPlEng.BeginGenerateGraphics(null);
									acPlEng.EndGenerateGraphics(null);

									acPlEng.EndPage(null);
									//* ここまで

									acPlProgDlg.SheetProgressPos = 100;
									acPlProgDlg.OnEndSheet();
									acPlEng.EndDocument(null);
									acPlProgDlg.PlotProgressPos = 100;
									acPlProgDlg.OnEndPlot();
									acPlEng.EndPlot(null);
								}
							}
						}
					}
				}
				acTrans.Commit();
			}
			Application.SetSystemVariable("BACKGROUNDPLOT", 1);
			Process p = Process.Start(plotFile);
		}
	}
}
