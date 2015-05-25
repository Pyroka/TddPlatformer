using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public static class CommandLine
{
    public static void Build()
    {
        string[] scenes =
        {
            "Assets/Scenes/Main.unity",
        };

        var buildOptions = BuildOptions.None;
        var buildDir = "";
        var devBuild = false;

        var args = Environment.GetCommandLineArgs();
        for (var i = 0; i < args.Length; ++i)
        {
            if (args[i] == "--BuildDir")
            {
                buildDir = args[i + 1];
            }

            if (args[i] == "--MakeDevBuild")
            {
                devBuild = true;
            }
        }

        if (devBuild)
        {
            buildOptions |= (BuildOptions.Development | BuildOptions.AllowDebugging);
        }

        const BuildTarget buildTarget = BuildTarget.StandaloneWindows64;
        var fullBuildPath = Path.Combine(buildDir, Path.Combine(buildTarget.ToString(), "TestProject"));
        var buildError = BuildPipeline.BuildPlayer(scenes, fullBuildPath, buildTarget, buildOptions);
        if (!string.IsNullOrEmpty(buildError))
        {
            throw new Exception("Error building " + buildTarget + ": " + buildError);
        }
    }

    private static string GetExtensionFromTarget(BuildTarget buildTarget)
    {
        switch (buildTarget)
        {
            case BuildTarget.StandaloneWindows:
                return "exe";

            case BuildTarget.StandaloneOSXIntel:
                return "app";

            case BuildTarget.StandaloneLinux:
                return ".x86";

            default:
                throw new NotImplementedException("Unknown build target: " + buildTarget);
        }
    }
}
