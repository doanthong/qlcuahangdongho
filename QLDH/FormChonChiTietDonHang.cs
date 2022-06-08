using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLDH
{
    public partial class FormChonChiTietDonHang : DevExpress.XtraEditors.XtraForm
    {
        public FormChonChiTietDonHang()
        {
            InitializeComponent();
        }

        private void cTDDHBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bdsCTDDH.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dataSet);

        }

        private void FormChonChiTietDonHang_Load(object sender, EventArgs e)
        {
            dataSet.EnforceConstraints = false;
            this.cTDDHTableAdapter.Connection.ConnectionString = Program.connstr;
            this.cTDDHTableAdapter.Fill(this.dataSet.CTDDH);
        }

        private void btnChon_Click(object sender, EventArgs e)
        {
            DataRowView drv = ((DataRowView)(bdsCTDDH.Current));
            string maDonHang = drv["MasoDDH"].ToString().Trim();
            string maDongHo = drv["MaDH"].ToString().Trim();
            int soLuong = int.Parse(drv["SOLUONG"].ToString().Trim());
            int donGia = int.Parse(drv["DONGIA"].ToString().Trim());


            /*Kiem tra xem ma don hang cua gcPhieuNhap co trung voi ma don hang duoc chon hay khong ?*/
            Program.maDonDatHangDuocChonChiTiet = maDonHang;
            if (Program.maDonDatHangDuocChon != Program.maDonDatHangDuocChonChiTiet)
            {
                MessageBox.Show("Bạn phải chọn chi tiết đơn hàng có mã đơn hàng là " + Program.maDonDatHangDuocChon, "Thông báo", MessageBoxButtons.OK);
                return;
            }

            Program.maDongHoDuocChon = maDongHo;
            Program.soLuongDongHo = soLuong;
            Program.donGia = donGia;
            this.Close();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}