using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TableHelper.Properties;
using mshtml;
using System.Collections.Specialized;
using System.Web;
using System.IO;
using System.Drawing.Imaging;
using Microsoft.Office.Interop.Excel;

namespace TableHelper
{
    public partial class TableView : UserControl
    {
        public delegate void TableViewHeaderClickEventHandler(object sender, TableViewDataEventArgs e);
        public delegate void TableViewDataClickEventHandler(object sender, TableViewDataEventArgs e);
        public delegate void TableViewHeaderCheckBoxChangeEventHandler(object sender, TableViewDataEventArgs e);
        public delegate void TableViewDataCheckBoxChangeEventHandler(object sender, TableViewDataEventArgs e);
        public delegate void TableViewHeaderRadioClickEventHandler(object sender, TableViewDataEventArgs e);
        public delegate void TableViewDataRadioClickEventHandler(object sender, TableViewDataEventArgs e);
        public delegate void TableViewHeaderButtonClickEventHandler(object sender, TableViewDataEventArgs e);
        public delegate void TableViewDataButtonClickEventHandler(object sender, TableViewDataEventArgs e);
        public delegate void TableViewHeaderSelectChangeEventHandler(object sender, TableViewDataEventArgs e);
        public delegate void TableViewDataSelectChangeEventHandler(object sender, TableViewDataEventArgs e);

        /// <summary>
        /// 테이블 헤더 클릭 이벤트
        /// </summary>
        public event TableViewHeaderClickEventHandler TableViewHeaderClick;
        /// <summary>
        /// 테이블 데이터 클릭 이벤트
        /// </summary>
        public event TableViewDataClickEventHandler TableViewDataClick;
        /// <summary>
        /// 테이블 헤더 체크박스 변경 이벤트
        /// </summary>
        public event TableViewHeaderCheckBoxChangeEventHandler TableViewHeaderCheckBoxChange;
        /// <summary>
        /// 테이블 데이터 체크박스 변경 이벤트
        /// </summary>
        public event TableViewDataCheckBoxChangeEventHandler TableViewDataCheckBoxChange;
        /// <summary>
        /// 테이블 헤더 라디오버튼 변경 이벤트
        /// </summary>
        public event TableViewHeaderRadioClickEventHandler TableViewHeaderRadioClick;
        /// <summary>
        /// 테이블 데이터 라디오버튼 변경 이벤트
        /// </summary>
        public event TableViewDataRadioClickEventHandler TableViewDataRadioClick;
        /// <summary>
        /// 테이블 헤더 버튼 변경 이벤트
        /// </summary>
        public event TableViewHeaderButtonClickEventHandler TableViewHeaderButtonClick;
        /// <summary>
        /// 테이블 데이터 버튼 변경 이벤트
        /// </summary>
        public event TableViewDataButtonClickEventHandler TableViewDataButtonClick;
        /// <summary>
        /// 테이블 헤더 셀렉트박스 변경 이벤트
        /// </summary>
        public event TableViewHeaderSelectChangeEventHandler TableViewHeaderSelectChangeClick;
        /// <summary>
        /// 테이블 데이터 셀렉트박스 변경 이벤트
        /// </summary>
        public event TableViewDataSelectChangeEventHandler TableViewDataSelectChangeClick;

        /// <summary>
        /// 테이블 로드 제한시간
        /// </summary>
        public TimeSpan Timeout { get; set; }

        /// <summary>
        /// HTML 도큐먼트
        /// </summary>
        public HtmlDocument HtmlDocument { get; private set; }
        /// <summary>
        /// 브라우저
        /// </summary>
        public WebBrowser Browser { get; private set; } 
        /// <summary>
        /// 테이블 HTML 문자열
        /// </summary>
        public string Table { get; private set; }
        /// <summary>
        /// CSS 스타일 시트
        /// </summary>
        public string Style { get; set; }
        /// <summary>
        /// 스크립트
        /// 웹브라우저의 이벤트를 응용프로그램으로 전달하는 스크립트
        /// </summary>
        public string Script { get; set; }

