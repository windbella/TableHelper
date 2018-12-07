using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TableHelper
{
    public class TableData
    {
        public TableRow Parent { get; private set; }
        public HtmlElement HtmlElement { get; private set; }

        public TableData(TableRow parent, HtmlElement htmlElement)
        {
            Parent = parent;
            HtmlElement = htmlElement;
        }
    }
}
