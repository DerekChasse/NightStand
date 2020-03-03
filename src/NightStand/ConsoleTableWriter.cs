namespace NightStand
{
    using System;

    /// <summary>
    /// Text based table writer which writes to the system console.
    /// </summary>
    /// <typeparam name="T">The type of item being drawn.</typeparam>
    public class ConsoleTableWriter<T> : TextTableWriter<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleTableWriter{T}"/> class.
        /// </summary>
        public ConsoleTableWriter()
            : base(Console.Out)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleTableWriter{T}"/> class.
        /// </summary>
        /// <param name="resizeConsole">Attempt to resize the console buffer if necessary.</param>
        public ConsoleTableWriter(bool resizeConsole)
            : base(Console.Out)
        {
            this.ResizeConsole = resizeConsole;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleTableWriter{T}"/> class.
        /// </summary>
        /// <param name="resizeConsole">Attempt to resize the console buffer if necessary.</param>
        /// <param name="consoleResizePadding">Additional padding to apply to the buffer on resize.</param>
        public ConsoleTableWriter(bool resizeConsole, int consoleResizePadding)
           : base(Console.Out)
        {
            this.ResizeConsole = resizeConsole;
            this.ConsoleResizePadding = consoleResizePadding;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the console buffer should update
        /// when table widths are greater than the current buffer. <see langword="true"/> by default.
        /// </summary>
        public bool ResizeConsole { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating additional padding to apply the the console buffer on resize.
        /// 5 by default.
        /// </summary>
        public int ConsoleResizePadding { get; set; } = 5;

        /// <inheritdoc />
        protected override void PostInitialize()
        {
            base.PostInitialize();

            if (this.ResizeConsole)
            {
                this.TryAdjustConsole();
            }
        }

        private void TryAdjustConsole()
        {
            if (this.TableMaxWidth > Console.BufferWidth)
            {
                Console.BufferWidth = this.TableMaxWidth + this.ConsoleResizePadding;
            }
        }
    }
}
