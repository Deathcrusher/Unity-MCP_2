#pragma warning disable CS8632
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using com.IvanMurzak.Unity.MCP.Common;
using com.IvanMurzak.Unity.MCP.Utils;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace com.IvanMurzak.Unity.MCP.Editor.API
{
    public partial class Tool_Scene
    {
        [McpPluginTool(
            "Scene_Search",
            Title = "Search objects in scene"
        )]
        [Description("Search loaded scene for GameObjects whose names contain the given string.")]
        public string Search(string namePart, string? loadedSceneName = null)
        => MainThread.Instance.Run(() =>
        {
            if (string.IsNullOrEmpty(namePart))
                return "[Error] Name is empty.";
            var scenes = string.IsNullOrEmpty(loadedSceneName)
                ? SceneUtils.GetAllLoadedScenesInUnityEditor()
                : new[] { EditorSceneManager.GetSceneByName(loadedSceneName) };
            var results = new List<string>();
            foreach (var scene in scenes)
            {
                if (!scene.IsValid() || !scene.isLoaded) continue;
                foreach (var root in scene.GetRootGameObjects())
                {
                    foreach (var t in root.GetComponentsInChildren<Transform>(true))
                    {
                        if (t.name.Contains(namePart))
                            results.Add($"{scene.name}/{t.GetPath()}");
                    }
                }
            }
            if (results.Count == 0)
                return $"[Success] No GameObjects found for '{namePart}'.";
            var sb = new StringBuilder("[Success] Found objects:\n");
            foreach (var r in results)
                sb.AppendLine(r);
            return sb.ToString();
        });
    }
}
