using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.InnoSetup;
using Nuke.Common.Utilities.Collections;
using ricaun.Nuke.Components;
using ricaun.Nuke.Extensions;
using System;
using System.IO;
using System.Linq;

interface IBuildInstall : IRelease, ISign, IHazMainProject
{
    Target BuildInstall => _ => _
        .TriggeredBy(Sign)
        .Before(Release)
        .Executes(() =>
        {
            var project = MainProject;
            var projectName = project.Name;
            var projectVersion = project.GetInformationalVersion();
            var solutionDirectory = Solution.Directory;
            var folderOutput = "Install";

            Serilog.Log.Information($"{projectName} {projectVersion}");

            // Deploy File
            var outputInno = project.Directory / "bin" / folderOutput;
            var issFiles = Globbing.GlobFiles(solutionDirectory, $"**/*{projectName}.iss");

            if (issFiles.IsEmpty())
                Serilog.Log.Error($"Not found any .iss file in {solutionDirectory}");

            issFiles.ForEach(file =>
            {
                var tempFile = CreateTemporaryFile(file, projectVersion);
                try
                {
                    InnoSetupTasks.InnoSetup(config => config
                        .SetProcessToolPath(NuGetToolPathResolver.GetPackageExecutable("Tools.InnoSetup", "ISCC.exe"))
                        .SetScriptFile(tempFile)
                        .SetOutputDir(outputInno));
                }
                finally
                {
                    tempFile.DeleteFile();
                }
            });

            // Sign outputInno
            SignFolder(outputInno);

            // Zip exe Files
            var exeFiles = Globbing.GlobFiles(outputInno, "**/*.exe");
            exeFiles.ForEach(file => ZipExtension.ZipFileCompact(file));

            if (exeFiles.IsEmpty())
                Serilog.Log.Error($"Not found any .exe file in {outputInno}");

            var message = string.Join(" | ", exeFiles.Select(e => e.Name));
            ReportSummary(_ => _.AddPair("File", message));

            if (outputInno != ReleaseDirectory)
            {
                Globbing.GlobFiles(outputInno, "**/*.zip")
                    .ForEach(file => FileSystemTasks.CopyFileToDirectory(file, ReleaseDirectory));
            }

        });

    AbsolutePath CreateTemporaryFile(AbsolutePath file, string projectVersion)
    {
        //var tempFolder = Path.GetTempPath();
        //var tempFile = Path.Combine(tempFolder, Path.GetFileName(file));

        var folder = file.Parent;
        var tempFileName = $"{Path.GetFileNameWithoutExtension(file)}_{DateTime.Now.Ticks}{Path.GetExtension(file)}";

        var tempFile = Path.Combine(folder, tempFileName);

        FileSystemTasks.CopyFile(file, tempFile, FileExistsPolicy.Overwrite);

        var content = File.ReadAllText(tempFile);

        content = content.Replace("1.0.0", projectVersion);

        File.WriteAllText(tempFile, content);

        return tempFile;
    }
}
