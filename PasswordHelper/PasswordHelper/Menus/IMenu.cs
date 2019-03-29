using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordHelper
{
    interface IMenu
    {
        string Title { get; }
        List<Option> Options { get; }
        void Show();
    }
}
