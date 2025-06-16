#if !UNITY_5_3_OR_NEWER
using com.IvanMurzak.ReflectorNet.Model;
using ModelContextProtocol.Protocol;
using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Threading.Tasks;

namespace com.IvanMurzak.Unity.MCP.Server.API
{
    public partial class Tool_ScriptableObject
    {
        [McpServerTool(
            Name = "ScriptableObject_Modify",
            Title = "Modify ScriptableObject asset"
        )]
        [Description("Modify existing ScriptableObject using serialized diff data.")]
        public ValueTask<CallToolResponse> Modify(SerializedMember content, string assetPath)
        {
            return ToolRouter.Call("ScriptableObject_Modify", arguments =>
            {
                if (content != null)
                    arguments[nameof(content)] = content;
                arguments[nameof(assetPath)] = assetPath ?? string.Empty;
            });
        }
    }
}
#endif
