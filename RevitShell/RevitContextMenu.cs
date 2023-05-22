using ricaun.Revit.FileInfo;
using ricaun.Revit.FileInfo.Utils;
using ricaun.Revit.Installation;
using SharpShell.Attributes;
using SharpShell.SharpContextMenu;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace RevitShell
{
    [ComVisible(true)]
    [COMServerAssociation(AssociationType.ClassOfExtension, RevitExtension.Project)]
    [COMServerAssociation(AssociationType.ClassOfExtension, RevitExtension.Family)]
    [COMServerAssociation(AssociationType.ClassOfExtension, RevitExtension.ProjectTemplate)]
    [COMServerAssociation(AssociationType.ClassOfExtension, RevitExtension.FamilyTemplate)]
    public class RevitContextMenu : SharpContextMenu
    {
        protected override bool CanShowMenu()
        {
            return true;
        }

        protected override ContextMenuStrip CreateMenu()
        {
            var menu = new ContextMenuStrip();

            var title = nameof(RevitFileInfo) + " " + typeof(RevitFileInfo).Assembly.GetName().Version.ToString(3);

            var itemRevitVersionInfo = new ToolStripMenuItem
            {
                Text = nameof(RevitFileInfo),
                //Image = Properties.Resources.Revit.ToBitmap(),
            };

            itemRevitVersionInfo.Click += (sender, args) =>
            {
                var builder = new StringBuilder();

                bool ValidRevitFile(string revitFile)
                {
                    try
                    {
                        return new RevitFileInfo(revitFile).IsValid;
                    }
                    catch { }
                    return false;
                }

                var revitFiles = SelectedItemPaths.Where(ValidRevitFile).ToList();

                if (revitFiles.Any() == false)
                    return;

                foreach (var revitFile in revitFiles)
                {
                    var message = string.Format("[{1}] {0}", Path.GetFileName(revitFile), RevitFileInfo.GetRevitVersion(revitFile));
                    builder.AppendLine(message);
                }

                var revitFileFirst = revitFiles.First();
                RevitInstallationUtils.InstalledRevit.TryGetRevitInstallation(RevitFileInfo.GetRevitVersion(revitFileFirst), out RevitInstallation revitInstallation);

                if (revitFiles.Count == 1 && revitInstallation != null)
                {
                    var dialogResult = MessageBox.Show(
                        $"Open file using Revit.exe version {revitInstallation.Version}?\n\n" +
                        builder.ToString(),
                        title,
                        MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.None,
                        MessageBoxDefaultButton.Button1);

                    if (dialogResult == DialogResult.Yes)
                    {
                        revitInstallation.Start($"\"{revitFileFirst}\"");
                    }
                }
                else
                {
                    MessageBox.Show(builder.ToString(), title);
                }
            };

            menu.Items.Add(itemRevitVersionInfo);

            return menu;
        }
    }
}