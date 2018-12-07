namespace TableHelperTester
{
    partial class FrmMain
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.fullTableView = new TableHelper.TableView();
            this.scrollTableView = new TableHelper.TableView();
            this.btnFullExcel = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(116, 256);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "추가";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(197, 256);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 2;
            this.btnRemove.Text = "선택삭제";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // fullTableView
            // 
            this.fullTableView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fullTableView.DefaultType = "full";
            this.fullTableView.Location = new System.Drawing.Point(278, 12);
            this.fullTableView.Name = "fullTableView";
            this.fullTableView.Script = null;
            this.fullTableView.Size = new System.Drawing.Size(260, 238);
            this.fullTableView.Style = null;
            this.fullTableView.TabIndex = 3;
            this.fullTableView.Timeout = System.TimeSpan.Parse("00:00:05");
            // 
            // scrollTableView
            // 
            this.scrollTableView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.scrollTableView.DefaultType = "scroll";
            this.scrollTableView.Location = new System.Drawing.Point(12, 12);
            this.scrollTableView.Name = "scrollTableView";
            this.scrollTableView.Script = null;
            this.scrollTableView.Size = new System.Drawing.Size(260, 238);
            this.scrollTableView.Style = null;
            this.scrollTableView.TabIndex = 0;
            this.scrollTableView.Timeout = System.TimeSpan.Parse("00:00:05");
            this.scrollTableView.TableViewHeaderClick += new TableHelper.TableView.TableViewHeaderClickEventHandler(this.tableView_TableViewHeaderClick);
            this.scrollTableView.TableViewDataClick += new TableHelper.TableView.TableViewDataClickEventHandler(this.tableView_TableViewDataClick);
            // 
            // btnFullExcel
            // 
            this.btnFullExcel.Location = new System.Drawing.Point(463, 256);
            this.btnFullExcel.Name = "btnFullExcel";
            this.btnFullExcel.Size = new System.Drawing.Size(75, 23);
            this.btnFullExcel.TabIndex = 4;
            this.btnFullExcel.Text = "엑셀";
            this.btnFullExcel.UseVisualStyleBackColor = true;
            this.btnFullExcel.Click += new System.EventHandler(this.btnFullExcel_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(382, 256);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 5;
            this.btnPrint.Text = "인쇄";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(551, 287);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnFullExcel);
            this.Controls.Add(this.fullTableView);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.scrollTableView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmMain";
            this.ShowIcon = false;
            this.Text = "예제";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private TableHelper.TableView scrollTableView;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
        private TableHelper.TableView fullTableView;
        private System.Windows.Forms.Button btnFullExcel;
        private System.Windows.Forms.Button btnPrint;
    }
}

