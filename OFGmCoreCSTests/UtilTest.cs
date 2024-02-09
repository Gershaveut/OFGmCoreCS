using OFGmCoreCS.Util;

namespace OFGmCoreCSTests
{
    [TestClass]
    public class UtilTest
    {
        #pragma warning disable CS8602 // Разыменование вероятной пустой ссылки.
        readonly string logsDirectory = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "Test");
        #pragma warning restore CS8602

        [TestMethod]
        public void TestFileLogger()
        {
            Directory.CreateDirectory(logsDirectory);

            FileLogger fileLogger = new FileLogger("Test", logsDirectory);

            fileLogger.WriteLine("Test");
            fileLogger.SaveFile();
        }

        [TestMethod]
        public void TestCrashReporter()
        {
            Directory.CreateDirectory(logsDirectory);

            try
            {
                throw new Exception();
            }
            catch (Exception ex)
            {
                Console.Write(CrashReporter.CreateReport(ex, new FileLogger("CrashLog", logsDirectory)));
            }
        }
    }
}
