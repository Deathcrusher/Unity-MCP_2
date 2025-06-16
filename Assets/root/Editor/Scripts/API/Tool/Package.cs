#pragma warning disable CS8632
using System.IO;
using System.Text;
using System.Text.Json;
using com.IvanMurzak.Unity.MCP.Common;
using UnityEditor;

namespace com.IvanMurzak.Unity.MCP.Editor.API
{
    [McpPluginToolType]
    public partial class Tool_Package
    {
        const string ManifestPath = "Packages/manifest.json";

        public static class Error
        {
            public static string ManifestNotFound()
                => $"[Error] '{ManifestPath}' not found.";
            public static string PackageNameIsEmpty()
                => "[Error] Package name is empty.";
            public static string VersionIsEmpty()
                => "[Error] Package version is empty.";
        }

        static JsonDocument LoadManifest()
        {
            var json = File.ReadAllText(ManifestPath);
            return JsonDocument.Parse(json);
        }

        static void SaveManifest(JsonElement root)
        {
            var options = new JsonWriterOptions { Indented = true };
            using var stream = new MemoryStream();
            using (var writer = new Utf8JsonWriter(stream, options))
            {
                root.WriteTo(writer);
            }
            File.WriteAllBytes(ManifestPath, stream.ToArray());
            AssetDatabase.Refresh();
        }
    }
}
