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
    public partial class FormPhieuXuat : DevExpress.XtraEditors.XtraForm
    {

        int viTri = 0;
        bool dangThemMoi = false;
        Stack undoList = new Stack();

        BindingSource bds = null;
        GridControl gc = null;
        string type = "";

        public FormPhieuXuat()
        {
            InitializeComponent();
        }

        private void phieuXuatBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bdsPhieuXuat.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dataSet);

        }

        private void FormPhieuXuat_Load(object sender, EventArgs e)
        {
            dataSet.EnforceConstraints = false;

            this.cTPXTableAdapter.Connection.ConnectionString = Program.connstr;
            this.cTPXTableAdapter.Fill(this.dataSet.CTPX);

            this.phieuXuatTableAdapter.Connection.ConnectionString = Program.connstr;
            this.phieuXuatTableAdapter.Fill(this.dataSet.PhieuXuat);
        }

        private void btnPhieuXuat_ItemClick(object sender, ItemClickEventArgs e)
        {
            btnMENU.Links[0].Caption = "Phiếu Xuất";

            bds = bdsPhieuXuat;
            gc = gcCTPX;

            txtMAPX.Enabled = false;
            dteNGAY.Enabled = false;
            txtTENKH.Enabled = true;
            txtMANV.Enabled = false;

            txtMADHCTPX.Enabled = false;
            btnChonDongHo.Enabled = false;
            txtSOLUONGCTPX.Enabled = false;
            txtDONGIACTPX.Enabled = false;

            /*Bat cac grid control len*/
            gcPhieuXuat.Enabled = true;
            gcCTPX.Enabled = true;

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

            /* NHAN VIEN co the xem - xoa - sua du lieu */
            if (Program.role == "NHANVIEN")
            {
                this.btnTHEM.Enabled = true;
                bool turnOn = (bdsPhieuXuat.Count > 0) ? true : false;
                this.btnXOA.Enabled = turnOn;
                this.btnGHI.Enabled = true;
                this.btnHOANTAC.Enabled = false;
                this.btnLAMMOI.Enabled = true;
                this.btnMENU.Enabled = true;
            }
        }

        private void btnChiTietPhieuXuat_ItemClick(object sender, ItemClickEventArgs e)
        {
            btnMENU.Links[0].Caption = "Chi Tiết Phiếu Xuất";

            bds = bdsCTPX;

            txtMAPX.Enabled = false;
            dteNGAY.Enabled = false;
            txtTENKH.Enabled = false;
            txtMANV.Enabled = false;

            txtMADHCTPX.Enabled = false;
            txtSOLUONGCTPX.Enabled = false;
            txtDONGIACTPX.Enabled = false;

            /*Bat cac grid control len*/
            gcPhieuXuat.Enabled = true;
            gcCTPX.Enabled = true;

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

            /* NHANVIEN co the xem - xoa - sua du lieu */
            if (Program.role == "NHANVIEN")
            {
                this.btnTHEM.Enabled = true;
                bool turnOn = (bdsPhieuXuat.Count > 0) ? true : false;
                this.btnXOA.Enabled = true;
                this.btnGHI.Enabled = true;
                this.btnHOANTAC.Enabled = false;
                this.btnLAMMOI.Enabled = true;
                this.btnMENU.Enabled = true;
            }
        }

        private void btnTHEM_ItemClick(object sender, ItemClickEventArgs e)
        {
            viTri = bds.Position;
            dangThemMoi = true;

            bds.AddNew();
            if (btnMENU.Links[0].Caption == "Phiếu Xuất")
            {
                this.txtMAPX.Enabled = true;
                this.dteNGAY.EditValue = DateTime.Now;
                this.dteNGAY.Enabled = false;
                this.txtTENKH.Enabled = true;
                this.txtMANV.Text = Program.userName;
                this.txtMADHCTPX.Enabled = false;
                this.btnChonDongHo.Enabled = false;
                this.txtSOLUONGCTPX.Enabled = false;
                this.txtDONGIACTPX.Enabled = false;

                /*Gan tu dong may truong du lieu nay*/
                ((DataRowView)(bdsPhieuXuat.Current))["NGAY"] = DateTime.Now;
                ((DataRowView)(bdsPhieuXuat.Current))["MANV"] = Program.userName;
            }

            if (btnMENU.Links[0].Caption == "Chi Tiết Phiếu Xuất")
            {

                DataRowView drv = ((DataRowView)bdsPhieuXuat[bdsPhieuXuat.Position]);
                String maNhanVien = drv["MANV"].ToString();
                if (Program.userName != maNhanVien)
                {
                    MessageBox.Show("Không thể thêm chi tiết phiếu xuất trên phiếu  không phải do mình tạo", "Thông báo", MessageBoxButtons.OK);
                    return;
                }

               /*Gan tu dong may truong du lieu nay*/
               ((DataRowView)(bdsCTPX.Current))["MAPX"] = ((DataRowView)(bdsPhieuXuat.Current))["MAPX"];
                ((DataRowView)(bdsCTPX.Current))["MADH"] = Program.maDongHoDuocChon;

                this.txtMADHCTPX.Enabled = false;
                this.btnChonDongHo.Enabled = true;

                this.txtSOLUONGCTPX.Enabled = true;
                this.txtSOLUONGCTPX.EditValue = 1;

                this.txtDONGIACTPX.Enabled = true;
                this.txtDONGIACTPX.EditValue = 1;
            }

            this.btnTHEM.Enabled = false;
            this.btnXOA.Enabled = false;
            this.btnGHI.Enabled = true;
            this.btnHOANTAC.Enabled = true;
            this.btnLAMMOI.Enabled = false;
            this.btnMENU.Enabled = false;
            gcPhieuXuat.Enabled = false;
            gcCTPX.Enabled = false;
        }

        private void btnChonDongHo_Click(object sender, EventArgs e)
        {
            FormChonDongHo form = new FormChonDongHo();
            form.ShowDialog();
            this.txtMADHCTPX.Text = Program.maDongHoDuocChon;
        }

        private bool kiemTraDuLieuDauVao(string cheDo)
        {
            if (cheDo == "Phiếu Xuất")
            {
                DataRowView drv = ((DataRowView)bdsPhieuXuat[bdsPhieuXuat.Position]);
                String maNhanVien = drv["MANV"].ToString();
                if (Program.userName != maNhanVien)
                {
                    MessageBox.Show("Không thể sửa phiếu xuất do người khác tạo", "Thông báo", MessageBoxButtons.OK);
                    return false;
                }

                if (txtMAPX.Text == "")
                {
                    MessageBox.Show("Không bỏ trống mã phiếu xuất !", "Thông báo", MessageBoxButtons.OK);
                    txtMAPX.Focus();
                    return false;
                }

                if (txtMAPX.Text.Length > 8)
                {
                    MessageBox.Show("Mã phiếu xuất không thể quá 8 kí tự !", "Thông báo", MessageBoxButtons.OK);
                    txtMAPX.Focus();
                    return false;
                }

                if (txtTENKH.Text == "")
                {
                    MessageBox.Show("Không bỏ trống tên khách hàng !", "Thông báo", MessageBoxButtons.OK);
                    txtTENKH.Focus();
                    return false;
                }

                if (txtTENKH.Text.Length > 100)
                {
                    MessageBox.Show("Tên khách hàng không quá 100 kí tự !", "Thông báo", MessageBoxButtons.OK);
                    txtTENKH.Focus();
                    return false;
                }
            }

            if (cheDo == "Chi Tiết Phiếu Xuất")
            {
                DataRowView drv = ((DataRowView)bdsPhieuXuat[bdsPhieuXuat.Position]);
                String maNhanVien = drv["MANV"].ToString();
                if (Program.userName != maNhanVien)
                {
                    MessageBox.Show("Không thể thêm chi tiết phiếu xuất với phiếu xuất do người khác tạo !", "Thông báo", MessageBoxButtons.OK);
                    bdsCTPX.RemoveCurrent();
                    return false;
                }

                if (txtMAPX.Text == "")
                {
                    MessageBox.Show("Không bỏ trống mã phiếu xuất !", "Thông báo", MessageBoxButtons.OK);
                    txtMAPX.Focus();
                    return false;
                }

                if (txtMAPX.Text.Length > 8)
                {
                    MessageBox.Show("Mã phiếu xuất không thể quá 8 kí tự !", "Thông báo", MessageBoxButtons.OK);
                    txtMAPX.Focus();
                    return false;
                }

                if (txtMADHCTPX.Text == "")
                {
                    MessageBox.Show("Thiếu mã đồng hồ !", "Thông báo", MessageBoxButtons.OK);
                    txtMADHCTPX.Focus();
                    return false;
                }

                if (txtMADHCTPX.Text.Length > 4)
                {
                    MessageBox.Show("Mã đồng hồ không quá 4 kí tự !", "Thông báo", MessageBoxButtons.OK);
                    txtMADHCTPX.Focus();
                    return false;
                }

                if (txtSOLUONGCTPX.Value < 0 || txtSOLUONGCTPX.Value > Program.soLuongDongHo)
                {
                    MessageBox.Show("Số lượng đồng hồ không thể bé hơn 0 & lớn hơn số lượng đồng hồ đang có !", "Thông báo", MessageBoxButtons.OK);
                    txtSOLUONGCTPX.Focus();
                    return false;
                }

                if (txtDONGIACTPX.Value < 0)
                {
                    MessageBox.Show("Đơn giá không thể bé hơn 0 VND !", "Thông báo", MessageBoxButtons.OK);
                    txtDONGIACTPX.Focus();
                    return false;
                }
            }
            return true;
        }

        private string taoCauTruyVanHoanTac(string cheDo)
        {
            String cauTruyVan = "";
            DataRowView drv;

            /*TH1: dang sua phieu xuat*/
            if (cheDo == "Phiếu Xuất" && dangThemMoi == false)
            {
                drv = ((DataRowView)(bdsPhieuXuat.Current));
                DateTime ngay = (DateTime)drv["NGAY"];


                cauTruyVan = "UPDATE DBO.PHIEUXUAT " +
                    "SET " +
                    "NGAY = CAST('" + ngay.ToString("yyyy-MM-dd") + "' AS DATETIME), " +
                    "HOTENKH = '" + drv["HOTENKH"].ToString().Trim() + "', " +
                    "MANV = '" + drv["MANV"].ToString().Trim() + "' " +
                    "WHERE MAPX = '" + drv["MAPX"].ToString().Trim() + "' ";
            }

            /*TH2: them moi phieu xuat*/
            if (cheDo == "Phiếu Xuất" && dangThemMoi == true)
            {
                // tao trong btnGHI thi hon
            }

            /*TH3: them moi chi tiet phieu xuat*/
            if (cheDo == "Chi Tiết Phiếu Xuất" && dangThemMoi == true)
            {
                // tao trong btnGHI thi hon
            }

            /*TH4: dang sua chi tiet phieu xuat*/
            if (cheDo == "Chi Tiết Phiếu Xuất" && dangThemMoi == false)
            {
                drv = ((DataRowView)(bdsCTPX.Current));
                int soLuong = int.Parse(drv["SOLUONG"].ToString().Trim());
                float donGia = float.Parse(drv["DONGIA"].ToString().Trim());
                String maPhieuXuat = drv["MAPX"].ToString().Trim();
                String maDongHo = drv["MADH"].ToString().Trim();

                cauTruyVan = "UPDATE DBO.CTPX " +
                    "SET " +
                    "SOLUONG = '" + soLuong + "', " +
                    "DONGIA = '" + donGia + "' " +
                    "WHERE MAPX = '" + maPhieuXuat + "' " +
                    "AND MADH = '" + maDongHo + "' ";
            }
            return cauTruyVan;
        }

        private void btnGHI_ItemClick(object sender, ItemClickEventArgs e)
        {
            String cheDo = (btnMENU.Links[0].Caption == "Phiếu Xuất") ? "Phiếu Xuất" : "Chi Tiết Phiếu Xuất";

            bool ketQua = kiemTraDuLieuDauVao(cheDo);
            if (ketQua == false) return;

            string cauTruyVanHoanTac = taoCauTruyVanHoanTac(cheDo);

            String maPhieuXuat = txtMAPX.Text.Trim();
            String cauTruyVan =
                    "DECLARE	@result int " +
                    "EXEC @result = SP_KiemTraMaPhieuXuat '" +
                    maPhieuXuat + "' " +
                    "SELECT 'Value' = @result";
            SqlCommand sqlCommand = new SqlCommand(cauTruyVan, Program.conn);
            try
            {
                Program.myReader = Program.ExecSqlDataReader(cauTruyVan);
                /*khong co ket qua tra ve thi ket thuc luon*/
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

            int viTriConTro = bdsPhieuXuat.Position;
            int viTriMaPhieuXuat = bdsPhieuXuat.Find("MAPX", maPhieuXuat);

            /*Dang them moi phieu nhap*/
            if (result == 1 && cheDo == "Phiếu Xuất" && viTriMaPhieuXuat != viTriConTro)
            {
                MessageBox.Show("Mã phiếu xuất đã được sử dụng !", "Thông báo", MessageBoxButtons.OK);
                txtMAPX.Focus();
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
                        /*TH1: them moi phieu nhap*/
                        if (cheDo == "Phiếu Xuất" && dangThemMoi == true)
                        {
                            cauTruyVanHoanTac =
                                "DELETE FROM DBO.PHIEUXUAT " +
                                "WHERE MAPX = '" + maPhieuXuat + "'";
                        }

                        /*TH2: them moi chi tiet don hang*/
                        if (cheDo == "Chi Tiết Phiếu Xuất" && dangThemMoi == true)
                        {
                            cauTruyVanHoanTac =
                                "DELETE FROM DBO.CTPX " +
                                "WHERE MAPX = '" + maPhieuXuat + "' " +
                                "AND MADH = '" + Program.maDongHoDuocChon + "'";
                        }

                        /*TH4: chinh sua chi tiet phieu nhap */
                        undoList.Push(cauTruyVanHoanTac);
                        Console.WriteLine("cau truy van hoan tac");
                        Console.WriteLine(cauTruyVanHoanTac);

                        this.bdsPhieuXuat.EndEdit();
                        this.bdsCTPX.EndEdit();
                        this.phieuXuatTableAdapter.Update(this.dataSet.PhieuXuat);
                        this.cTPXTableAdapter.Update(this.dataSet.CTPX);
                        this.txtMAPX.Enabled = false;
                        this.btnTHEM.Enabled = true;
                        this.btnXOA.Enabled = true;
                        this.btnGHI.Enabled = true;
                        this.btnHOANTAC.Enabled = true;
                        this.btnLAMMOI.Enabled = true;
                        this.btnMENU.Enabled = true;
                        this.gcPhieuXuat.Enabled = true;
                        this.gcCTPX.Enabled = true;

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

        private void btnHOANTAC_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (dangThemMoi == true && this.btnTHEM.Enabled == false)
            {
                dangThemMoi = false;

                /*dang o che do Phiếu Xuất*/
                if (btnMENU.Links[0].Caption == "Phiếu Xuất")
                {
                    this.txtMAPX.Enabled = false;
                    this.dteNGAY.Enabled = false;
                    this.txtTENKH.Enabled = true;
                    this.txtMANV.Enabled = false;
                }
                /*dang o che do Chi Tiết Phiếu Nhập*/
                if (btnMENU.Links[0].Caption == "Chi Tiết Phiếu Nhập")
                {
                    this.txtMAPX.Enabled = false;
                    this.txtMADHCTPX.Enabled = false;
                    this.btnChonDongHo.Enabled = false;
                    this.txtSOLUONGCTPX.Enabled = true;
                    this.txtDONGIACTPX.Enabled = true;
                }

                this.btnTHEM.Enabled = true;
                this.btnXOA.Enabled = true;
                this.btnGHI.Enabled = true;
                this.btnLAMMOI.Enabled = true;
                this.btnMENU.Enabled = true;
                this.gcPhieuXuat.Enabled = true;
                this.gcCTPX.Enabled = true;

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

            this.phieuXuatTableAdapter.Fill(this.dataSet.PhieuXuat);
            this.cTPXTableAdapter.Fill(this.dataSet.CTPX);

            bdsPhieuXuat.Position = viTri;
        }

        private void btnLAMMOI_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                this.phieuXuatTableAdapter.Fill(this.dataSet.PhieuXuat);
                this.cTPXTableAdapter.Fill(this.dataSet.CTPX);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi lam moi \n\n" + ex.Message, "Thông báo", MessageBoxButtons.OK);
            }
        }

        private void btnXOA_ItemClick(object sender, ItemClickEventArgs e)
        {
            DataRowView drv;
            string cauTruyVanHoanTac = "";
            string cheDo = (btnMENU.Links[0].Caption == "Phiếu Xuất") ? "Phiếu Xuất" : "Chi Tiết Phiếu Xuất";

            if (cheDo == "Phiếu Xuất")
            {
                drv = ((DataRowView)bdsPhieuXuat[bdsPhieuXuat.Position]);
                String maNhanVien = drv["MANV"].ToString();
                if (Program.userName != maNhanVien)
                {
                    MessageBox.Show("Không xóa chi tiết phiếu xuất không phải do mình tạo", "Thông báo", MessageBoxButtons.OK);
                    return;
                }

                if (bdsCTPX.Count > 0)
                {
                    MessageBox.Show("Không thể xóa vì có chi tiết phiếu xuất", "Thông báo", MessageBoxButtons.OK);
                    return;
                }

                drv = ((DataRowView)bdsPhieuXuat[bdsPhieuXuat.Position]);
                DateTime ngay = ((DateTime)drv["NGAY"]);

                cauTruyVanHoanTac = "INSERT INTO DBO.PHIEUXUAT(MAPX, NGAY, HOTENKH, MANV) " +
                    "VALUES( '" + drv["MAPX"].ToString().Trim() + "', '" +
                    ngay.ToString("yyyy-MM-dd") + "', '" +
                    drv["HOTENKH"].ToString() + "', '" +
                    drv["MANV"].ToString() + "')";
            }

            if (cheDo == "Chi Tiết Phiếu Xuất")
            {
                drv = ((DataRowView)bdsPhieuXuat[bdsPhieuXuat.Position]);
                String maNhanVien = drv["MANV"].ToString();
                if (Program.userName != maNhanVien)
                {
                    MessageBox.Show("Bạn không xóa chi tiết phiếu xuất không phải do mình tạo", "Thông báo", MessageBoxButtons.OK);
                    return;
                }


                drv = ((DataRowView)bdsCTPX[bdsCTPX.Position]);
                cauTruyVanHoanTac = "INSERT INTO DBO.CTPX(MAPX, MADH, SOLUONG, DONGIA) " +
                    "VALUES('" + drv["MAPX"].ToString().Trim() + "', '" +
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
                    if (cheDo == "Phiếu Xuất")
                    {
                        bdsPhieuXuat.RemoveCurrent();
                    }
                    if (cheDo == "Chi Tiết Phiếu Xuất")
                    {
                        bdsCTPX.RemoveCurrent();
                    }


                    this.phieuXuatTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.phieuXuatTableAdapter.Update(this.dataSet.PhieuXuat);

                    this.cTPXTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.cTPXTableAdapter.Update(this.dataSet.CTPX);

                    dangThemMoi = false;
                    MessageBox.Show("Xóa thành công ", "Thông báo", MessageBoxButtons.OK);
                    this.btnHOANTAC.Enabled = true;
                }
                catch (Exception ex)
                {
                    /*Step 4*/
                    MessageBox.Show("Lỗi xóa phiếu xuất. Hãy thử lại\n" + ex.Message, "Thông báo", MessageBoxButtons.OK);
                    this.phieuXuatTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.phieuXuatTableAdapter.Update(this.dataSet.PhieuXuat);

                    this.cTPXTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.cTPXTableAdapter.Update(this.dataSet.CTPX);
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