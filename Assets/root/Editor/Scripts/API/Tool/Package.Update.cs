#pragma warning disable CS8632
using System.ComponentModel;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using com.IvanMurzak.Unity.MCP.Common;
using UnityEditor;

namespace com.IvanMurzak.Unity.MCP.Editor.API
{
    public partial class Tool_Package
    {
        [McpPluginTool(
            "Package_Update",
            Title = "Update package"
        )]
        [Description("Update package version in Packages/manifest.json")]
        public string Update(string packageName, string version)
        => MainThread.Instance.Run(() =>
        {
            if (string.IsNullOrEmpty(packageName))
                return Error.PackageNameIsEmpty();
            if (string.IsNullOrEmpty(version))
                return Error.VersionIsEmpty();

            var request = UnityEditor.PackageManager.Client.Add($"{packageName}@{version}");
            while (!request.IsCompleted) {}
            if (request.Status == UnityEditor.PackageManager.StatusCode.Failure)
                return $"[Error] {request.Error.message}";

            AssetDatabase.Refresh();
            return $"[Success] Package '{packageName}' updated to version '{version}'.";
        });
    }
}
