#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
using com.IvanMurzak.Unity.MCP.Common;

namespace com.IvanMurzak.Unity.MCP.Editor.API
{
    [McpPluginToolType]
    public partial class Tool_Debug
    {
        public static class Error
        {
            public static string LogFileNotFound(string path)
                => $"[Error] Log file not found. Path: '{path}'.";
        }
    }
}
