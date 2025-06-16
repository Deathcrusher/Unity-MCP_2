#if !UNITY_5_3_OR_NEWER
using ModelContextProtocol.Protocol;
using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Threading.Tasks;
using UnityEngine;

namespace com.IvanMurzak.Unity.MCP.Server.API
{
    public partial class Tool_Scene
    {
        [McpServerTool(
            Name = "Scene_Raycast",
            Title = "Raycast in scene"
        )]
        [Description("Perform Physics.RaycastAll and return hit GameObjects paths")]
        public ValueTask<CallToolResponse> Raycast(Vector3 origin, Vector3 direction, float distance = 100f, string? loadedSceneName = null)
        {
            return ToolRouter.Call("Scene_Raycast", arguments =>
            {
                arguments[nameof(origin)] = origin;
                arguments[nameof(direction)] = direction;
                arguments[nameof(distance)] = distance;
                if (!string.IsNullOrEmpty(loadedSceneName))
                    arguments[nameof(loadedSceneName)] = loadedSceneName;
            });
        }
    }
}
#endif
