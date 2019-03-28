using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordHelper
{
    internal class Menu
    {
        private string title;
        private List<Option> options;

        public Menu(List<Option> options, string title = null)
        {
            if (options != null)
            {
                this.options = options;
                this.title = title;
            }
            else throw new ArgumentNullException("Не допускается передача значения null для параметра options.");
        }

        public void Show()
        {
            if (title != null) Console.WriteLine(title);

            for(int i = 0; i < options.Count; i++)
                Console.WriteLine($"{i + 1}. {options[i].Name}");

            int selectedOption = int.Parse(Console.ReadLine());

            options[selectedOption - 1].Action();
        }
    }
}
