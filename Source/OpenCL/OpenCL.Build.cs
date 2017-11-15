// Copyright 1998-2014 Epic Games, Inc. All Rights Reserved.

using System.IO;

namespace UnrealBuildTool.Rules
{
    public class OpenCL : ModuleRules
    {
        private string ModulePath
        {
            get { return ModuleDirectory; }
        }

        private string ThirdPartyPath
        {
            get { return Path.GetFullPath(Path.Combine(ModulePath, "../../ThirdParty/")); }
        }

        public OpenCL(ReadOnlyTargetRules Target) : base(Target)
        {
            PublicIncludePaths.AddRange(new string[] {
                "OpenCL/Public",
                Path.Combine(ThirdPartyPath, "OpenCL", "Include")
            });

            PublicDependencyModuleNames.AddRange(
                new string[]
                {
                    "Core",
                    "CoreUObject",
                    "Engine",
                    "RenderCore",
                    "ShaderCore",
                    "RHI"
                }
                );

            string PlatformString = (Target.Platform == UnrealTargetPlatform.Win64) ? "Win64" : "Win32";
            string OpenCLLibrariesPath = Path.Combine(ThirdPartyPath, "OpenCL", "Lib");
            string NvidiaLibrariesPath = Path.Combine(OpenCLLibrariesPath, "NVIDIA", PlatformString);
            string InelLibrariesPath = Path.Combine(OpenCLLibrariesPath, "Intel", PlatformString);
            if (Target.Platform == UnrealTargetPlatform.Win64 || Target.Platform == UnrealTargetPlatform.Win32)
            {
                PublicAdditionalLibraries.Add(Path.Combine(NvidiaLibrariesPath, "OpenCL.lib"));
                PublicAdditionalLibraries.Add(Path.Combine(InelLibrariesPath, "OpenCL.lib"));
            }
            else if (Target.Platform == UnrealTargetPlatform.Mac)
            {
                PublicAdditionalFrameworks.Add(new UEBuildFramework("OpenCL"));
            }
        }
    }
}
