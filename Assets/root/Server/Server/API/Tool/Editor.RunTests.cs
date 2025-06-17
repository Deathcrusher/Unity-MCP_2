#if !UNITY_5_3_OR_NEWER
using ModelContextProtocol.Protocol;
using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Threading.Tasks;

namespace com.IvanMurzak.Unity.MCP.Server.API
{
    public partial class Tool_Editor
    {
        [McpServerTool(
            Name = "Editor_RunTests",
            Title = "Run EditMode tests")]
        [Description("Run all EditMode tests using Unity Test Runner and return a summary of results.")]
        public ValueTask<CallToolResponse> RunTests()
        {
            return ToolRouter.Call("Editor_RunTests");
        }
    }
}
#endif
