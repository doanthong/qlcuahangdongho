
namespace QLDH
{
    partial class FormChinh
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormChinh));
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.btnDangNhap = new DevExpress.XtraBars.BarButtonItem();
            this.btnDangXuat = new DevExpress.XtraBars.BarButtonItem();
            this.btnTaoTaiKhoan = new DevExpress.XtraBars.BarButtonItem();
            this.btnNhanVien = new DevExpress.XtraBars.BarButtonItem();
            this.btnDongHo = new DevExpress.XtraBars.BarButtonItem();
            this.btnDonDatHang = new DevExpress.XtraBars.BarButtonItem();
            this.btnPhieuNhap = new DevExpress.XtraBars.BarButtonItem();
            this.btnPhieuXuat = new DevExpress.XtraBars.BarButtonItem();
            this.pageNhapXuat = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.pageNghiepVu = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.pageHeThong = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup3 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.MANHANVIEN = new System.Windows.Forms.ToolStripStatusLabel();
            this.HOTEN = new System.Windows.Forms.ToolStripStatusLabel();
            this.NHOM = new System.Windows.Forms.ToolStripStatusLabel();
            this.xtraTabbedMdiManager1 = new DevExpress.XtraTabbedMdi.XtraTabbedMdiManager(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.ribbonControl1.SearchEditItem,
            this.btnDangNhap,
            this.btnDangXuat,
            this.btnTaoTaiKhoan,
            this.btnNhanVien,
            this.btnDongHo,
            this.btnDonDatHang,
            this.btnPhieuNhap,
            this.btnPhieuXuat});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 9;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.pageNhapXuat,
            this.pageNghiepVu,
            this.pageHeThong});
            this.ribbonControl1.Size = new System.Drawing.Size(1288, 158);
            // 
            // btnDangNhap
            // 
            this.btnDangNhap.Caption = "ĐĂNG NHẬP";
            this.btnDangNhap.Id = 1;
            this.btnDangNhap.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnDangNhap.ImageOptions.Image")));
            this.btnDangNhap.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnDangNhap.ImageOptions.LargeImage")));
            this.btnDangNhap.LargeWidth = 100;
            this.btnDangNhap.Name = "btnDangNhap";
            this.btnDangNhap.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnDangNhap_ItemClick);
            // 
            // btnDangXuat
            // 
            this.btnDangXuat.Caption = "ĐĂNG XUẤT";
            this.btnDangXuat.Enabled = false;
            this.btnDangXuat.Id = 2;
            this.btnDangXuat.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnDangXuat.ImageOptions.Image")));
            this.btnDangXuat.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnDangXuat.ImageOptions.LargeImage")));
            this.btnDangXuat.LargeWidth = 100;
            this.btnDangXuat.Name = "btnDangXuat";
            this.btnDangXuat.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnDangXuat_ItemClick);
            // 
            // btnTaoTaiKhoan
            // 
            this.btnTaoTaiKhoan.Caption = "TẠO TÀI KHOẢN";
            this.btnTaoTaiKhoan.Enabled = false;
            this.btnTaoTaiKhoan.Id = 3;
            this.btnTaoTaiKhoan.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnTaoTaiKhoan.ImageOptions.Image")));
            this.btnTaoTaiKhoan.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnTaoTaiKhoan.ImageOptions.LargeImage")));
            this.btnTaoTaiKhoan.LargeWidth = 100;
            this.btnTaoTaiKhoan.Name = "btnTaoTaiKhoan";
            this.btnTaoTaiKhoan.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnTaoTaiKhoan_ItemClick);
            // 
            // btnNhanVien
            // 
            this.btnNhanVien.Caption = "NHÂN VIÊN";
            this.btnNhanVien.Id = 4;
            this.btnNhanVien.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnNhanVien.ImageOptions.Image")));
            this.btnNhanVien.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnNhanVien.ImageOptions.LargeImage")));
            this.btnNhanVien.LargeWidth = 100;
            this.btnNhanVien.Name = "btnNhanVien";
            this.btnNhanVien.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnNhanVien_ItemClick);
            // 
            // btnDongHo
            // 
            this.btnDongHo.Caption = "ĐỒNG HỒ";
            this.btnDongHo.Id = 5;
            this.btnDongHo.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnDongHo.ImageOptions.Image")));
            this.btnDongHo.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnDongHo.ImageOptions.LargeImage")));
            this.btnDongHo.LargeWidth = 100;
            this.btnDongHo.Name = "btnDongHo";
            this.btnDongHo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnDongHo_ItemClick);
            // 
            // btnDonDatHang
            // 
            this.btnDonDatHang.Caption = "ĐƠN ĐẶT HÀNG";
            this.btnDonDatHang.Id = 6;
            this.btnDonDatHang.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnDonDatHang.ImageOptions.Image")));
            this.btnDonDatHang.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnDonDatHang.ImageOptions.LargeImage")));
            this.btnDonDatHang.LargeWidth = 100;
            this.btnDonDatHang.Name = "btnDonDatHang";
            this.btnDonDatHang.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnDonDatHang_ItemClick);
            // 
            // btnPhieuNhap
            // 
            this.btnPhieuNhap.Caption = "PHIẾU NHẬP";
            this.btnPhieuNhap.Id = 7;
            this.btnPhieuNhap.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnPhieuNhap.ImageOptions.Image")));
            this.btnPhieuNhap.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnPhieuNhap.ImageOptions.LargeImage")));
            this.btnPhieuNhap.LargeWidth = 100;
            this.btnPhieuNhap.Name = "btnPhieuNhap";
            this.btnPhieuNhap.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnPhieuNhap_ItemClick);
            // 
            // btnPhieuXuat
            // 
            this.btnPhieuXuat.Caption = "PHIẾU XUẤT";
            this.btnPhieuXuat.Id = 8;
            this.btnPhieuXuat.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnPhieuXuat.ImageOptions.Image")));
            this.btnPhieuXuat.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnPhieuXuat.ImageOptions.LargeImage")));
            this.btnPhieuXuat.LargeWidth = 100;
            this.btnPhieuXuat.Name = "btnPhieuXuat";
            this.btnPhieuXuat.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnPhieuXuat_ItemClick);
            // 
            // pageNhapXuat
            // 
            this.pageNhapXuat.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1});
            this.pageNhapXuat.Name = "pageNhapXuat";
            this.pageNhapXuat.Text = "NHẬP XUẤT";
            this.pageNhapXuat.Visible = false;
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.btnNhanVien);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnDongHo);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "QUẢN LÝ NHẬP XUẤT";
            // 
            // pageNghiepVu
            // 
            this.pageNghiepVu.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup2});
            this.pageNghiepVu.Name = "pageNghiepVu";
            this.pageNghiepVu.Text = "NGHIỆP VỤ";
            this.pageNghiepVu.Visible = false;
            // 
            // ribbonPageGroup2
            // 
            this.ribbonPageGroup2.ItemLinks.Add(this.btnDonDatHang);
            this.ribbonPageGroup2.ItemLinks.Add(this.btnPhieuNhap);
            this.ribbonPageGroup2.ItemLinks.Add(this.btnPhieuXuat);
            this.ribbonPageGroup2.Name = "ribbonPageGroup2";
            this.ribbonPageGroup2.Text = "QUẢN LÝ NGHIỆP VỤ";
            // 
            // pageHeThong
            // 
            this.pageHeThong.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup3});
            this.pageHeThong.Name = "pageHeThong";
            this.pageHeThong.Text = "HỆ THỐNG";
            // 
            // ribbonPageGroup3
            // 
            this.ribbonPageGroup3.ItemLinks.Add(this.btnDangNhap);
            this.ribbonPageGroup3.ItemLinks.Add(this.btnDangXuat);
            this.ribbonPageGroup3.ItemLinks.Add(this.btnTaoTaiKhoan);
            this.ribbonPageGroup3.Name = "ribbonPageGroup3";
            this.ribbonPageGroup3.Text = "QUẢN LÝ TÀI KHOẢN";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MANHANVIEN,
            this.HOTEN,
            this.NHOM});
            this.statusStrip1.Location = new System.Drawing.Point(0, 591);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 12, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1288, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // MANHANVIEN
            // 
            this.MANHANVIEN.Name = "MANHANVIEN";
            this.MANHANVIEN.Size = new System.Drawing.Size(92, 17);
            this.MANHANVIEN.Text = "MÃ NHÂN VIÊN";
            // 
            // HOTEN
            // 
            this.HOTEN.Name = "HOTEN";
            this.HOTEN.Size = new System.Drawing.Size(49, 17);
            this.HOTEN.Text = "HỌ TÊN";
            // 
            // NHOM
            // 
            this.NHOM.Name = "NHOM";
            this.NHOM.Size = new System.Drawing.Size(49, 17);
            this.NHOM.Text = "VAI TRÒ";
            // 
            // xtraTabbedMdiManager1
            // 
            this.xtraTabbedMdiManager1.MdiParent = this;
            // 
            // FormChinh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1288, 613);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.ribbonControl1);
            this.IsMdiContainer = true;
            this.Name = "FormChinh";
            this.Ribbon = this.ribbonControl1;
            this.Text = "QUẢN LÝ CỬA HÀNG BÁN ĐỒNG HỒ";
            this.Load += new System.EventHandler(this.FormChinh_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPage pageNhapXuat;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.BarButtonItem btnDangNhap;
        private DevExpress.XtraBars.BarButtonItem btnDangXuat;
        private DevExpress.XtraBars.BarButtonItem btnTaoTaiKhoan;
        private DevExpress.XtraBars.Ribbon.RibbonPage pageNghiepVu;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.XtraBars.Ribbon.RibbonPage pageHeThong;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup3;
        private System.Windows.Forms.StatusStrip statusStrip1;
        public System.Windows.Forms.ToolStripStatusLabel MANHANVIEN;
        public System.Windows.Forms.ToolStripStatusLabel HOTEN;
        public System.Windows.Forms.ToolStripStatusLabel NHOM;
        private DevExpress.XtraBars.BarButtonItem btnNhanVien;
        private DevExpress.XtraBars.BarButtonItem btnDongHo;
        private DevExpress.XtraBars.BarButtonItem btnDonDatHang;
        private DevExpress.XtraBars.BarButtonItem btnPhieuNhap;
        private DevExpress.XtraBars.BarButtonItem btnPhieuXuat;
        private DevExpress.XtraTabbedMdi.XtraTabbedMdiManager xtraTabbedMdiManager1;
    }
}

