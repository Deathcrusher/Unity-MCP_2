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
            Name = "Package_Remove",
            Title = "Remove package"
        )]
        [Description("Remove a package from Packages/manifest.json")]
        public ValueTask<CallToolResponse> Remove(string packageName)
        {
            return ToolRouter.Call("Package_Remove", arguments =>
            {
                arguments[nameof(packageName)] = packageName ?? string.Empty;
            });
        }
    }
}
#endif
