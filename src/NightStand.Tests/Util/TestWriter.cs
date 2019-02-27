namespace NightStand.Tests.Util
{
    using System.IO;

    internal class TestWriter<T> : TextTableWriter<T>
    {
        public TestWriter(TextWriter writer)
            : base(writer)
        {
        }
    }
}
