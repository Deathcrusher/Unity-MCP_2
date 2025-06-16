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
            if (!File.Exists(ManifestPath))
                return Error.ManifestNotFound();
            using var doc = LoadManifest();
            if (!doc.RootElement.TryGetProperty("dependencies", out var deps))
                return "[Error] No 'dependencies' section found.";
            var sb = new StringBuilder();
            foreach (var pkg in deps.EnumerateObject())
                sb.AppendLine($"{pkg.Name}: {pkg.Value.GetString()}");
            return "[Success] Installed packages:\n" + sb.ToString();
        });
    }
}
