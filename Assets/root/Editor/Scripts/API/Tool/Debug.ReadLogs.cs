#pragma warning disable CS8632
using System.ComponentModel;
using System.IO;
using System.Linq;
using com.IvanMurzak.Unity.MCP.Common;
using com.IvanMurzak.Unity.MCP.Utils;

namespace com.IvanMurzak.Unity.MCP.Editor.API
{
    public partial class Tool_Debug
    {
        [McpPluginTool(
            "Debug_ReadLogs",
            Title = "Read MCP server logs"
        )]
        [Description("Reads the last lines from the MCP server log file.")]
        public string ReadLogs(
            [Description("Number of lines from the end of the log file.")]
            int lines = 20)
            => MainThread.Instance.Run(() =>
        {
            var logFile = Startup.ServerLogsPath;
            if (!File.Exists(logFile))
                return Error.LogFileNotFound(logFile);

            var safeLines = Mathf.Clamp(lines, 1, Consts.MCP.LinesLimit);
            var logLines = File.ReadLines(logFile)
                .Reverse()
                .Take(safeLines)
                .Reverse();
            return "[Success] Logs:\n" + string.Join("\n", logLines);
        });
    }
}
