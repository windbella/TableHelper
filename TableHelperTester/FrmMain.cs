using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TableHelper;
using TableHelperTester.Properties;

namespace TableHelperTester
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            scrollTableView.LoadTable(Resources.table_scroll);
            for (int i = 0; i < 50; i++)
            {
                scrollTableView.TableBody.HtmlElement.AppendChild(GetTableRow());
            }
            scrollTableView.UpdateTable();

            fullTableView.LoadTable(Resources.table_full);
            foreach(TableRow row in fullTableView.TableBody)
            {
                foreach (TableData item in row)
                {
                    item.HtmlElement.InnerHtml = "1";
                }
            }
        }

        private HtmlElement GetTableRow()
        {
            HtmlElement tr = scrollTableView.HtmlDocument.CreateElement("tr");
            for (int i = 0; i < 4; i++)
            {
                HtmlElement td = scrollTableView.HtmlDocument.CreateElement("td");
                if(i == 0)
                {
                    HtmlElement checkBox = scrollTableView.HtmlDocument.CreateElement("input");
                    checkBox.SetAttribute("type", "checkbox");
                    td.AppendChild(checkBox);
                }
                tr.AppendChild(td);
            }
            return tr;
        }

        private void tableView_TableViewHeaderClick(object sender, TableHelper.TableViewDataEventArgs e)
        {
            HtmlElement item = scrollTableView.TableHeader[e.RowIndex][e.ColumnIndex].HtmlElement;
            if (item.Children.Count == 0)
            {
                item.InnerText = item.InnerText + "!";
            }
        }

        private void tableView_TableViewDataClick(object sender, TableHelper.TableViewDataEventArgs e)
        {
            HtmlElement item = scrollTableView.TableBody[e.RowIndex][e.ColumnIndex].HtmlElement;
            if(item.Children.Count == 0)
            {
                item.InnerText = "클릭";
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            scrollTableView.TableBody.HtmlElement.AppendChild(GetTableRow());
            scrollTableView.UpdateTable();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            foreach(TableRow item in scrollTableView.TableBody.TableRows)
            {
                bool isChecked = "True".Equals(item[0].HtmlElement.FirstChild.GetAttribute("checked"));
                if(isChecked)
                {
                    item.HtmlElement.OuterHtml = string.Empty;
                }
            }
            scrollTableView.UpdateTable();
        }

        private void btnFullExcel_Click(object sender, EventArgs e)
        {
            fullTableView.SaveExcel(Application.StartupPath + "\\test.xlsx");
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Bitmap image = new Bitmap(scrollTableView.Width, scrollTableView.Height);
            TableView.DrawToBitmap(scrollTableView, image, new Rectangle(0, 0, scrollTableView.Width, scrollTableView.Height));
            image.Save("test.bmp");
        }
    }
}
