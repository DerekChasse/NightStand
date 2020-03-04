// <copyright file="TestWriter.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

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
