using OFGmCoreCS.Util;

namespace OFGmCoreCSTests
{
    [TestClass]
    public class UtilTest
    {
        [TestMethod]
        public void TestFileLogger()
        {
            FileLogger fileLogger = new FileLogger("Test", Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "Test"));

            fileLogger.Write("Test");
            fileLogger.SaveFile();
        }
    }
}
