using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace database_logic.comboboxitem
{
    public class combobox
    {
        public object value { get; set; }
        public string categoria { get; set; }

        public override string ToString()
        {
            return categoria;
        }

    }
}
