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
            Name = "ScriptableObject_Read",
            Title = "Read ScriptableObject asset"
        )]
        [Description("Read ScriptableObject at given asset path.")]
        public ValueTask<CallToolResponse> Read(string assetPath)
        {
            return ToolRouter.Call("ScriptableObject_Read", arguments =>
            {
                arguments[nameof(assetPath)] = assetPath ?? string.Empty;
            });
        }
    }
}
#endif
