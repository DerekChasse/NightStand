namespace NightStand.Tests.Util
{
    using System.IO;

    internal class TestWriter<T> : TableWriter<T>
    {
        public TestWriter(TextWriter writer)
            : base(writer)
        {
        }
    }
}
