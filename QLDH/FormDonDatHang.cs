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
    public partial class FormDonDatHang : DevExpress.XtraEditors.XtraForm
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

        public FormDonDatHang()
        {
            InitializeComponent();
        }

        private void datHangBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bdsDatHang.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dataSet);

        }

        private void FormDonDatHang_Load(object sender, EventArgs e)
        {
            dataSet.EnforceConstraints = false;

            this.phieuNhapTableAdapter.Connection.ConnectionString = Program.connstr;
            this.phieuNhapTableAdapter.Fill(this.dataSet.PhieuNhap);

            this.cTDDHTableAdapter.Connection.ConnectionString = Program.connstr;
            this.cTDDHTableAdapter.Fill(this.dataSet.CTDDH);

            this.datHangTableAdapter.Connection.ConnectionString = Program.connstr;
            this.datHangTableAdapter.Fill(this.dataSet.DatHang);

            bds = bdsDatHang;
            gc = gcDatHang;
        }

        private void btnDonDatHang_ItemClick(object sender, ItemClickEventArgs e)
        {
            /*Step 0: Hiện chế độ làm việc*/
            btnMENU.Links[0].Caption = "Đơn Đặt Hàng";

            /*Step 1: cập nhật binding source và grid control*/
            bds = bdsDatHang;
            gc = gcDatHang;

            /*Step 2*/
            /*Bat chuc nang cua don dat hang*/
            txtMADDH.Enabled = false;
            dteNGAY.Enabled = false;
            txtNHACC.Enabled = true;
            txtMANV.Enabled = false;
            /*Tat chuc nang cua chi tiet don hang*/
            txtMADH.Enabled = false;
            btnChonDongHo.Enabled = false;
            txtSOLUONG.Enabled = false;
            txtDONGIA.Enabled = false;
            /*Bat cac grid control len*/
            gcDatHang.Enabled = true;
            gcCTDDH.Enabled = true;

            /*Step 3*/
            /*CUA HANG chi xem du lieu*/
            if (Program.role == "CUAHANG")
            {
                this.btnTHEM.Enabled = false;
                this.btnXOA.Enabled = false;
                this.btnGHI.Enabled = false;
                this.btnHOANTAC.Enabled = false;
                this.btnLAMMOI.Enabled = true;
                this.btnMENU.Enabled = false;
                this.groupBoxDonDatHang.Enabled = false;
            }

            /* NHAN VIEN co the xem - xoa - sua du lieu */
            if (Program.role == "NHANVIEN")
            {
                this.btnTHEM.Enabled = true;
                bool turnOn = (bdsDatHang.Count > 0) ? true : false;
                this.btnXOA.Enabled = turnOn;
                this.btnGHI.Enabled = true;
                this.btnHOANTAC.Enabled = false;
                this.btnLAMMOI.Enabled = true;
                this.btnMENU.Enabled = true;
                this.txtMADDH.Enabled = false;
            }
        }

        private void btnChiTietDonDatHang_ItemClick(object sender, ItemClickEventArgs e)
        {
            /*Step 0*/
            btnMENU.Links[0].Caption = "Chi Tiết Đơn Đặt Hàng";

            /*Step 1*/
            bds = bdsCTDDH;
            gc = gcCTDDH;

            /*Step 2*/
            /*Tat chuc nang don dat hang*/
            txtMADDH.Enabled = false;
            dteNGAY.Enabled = false;

            txtNHACC.Enabled = false;
            txtMANV.Enabled = false;
            /*Bat chuc nang cua chi tiet don hang*/
            txtMADH.Enabled = false;
            btnChonDongHo.Enabled = false;
            txtSOLUONG.Enabled = true;
            txtDONGIA.Enabled = true;
            /*Bat cac grid control len*/
            gcDatHang.Enabled = true;
            gcCTDDH.Enabled = true;

            /*Step 3*/
            /*CUA HANG chi xem du lieu*/
            if (Program.role == "CUAHANG")
            {
                this.btnTHEM.Enabled = false;
                this.btnXOA.Enabled = false;
                this.btnGHI.Enabled = false;
                this.btnHOANTAC.Enabled = false;
                this.btnLAMMOI.Enabled = true;
                this.btnMENU.Enabled = false;
                this.groupBoxDonDatHang.Enabled = false;
            }

            /* NHAN VIEN co the xem - xoa - sua du lieu */
            if (Program.role == "NHANVIEN")
            {
                this.btnTHEM.Enabled = true;
                bool turnOn = (bdsCTDDH.Count > 0) ? true : false;
                this.btnXOA.Enabled = turnOn;
                this.btnGHI.Enabled = true;
                this.btnHOANTAC.Enabled = false;
                this.btnLAMMOI.Enabled = true;
                this.btnMENU.Enabled = true;
                this.txtMADDH.Enabled = false;
            }
        }

        private void btnTHEM_ItemClick(object sender, ItemClickEventArgs e)
        {
            /*Step 1*/
            /*lấy vị trí hiện tại của con trỏ*/
            viTri = bds.Position;
            dangThemMoi = true;


            /*Step 2*/
            /*AddNew tự động nhảy xuống cuối thêm 1 dòng mới*/
            bds.AddNew();
            if (btnMENU.Links[0].Caption == "Đơn Đặt Hàng")
            {
                this.txtMADDH.Enabled = true;
                this.dteNGAY.EditValue = DateTime.Now;
                this.dteNGAY.Enabled = false;
                this.txtNHACC.Enabled = true;
                this.txtMANV.Text = Program.userName;

                /*Gan tu dong may truong du lieu nay*/
                ((DataRowView)(bdsDatHang.Current))["MANV"] = Program.userName;
                ((DataRowView)(bdsDatHang.Current))["NGAY"] = DateTime.Now;
            }

            if (btnMENU.Links[0].Caption == "Chi Tiết Đơn Đặt Hàng")
            {
                DataRowView drv = ((DataRowView)bdsDatHang[bdsDatHang.Position]);
                String maNhanVien = drv["MANV"].ToString();
                if (Program.userName != maNhanVien)
                {
                    MessageBox.Show("Bạn không thêm chi tiết đơn hàng trên phiếu không phải do mình tạo", "Thông báo", MessageBoxButtons.OK);
                    bdsCTDDH.RemoveCurrent();
                    return;
                }

                this.txtMADH.Enabled = false;
                this.btnChonDongHo.Enabled = true;

                this.txtSOLUONG.Enabled = true;
                this.txtSOLUONG.EditValue = 1;

                this.txtDONGIA.Enabled = true;
                this.txtDONGIA.EditValue = 1;
            }

            this.btnTHEM.Enabled = false;
            this.btnXOA.Enabled = false;
            this.btnGHI.Enabled = true;
            this.btnHOANTAC.Enabled = true;
            this.btnLAMMOI.Enabled = false;
            this.btnMENU.Enabled = false;
            gcDatHang.Enabled = false;
            gcCTDDH.Enabled = false;
        }

        private bool kiemTraDuLieuDauVao(String cheDo)
        {
            if (cheDo == "Đơn Đặt Hàng")
            {
                if (txtMADDH.Text == "")
                {
                    MessageBox.Show("Không thể bỏ trống mã đơn hàng", "Thông báo", MessageBoxButtons.OK);
                    return false;
                }
                if (txtMADDH.Text.Length > 8)
                {
                    MessageBox.Show("Mã đơn đặt hàng không quá 8 kí tự", "Thông báo", MessageBoxButtons.OK);
                    return false;
                }
                if (txtMANV.Text == "")
                {
                    MessageBox.Show("Không thể bỏ trống mã nhân viên", "Thông báo", MessageBoxButtons.OK);
                    return false;
                }
                if (txtNHACC.Text == "")
                {
                    MessageBox.Show("Không thể bỏ trống nhà cung cấp", "Thông báo", MessageBoxButtons.OK);
                    return false;
                }
                if (txtNHACC.Text.Length > 100)
                {
                    MessageBox.Show("Tên nhà cung cấp không quá 100 kí tự", "Thông báo", MessageBoxButtons.OK);
                    return false;
                }
            }

            if (cheDo == "Chi Tiết Đơn Đặt Hàng")
            {
                if (txtMADH.Text == "")
                {
                    MessageBox.Show("Không thể bỏ trống mã đồng hồ", "Thông báo", MessageBoxButtons.OK);
                    return false;
                }
                if (txtSOLUONG.Value < 0 || txtDONGIA.Value < 0)
                {
                    MessageBox.Show("Không thể nhỏ hơn 1", "Thông báo", MessageBoxButtons.OK);
                    return false;
                }
            }
            return true;
        }

        private String taoCauTruyVanHoanTac(String cheDo)
        {
            String cauTruyVan = "";
            DataRowView drv;


            /*Dang chinh sua don dat hang*/
            if (cheDo == "Đơn Đặt Hàng" && dangThemMoi == false)
            {
                drv = ((DataRowView)bdsDatHang[bdsDatHang.Position]);
                /*Ngay can duoc xu ly dac biet hon*/
                DateTime ngay = ((DateTime)drv["NGAY"]);

                cauTruyVan = "UPDATE DBO.DATHANG " +
                    "SET " +
                    "NGAY = CAST('" + ngay.ToString("yyyy-MM-dd") + "' AS DATETIME), " +
                    "NhaCC = '" + drv["NhaCC"].ToString().Trim() + "', " +
                    "MANV = '" + drv["MANV"].ToString().Trim() + "' " +
                    "WHERE MasoDDH = '" + drv["MasoDDH"].ToString().Trim() + "'";
            }
            /*Dang xoa don dat hang*/
            if (cheDo == "Đơn Đặt Hàng" && dangThemMoi == true)
            {
                drv = ((DataRowView)bdsDatHang[bdsDatHang.Position]);
                DateTime ngay = ((DateTime)drv["NGAY"]);
                cauTruyVan = "INSERT INTO DBO.DATHANG(MasoDDH, NGAY, NhaCC, MaNV) " +
                    "VALUES('" + drv["MasoDDH"] + "', '" +
                    ngay.ToString("yyyy-MM-dd") + "', '" +
                    drv["NhaCC"].ToString() + "', '" +
                    drv["MaNV"].ToString() + "' )";
            }

            /*Dang chinh sua chi tiet don dat hang*/
            if (cheDo == "Chi Tiết Đơn Đặt Hàng" && dangThemMoi == false)
            {
                drv = ((DataRowView)bdsCTDDH[bdsCTDDH.Position]);

                cauTruyVan = "UPDATE DBO.CTDDH " +
                    "SET " +
                    "SOLUONG = " + drv["SOLUONG"].ToString() + " , " +
                    "DONGIA = " + drv["DONGIA"].ToString() + " " +
                    "WHERE MasoDDH = '" + drv["MasoDDH"].ToString().Trim() + "'" +
                    " AND MADH = '" + drv["MADH"].ToString().Trim() + "'";

            }

            /*Dang xoa chi tiet don dat hang*/
            if (cheDo == "Chi Tiết Đơn Đặt Hàng" && dangThemMoi == true)
            {
                drv = ((DataRowView)bdsCTDDH[bdsCTDDH.Position]);
                cauTruyVan = "INSERT INTO DBO.CTDDH(MasoDDH, MADH, SOLUONG, DONGIA) " +
                    "VALUES('" + drv["MasoDDH"].ToString().Trim() + "', '" +
                    drv["MADH"].ToString().Trim() + "', '" +
                    drv["SOLUONG"].ToString().Trim() + "', '" +
                    drv["DONGIA"].ToString().Trim() + "' )";
            }

            return cauTruyVan;
        }

        private void btnGHI_ItemClick(object sender, ItemClickEventArgs e)
        {
            viTri = bdsDatHang.Position;
            /*Step 1: Kiem tra xem day co phai nguoi lap don hang hay không*/
            DataRowView drv = ((DataRowView)bdsDatHang[bdsDatHang.Position]);
            /*lay maNhanVien & maDonDatHang de phong truong hop them chi tiet don hang thi se co ngay*/
            String maNhanVien = drv["MANV"].ToString();
            String maDonDatHang = drv["MasoDDH"].ToString().Trim();

            if (Program.userName != maNhanVien && dangThemMoi == false)
            {
                MessageBox.Show("Bạn không thể sửa phiếu do người khác lập", "Thông báo", MessageBoxButtons.OK);
                return;
            }



            /*Step 2: lay che do dang lam viec, kiem tra du lieu dau vao. Neu OK thi tiep tuc tao cau truy van hoan tac neu dangThemMoi = false*/
            String cheDo = (btnMENU.Links[0].Caption == "Đơn Đặt Hàng") ? "Đơn Đặt Hàng" : "Chi Tiết Đơn Đặt Hàng";

            bool ketQua = kiemTraDuLieuDauVao(cheDo);
            if (ketQua == false) return;

            String cauTruyVanHoanTac = taoCauTruyVanHoanTac(cheDo);

            /*Step 3: kiem tra xem cai ma don hang nay da ton tai chua ? Neu co thi ket thuc luon Neu khong thi cho them moi*/
            String maDonDatHangMoi = txtMADDH.Text;
            String cauTruyVan =
                    "DECLARE	@result int " +
                    "EXEC @result = SP_KiemTraMaDonDatHang '" +
                    maDonDatHangMoi + "' " +
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



            /*Step 4*/
            int viTriHienTai = bds.Position;
            int viTriMaDonDatHang = bdsDatHang.Find("MasoDDH", txtMADDH.Text);
            /******************************************************************
             * truong hop them moi don dat hang moi quan tam xem no ton tai hay
             * chua ?
             ******************************************************************/
            if (result == 1 && cheDo == "Đơn Đặt Hàng" && viTriHienTai != viTriMaDonDatHang)
            {
                MessageBox.Show("Mã đơn hàng này đã được sử dụng !\n\n", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            /*****************************************************************
             * tat ca cac truong hop khac ko can quan tam !!
             *****************************************************************/

            else
            {
                DialogResult dr = MessageBox.Show("Bạn có chắc muốn ghi dữ liệu vào cơ sở dữ liệu ?", "Thông báo",
                         MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {
                    try
                    {
                        /*TH1: them moi don dat hang*/
                        if (cheDo == "Đơn Đặt Hàng" && dangThemMoi == true)
                        {
                            cauTruyVanHoanTac =
                                "DELETE FROM DBO.DATHANG " +
                                "WHERE MasoDDH = '" + maDonDatHang + "'";
                        }

                        /*TH2: them moi chi tiet don hang*/
                        if (cheDo == "Chi Tiết Đơn Đặt Hàng" && dangThemMoi == true)
                        {
                            /*Gan tu dong may truong du lieu nay*/
                            ((DataRowView)(bdsCTDDH.Current))["MasoDDH"] = ((DataRowView)(bdsDatHang.Current))["MasoDDH"];
                            ((DataRowView)(bdsCTDDH.Current))["MADH"] = Program.maDongHoDuocChon;
                            ((DataRowView)(bdsCTDDH.Current))["SOLUONG"] =
                                txtSOLUONG.Value;
                            ((DataRowView)(bdsCTDDH.Current))["DONGIA"] =
                                (int)txtDONGIA.Value;

                            cauTruyVanHoanTac =
                                "DELETE FROM DBO.CTDDH " +
                                "WHERE MasoDDH = '" + maDonDatHang + "' " +
                                "AND MADH = '" + txtMADH.Text.Trim() + "'";
                        }

                        /*TH3: chinh sua don hang */
                        /*TH4: chinh sua chi tiet don hang - > thi chi can may dong lenh duoi la xong*/
                        undoList.Push(cauTruyVanHoanTac);

                        this.bdsDatHang.EndEdit();
                        this.bdsCTDDH.EndEdit();
                        this.datHangTableAdapter.Update(this.dataSet.DatHang);
                        this.cTDDHTableAdapter.Update(this.dataSet.CTDDH);
                        this.btnTHEM.Enabled = true;
                        this.btnXOA.Enabled = true;
                        this.btnGHI.Enabled = true;
                        this.btnHOANTAC.Enabled = true;
                        this.btnLAMMOI.Enabled = true;
                        this.btnMENU.Enabled = true;
                        this.gcDatHang.Enabled = true;
                        this.gcCTDDH.Enabled = true;

                        /*cập nhật lại trạng thái thêm mới cho chắc*/
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
            /* Step 0: trường hợp đã ấn btnTHEM nhưng chưa ấn btnGHI */
            if (dangThemMoi == true && this.btnTHEM.Enabled == false)
            {
                dangThemMoi = false;

                /*dang o che do Don Dat Hang*/
                if (btnMENU.Links[0].Caption == "Đơn Đặt Hàng")
                {
                    this.txtMADDH.Enabled = false;
                    this.dteNGAY.Enabled = false;
                    this.txtNHACC.Enabled = true;
                }
                /*dang o che do Chi Tiet Don Dat Hang*/
                if (btnMENU.Links[0].Caption == "Chi Tiết Đơn Đặt Hàng")
                {
                    this.txtMADH.Enabled = false;
                    this.btnChonDongHo.Enabled = true;

                    this.txtSOLUONG.Enabled = true;
                    this.txtSOLUONG.EditValue = 1;

                    this.txtDONGIA.Enabled = true;
                    this.txtDONGIA.EditValue = 1;
                }

                this.btnTHEM.Enabled = true;
                this.btnXOA.Enabled = true;
                this.btnGHI.Enabled = true;

                this.btnLAMMOI.Enabled = true;
                this.btnMENU.Enabled = true;
                this.gcDatHang.Enabled = true;
                this.gcCTDDH.Enabled = true;

                bds.CancelEdit();
                /*xoa dong hien tai*/
                bds.RemoveCurrent();
                /* trở về lúc đầu con trỏ đang đứng*/
                bds.Position = viTri;
                return;
            }

            /*Step 1: kiểm tra undoList có trông hay không ?*/
            if (undoList.Count == 0)
            {
                MessageBox.Show("Không còn thao tác nào để khôi phục", "Thông báo", MessageBoxButtons.OK);
                btnHOANTAC.Enabled = false;
                return;
            }

            /*Step 2: Neu undoList khong trống thì lấy ra khôi phục*/
            bds.CancelEdit();
            String cauTruyVanHoanTac = undoList.Pop().ToString();

            Console.WriteLine(cauTruyVanHoanTac);
            int n = Program.ExecSqlNonQuery(cauTruyVanHoanTac);

            this.datHangTableAdapter.Fill(this.dataSet.DatHang);
            this.cTDDHTableAdapter.Fill(this.dataSet.CTDDH);

            bdsDatHang.Position = viTri;
        }

        private void btnLAMMOI_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                // do du lieu moi tu dataSet vao gridControl NHANVIEN
                this.datHangTableAdapter.Fill(this.dataSet.DatHang);
                this.cTDDHTableAdapter.Fill(this.dataSet.CTDDH);

                this.gcDatHang.Enabled = true;
                this.gcCTDDH.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi Làm mới" + ex.Message, "Thông báo", MessageBoxButtons.OK);
                return;
            }
        }

        private void btnChonDongHo_Click(object sender, EventArgs e)
        {
            FormChonDongHo form = new FormChonDongHo();
            form.ShowDialog();
            this.txtMADH.Text = Program.maDongHoDuocChon;
        }

        private void btnXOA_ItemClick(object sender, ItemClickEventArgs e)
        {
            string cauTruyVan = "";
            string cheDo = (btnMENU.Links[0].Caption == "Đơn Đặt Hàng") ? "Đơn Đặt Hàng" : "Chi Tiết Đơn Đặt Hàng";

            dangThemMoi = true;// bat cai nay len de ung voi dieu kien tao cau truy van

            if (cheDo == "Đơn Đặt Hàng")
            {
                if (bdsCTDDH.Count > 0)
                {
                    MessageBox.Show("Không thể xóa đơn đặt hàng này vì có chi tiết đơn đặt hàng", "Thông báo", MessageBoxButtons.OK);
                    return;
                }

                if (bdsPhieuNhap.Count > 0)
                {
                    MessageBox.Show("Không thể xóa đơn đặt hàng này vì có phiếu nhập", "Thông báo", MessageBoxButtons.OK);
                    return;
                }


            }
            if (cheDo == "Chi Tiết Đơn Đặt Hàng")
            {
                DataRowView drv = ((DataRowView)bdsDatHang[bdsDatHang.Position]);
                String maNhanVien = drv["MANV"].ToString();
                if (Program.userName != maNhanVien)
                {
                    MessageBox.Show("Bạn không xóa chi tiết đơn hàng trên phiếu không phải do mình tạo", "Thông báo", MessageBoxButtons.OK);
                    return;
                }
            }

            cauTruyVan = taoCauTruyVanHoanTac(cheDo);
            undoList.Push(cauTruyVan);

            /*Step 2*/
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không ?", "Thông báo",
                MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    /*Step 3*/
                    viTri = bds.Position;
                    if (cheDo == "Đơn Đặt Hàng")
                    {
                        bdsDatHang.RemoveCurrent();
                    }
                    if (cheDo == "Chi Tiết Đơn Đặt Hàng")
                    {
                        bdsCTDDH.RemoveCurrent();
                    }


                    this.datHangTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.datHangTableAdapter.Update(this.dataSet.DatHang);

                    this.cTDDHTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.cTDDHTableAdapter.Update(this.dataSet.CTDDH);

                    /*Cap nhat lai do ben tren can tao cau truy van nen da dat dangThemMoi = true*/
                    dangThemMoi = false;
                    MessageBox.Show("Xóa thành công ", "Thông báo", MessageBoxButtons.OK);
                    this.btnHOANTAC.Enabled = true;
                }
                catch (Exception ex)
                {
                    /*Step 4*/
                    MessageBox.Show("Lỗi xóa đơn đặt hàng. Hãy thử lại\n" + ex.Message, "Thông báo", MessageBoxButtons.OK);
                    this.datHangTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.datHangTableAdapter.Update(this.dataSet.DatHang);

                    this.cTDDHTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.cTDDHTableAdapter.Update(this.dataSet.CTDDH);
                    // tro ve vi tri cua nhan vien dang bi loi
                    bds.Position = viTri;
                    return;
                }
            }
            else
            {
                // xoa cau truy van hoan tac di
                undoList.Pop();
            }
        }
    }
}