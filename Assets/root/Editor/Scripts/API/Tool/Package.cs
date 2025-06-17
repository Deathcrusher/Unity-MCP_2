#pragma warning disable CS8632
using com.IvanMurzak.Unity.MCP.Common;
using UnityEditor;

namespace com.IvanMurzak.Unity.MCP.Editor.API
{
    [McpPluginToolType]
    public partial class Tool_Package
    {
        public static class Error
        {
            public static string PackageNameIsEmpty()
                => "[Error] Package name is empty.";
            public static string VersionIsEmpty()
                => "[Error] Package version is empty.";
        }
    }
}
