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
            if (!File.Exists(ManifestPath))
                return Error.ManifestNotFound();
            using var doc = LoadManifest();
            var root = doc.RootElement.Clone();
            var deps = root.GetProperty("dependencies").EnumerateObject().ToDictionary(p => p.Name, p => p.Value.GetString());
            if (!deps.ContainsKey(packageName))
                return $"[Error] Package '{packageName}' not found.";
            deps[packageName] = version;
            var newRoot = new Dictionary<string, object>{ {"dependencies", deps} };
            var json = JsonSerializer.Serialize(newRoot, new JsonSerializerOptions{WriteIndented=true});
            File.WriteAllText(ManifestPath, json);
            AssetDatabase.Refresh();
            return $"[Success] Package '{packageName}' updated to version '{version}'.";
        });
    }
}
