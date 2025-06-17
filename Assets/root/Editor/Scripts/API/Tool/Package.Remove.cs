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
            "Package_Remove",
            Title = "Remove package"
        )]
        [Description("Remove a package from Packages/manifest.json")]
        public string Remove(string packageName)
        => MainThread.Instance.Run(() =>
        {
            if (string.IsNullOrEmpty(packageName))
                return Error.PackageNameIsEmpty();

            var request = UnityEditor.PackageManager.Client.Remove(packageName);
            while (!request.IsCompleted) {}
            if (request.Status == UnityEditor.PackageManager.StatusCode.Failure)
                return $"[Error] {request.Error.message}";

            AssetDatabase.Refresh();
            return $"[Success] Package '{packageName}' removed.";
        });
    }
}
