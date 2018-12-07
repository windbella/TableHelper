using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TableHelper
{
    public class TableSet : IReadOnlyList<TableRow>
    {
        public HtmlElement HtmlElement { get; private set; }
        public List<TableRow> TableRows = new List<TableRow>();

        public TableRow this[int index]
        {
            get { return TableRows[index]; }
        }

        public TableSet(HtmlElement htmlElement)
        {
            HtmlElement = htmlElement;
            foreach(HtmlElement item in HtmlElement.Children)
            {
                TableRows.Add(new TableRow(this, item));
            }
        }

        public int Count
        {
            get { return TableRows.Count; }
        }

        public IEnumerator<TableRow> GetEnumerator()
        {
            return TableRows.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return TableRows.GetEnumerator();
        }

        public int IndexOf(TableRow item)
        {
            return TableRows.IndexOf(item);
        }

        public bool Contains(TableRow item)
        {
            return TableRows.Contains(item);
        }
    }
}
