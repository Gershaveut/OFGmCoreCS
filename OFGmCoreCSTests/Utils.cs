namespace OFGmCoreCSTests
{
    public static class Utils
    {
        public static T AssertReturn<T>(T expected, T actual)
        {
            Assert.AreEqual(expected, actual);
            return actual;
        }
    }
}
