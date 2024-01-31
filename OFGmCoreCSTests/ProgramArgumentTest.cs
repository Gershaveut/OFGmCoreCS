using OFGmCoreCS.ProgramArgument;

namespace OFGmCoreCSTests
{
    [TestClass]
    public class ProgramArgumentTest
    {
        [TestMethod]
        public void TestArgumentHandler()
        {
            bool? testSimple = null;
            string? testString = null;
            bool? testBool = null;
            int? testInt = null;
            double? testDouble = null;

            HashSet<IArgument> arguments = new HashSet<IArgument>
            {
                new Argument("testSimple", () => testSimple = true),
                new Argument<string>("testString", (arg) => testString = arg),
                new Argument<bool>("testBool", (arg) => testBool = arg),
                new Argument<int>("testInt", (arg) => testInt = arg),
                new Argument<double>("testDouble", (arg) => testDouble = arg)
            };

            ArgumentHandler argumentHandler = new(arguments);

            argumentHandler.ArgumentsInvoke(new string[] { "-testSimple", "--testString=pass", "--testBool=true", "--testInt=1", "--testDouble=1" });

            Assert.IsTrue(testSimple);
            Assert.AreEqual("pass", testString);
            Assert.IsTrue(testBool);
            Assert.AreEqual(1, testInt);
            Assert.AreEqual(1, testDouble);
        }

        [TestMethod]
        public void TestArgumentHandlerMethods()
        {
            bool? testSimple = null;
            string? testValue = null;

            HashSet<IArgument> arguments = new HashSet<IArgument>();

            Argument argumentTestSimple = new Argument("testSimple", () => testSimple = true);
            Argument<string> argumentTestValue = new Argument<string>("testValue", (arg) => testValue = arg);

            arguments.Add(argumentTestSimple);
            arguments.Add(argumentTestValue);

            ArgumentHandler argumentHandler = new(arguments);

            argumentHandler.ArgumentInvoke("testSimple");
            Assert.IsTrue(testSimple);

            ArgumentHandler.ArgumentInvoke(argumentTestSimple);
            Assert.IsTrue(testSimple);

            argumentHandler.ArgumentInvoke("testValue", "pass");
            Assert.AreEqual("pass", testValue);

            ArgumentHandler.ArgumentInvoke(argumentTestValue, "pass");
            Assert.AreEqual("pass", testValue);
        }
    }
}
