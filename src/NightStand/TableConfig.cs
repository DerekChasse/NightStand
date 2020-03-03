namespace NightStand
{
    using System;

    /// <summary>
    /// Table customization class.
    /// </summary>
    public class TableConfig
    {
        private int cellRightPadding = 1;
        private int cellLeftPadding = 1;

        /// <summary>
        /// Gets an instance of the default table rendering settings.
        /// </summary>
        public static TableConfig Default => new TableConfig();

        /// <summary>
        /// Gets or sets the character to use for the top-left corner.
        /// </summary>
        public char TopLeftCharacter { get; set; } = '┌';

        /// <summary>
        /// Gets or sets the character to use for the top-right corner.
        /// </summary>
        public char TopRightCharacter { get; set; } = '┐';

        /// <summary>
        /// Gets or sets the character to use for the bottom-left corner.
        /// </summary>
        public char BottomLeftCharacter { get; set; } = '└';

        /// <summary>
        /// Gets or sets the character to use for the bottom-right corner.
        /// </summary>
        public char BottomRightCharacter { get; set; } = '┘';

        /// <summary>
        /// Gets or sets the character to use for the horizontal top joint.
        /// </summary>
        public char HorizontalTopJointCharacter { get; set; } = '┬';

        /// <summary>
        /// Gets or sets the character to use for the horizontal bottom joint.
        /// </summary>
        public char HorizontalBottomJointCharacter { get; set; } = '┴';

        /// <summary>
        /// Gets or sets the character to use for the intersection joint..
        /// </summary>
        public char IntersectionJointCharacter { get; set; } = '┼';

        /// <summary>
        /// Gets or sets the character to use for a standard vertical line.
        /// </summary>
        public char VerticalCharacter { get; set; } = '│';

        /// <summary>
        /// Gets or sets the character to use for the vertical left joint.
        /// </summary>
        public char VerticalLeftJointCharacter { get; set; } = '├';

        /// <summary>
        /// Gets or sets the character to use for the vertical right joint.
        /// </summary>
        public char VerticalRightJointCharacter { get; set; } = '┤';

        /// <summary>
        /// Gets or sets the character to use for a standard horizontal line.
        /// </summary>
        public char HorizontalCharacter { get; set; } = '─';

        /// <summary>
        /// Gets or sets the string to use if a cell evaluates to <see langword="null"/>. The value is <see cref="string.Empty"/> by default.
        /// </summary>
        public string NullCellDefaultValue { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the number of spaces to pad the right side of a column. The value is 1 by default.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the number of spaces to pad the left side of a column. The value is 1 by default.
        /// </summary>
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
