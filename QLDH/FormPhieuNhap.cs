using DevExpress.XtraBars;
using DevExpress.XtraGrid;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLDH
{
    public partial class FormPhieuNhap : DevExpress.XtraEditors.XtraForm
    {

        int viTri = 0;
        bool dangThemMoi = false;
        Stack undoList = new Stack();

        BindingSource bds = null;
        GridControl gc = null;
        string type = "";

        private Form CheckExists(Type ftype)
        {
            foreach (Form f in this.MdiChildren)
                if (f.GetType() == ftype)
                    return f;
            return null;
        }

        public FormPhieuNhap()
        {
            InitializeComponent();
        }

        private void phieuNhapBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bdsPhieuNhap.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dataSet);

        }

        private void FormPhieuNhap_Load(object sender, EventArgs e)
        {
            dataSet.EnforceConstraints = false;

            this.cTPNTableAdapter.Connection.ConnectionString = Program.connstr;
            this.cTPNTableAdapter.Fill(this.dataSet.CTPN);

            this.phieuNhapTableAdapter.Connection.ConnectionString = Program.connstr;
            this.phieuNhapTableAdapter.Fill(this.dataSet.PhieuNhap);

        }

        private void btnPhieuNhap_ItemClick(object sender, ItemClickEventArgs e)
        {
            btnMENU.Links[0].Caption = "Phiếu Nhập";

            bds = bdsPhieuNhap;
            gc = gcPhieuNhap;

            txtMAPN.Enabled = false;
            dteNGAY.Enabled = false;
            txtMADDH.Enabled = false;
            btnChonDonHang.Enabled = false;
            txtMANV.Enabled = false;
            btnChonChiTietDonHang.Enabled = false;

            txtMADHCTPN.Enabled = false;
            txtSOLUONGCTPN.Enabled = false;
            txtDONGIACTPN.Enabled = false;

            gcPhieuNhap.Enabled = true;
            gcCTPN.Enabled = true;


            /*Step 3*/
            /*CUA HANG chi xem du lieu*/
            if (Program.role == "CUAHANG")
            {
                this.btnTHEM.Enabled = false;
                this.btnXOA.Enabled = false;
                this.btnGHI.Enabled = false;
                this.btnHOANTAC.Enabled = false;
                this.btnLAMMOI.Enabled = true;
                this.btnMENU.Enabled = true;
                this.groupBoxPhieuNhap.Enabled = false;
            }

            /* NHAN VIEN co the xem - xoa - sua du lieu*/
            if (Program.role == "NHANVIEN")
            {
                this.btnTHEM.Enabled = true;
                bool turnOn = (bdsPhieuNhap.Count > 0) ? true : false;
                this.btnXOA.Enabled = turnOn;
                this.btnGHI.Enabled = true;
                this.btnHOANTAC.Enabled = false;
                this.btnLAMMOI.Enabled = true;
                this.btnMENU.Enabled = true;
            }
        }

        private void btnChonChiTietDonHang_Click(object sender, EventArgs e)
        {
            Program.maDonDatHangDuocChon = ((DataRowView)(bdsPhieuNhap.Current))["MasoDDH"].ToString().Trim();
            FormChonChiTietDonHang form = new FormChonChiTietDonHang();
            form.ShowDialog();

            this.txtMADHCTPN.Text = Program.maDongHoDuocChon;
            this.txtSOLUONGCTPN.Value = Program.soLuongDongHo;
            this.txtDONGIACTPN.Value = Program.donGia;
        }

        private void btnChiTietPhieuNhap_ItemClick(object sender, ItemClickEventArgs e)
        {
            btnMENU.Links[0].Caption = "Chi Tiết Phiếu Nhập";

            bds = bdsCTPN;
            gc = gcPhieuNhap;

            txtMAPN.Enabled = false;
            dteNGAY.Enabled = false;
            txtMADDH.Enabled = false;
            btnChonDonHang.Enabled = false;
            txtMANV.Enabled = false;

            txtMADHCTPN.Enabled = false;
            txtSOLUONGCTPN.Enabled = false;
            txtDONGIACTPN.Enabled = false;

            btnChonChiTietDonHang.Enabled = false;

            gcPhieuNhap.Enabled = true;
            gcCTPN.Enabled = true;

            /*Step 3*/
            /*CUA HANG chi xem du lieu*/
            if (Program.role == "CUAHANG")
            {
                this.btnTHEM.Enabled = false;
                this.btnXOA.Enabled = false;
                this.btnGHI.Enabled = false;
                this.btnHOANTAC.Enabled = false;
                this.btnLAMMOI.Enabled = true;
                this.btnMENU.Enabled = true;
            }

            /* NHAN VIEN co the xem - xoa - sua du lieu*/
            if (Program.role == "NHANVIEN")
            {
                this.btnTHEM.Enabled = true;
                this.btnXOA.Enabled = true;
                this.btnGHI.Enabled = true;
                this.btnHOANTAC.Enabled = false;
                this.btnLAMMOI.Enabled = true;
                this.btnMENU.Enabled = true;
            }
        }

        private void btnChonDonHang_Click(object sender, EventArgs e)
        {
            FormChonDonDatHang form = new FormChonDonDatHang();
            form.ShowDialog();
            this.txtMADDH.Text = Program.maDonDatHangDuocChon;
        }

        private void btnTHEM_ItemClick(object sender, ItemClickEventArgs e)
        {
            viTri = bds.Position;
            dangThemMoi = true;

            bds.AddNew();
            if (btnMENU.Links[0].Caption == "Phiếu Nhập")
            {
                this.txtMAPN.Enabled = true;

                this.dteNGAY.EditValue = DateTime.Now;
                this.dteNGAY.Enabled = false;

                this.txtMADDH.Enabled = false;
                this.btnChonDonHang.Enabled = true;

                this.txtMANV.Text = Program.userName;

                ((DataRowView)(bdsPhieuNhap.Current))["NGAY"] = DateTime.Now;
                ((DataRowView)(bdsPhieuNhap.Current))["MasoDDH"] = Program.maDonDatHangDuocChon;
                ((DataRowView)(bdsPhieuNhap.Current))["MANV"] = Program.userName;
            }

            if (btnMENU.Links[0].Caption == "Chi Tiết Phiếu Nhập")
            {
                DataRowView drv = ((DataRowView)bdsPhieuNhap[bdsPhieuNhap.Position]);
                String maNhanVien = drv["MANV"].ToString();
                if (Program.userName != maNhanVien)
                {
                    MessageBox.Show("Bạn không thêm chi tiết phiếu nhập trên phiếu không phải do mình tạo", "Thông báo", MessageBoxButtons.OK);
                    bdsCTPN.RemoveCurrent();
                    return;
                }

                /*Gan tu dong may truong du lieu nay*/
                ((DataRowView)(bdsCTPN.Current))["MAPN"] = ((DataRowView)(bdsPhieuNhap.Current))["MAPN"];
                ((DataRowView)(bdsCTPN.Current))["MADH"] =
                    Program.maDongHoDuocChon;
                ((DataRowView)(bdsCTPN.Current))["SOLUONG"] =
                    Program.soLuongDongHo;
                ((DataRowView)(bdsCTPN.Current))["DONGIA"] =
                    Program.donGia;

                this.txtMADHCTPN.Enabled = false;
                this.btnChonChiTietDonHang.Enabled = true;

                this.txtSOLUONGCTPN.Enabled = true;
                this.txtSOLUONGCTPN.EditValue = 1;

                this.txtDONGIACTPN.Enabled = true;
                this.txtDONGIACTPN.EditValue = 1;

                this.txtSOLUONGCTPN.Enabled = true;
                this.txtDONGIACTPN.Enabled = true;
            }

            this.btnTHEM.Enabled = false;
            this.btnXOA.Enabled = false;
            this.btnGHI.Enabled = true;

            this.btnHOANTAC.Enabled = true;
            this.btnLAMMOI.Enabled = false;
            this.btnMENU.Enabled = false;

            gcPhieuNhap.Enabled = false;
            gcCTPN.Enabled = false;
        }

        private void btnLAMMOI_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                this.phieuNhapTableAdapter.Fill(this.dataSet.PhieuNhap);
                this.cTPNTableAdapter.Fill(this.dataSet.CTPN);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi làm mời dữ liệu\n\n" + ex.Message, "Thông Báo", MessageBoxButtons.OK);
                Console.WriteLine(ex.Message);
                return;
            }
        }

        private void btnHOANTAC_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (dangThemMoi == true && this.btnTHEM.Enabled == false)
            {
                dangThemMoi = false;

                /*dang o che do Phiếu Nhập*/
                if (btnMENU.Links[0].Caption == "Phiếu Nhập")
                {
                    this.txtMADDH.Enabled = false;
                    dteNGAY.Enabled = false;
                    btnChonDonHang.Enabled = false;
                }
                /*dang o che do Chi Tiết Phiếu Nhập*/
                if (btnMENU.Links[0].Caption == "Chi Tiết Phiếu Nhập")
                {
                    this.txtMADDH.Enabled = false;
                    this.btnChonChiTietDonHang.Enabled = false;
                    this.txtMADHCTPN.Enabled = false;
                    this.txtSOLUONGCTPN.Enabled = false;
                    this.txtDONGIACTPN.Enabled = false;
                    this.btnXOA.Enabled = false;
                }

                this.btnTHEM.Enabled = true;
                this.btnXOA.Enabled = true;
                this.btnGHI.Enabled = true;
                this.btnLAMMOI.Enabled = true;
                this.btnMENU.Enabled = true;
                this.gcPhieuNhap.Enabled = true;
                this.gcCTPN.Enabled = true;

                bds.CancelEdit();
                bds.RemoveCurrent();
                /* trở về lúc đầu con trỏ đang đứng*/
                bds.Position = viTri;
                return;
            }

            if (undoList.Count == 0)
            {
                MessageBox.Show("Không còn thao tác nào để khôi phục", "Thông báo", MessageBoxButtons.OK);
                btnHOANTAC.Enabled = false;
                return;
            }

            bds.CancelEdit();
            String cauTruyVanHoanTac = undoList.Pop().ToString();

            Console.WriteLine(cauTruyVanHoanTac);
            int n = Program.ExecSqlNonQuery(cauTruyVanHoanTac);

            this.phieuNhapTableAdapter.Fill(this.dataSet.PhieuNhap);
            this.cTPNTableAdapter.Fill(this.dataSet.CTPN);

            bdsPhieuNhap.Position = viTri;
        }

        private String taoCauTruyVanHoanTac(String cheDo)
        {
            String cauTruyVan = "";
            DataRowView drv;

            /*TH1: dang sua phieu nhap - nhung ko co truong du lieu nao co the cho sua duoc ca*/
            if (cheDo == "Phiếu Nhập" && dangThemMoi == false)
            {
                // khong co gi ca
            }

            /*TH2: them moi phieu nhap*/
            if (cheDo == "Phiếu Nhập" && dangThemMoi == true)
            {
                // tao trong btnGHI thi hon
            }

            /*TH3: them moi chi tiet phieu nhap*/
            if (cheDo == "Chi Tiết Phiếu Nhập" && dangThemMoi == true)
            {
                // tao trong btnGHI thi hon
            }

            /*TH4: dang sua chi tiet phieu nhap*/
            if (cheDo == "Chi Tiết Phiếu Nhập" && dangThemMoi == false)
            {
                drv = ((DataRowView)(bdsCTPN.Current));
                int soLuong = int.Parse(drv["SOLUONG"].ToString().Trim());
                float donGia = float.Parse(drv["DONGIA"].ToString().Trim());
                String maPhieuNhap = drv["MAPN"].ToString().Trim();
                String maDongHo = drv["MADH"].ToString().Trim();

                cauTruyVan = "UPDATE DBO.CTPN " +
                    "SET " +
                    "SOLUONG = " + soLuong + ", " +
                    "DONGIA = " + donGia + " " +
                    "WHERE MAPN = '" + maPhieuNhap + "' " +
                    "AND MADH = '" + maDongHo + "' ";
            }

            return cauTruyVan;
        }

        private bool kiemTraDuLieuDauVao(String cheDo)
        {
            if (cheDo == "Phiếu Nhập")
            {
                if (txtMAPN.Text == "")
                {
                    MessageBox.Show("Không bỏ trống mã phiếu nhập !", "Thông báo", MessageBoxButtons.OK);
                    txtMAPN.Focus();
                    return false;
                }


                if (txtMANV.Text == "")
                {
                    MessageBox.Show("Không bỏ trống mã nhân viên !", "Thông báo", MessageBoxButtons.OK);
                    return false;
                }

                if (txtMADDH.Text == "")
                {
                    MessageBox.Show("Không bỏ trống mã đơn đặt hàng !", "Thông báo", MessageBoxButtons.OK);
                    return false;
                }
            }

            if (cheDo == "Chi Tiết Phiếu Nhập")
            {

                if (txtMADHCTPN.Text == "")
                {
                    MessageBox.Show("Không bỏ trống mã đồng hồ !", "Thông báo", MessageBoxButtons.OK);
                    return false;
                }

                if (txtSOLUONGCTPN.Value < 0 ||
                    txtSOLUONGCTPN.Value > Program.soLuongDongHo)
                {
                    MessageBox.Show("Số lượng đồng hồ không thể lớn hơn số lượng đồng hồ trong chi tiết đơn hàng !", "Thông báo", MessageBoxButtons.OK);
                    txtSOLUONGCTPN.Focus();
                    return false;
                }

                if (txtDONGIACTPN.Value < 1)
                {
                    MessageBox.Show("Đơn giá không thể nhỏ hơn 1 VND", "Thông báo", MessageBoxButtons.OK);
                    txtDONGIACTPN.Focus();
                    return false;
                }
            }

            return true;
        }

        private void btnGHI_ItemClick(object sender, ItemClickEventArgs e)
        {
            String cheDo = (btnMENU.Links[0].Caption == "Phiếu Nhập") ? "Phiếu Nhập" : "Chi Tiết Phiếu Nhập";

            bool ketQua = kiemTraDuLieuDauVao(cheDo);
            if (ketQua == false) return;

            string cauTruyVanHoanTac = taoCauTruyVanHoanTac(cheDo);

            String maPhieuNhap = txtMAPN.Text.Trim();

            String cauTruyVan =
                    "DECLARE	@result int " +
                    "EXEC @result = SP_KiemTraMaPhieuNhap '" +
                    maPhieuNhap + "' " +
                    "SELECT 'Value' = @result";
            SqlCommand sqlCommand = new SqlCommand(cauTruyVan, Program.conn);
            try
            {
                Program.myReader = Program.ExecSqlDataReader(cauTruyVan);
                if (Program.myReader == null)
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Thực thi database thất bại!\n\n" + ex.Message, "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(ex.Message);
                return;
            }
            Program.myReader.Read();
            int result = int.Parse(Program.myReader.GetValue(0).ToString());
            Program.myReader.Close();

            int viTriConTro = bdsPhieuNhap.Position;
            int viTriMaPhieuNhap = bdsPhieuNhap.Find("MAPN", maPhieuNhap);

            /*Dang them moi phieu nhap*/
            if (result == 1 && cheDo == "Phiếu Nhập" && viTriMaPhieuNhap != viTriConTro)
            {
                MessageBox.Show("Mã phiếu nhập đã được sử dụng !", "Thông báo", MessageBoxButtons.OK);
                txtMAPN.Focus();
                return;
            }
            else
            {
                DialogResult dr = MessageBox.Show("Bạn có chắc muốn ghi dữ liệu vào cơ sở dữ liệu ?", "Thông báo",
                         MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {
                    try
                    {
                        if (cheDo == "Phiếu Nhập" && dangThemMoi == true)
                        {
                            cauTruyVanHoanTac =
                                "DELETE FROM DBO.PHIEUNHAP " +
                                "WHERE MAPN = '" + maPhieuNhap + "'";
                        }

                        if (cheDo == "Chi Tiết Phiếu Nhập" && dangThemMoi == true)
                        {
                            cauTruyVanHoanTac =
                                "DELETE FROM DBO.CTPN " +
                                "WHERE MAPN = '" + maPhieuNhap + "' " +
                                "AND MADH = '" + Program.maDongHoDuocChon + "'";
                        }

                        /* chinh sua chi tiet phieu nhap */
                        undoList.Push(cauTruyVanHoanTac);
                        Console.WriteLine("cau truy van hoan tac");
                        Console.WriteLine(cauTruyVanHoanTac);

                        this.bdsPhieuNhap.EndEdit();
                        this.bdsCTPN.EndEdit();
                        this.phieuNhapTableAdapter.Update(this.dataSet.PhieuNhap);
                        this.cTPNTableAdapter.Update(this.dataSet.CTPN);
                        this.btnTHEM.Enabled = true;
                        this.btnXOA.Enabled = true;
                        this.btnGHI.Enabled = true;
                        this.btnHOANTAC.Enabled = true;
                        this.btnLAMMOI.Enabled = true;
                        this.btnMENU.Enabled = true;
                        this.gcPhieuNhap.Enabled = true;
                        this.gcCTPN.Enabled = true;
                        this.txtSOLUONGCTPN.Enabled = false;
                        this.txtDONGIACTPN.Enabled = false;
                        this.txtMAPN.Enabled = false;

                        dangThemMoi = false;
                        MessageBox.Show("Ghi thành công", "Thông báo", MessageBoxButtons.OK);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        bds.RemoveCurrent();
                        MessageBox.Show("Da xay ra loi !\n\n" + ex.Message, "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
        }

        private void btnXOA_ItemClick(object sender, ItemClickEventArgs e)
        {
            DataRowView drv;
            string cauTruyVanHoanTac = "";
            string cheDo = (btnMENU.Links[0].Caption == "Phiếu Nhập") ? "Phiếu Nhập" : "Chi Tiết Phiếu Nhập";



            if (cheDo == "Phiếu Nhập")
            {
                drv = ((DataRowView)bdsPhieuNhap[bdsPhieuNhap.Position]);
                String maNhanVien = drv["MANV"].ToString();
                if (Program.userName != maNhanVien)
                {
                    MessageBox.Show("Không xóa chi tiết phiếu nhập không phải do mình tạo", "Thông báo", MessageBoxButtons.OK);
                    return;
                }

                if (bdsCTPN.Count > 0)
                {
                    MessageBox.Show("Không thể xóa phiếu nhập vì có chi tiết phiếu nhập", "Thông báo", MessageBoxButtons.OK);
                    return;
                }

                drv = ((DataRowView)bdsPhieuNhap[bdsPhieuNhap.Position]);
                DateTime ngay = ((DateTime)drv["NGAY"]);

                cauTruyVanHoanTac = "INSERT INTO DBO.PHIEUNHAP(MAPN, NGAY, MasoDDH, MANV) " +
                    "VALUES( '" + drv["MAPN"].ToString().Trim() + "', '" +
                    ngay.ToString("yyyy-MM-dd") + "', '" +
                    drv["MasoDDH"].ToString() + "', '" +
                    drv["MANV"].ToString() + "')";
            }

            if (cheDo == "Chi Tiết Phiếu Nhập")
            {
                drv = ((DataRowView)bdsPhieuNhap[bdsPhieuNhap.Position]);
                String maNhanVien = drv["MANV"].ToString();
                if (Program.userName != maNhanVien)
                {
                    MessageBox.Show("Bạn không xóa chi tiết phiếu nhập không phải do mình tạo", "Thông báo", MessageBoxButtons.OK);
                    return;
                }


                drv = ((DataRowView)bdsCTPN[bdsCTPN.Position]);
                cauTruyVanHoanTac = "INSERT INTO DBO.CTPN(MAPN, MADH, SOLUONG, DONGIA) " +
                    "VALUES('" + drv["MAPN"].ToString().Trim() + "', '" +
                    drv["MADH"].ToString().Trim() + "', " +
                    drv["SOLUONG"].ToString().Trim() + ", " +
                    drv["DONGIA"].ToString().Trim() + ")";
            }

            undoList.Push(cauTruyVanHoanTac);

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không ?", "Thông báo",
                MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    viTri = bds.Position;
                    if (cheDo == "Phiếu Nhập")
                    {
                        bdsPhieuNhap.RemoveCurrent();
                    }
                    if (cheDo == "Chi Tiết Phiếu Nhập")
                    {
                        bdsCTPN.RemoveCurrent();
                    }


                    this.phieuNhapTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.phieuNhapTableAdapter.Update(this.dataSet.PhieuNhap);

                    this.cTPNTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.cTPNTableAdapter.Update(this.dataSet.CTPN);

                    dangThemMoi = false;
                    MessageBox.Show("Xóa thành công ", "Thông báo", MessageBoxButtons.OK);
                    this.btnHOANTAC.Enabled = true;
                }
                catch (Exception ex)
                {
                    /*Step 4*/
                    MessageBox.Show("Lỗi xóa nhân viên. Hãy thử lại\n" + ex.Message, "Thông báo", MessageBoxButtons.OK);
                    this.phieuNhapTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.phieuNhapTableAdapter.Update(this.dataSet.PhieuNhap);

                    this.cTPNTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.cTPNTableAdapter.Update(this.dataSet.CTPN);
                    // tro ve vi tri cua nhan vien dang bi loi
                    bds.Position = viTri;
                    return;
                }
            }
            else
            {
                undoList.Pop();
            }
        }
    }
}