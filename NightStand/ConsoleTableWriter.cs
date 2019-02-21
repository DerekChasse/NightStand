namespace NightStand
{
    using System;

    public class ConsoleTableWriter<T> : TableWriter<T>
    {
        public ConsoleTableWriter()
            : base(Console.Out)
        {
        }

        public ConsoleTableWriter(bool resizeConsole)
            : base(Console.Out)
        {
            this.ResizeConsole = resizeConsole;
        }

        public ConsoleTableWriter(bool resizeConsole, int consoleResizePadding)
           : base(Console.Out)
        {
            this.ResizeConsole = resizeConsole;
            this.ConsoleResizePadding = consoleResizePadding;
        }

        public bool ResizeConsole { get; set; } = true;

        public int ConsoleResizePadding { get; set; } = 5;

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
            if (this.TableTotalWidth > Console.BufferWidth)
            {
                Console.BufferWidth = this.TableTotalWidth + this.ConsoleResizePadding;
            }
        }
    }
}
