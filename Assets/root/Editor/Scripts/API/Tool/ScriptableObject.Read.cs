#pragma warning disable CS8632
using System.ComponentModel;
using com.IvanMurzak.ReflectorNet;
using com.IvanMurzak.ReflectorNet.Utils;
using com.IvanMurzak.Unity.MCP.Common;
using UnityEditor;
using UnityEngine;

namespace com.IvanMurzak.Unity.MCP.Editor.API
{
    public partial class Tool_ScriptableObject
    {
        [McpPluginTool(
            "ScriptableObject_Read",
            Title = "Read ScriptableObject asset"
        )]
        [Description("Read ScriptableObject at the given asset path and return its serialized data.")]
        public string Read(
            [Description("Path to asset. Starts with 'Assets/'. Ends with '.asset'.")]
            string assetPath
        )
        => MainThread.Instance.Run(() =>
        {
            if (string.IsNullOrEmpty(assetPath))
                return Error.AssetPathIsEmpty();
            var obj = AssetDatabase.LoadAssetAtPath<ScriptableObject>(assetPath);
            if (obj == null)
                return Error.AssetNotFound(assetPath);
            var result = Reflector.Instance.Serialize(
                obj,
                name: obj.name,
                logger: McpPlugin.Instance.Logger
            );
            return $"[Success] ScriptableObject loaded from '{assetPath}'.\n{result}";
        });
    }
}
