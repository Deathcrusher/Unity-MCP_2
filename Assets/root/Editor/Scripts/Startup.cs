using com.IvanMurzak.Unity.MCP.Common;
using Debug = UnityEngine.Debug;
using Microsoft.Extensions.Logging;
using System.Net;
using UnityEditor;

namespace com.IvanMurzak.Unity.MCP.Editor
{
    [InitializeOnLoad]
    static partial class Startup
    {
        const string PackageName = "com.ivanmurzak.unity.mcp";

        static Startup()
        {
            BuildAndStart();
            BuildServerIfNeeded(force: true);
        }

        public static void BuildAndStart()
        {
            var message = "<b><color=yellow>STARTUP</color></b>";
            Debug.Log($"{Consts.Log.Tag} {message} <color=orange>ಠ‿ಠ</color>");

            new McpPluginBuilder()
                .WithAppFeatures()
                .WithConfig(config =>
                {

                })
                .AddLogging(loggingBuilder =>
                {
                    loggingBuilder.ClearProviders(); // 👈 Clears the default providers
                    loggingBuilder.AddProvider(new UnityLoggerProvider());
                    loggingBuilder.SetMinimumLevel(LogLevel.Trace);
                })
                .WithToolsFromAssembly(typeof(Startup).Assembly)
                .WithPromptsFromAssembly(typeof(Startup).Assembly)
                .WithResourcesFromAssembly(typeof(Startup).Assembly)
                .Build()
                .Connect();
        }
    }
}