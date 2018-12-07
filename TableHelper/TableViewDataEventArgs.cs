using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TableHelper
{
    public class TableViewDataEventArgs : EventArgs
    {
        public TableData TableData { get; private set; }
        public int RowIndex { get; private set; }
        public int ColumnIndex { get; private set; }
        public object Data { get; private set; }

        public TableViewDataEventArgs(TableData tableData, int rowIndex, int columnIndex, object data)
        {
            TableData = tableData;
            RowIndex = rowIndex;
            ColumnIndex = columnIndex;
            Data = data;
        }
    }
}
