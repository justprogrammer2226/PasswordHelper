using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordHelper
{
    internal struct Option
    {
        public string Name { get; }
        public Action Action { get; }

        public Option(string optionName, Action action)
        {
            Name = optionName;
            Action = action;
        }
    }
}
