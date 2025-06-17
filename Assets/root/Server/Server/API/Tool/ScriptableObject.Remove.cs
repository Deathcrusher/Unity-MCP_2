#if !UNITY_5_3_OR_NEWER
using ModelContextProtocol.Protocol;
using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Threading.Tasks;

namespace com.IvanMurzak.Unity.MCP.Server.API
{
    public partial class Tool_ScriptableObject
    {
        [McpServerTool(
            Name = "ScriptableObject_Remove",
            Title = "Remove ScriptableObject asset"
        )]
        [Description("Delete ScriptableObject asset at the given path.")]
        public ValueTask<CallToolResponse> Remove(string assetPath)
        {
            return ToolRouter.Call("ScriptableObject_Remove", arguments =>
            {
                arguments[nameof(assetPath)] = assetPath ?? string.Empty;
            });
        }
    }
}
#endif
