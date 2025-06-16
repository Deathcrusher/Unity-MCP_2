#if !UNITY_5_3_OR_NEWER
using ModelContextProtocol.Protocol;
using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Threading.Tasks;

namespace com.IvanMurzak.Unity.MCP.Server.API
{
    public partial class Tool_Debug
    {
        [McpServerTool(
            Name = "Debug_ReadLogs",
            Title = "Read MCP server logs"
        )]
        [Description("Reads the last lines from the MCP server log file.")]
        public ValueTask<CallToolResponse> ReadLogs(
            [Description("Number of lines from the end of the log file.")]
            int lines = 20)
        {
            return ToolRouter.Call("Debug_ReadLogs", arguments =>
            {
                arguments[nameof(lines)] = lines;
            });
        }
    }
}
#endif
