#pragma warning disable CS8632
using System.ComponentModel;
using com.IvanMurzak.ReflectorNet;
using com.IvanMurzak.ReflectorNet.Model;
using com.IvanMurzak.ReflectorNet.Utils;
using com.IvanMurzak.Unity.MCP.Common;
using UnityEditor;
using UnityEngine;

namespace com.IvanMurzak.Unity.MCP.Editor.API
{
    public partial class Tool_ScriptableObject
    {
        [McpPluginTool(
            "ScriptableObject_Modify",
            Title = "Modify ScriptableObject asset"
        )]
        [Description("Modify existing ScriptableObject using serialized diff data.")]
        public string Modify(
            SerializedMember content,
            [Description("Asset path. Starts with 'Assets/'. Ends with '.asset'.")]
            string assetPath
        )
        => MainThread.Instance.Run(() =>
        {
            if (string.IsNullOrEmpty(assetPath))
                return Error.AssetPathIsEmpty();
            var obj = AssetDatabase.LoadAssetAtPath<ScriptableObject>(assetPath);
            if (obj == null)
                return Error.AssetNotFound(assetPath);
            object target = obj;
            var result = Reflector.Instance.Populate(ref target, content);
            EditorUtility.SetDirty(obj);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            return result.ToString();
        });
    }
}
