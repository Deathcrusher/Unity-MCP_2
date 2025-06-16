#pragma warning disable CS8632
using System.ComponentModel;
using System.Text;
using System.Threading;
using UnityEditor;
using UnityEditor.TestTools.TestRunner.Api;
using UnityEngine;
using com.IvanMurzak.Unity.MCP.Common;

namespace com.IvanMurzak.Unity.MCP.Editor.API
{
    public partial class Tool_Editor
    {
        [McpPluginTool(
            "Editor_RunTests",
            Title = "Run EditMode tests")]
        [Description("Run all EditMode tests using Unity Test Runner and return a summary of results.")]
        public string RunTests() => MainThread.Instance.Run(() =>
        {
            var sb = new StringBuilder();
            var completed = new ManualResetEvent(false);
            var api = ScriptableObject.CreateInstance<TestRunnerApi>();
            api.RegisterCallbacks(new RunTestsCallbacks(sb, completed));
            var filter = new Filter { testMode = TestMode.EditMode };
            api.Execute(new ExecutionSettings(filter));
            while (!completed.WaitOne(100)) { }
            Object.DestroyImmediate(api);
            return "[Success] Test run finished.\n" + sb.ToString();
        });

        private class RunTestsCallbacks : ICallbacks
        {
            private readonly StringBuilder _sb;
            private readonly ManualResetEvent _done;
            private int _total;
            private int _passed;
            private int _failed;

            public RunTestsCallbacks(StringBuilder sb, ManualResetEvent done)
            {
                _sb = sb;
                _done = done;
            }

            public void RunStarted(ITestAdaptor testsToRun)
            {
                _sb.AppendLine($"Run started: {testsToRun.TestCaseCount} tests");
            }

            public void TestStarted(ITestAdaptor test) { }

            public void TestFinished(ITestResultAdaptor result)
            {
                _total++;
                if (result.TestStatus == TestStatus.Passed) _passed++;
                else if (result.TestStatus == TestStatus.Failed) _failed++;
                _sb.AppendLine($"{result.Name}: {result.TestStatus}");
            }

            public void RunFinished(ITestResultAdaptor result)
            {
                _sb.AppendLine($"Total: {_total}, Passed: {_passed}, Failed: {_failed}");
                _done.Set();
            }
        }
    }
}
