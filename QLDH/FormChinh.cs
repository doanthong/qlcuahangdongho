using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QLDH
{
    public partial class FormChinh : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public FormChinh()
        {
            InitializeComponent();
        }

        private Form CheckExists(Type ftype)
        {
            foreach (Form f in this.MdiChildren)
                if (f.GetType() == ftype)
                    return f;
            return null;
        }

        /************************************************************
         *enableButtons: kích hoạt các tab chức năng và nút đăng xuất
         ************************************************************/

        internal void enableButtons()
        {
            btnDangNhap.Enabled = false;
            btnDangXuat.Enabled = true;

            pageNhapXuat.Visible = true;
            pageNghiepVu.Visible = true;
            btnTaoTaiKhoan.Enabled = true;

            if (Program.role == "NHANVIEN")
            {
                btnTaoTaiKhoan.Enabled = false;
            }
        }

        /************************************************************
         * Dispose: giải phóng các form khỏi bộ nhớ. Ví dụ form nhân viên,...
         * Close: đóng hoàn toàn chương trình lại
         ************************************************************/

        private void logout()
        {
            foreach (Form f in this.MdiChildren)
                f.Dispose();
        }

        /************************************************************
         * Step 1: giải phóng các form khỏi bộ nhớ
         * Step 2: vô hiệu hóa các tab
         * Step 3: gọi lại form đăng nhập
         ************************************************************/

        private void btnDangXuat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            logout();

            btnDangNhap.Enabled = true;
            btnDangXuat.Enabled = false;

            pageNhapXuat.Visible = false;
            pageNghiepVu.Visible = false;

            Form f = this.CheckExists(typeof(FormDangNhap));
            if (f != null)
            {
                f.Activate();
            }
            else
            {
                FormDangNhap form = new FormDangNhap();
                form.Show();
            }

            Program.formChinh.MANHANVIEN.Text = "MÃ NHÂN VIÊN:";
            Program.formChinh.HOTEN.Text = "HỌ TÊN:";
            Program.formChinh.NHOM.Text = "VAI TRÒ:";
        }

        /************************************************************
         * f.MdiParent = this; cái form này có form cha là this - tức form chính
         ************************************************************/

        private void btnDangNhap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form f = this.CheckExists(typeof(FormDangNhap));
            if (f != null)
            {
                f.Activate();
            }
            else
            {
                FormDangNhap form = new FormDangNhap();
                form.Show();
            }
        }

        private void FormChinh_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void btnTaoTaiKhoan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form f = this.CheckExists(typeof(FormTaoTaiKhoan));
            if (f != null)
            {
                f.Activate();
            }
            else
            {
                FormTaoTaiKhoan form = new FormTaoTaiKhoan();
                form.Show();
            }
        }

        private void btnNhanVien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form f = this.CheckExists(typeof(FormNhanVien));
            if (f != null)
            {
                f.Activate();
            }
            else
            {
                FormNhanVien form = new FormNhanVien();
                form.MdiParent = this;
                form.Show();
            }
        }

        private void btnDongHo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form f = this.CheckExists(typeof(FormDongHo));
            if (f != null)
            {
                f.Activate();
            }
            else
            {
                FormDongHo form = new FormDongHo();
                form.MdiParent = this;
                form.Show();
            }
        }

        private void btnDonDatHang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form f = this.CheckExists(typeof(FormDonDatHang));
            if (f != null)
            {
                f.Activate();
            }
            else
            {
                FormDonDatHang form = new FormDonDatHang();
                form.MdiParent = this;
                form.Show();
            }
        }

        private void btnPhieuNhap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form f = this.CheckExists(typeof(FormPhieuNhap));
            if (f != null)
            {
                f.Activate();
            }
            else
            {
                FormPhieuNhap form = new FormPhieuNhap();
                form.MdiParent = this;
                form.Show();
            }
        }

        private void btnPhieuXuat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form f = this.CheckExists(typeof(FormPhieuXuat));
            if (f != null)
            {
                f.Activate();
            }
            else
            {
                FormPhieuXuat form = new FormPhieuXuat();
                form.MdiParent = this;
                form.Show();
            }
        }
    }
}
