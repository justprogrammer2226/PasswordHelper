using System.Collections.Generic;

namespace PasswordHelper
{
    internal interface IMenu
    {
        string Title { get; }
        List<Option> Options { get; }
        void Show();
    }
}
