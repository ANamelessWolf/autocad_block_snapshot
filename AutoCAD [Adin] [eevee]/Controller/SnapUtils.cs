using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.GraphicsInterface;
using Autodesk.AutoCAD.GraphicsSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nameless.AutoCAD.eevee.Controller
{
    /// <summary>
    /// A group of tools to create a snap shot
    /// </summary>
    public static class SnapUtils
    {
        /// <summary>
        /// Snapshots a block table record to a file.
        /// </summary>
        /// <param name="file">The file where the snapshot is taken.</param>
        /// <param name="blockname">The name of block.</param>
        /// <param name="vst">The Visual style of the block.</param>
        public static void Snapshot(this FileInfo file, string blockname, VisualStyleType vst)
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Editor ed = doc.Editor;
            Database db = doc.Database;
            Manager gsm = doc.GraphicsManager;
            int vpn = System.Convert.ToInt32(Application.GetSystemVariable("CVPORT"));
            View gsv = gsm.GetCurrentAcGsView(vpn);
            using (View view = new View())
            {
                view.SetView(
                  gsv.Position,
                  gsv.Target,
                  gsv.UpVector,
                  gsv.FieldWidth,
                  gsv.FieldHeight);
        }
        }
        /// <summary>
        /// Runs a void transaction
        /// </summary>
        /// <param name="task">The task to run.</param>
        public static void VoidTransaction(Action<Document, Transaction> task)
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor ed = doc.Editor;
            using (Transaction tr = doc.TransactionManager.StartTransaction())
            {
                try
                {
                    task(doc, tr);
                    tr.Commit();
                }
                catch (Exception exc)
                {
                    tr.Dispose();
                    ed.WriteMessage(exc.Message);
                }
            }
        }
    }
}
