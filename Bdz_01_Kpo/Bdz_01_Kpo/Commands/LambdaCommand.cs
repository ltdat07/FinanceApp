using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bdz_01_Kpo.Commands
{
    public class LambdaCommand : ICommand
    {
        private readonly Action action;
        public LambdaCommand(Action action) { this.action = action; }
        public void Execute() => action();
    }
}
