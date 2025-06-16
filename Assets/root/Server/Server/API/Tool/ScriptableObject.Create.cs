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
            Name = "ScriptableObject_Create",
            Title = "Create ScriptableObject asset"
        )]
        [Description("Create ScriptableObject asset of specified type at asset path.")]
        public ValueTask<CallToolResponse> Create(string assetPath, string typeName)
        {
            return ToolRouter.Call("ScriptableObject_Create", arguments =>
            {
                arguments[nameof(assetPath)] = assetPath ?? string.Empty;
                arguments[nameof(typeName)] = typeName ?? string.Empty;
            });
        }
    }
}
#endif
