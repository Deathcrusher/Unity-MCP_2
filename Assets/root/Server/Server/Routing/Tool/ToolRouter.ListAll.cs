using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using com.IvanMurzak.Unity.MCP.Common;
using com.IvanMurzak.Unity.MCP.Common.Data;
using ModelContextProtocol.Protocol.Types;
using ModelContextProtocol.Server;
using NLog;

namespace com.IvanMurzak.Unity.MCP.Server
{
    public static partial class ToolRouter
    {
        public static async Task<ListToolsResult> ListAll(RequestContext<ListToolsRequestParams> request, CancellationToken cancellationToken)
        {
            var logger = LogManager.GetCurrentClassLogger();
            logger.Trace("ListAll called");

            var mcpServerService = McpServerService.Instance;
            if (mcpServerService == null)
                return new ListToolsResult().SetError("[Error] 'McpServerService' is null");

            var remoteApp = mcpServerService.RemoteApp;
            if (remoteApp == null)
                return new ListToolsResult().SetError("[Error] 'RemoteApp' is null");

            var requestData = new RequestListTool();

            var response = await remoteApp.RunListTool(requestData, cancellationToken: cancellationToken);
            if (response == null)
                return new ListToolsResult().SetError("[Error] Resource is null");

            if (response.IsError)
                return new ListToolsResult().SetError(response.Message ?? "[Error] Got an error during reading resources");

            if (response.Value == null)
                return new ListToolsResult().SetError("[Error] Resource value is null");

            var result = new ListToolsResult()
            {
                Tools = response.Value
                    .Where(x => x != null)
                    .Select(x => x!.ToTool())
                    .ToList()
            };

            logger.Trace("ListAll, result: {0}", JsonUtils.Serialize(result));
            return result;
        }
    }
}