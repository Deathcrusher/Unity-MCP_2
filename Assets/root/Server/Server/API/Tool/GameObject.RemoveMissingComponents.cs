#if !UNITY_5_3_OR_NEWER
using com.IvanMurzak.ReflectorNet.Model.Unity;
using ModelContextProtocol.Protocol;
using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Threading.Tasks;

namespace com.IvanMurzak.Unity.MCP.Server.API
{
    public partial class Tool_GameObject
    {
        [McpServerTool(
            Name = "GameObject_RemoveMissingComponents",
            Title = "Remove Missing Components from GameObject")]
        [Description("Remove all missing script components from GameObject. Optionally process children as well.")]
        public ValueTask<CallToolResponse> RemoveMissingComponents(
            GameObjectRef gameObjectRef,
            bool recursive = false)
        {
            return ToolRouter.Call("GameObject_RemoveMissingComponents", arguments =>
            {
                arguments[nameof(gameObjectRef)] = gameObjectRef;
                arguments[nameof(recursive)] = recursive;
            });
        }
    }
}
#endif
