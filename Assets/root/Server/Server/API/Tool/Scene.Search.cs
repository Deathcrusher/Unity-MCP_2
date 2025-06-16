#if !UNITY_5_3_OR_NEWER
using ModelContextProtocol.Protocol;
using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Threading.Tasks;

namespace com.IvanMurzak.Unity.MCP.Server.API
{
    public partial class Tool_Scene
    {
        [McpServerTool(
            Name = "Scene_Search",
            Title = "Search objects in scene"
        )]
        [Description("Search loaded scene for GameObjects matching name substring")]
        public ValueTask<CallToolResponse> Search(string namePart, string? loadedSceneName = null)
        {
            return ToolRouter.Call("Scene_Search", arguments =>
            {
                arguments[nameof(namePart)] = namePart ?? string.Empty;
                if (!string.IsNullOrEmpty(loadedSceneName))
                    arguments[nameof(loadedSceneName)] = loadedSceneName;
            });
        }
    }
}
#endif
