#pragma warning disable CS8632
using System.ComponentModel;
using com.IvanMurzak.Unity.MCP.Common;
using UnityEditor;

namespace com.IvanMurzak.Unity.MCP.Editor.API
{
    public partial class Tool_ScriptableObject
    {
        [McpPluginTool(
            "ScriptableObject_Remove",
            Title = "Remove ScriptableObject asset"
        )]
        [Description("Delete ScriptableObject asset at the given path.")]
        public string Remove(
            [Description("Asset path. Starts with 'Assets/'. Ends with '.asset'.")]
            string assetPath
        )
        => MainThread.Instance.Run(() =>
        {
            if (string.IsNullOrEmpty(assetPath))
                return Error.AssetPathIsEmpty();
            if (!AssetDatabase.DeleteAsset(assetPath))
                return Error.AssetNotFound(assetPath);
            AssetDatabase.Refresh();
            return $"[Success] Removed ScriptableObject asset at '{assetPath}'.";
        });
    }
}
