using System;

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
