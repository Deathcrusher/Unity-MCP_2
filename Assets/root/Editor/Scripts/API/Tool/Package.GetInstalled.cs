#pragma warning disable CS8632
using System.ComponentModel;
using System.IO;
using System.Text.Json;
using System.Text;
using com.IvanMurzak.Unity.MCP.Common;
using UnityEditor;

namespace com.IvanMurzak.Unity.MCP.Editor.API
{
    public partial class Tool_Package
    {
        [McpPluginTool(
            "Package_GetInstalled",
            Title = "Get installed packages"
        )]
        [Description("List installed packages from Packages/manifest.json")] 
        public string GetInstalled()
        => MainThread.Instance.Run(() =>
        {
            var request = UnityEditor.PackageManager.Client.List(true, false);
            while (!request.IsCompleted) {}
            if (request.Status == UnityEditor.PackageManager.StatusCode.Failure)
                return $"[Error] {request.Error.message}";

            var sb = new StringBuilder();
            foreach (var pkg in request.Result)
                sb.AppendLine($"{pkg.name}: {pkg.version}");
            return "[Success] Installed packages:\n" + sb.ToString();
        });
    }
}
