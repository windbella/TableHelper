using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TableHelper
{
    public class TableRow : IReadOnlyList<TableData>
    {
        public TableSet Parent { get; private set; }
        public HtmlElement HtmlElement { get; private set; }
        public List<TableData> TableDatas = new List<TableData>();  

        public TableData this[int i]
        {
            get { return TableDatas[i]; }
        }

        public TableRow(TableSet parent, HtmlElement htmlElement)
        {
            Parent = parent;
            HtmlElement = htmlElement;
            foreach (HtmlElement item in HtmlElement.Children)
            {
                TableDatas.Add(new TableData(this, item));
            }
        }

        public int Count
        {
            get { return TableDatas.Count; }
        }

        public IEnumerator<TableData> GetEnumerator()
        {
            return TableDatas.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return TableDatas.GetEnumerator();
        }

        public int IndexOf(TableData item)
        {
            return TableDatas.IndexOf(item);
        }

        public bool Contains(TableData item)
        {
            return TableDatas.Contains(item);
        }
    }
}