        public string DefaultStyle { get; private set; }
        public string DefaultTable { get; private set; }
        private string defaultType = null;
        public string DefaultType
        {
            get
            {
                return defaultType;
            }
            set
            {
                switch(value)
                {
                    case "scroll":
                        defaultType = value;
                        DefaultStyle = Resources.style_scroll;
                        DefaultTable = Resources.table_scroll;
                        break;
                    case "full":
                    default:
                        defaultType = "full";
                        DefaultStyle = Resources.style_full;
                        DefaultTable = Resources.table_full;
                        break;
                }
            }
        }

        /// <summary>
        /// thead 데이터
        /// </summary>
        public TableSet TableHeader { get; private set; }
        /// <summary>
        /// tbody 데이터
        /// </summary>
        public TableSet TableBody { get; private set; }

        public TableView()
        {
            InitializeComponent();
            Browser = browser;
            Timeout = new TimeSpan(0, 0, 5);
            DefaultType = "full";
        }

        private bool isLoad = false;

        /// <summary>
        /// Html 문자열로 테이블 불러오기
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public bool LoadTable(string table)
        {
            if (string.IsNullOrEmpty(Style))
            {
                Style = DefaultStyle;
            }
            if (string.IsNullOrEmpty(Script))
            {
                Script = Resources.script;
            }

            Table = table;
            bool result = true;
            isLoad = true;
            browser.DocumentText = table;
            Task timer = Task.Delay(Timeout);

            while (browser.ReadyState != WebBrowserReadyState.Complete && result)
            {
                System.Windows.Forms.Application.DoEvents();
                result = timer.Status != TaskStatus.RanToCompletion;
            }
            return result;
        }

        /// <summary>
        /// 새로운 빈 테이블 생성
        /// </summary>
        /// <returns></returns>
        public bool NewTable()
        {
            return LoadTable(DefaultTable);
        }

        /// <summary>
        /// HTML 문서를 탐색하여 TableHeader, TableBody 배열을 업데이트
        /// HtmlDocument를 프로그램 상에서 수정하여 테이블의 행이나 열을 추가했을 때 실행
        /// </summary>
        public void UpdateTable()
        {
            HtmlDocument = browser.Document;
            TableHeader = new TableSet(browser.Document.GetElementsByTagName("thead")[0]);
            TableBody = new TableSet(browser.Document.GetElementsByTagName("tbody")[0]);
        }

        public void SaveExcel(string fullPath)
        {
            object temp = Clipboard.GetDataObject();
            Clipboard.SetText(HtmlDocument.Body.Parent.OuterHtml);
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.WindowState = XlWindowState.xlMaximized;
            Workbook workbook = excel.Workbooks.Add(XlSheetType.xlWorksheet);
            Worksheet sheet = workbook.Sheets[1];
            excel.ActiveWorkbook.Sheets[1].Activate();
            sheet.Range["A1"].Select();
            sheet.PasteSpecial(Type.Missing, false, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            sheet.Range["A1"].Select();
            workbook.SaveAs(fullPath);
            workbook.Close();
            excel.Quit();
            Clipboard.SetDataObject(temp);
        }

        /// <summary>
        /// 이미지를 Base64 형태의 문자열로 변경 (image 태그의 src에 사용)
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static string ImageToBase64(Image image, ImageFormat foramt)
        {
            string result = null;
            using (MemoryStream m = new MemoryStream())
            {
                image.Save(m, foramt);
                byte[] imageBytes = m.ToArray();
                result = string.Format("data:image/bmp;base64,{0}", Convert.ToBase64String(imageBytes));
            }
            return result;
        }

        private void browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            HtmlDocument document = browser.Document;
            HtmlElement head = document.GetElementsByTagName("head")[0];
            HTMLDocument mshtml = (HTMLDocument)document.DomDocument;
            IHTMLDOMNode msHead = null;
            foreach (IHTMLDOMNode item in mshtml.getElementsByTagName("head"))
            {
                msHead = item;
            }
            List<IHTMLDOMNode> origins = new List<IHTMLDOMNode>();
            foreach (IHTMLDOMNode item in msHead.childNodes)
            {
                origins.Add(item);
                msHead.removeChild(item);
            }
            IHTMLStyleSheet sytle = mshtml.createStyleSheet();
            sytle.cssText = Style;
            HtmlElement jquery = document.CreateElement("script");
            ((IHTMLScriptElement)jquery.DomElement).text = Resources.jquery;
            HtmlElement script = document.CreateElement("script");
            ((IHTMLScriptElement)script.DomElement).text = Script;
            head.AppendChild(jquery);
            head.AppendChild(script);
            foreach (IHTMLDOMNode item in origins)
            {
                msHead.appendChild(item);
            }
            UpdateTable();
        }

