namespace Secucard.Connect.Test
{
    using System.Diagnostics;
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Secucard.Connect.Trace;

    [TestClass]
    public class Test_Trace : Test_Base
    {
        [TestMethod]
        [TestCategory("trace")]
        public void Test_FileTrace()
        {
            File.Delete(fullTracePath);
            var tracer = new SecucardTraceFile(fullTracePath);
            tracer.Info("Message 1");
            Process.Start(fullTracePath);
        }
    }
}