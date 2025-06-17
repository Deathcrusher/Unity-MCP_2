#if !UNITY_5_3_OR_NEWER
using ModelContextProtocol.Protocol;
using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Threading.Tasks;

namespace com.IvanMurzak.Unity.MCP.Server.API
{
    public partial class Tool_Package
    {
        [McpServerTool(
            Name = "Package_GetInstalled",
            Title = "Get installed packages"
        )]
        [Description("List installed packages from Packages/manifest.json")]
        public ValueTask<CallToolResponse> GetInstalled()
        {
            return ToolRouter.Call("Package_GetInstalled");
        }
    }
}
#endif