        private void browser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            if ("event".Equals(e.Url.Scheme))
            {
                string objectType = e.Url.Host;
                string eventType = e.Url.Segments[1];
                if ("th".Equals(objectType) && "click".Equals(eventType))
                {
                    NameValueCollection parameters = HttpUtility.ParseQueryString(e.Url.Query);
                    if (TableViewHeaderClick != null)
                    {
                        int rowIndex = int.Parse(parameters["rowIndex"]);
                        int columnIndex = int.Parse(parameters["columnIndex"]);
                        string value = parameters["data"];
                        TableViewDataEventArgs args = new TableViewDataEventArgs(TableHeader[rowIndex][columnIndex], rowIndex, columnIndex, value);
                        TableViewHeaderClick(this, args);
                    }
                }
                else if ("td".Equals(objectType) && "click".Equals(eventType))
                {
                    NameValueCollection parameters = HttpUtility.ParseQueryString(e.Url.Query);
                    if (TableViewDataClick != null)
                    {
                        int rowIndex = int.Parse(parameters["rowIndex"]);
                        int columnIndex = int.Parse(parameters["columnIndex"]);
                        string value = parameters["data"];
                        TableViewDataEventArgs args = new TableViewDataEventArgs(TableBody[rowIndex][columnIndex], rowIndex, columnIndex, value);
                        TableViewDataClick(this, args);
                    }
                }
                else if ("th.checkbox".Equals(objectType) && "change".Equals(eventType))
                {
                    NameValueCollection parameters = HttpUtility.ParseQueryString(e.Url.Query);
                    if (TableViewHeaderCheckBoxChange != null)
                    {
                        int rowIndex = int.Parse(parameters["rowIndex"]);
                        int columnIndex = int.Parse(parameters["columnIndex"]);
                        bool isChecked = "true".Equals(parameters["data"]);
                        TableViewDataEventArgs args = new TableViewDataEventArgs(TableBody[rowIndex][columnIndex], rowIndex, columnIndex, isChecked);
                        TableViewHeaderCheckBoxChange(this, args);
                    }
                }
                else if ("td.checkbox".Equals(objectType) && "change".Equals(eventType))
                {
                    NameValueCollection parameters = HttpUtility.ParseQueryString(e.Url.Query);
                    if (TableViewDataCheckBoxChange != null)
                    {
                        int rowIndex = int.Parse(parameters["rowIndex"]);
                        int columnIndex = int.Parse(parameters["columnIndex"]);
                        bool isChecked = "true".Equals(parameters["data"]);
                        TableViewDataEventArgs args = new TableViewDataEventArgs(TableBody[rowIndex][columnIndex], rowIndex, columnIndex, isChecked);
                        TableViewDataCheckBoxChange(this, args);
                    }
                }
                else if ("th.radio".Equals(objectType) && "click".Equals(eventType))
                {
                    NameValueCollection parameters = HttpUtility.ParseQueryString(e.Url.Query);
                    if (TableViewHeaderRadioClick != null)
                    {
                        int rowIndex = int.Parse(parameters["rowIndex"]);
                        int columnIndex = int.Parse(parameters["columnIndex"]);
                        string value = parameters["data"];
                        TableViewDataEventArgs args = new TableViewDataEventArgs(TableBody[rowIndex][columnIndex], rowIndex, columnIndex, value);
                        TableViewHeaderRadioClick(this, args);
                    }
                }
                else if ("td.radio".Equals(objectType) && "click".Equals(eventType))
                {
                    NameValueCollection parameters = HttpUtility.ParseQueryString(e.Url.Query);
                    if (TableViewDataRadioClick != null)
                    {
                        int rowIndex = int.Parse(parameters["rowIndex"]);
                        int columnIndex = int.Parse(parameters["columnIndex"]);
                        string value = parameters["data"];
                        TableViewDataEventArgs args = new TableViewDataEventArgs(TableBody[rowIndex][columnIndex], rowIndex, columnIndex, value);
                        TableViewDataRadioClick(this, args);
                    }
                }
                else if ("th.button".Equals(objectType) && "click".Equals(eventType))
                {
                    NameValueCollection parameters = HttpUtility.ParseQueryString(e.Url.Query);
                    if (TableViewHeaderButtonClick != null)
                    {
                        int rowIndex = int.Parse(parameters["rowIndex"]);
                        int columnIndex = int.Parse(parameters["columnIndex"]);
                        string value = parameters["data"];
                        TableViewDataEventArgs args = new TableViewDataEventArgs(TableBody[rowIndex][columnIndex], rowIndex, columnIndex, value);
                        TableViewHeaderButtonClick(this, args);
                    }
                }
                else if ("td.button".Equals(objectType) && "click".Equals(eventType))
                {
                    NameValueCollection parameters = HttpUtility.ParseQueryString(e.Url.Query);
                    if (TableViewDataButtonClick != null)
                    {
                        int rowIndex = int.Parse(parameters["rowIndex"]);
                        int columnIndex = int.Parse(parameters["columnIndex"]);
                        string value = parameters["data"];
                        TableViewDataEventArgs args = new TableViewDataEventArgs(TableBody[rowIndex][columnIndex], rowIndex, columnIndex, value);
                        TableViewDataButtonClick(this, args);
                    }
                }
                else if ("th.select".Equals(objectType) && "change".Equals(eventType))
                {
                    NameValueCollection parameters = HttpUtility.ParseQueryString(e.Url.Query);
                    if (TableViewHeaderSelectChangeClick != null)
                    {
                        int rowIndex = int.Parse(parameters["rowIndex"]);
                        int columnIndex = int.Parse(parameters["columnIndex"]);
                        string value = parameters["data"];
                        TableViewDataEventArgs args = new TableViewDataEventArgs(TableBody[rowIndex][columnIndex], rowIndex, columnIndex, value);
                        TableViewHeaderSelectChangeClick(this, args);
                    }
                }
                else if ("td.select".Equals(objectType) && "change".Equals(eventType))
                {
                    NameValueCollection parameters = HttpUtility.ParseQueryString(e.Url.Query);
                    if (TableViewDataSelectChangeClick != null)
                    {
                        int rowIndex = int.Parse(parameters["rowIndex"]);
                        int columnIndex = int.Parse(parameters["columnIndex"]);
                        string value = parameters["data"];
                        TableViewDataEventArgs args = new TableViewDataEventArgs(TableBody[rowIndex][columnIndex], rowIndex, columnIndex, value);
                        TableViewDataSelectChangeClick(this, args);
                    }
                }
                e.Cancel = true;
            }
            else if("about:black".Equals(e.Url.ToString()))
            {
                if(isLoad)
                {
                    isLoad = false;
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void browser_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if(e.Control && e.KeyCode == Keys.C)
            {
                browser.Document.ExecCommand("Copy", false, null);
            }
        }

        public static void DrawToBitmap(ContainerControl control, Bitmap bitmap, System.Drawing.Rectangle targetBounds)
        {
            ContainerControl parent = control.ParentForm;
            System.Drawing.Point location = control.Location;
            if(parent == null)
            {
                parent = control;
                location = System.Drawing.Point.Empty;
            }
            location = parent.PointToScreen(location);
            Bitmap image = new Bitmap(control.ClientSize.Width, control.ClientSize.Height, PixelFormat.Format32bppArgb);
            Graphics graphics = Graphics.FromImage(image);
            graphics.CopyFromScreen(location.X, location.Y, 0, 0, image.Size, CopyPixelOperation.SourceCopy);
            Graphics origin = Graphics.FromImage(bitmap);
            origin.DrawImage(image, new RectangleF(0, 0, bitmap.Width, bitmap.Height));
            graphics.Dispose();
            origin.Dispose();
        }
    }
}