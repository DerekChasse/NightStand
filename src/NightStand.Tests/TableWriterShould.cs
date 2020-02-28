namespace NightStand.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NightStand.Tests.Util;

    [TestClass]
    public class TableWriterShould
    {
        public StringWriter Writer { get; private set; }

        [TestInitialize]
        public void Setup()
        {
            this.Writer = new StringWriter();
        }

        [TestMethod]
        public void Draw_Success()
        {
            // Arrange
            List<TestEntity> testData = new List<TestEntity>
            {
                new TestEntity { Id = 1, Value = "Short" },
                new TestEntity { Id = 2, Value = "Loooooooooooong" },
            };

            Table<TestEntity> table = new Table<TestEntity>
            {
                Columns =
                {
                    new Column<TestEntity>("Id", c => c.Id.ToString()),
                    new Column<TestEntity>("Value", c => c.Value)
                }
            };

            TestWriter<TestEntity> testWriter = new TestWriter<TestEntity>(this.Writer);

            // Act
            testWriter.Draw(table, testData);

            // Assert
            var result = this.Writer.ToString();
            result.Should().NotBeNullOrWhiteSpace("Draw should render the table.");

            var lines = result.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            lines.Length.Should().Be(6, "Draw should generate the correct number of lines.");
        }

        [TestMethod]
        public void Draw_DefaultTableConfig_Success()
        {
            // Arrange
            List<TestEntity> testData = new List<TestEntity>
            {
                new TestEntity { Id = 1, Value = "Short" },
                new TestEntity { Id = 2, Value = "Loooooooooooong" },
            };

            Table<TestEntity> table = new Table<TestEntity>
            {
                Columns =
                {
                    new Column<TestEntity>("Id", c => c.Id.ToString()),
                    new Column<TestEntity>("Value", c => c.Value)
                }
            };

            TestWriter<TestEntity> testWriter = new TestWriter<TestEntity>(this.Writer);

            // Act
            testWriter.Draw(table, testData);

            // Assert
            var result = this.Writer.ToString();
            result.Should().NotBeNullOrWhiteSpace("Draw should render the table.");

            var lines = result.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            lines.Length.Should().Be(6, "Draw should generate the correct number of lines.");

            lines[0].Should().Be("┌────┬─────────────────┐");
            lines[1].Should().Be("│ Id │ Value           │");
            lines[2].Should().Be("├────┼─────────────────┤");
            lines[3].Should().Be("│ 1  │ Short           │");
            lines[4].Should().Be("│ 2  │ Loooooooooooong │");
            lines[5].Should().Be("└────┴─────────────────┘");
        }

        [TestMethod]
        public void Draw_CustomTableConfig_Success()
        {
            // Arrange
            List<TestEntity> testData = new List<TestEntity>
            {
                new TestEntity { Id = 1, Value = "Short" },
                new TestEntity { Id = 2, Value = "Loooooooooooong" },
            };

            Table<TestEntity> table = new Table<TestEntity>
            {
                Columns =
                {
                    new Column<TestEntity>("Id", c => c.Id.ToString()),
                    new Column<TestEntity>("Value", c => c.Value)
                }
            };

            TestWriter<TestEntity> testWriter = new TestWriter<TestEntity>(this.Writer);

            TableConfig customTableConfig = new TableConfig
            {
                BottomLeftCharacter = '#',
                HorizontalBottomJointCharacter = '#',
                BottomRightCharacter = '#',
                HorizontalCharacter = '#',
                HorizontalTopJointCharacter = '#',
                IntersectionJointCharacter = '#',
                TopLeftCharacter = '#',
                TopRightCharacter = '#',
                VerticalCharacter = '#',
                VerticalLeftJointCharacter = '#',
                VerticalRightJointCharacter = '#',
            };

            // Act
            testWriter.Draw(table, testData, customTableConfig);

            // Assert
            var result = this.Writer.ToString();
            result.Should().NotBeNullOrWhiteSpace("Draw should render the table.");

            var lines = result.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            lines.Length.Should().Be(6, "Draw should generate the correct number of lines.");

            lines[0].Should().Be("########################");
            lines[1].Should().Be("# Id # Value           #");
            lines[2].Should().Be("########################");
            lines[3].Should().Be("# 1  # Short           #");
            lines[4].Should().Be("# 2  # Loooooooooooong #");
            lines[5].Should().Be("########################");
        }

        [TestMethod]
        public void Draw_DefaultNullColumnValue_Success()
        {
            // Arrange
            List<TestEntity> testData = new List<TestEntity>
            {
                new TestEntity { Id = 1, Value = null },
            };

            Table<TestEntity> table = new Table<TestEntity>
            {
                Columns =
                {
                    new Column<TestEntity>("Id", c => c.Id.ToString()),
                    new Column<TestEntity>("Value", c => c.Value)
                }
            };

            TestWriter<TestEntity> testWriter = new TestWriter<TestEntity>(this.Writer);

            // Act
            testWriter.Draw(table, testData);

            // Assert
            var result = this.Writer.ToString();
            result.Should().NotBeNullOrWhiteSpace("Draw should render the table.");

            var lines = result.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            lines.Length.Should().Be(5, "Draw should generate the correct number of lines.");

            lines[0].Should().Be("┌────┬───────┐");
            lines[1].Should().Be("│ Id │ Value │");
            lines[2].Should().Be("├────┼───────┤");
            lines[3].Should().Be("│ 1  │       │");
            lines[4].Should().Be("└────┴───────┘");
        }

        [TestMethod]
        public void Draw_CustomNullColumnValue_Success()
        {
            // Arrange
            List<TestEntity> testData = new List<TestEntity>
            {
                new TestEntity { Id = 1, Value = null },
            };

            Table<TestEntity> table = new Table<TestEntity>
            {
                Columns =
                {
                    new Column<TestEntity>("Id", c => c.Id.ToString()),
                    new Column<TestEntity>("Value", c => c.Value)
                }
            };

            TestWriter<TestEntity> testWriter = new TestWriter<TestEntity>(this.Writer);

            var customDataConfig = TableConfig.Default;
            customDataConfig.NullCellDefaultValue = "n/a";

            // Act
            testWriter.Draw(table, testData, customDataConfig);

            // Assert
            var result = this.Writer.ToString();
            result.Should().NotBeNullOrWhiteSpace("Draw should render the table.");

            var lines = result.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            lines.Length.Should().Be(5, "Draw should generate the correct number of lines.");

            lines[0].Should().Be("┌────┬───────┐");
            lines[1].Should().Be("│ Id │ Value │");
            lines[2].Should().Be("├────┼───────┤");
            lines[3].Should().Be("│ 1  │ n/a   │");
            lines[4].Should().Be("└────┴───────┘");
        }
    }
}
