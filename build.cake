var npi = EnvironmentVariable("npi");
var name = "ThaiSplitLib";
var project = $"src/{name}/{name}.csproj";

Action<string,string,string> ps = (cmd, args, dir) => {
    StartProcess(cmd, new ProcessSettings {
        Arguments = args,
        WorkingDirectory = dir
    });
};

Task("Build").Does(() => {
    DotNetCoreBuild(project);
});

Task("Create-Nuget-Package")
    .IsDependentOn("Build")
    .Does(() => {
        DotNetCorePack(project);
    });

Task("Publish-Nuget")
    .IsDependentOn("Create-Nuget-Package")
    .Does(() => {
        var nupkg = new DirectoryInfo($"src/{name}/bin/Debug").GetFiles("*.nupkg").LastOrDefault();
        var package = nupkg.FullName;
        NuGetPush(package, new NuGetPushSettings {
            Source = "https://www.nuget.org/api/v2/package",
            ApiKey = npi
        });
});

Task("Restore").Does(() => {
    DotNetCoreRestore(project);
});

var target = Argument("target", "default");
RunTarget(target);