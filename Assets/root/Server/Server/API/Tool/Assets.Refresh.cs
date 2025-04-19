using ModelContextProtocol.Protocol.Types;
using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Threading.Tasks;

namespace com.IvanMurzak.Unity.MCP.Server.API
{
    public partial class Tool_Assets
    {
        [McpServerTool
        (
            Name = "Assets_Refresh",
            Title = "Assets Refresh"
        )]
        [Description(@"Refreshes the AssetDatabase. Use it if any new files were added or updated in the project.
It also triggers scripts recompilation if any changes in '.cs' files.")]
        public Task<CallToolResponse> Refresh()
        {
            return ToolRouter.Call("Assets_Search");
        }
    }
}