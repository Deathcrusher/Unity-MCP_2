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
            if (!File.Exists(ManifestPath))
                return Error.ManifestNotFound();
            using var doc = LoadManifest();
            var root = doc.RootElement.Clone();
            var deps = root.GetProperty("dependencies").EnumerateObject().ToDictionary(p => p.Name, p => p.Value.GetString());
            if (!deps.Remove(packageName))
                return $"[Error] Package '{packageName}' not found.";
            var newRoot = new Dictionary<string, object>{ {"dependencies", deps} };
            var json = JsonSerializer.Serialize(newRoot, new JsonSerializerOptions{WriteIndented=true});
            File.WriteAllText(ManifestPath, json);
            AssetDatabase.Refresh();
            return $"[Success] Package '{packageName}' removed.";
        });
    }
}
