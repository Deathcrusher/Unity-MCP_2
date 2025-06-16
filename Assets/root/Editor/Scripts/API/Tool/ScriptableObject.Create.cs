#pragma warning disable CS8632
using System;
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
            "ScriptableObject_Create",
            Title = "Create ScriptableObject asset"
        )]
        [Description("Create ScriptableObject asset of specified type at asset path.")]
        public string Create(
            [Description("Asset path. Starts with 'Assets/'. Ends with '.asset'.")]
            string assetPath,
            [Description("Full name of ScriptableObject type.")]
            string typeName
        )
        => MainThread.Instance.Run(() =>
        {
            if (string.IsNullOrEmpty(assetPath))
                return Error.AssetPathIsEmpty();
            if (!assetPath.StartsWith("Assets/"))
                return Error.AssetPathMustStartWithAssets(assetPath);
            if (!assetPath.EndsWith(".asset"))
                return Error.AssetPathMustEndWithAsset(assetPath);

            var type = Type.GetType(typeName);
            if (type == null || !typeof(ScriptableObject).IsAssignableFrom(type))
                return Error.InvalidType(typeName);

            var obj = ScriptableObject.CreateInstance(type);
            AssetDatabase.CreateAsset(obj, assetPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            var result = Reflector.Instance.Serialize(
                obj,
                name: obj.name,
                logger: McpPlugin.Instance.Logger
            );
            return $"[Success] ScriptableObject '{typeName}' created at '{assetPath}'.\n{result}";
        });
    }
}
