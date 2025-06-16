#pragma warning disable CS8632
using System;
using com.IvanMurzak.Unity.MCP.Common;
using UnityEditor;

namespace com.IvanMurzak.Unity.MCP.Editor.API
{
    [McpPluginToolType]
    public partial class Tool_ScriptableObject
    {
        public static class Error
        {
            public static string AssetPathIsEmpty()
                => "[Error] Asset path is empty.";
            public static string AssetPathMustStartWithAssets(string path)
                => $"[Error] Asset path '{path}' must start with 'Assets/'.";
            public static string AssetPathMustEndWithAsset(string path)
                => $"[Error] Asset path '{path}' must end with '.asset'.";
            public static string InvalidType(string typeName)
                => $"[Error] Type '{typeName}' not found or not a ScriptableObject.";
            public static string AssetNotFound(string path)
                => $"[Error] ScriptableObject asset not found at '{path}'.";
        }
    }
}
