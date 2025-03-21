using Bdz_01_Kpo.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bdz_01_Kpo.Decorators
{
    public class TimeMeasurementDecorator : ICommand
    {
        private readonly ICommand command;
        public TimeMeasurementDecorator(ICommand command) { this.command = command; }
        public void Execute()
        {
            var start = DateTime.Now;
            command.Execute();
            var end = DateTime.Now;
            Console.WriteLine($"Время выполнения: {(end - start).TotalMilliseconds} мс");
        }
    }
}
