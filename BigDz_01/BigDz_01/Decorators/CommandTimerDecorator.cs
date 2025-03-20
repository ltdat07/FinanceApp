using System;
using System.Diagnostics;
using BigDz_01.Commands;

namespace BigDz_01.Decorators
{
    public class CommandTimerDecorator : ICommand
    {
        private readonly ICommand _innerCommand;

        public CommandTimerDecorator(ICommand command)
        {
            _innerCommand = command;
        }

        public void Execute()
        {
            var sw = Stopwatch.StartNew();
            _innerCommand.Execute();
            sw.Stop();
            Console.WriteLine("Execution time: {0} ms", sw.ElapsedMilliseconds);
        }
    }
}
