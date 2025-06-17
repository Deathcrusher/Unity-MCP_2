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
            Name = "Package_Update",
            Title = "Update package"
        )]
        [Description("Update package version in Packages/manifest.json")]
        public ValueTask<CallToolResponse> Update(string packageName, string version)
        {
            return ToolRouter.Call("Package_Update", arguments =>
            {
                arguments[nameof(packageName)] = packageName ?? string.Empty;
                arguments[nameof(version)] = version ?? string.Empty;
            });
        }
    }
}
#endif
