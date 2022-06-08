
namespace QLDH
{
    partial class FormChonDongHo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dataSet = new QLDH.DataSet();
            this.bdsDongHo = new System.Windows.Forms.BindingSource(this.components);
            this.donghoTableAdapter = new QLDH.DataSetTableAdapters.DonghoTableAdapter();
            this.tableAdapterManager = new QLDH.DataSetTableAdapters.TableAdapterManager();
            this.gcDongHo = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnTHOAT = new System.Windows.Forms.Button();
            this.btnCHON = new System.Windows.Forms.Button();
            this.colMADH = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTENDH = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDVT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSOLUONGTON = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsDongHo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDongHo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataSet
            // 
            this.dataSet.DataSetName = "DataSet";
            this.dataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // bdsDongHo
            // 
            this.bdsDongHo.DataMember = "Dongho";
            this.bdsDongHo.DataSource = this.dataSet;
            // 
            // donghoTableAdapter
            // 
            this.donghoTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.CTDDHTableAdapter = null;
            this.tableAdapterManager.CTPNTableAdapter = null;
            this.tableAdapterManager.CTPXTableAdapter = null;
            this.tableAdapterManager.DatHangTableAdapter = null;
            this.tableAdapterManager.DonghoTableAdapter = this.donghoTableAdapter;
            this.tableAdapterManager.NhanVienTableAdapter = null;
            this.tableAdapterManager.PhieuNhapTableAdapter = null;
            this.tableAdapterManager.PhieuXuatTableAdapter = null;
            this.tableAdapterManager.UpdateOrder = QLDH.DataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // gcDongHo
            // 
            this.gcDongHo.DataSource = this.bdsDongHo;
            this.gcDongHo.Dock = System.Windows.Forms.DockStyle.Top;
            this.gcDongHo.Location = new System.Drawing.Point(0, 0);
            this.gcDongHo.MainView = this.gridView1;
            this.gcDongHo.Name = "gcDongHo";
            this.gcDongHo.Size = new System.Drawing.Size(738, 278);
            this.gcDongHo.TabIndex = 1;
            this.gcDongHo.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colMADH,
            this.colTENDH,
            this.colDVT,
            this.colSOLUONGTON});
            this.gridView1.GridControl = this.gcDongHo;
            this.gridView1.Name = "gridView1";
            // 
            // btnTHOAT
            // 
            this.btnTHOAT.BackColor = System.Drawing.Color.Red;
            this.btnTHOAT.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTHOAT.ForeColor = System.Drawing.Color.White;
            this.btnTHOAT.Location = new System.Drawing.Point(419, 343);
            this.btnTHOAT.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnTHOAT.Name = "btnTHOAT";
            this.btnTHOAT.Size = new System.Drawing.Size(116, 38);
            this.btnTHOAT.TabIndex = 14;
            this.btnTHOAT.Text = "THOÁT";
            this.btnTHOAT.UseVisualStyleBackColor = false;
            this.btnTHOAT.Click += new System.EventHandler(this.btnTHOAT_Click);
            // 
            // btnCHON
            // 
            this.btnCHON.BackColor = System.Drawing.Color.Blue;
            this.btnCHON.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCHON.ForeColor = System.Drawing.Color.White;
            this.btnCHON.Location = new System.Drawing.Point(179, 343);
            this.btnCHON.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCHON.Name = "btnCHON";
            this.btnCHON.Size = new System.Drawing.Size(116, 38);
            this.btnCHON.TabIndex = 13;
            this.btnCHON.Text = "CHỌN";
            this.btnCHON.UseVisualStyleBackColor = false;
            this.btnCHON.Click += new System.EventHandler(this.btnCHON_Click);
            // 
            // colMADH
            // 
            this.colMADH.Caption = "Mã Đồng Hồ";
            this.colMADH.FieldName = "MADH";
            this.colMADH.Name = "colMADH";
            this.colMADH.OptionsColumn.AllowEdit = false;
            this.colMADH.Visible = true;
            this.colMADH.VisibleIndex = 0;
            // 
            // colTENDH
            // 
            this.colTENDH.Caption = "Tên Đồng Hồ ";
            this.colTENDH.FieldName = "TENDH";
            this.colTENDH.Name = "colTENDH";
            this.colTENDH.OptionsColumn.AllowEdit = false;
            this.colTENDH.Visible = true;
            this.colTENDH.VisibleIndex = 1;
            // 
            // colDVT
            // 
            this.colDVT.Caption = "Đơn Vị Tính";
            this.colDVT.FieldName = "DVT";
            this.colDVT.Name = "colDVT";
            this.colDVT.OptionsColumn.AllowEdit = false;
            this.colDVT.Visible = true;
            this.colDVT.VisibleIndex = 2;
            // 
            // colSOLUONGTON
            // 
            this.colSOLUONGTON.Caption = "Số Lượng Tồn";
            this.colSOLUONGTON.FieldName = "SOLUONGTON";
            this.colSOLUONGTON.Name = "colSOLUONGTON";
            this.colSOLUONGTON.OptionsColumn.AllowEdit = false;
            this.colSOLUONGTON.Visible = true;
            this.colSOLUONGTON.VisibleIndex = 3;
            // 
            // FormChonDongHo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(738, 451);
            this.Controls.Add(this.btnTHOAT);
            this.Controls.Add(this.btnCHON);
            this.Controls.Add(this.gcDongHo);
            this.Name = "FormChonDongHo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CHỌN ĐỒNG HỒ";
            this.Load += new System.EventHandler(this.FormChonDongHo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsDongHo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDongHo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DataSet dataSet;
        private System.Windows.Forms.BindingSource bdsDongHo;
        private DataSetTableAdapters.DonghoTableAdapter donghoTableAdapter;
        private DataSetTableAdapters.TableAdapterManager tableAdapterManager;
        private DevExpress.XtraGrid.GridControl gcDongHo;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colMADH;
        private DevExpress.XtraGrid.Columns.GridColumn colTENDH;
        private DevExpress.XtraGrid.Columns.GridColumn colDVT;
        private DevExpress.XtraGrid.Columns.GridColumn colSOLUONGTON;
        private System.Windows.Forms.Button btnTHOAT;
        private System.Windows.Forms.Button btnCHON;
    }
}