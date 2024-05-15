using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.InnoSetup;
using Nuke.Common.Utilities.Collections;
using ricaun.Nuke.Components;
using ricaun.Nuke.Extensions;

interface IBuildInstall : IRelease, ISign, IHazMainProject
{
    Target BuildInstall => _ => _
        .TriggeredBy(Sign)
        .Before(Release)
        .Executes(() =>
        {
            var project = MainProject;
            var projectName = project.Name;
            var solutionDirectory = Solution.Directory;
            var folderOutput = "Install";

            // Deploy File
            var outputInno = project.Directory / "bin" / folderOutput;
            var issFiles = Globbing.GlobFiles(solutionDirectory, $"**/*{projectName}.iss");

            if (issFiles.IsEmpty())
                Serilog.Log.Error($"Not found any .iss file in {solutionDirectory}");

            issFiles.ForEach(file =>
            {
                InnoSetupTasks.InnoSetup(config => config
                    .SetProcessToolPath(NuGetToolPathResolver.GetPackageExecutable("Tools.InnoSetup", "ISCC.exe"))
                    .SetScriptFile(file)
                    .SetOutputDir(outputInno));

            });

            // Sign outputInno
            SignFolder(outputInno);

            // Zip exe Files
            var exeFiles = Globbing.GlobFiles(outputInno, "**/*.exe");
            exeFiles.ForEach(file => ZipExtension.ZipFileCompact(file));

            if (exeFiles.IsEmpty())
                Serilog.Log.Error($"Not found any .exe file in {outputInno}");

            if (outputInno != ReleaseDirectory)
            {
                Globbing.GlobFiles(outputInno, "**/*.zip")
                    .ForEach(file => FileSystemTasks.CopyFileToDirectory(file, ReleaseDirectory));
            }

        });
}
