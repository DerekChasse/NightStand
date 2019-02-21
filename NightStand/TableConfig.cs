namespace NightStand
{
    using System;

    public class TableConfig
    {
        private int cellRightPadding = 1;
        private int cellLeftPadding = 1;

        public static TableConfig Default => new TableConfig();

        public char TopLeftCharacter { get; set; } = '┌';

        public char TopRightCharacter { get; set; } = '┐';

        public char BottomLeftCharacter { get; set; } = '└';

        public char BottomRightCharacter { get; set; } = '┘';

        public char HorizontalTopJointCharacter { get; set; } = '┬';

        public char HorizontalBottomJointCharacter { get; set; } = '┴';

        public char IntersectionJointCharacter { get; set; } = '┼';

        public char VerticalCharacter { get; set; } = '│';

        public char VerticalLeftJointCharacter { get; set; } = '├';

        public char VerticalRightJointCharacter { get; set; } = '┤';

        public char HorizontalCharacter { get; set; } = '─';

        public int CellRightPadding
        {
            get => this.cellRightPadding;

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value, "Value must be 0 or greater.");
                }

                this.cellRightPadding = value;
            }
        }

        public int CellLeftPadding
        {
            get => this.cellLeftPadding;

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value, "Value must be 0 or greater.");
                }

                this.cellLeftPadding = value;
            }
        }
    }
}
