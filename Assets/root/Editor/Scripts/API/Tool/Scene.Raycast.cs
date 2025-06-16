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
            "Scene_Raycast",
            Title = "Raycast in scene"
        )]
        [Description("Perform Physics.RaycastAll and return hit GameObjects paths.")]
        public string Raycast(Vector3 origin, Vector3 direction, float distance = 100f, string? loadedSceneName = null)
        => MainThread.Instance.Run(() =>
        {
            var scenes = string.IsNullOrEmpty(loadedSceneName)
                ? SceneUtils.GetAllLoadedScenesInUnityEditor()
                : new[] { EditorSceneManager.GetSceneByName(loadedSceneName) };
            var hits = new List<string>();
            foreach (var scene in scenes)
            {
                if (!scene.IsValid() || !scene.isLoaded) continue;
                foreach (var go in scene.GetRootGameObjects())
                {
                    foreach (var collider in go.GetComponentsInChildren<Collider>(true))
                    {
                        if (Physics.Raycast(origin, direction, out var hit, distance) && hit.collider == collider)
                            hits.Add($"{scene.name}/{hit.collider.transform.GetPath()}");
                    }
                }
            }
            if (hits.Count == 0)
                return "[Success] No objects hit.";
            var sb = new StringBuilder("[Success] Hits:\n");
            foreach (var h in hits)
                sb.AppendLine(h);
            return sb.ToString();
        });
    }
}
