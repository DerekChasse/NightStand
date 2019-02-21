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
        private readonly List<TestEntity> testData = new List<TestEntity>
        {
            new TestEntity { Id = 1, Value = "Short" },
            new TestEntity { Id = 2, Value = "Loooooooooooong" }
        };

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
            testWriter.Draw(table, this.testData);

            // Assert
            var result = this.Writer.ToString();
            result.Should().NotBeNullOrWhiteSpace("Draw should render the table.");

            var lines = result.Split(Environment.NewLine);
            lines.Length.Should().Be(7, "Draw should generate the correct number of lines.");
        }

        [TestMethod]
        public void Draw_DefaultTableConfig_CorrectCharacters()
        {
            // Arrange
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
            testWriter.Draw(table, this.testData);

            // Assert
            var result = this.Writer.ToString();
            result.Should().NotBeNullOrWhiteSpace("Draw should render the table.");

            var lines = result.Split(Environment.NewLine);
            lines.Length.Should().Be(7, "Draw should generate the correct number of lines.");

            // Top Line
            lines[0].Distinct().Should()
                .Contain(TableConfig.Default.TopLeftCharacter, $"{TableConfig.Default.TopLeftCharacter} should be in the table top line.")
                .And
                .Contain(TableConfig.Default.HorizontalCharacter, $"{TableConfig.Default.HorizontalCharacter} should be in the table top line.")
                .And
                .Contain(TableConfig.Default.HorizontalTopJointCharacter, $"{TableConfig.Default.HorizontalTopJointCharacter} should be in the table top line with more than one column.")
                .And
                .Contain(TableConfig.Default.TopRightCharacter, $"{TableConfig.Default.TopRightCharacter} should be in the table top line.");

            // Header
            lines[1].Distinct().Should()
                .Contain(TableConfig.Default.VerticalCharacter, $"{TableConfig.Default.VerticalCharacter} should be in the table header");

            // Divider
            lines[2].Distinct().Should()
                .Contain(TableConfig.Default.VerticalLeftJointCharacter, $"{TableConfig.Default.VerticalLeftJointCharacter} should be in the table divider.")
                .And
                .Contain(TableConfig.Default.HorizontalCharacter, $"{TableConfig.Default.HorizontalCharacter} should be in the table divider.")
                .And
                .Contain(TableConfig.Default.IntersectionJointCharacter, $"{TableConfig.Default.IntersectionJointCharacter} should be in the table divider with more than one column.")
                .And
                .Contain(TableConfig.Default.VerticalRightJointCharacter, $"{TableConfig.Default.VerticalRightJointCharacter} should be in the table divider.");

            // Content
            lines[3].Distinct().Should()
                .Contain(TableConfig.Default.VerticalCharacter, $"{TableConfig.Default.VerticalCharacter} should be in the table content.");

            // Bottom Line
            lines[5].Distinct().Should()
                .Contain(TableConfig.Default.BottomLeftCharacter, $"{TableConfig.Default.BottomLeftCharacter} should be in the table bottom line.")
                .And
                .Contain(TableConfig.Default.HorizontalCharacter, $"{TableConfig.Default.HorizontalCharacter} should be in the table bottom line.")
                .And
                .Contain(TableConfig.Default.HorizontalBottomJointCharacter, $"{TableConfig.Default.HorizontalBottomJointCharacter} should be in the table bottom line with more than one column.")
                .And
                .Contain(TableConfig.Default.BottomRightCharacter, $"{TableConfig.Default.BottomRightCharacter} should be in the table bottom line.");
        }
    }
}
