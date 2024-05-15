using Nuke.Common;
using Nuke.Common.Execution;
using ricaun.Nuke;

class Build : NukeBuild, IPublish, IBuildInstall
{
    public static int Main() => Execute<Build>(x => x.From<IPublish>().Build);
}
