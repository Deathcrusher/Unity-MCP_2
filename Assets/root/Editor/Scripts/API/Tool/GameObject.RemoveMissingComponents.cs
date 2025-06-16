#pragma warning disable CS8632
using System.ComponentModel;
using com.IvanMurzak.ReflectorNet.Model.Unity;
using com.IvanMurzak.Unity.MCP.Common;
using com.IvanMurzak.Unity.MCP.Utils;
using UnityEditor;
using com.IvanMurzak.ReflectorNet.Utils;

namespace com.IvanMurzak.Unity.MCP.Editor.API
{
    public partial class Tool_GameObject
    {
        [McpPluginTool(
            "GameObject_RemoveMissingComponents",
            Title = "Remove Missing Components from GameObject")]
        [Description("Remove all missing script components from GameObject. Optionally process children as well.")]
        public string RemoveMissingComponents(
            GameObjectRef gameObjectRef,
            [Description("If true, children will be scanned too.")]
            bool recursive = false)
        => MainThread.Instance.Run(() =>
        {
            var go = GameObjectUtils.FindBy(gameObjectRef, out var error);
            if (error != null)
                return error;

            int removed = 0;

            void Process(GameObject target)
            {
                removed += GameObjectUtility.RemoveMonoBehavioursWithMissingScript(target);
            }

            if (recursive)
            {
                foreach (var child in GameObjectUtils.GetAllRecursively(go))
                    Process(child.Value);
            }
            else
            {
                Process(go);
            }

            return $"[Success] Removed {removed} missing components.";
        });
    }
}
