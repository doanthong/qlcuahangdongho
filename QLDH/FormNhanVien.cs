using DevExpress.XtraBars;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLDH
{
    public partial class FormNhanVien : DevExpress.XtraEditors.XtraForm
    {

        /* vị trí của con trỏ trên grid view*/
        int viTri = 0;
        /********************************************
         * đang thêm mới -> true -> đang dùng btnTHEM
         *              -> false -> có thể là btnGHI( chỉnh sửa) hoặc btnXOA
         *              
         * Mục đích: dùng biến này để phân biệt giữa btnTHEM - thêm mới hoàn toàn
         * và việc chỉnh sửa nhân viên( do ko dùng thêm btnXOA )
         * Trạng thái true or false sẽ được sử dụng 
         * trong btnGHI - việc này để phục vụ cho btnHOANTAC
         ********************************************/
        bool dangThemMoi = false;
        /**********************************************************
         * undoList - phục vụ cho btnHOANTAC -  chứa các thông tin của đối tượng bị tác động 
         * 
         * nó là nơi lưu trữ các đối tượng cần thiết để hoàn tác các thao tác
         * 
         * nếu btnGHI sẽ ứng với INSERT
         * nếu btnXOA sẽ ứng với DELETE
         **********************************************************/
        Stack undoList = new Stack();



        private static int CalculateAge(DateTime dateOfBirth)
        {
            int age = 0;
            age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
                age = age - 1;

            return age;
        }



        /************************************************************
         * CheckExists:
         * Để tránh việc người dùng ấn vào 1 form đến 2 lần chúng ta 
         * cần sử dụng hàm này để kiểm tra xem cái form hiện tại đã 
         * có trong bộ nhớ chưa
         * Nếu có trả về "f"
         * Nếu không trả về "null"
         ************************************************************/
        private Form CheckExists(Type ftype)
        {
            foreach (Form f in this.MdiChildren)
                if (f.GetType() == ftype)
                    return f;
            return null;
        }

        public FormNhanVien()
        {
            InitializeComponent();
        }

        private void nhanVienBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bdsNhanVien.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dataSet);

        }

        private void FormNhanVien_Load(object sender, EventArgs e)
        {
            /*không kiểm tra khóa ngoại nữa*/
            dataSet.EnforceConstraints = false;

            this.datHangTableAdapter.Connection.ConnectionString = Program.connstr;
            this.datHangTableAdapter.Fill(this.dataSet.DatHang);

            this.phieuNhapTableAdapter.Connection.ConnectionString = Program.connstr;
            this.phieuNhapTableAdapter.Fill(this.dataSet.PhieuNhap);

            this.phieuXuatTableAdapter.Connection.ConnectionString = Program.connstr;
            this.phieuXuatTableAdapter.Fill(this.dataSet.PhieuXuat);

            this.nhanVienTableAdapter.Connection.ConnectionString = Program.connstr;
            this.nhanVienTableAdapter.Fill(this.dataSet.NhanVien);

            /*NHAN VIEN chi xem du lieu*/
            if (Program.role == "NHANVIEN")
            {
                this.btnTHEM.Enabled = false;
                this.btnXOA.Enabled = false;
                this.btnGHI.Enabled = false;
                this.btnHOANTAC.Enabled = false;
                this.btnLAMMOI.Enabled = true;
                this.panelNhapLieu.Enabled = false;
            }

            /* CUA HANG co the xem - xoa - sua du lieu */
            if (Program.role == "CUAHANG")
            {
                this.btnTHEM.Enabled = true;
                this.btnXOA.Enabled = true;
                this.btnGHI.Enabled = true;
                this.btnHOANTAC.Enabled = false;
                this.btnLAMMOI.Enabled = true;
                this.panelNhapLieu.Enabled = true;
                this.txtMANV.Enabled = false;
            }
        }

        /*********************************************************************
         * bdsNhanVien.Position - vitri phuc vu cho btnHOANTAC. Gia su, co 5 nhan vien, con tro chuot
         * dang dung o vi tri nhan vien thu 2 thi chung ta an nut THEM
         * nhung neu chon btnHOANTAC, con tro chuot phai quay lai vi 
         * tri nhan vien thu 2, thay vi o vi tri duoi cung - tuc nhan vien so 5
         *********************************************************************/

        private void btnTHEM_ItemClick(object sender, ItemClickEventArgs e)
        {
            /*Step 1: Kich hoat panel Nhap lieu & lay vi tri cua nhan vien hien tai*/
            /*lấy vị trí hiện tại của con trỏ*/
            viTri = bdsNhanVien.Position;
            this.panelNhapLieu.Enabled = true;
            dangThemMoi = true;


            /*Step 2: AddNew tự động nhảy xuống cuối thêm 1 dòng mới*/
            bdsNhanVien.AddNew();
            dteNGAYSINH.EditValue = "9/19/2001";
            txtLUONG.Value = 4000000;// dat san muc luong toi thieu


            /*Step 3: vo hieu hoa cac nut chuc nang & gridControl - chi btnGHI & btnHOANTAC moi duoc hoat dong*/
            this.txtMANV.Enabled = true;
            this.btnTHEM.Enabled = false;
            this.btnXOA.Enabled = false;
            this.btnGHI.Enabled = true;
            this.btnHOANTAC.Enabled = true;
            this.btnLAMMOI.Enabled = false;
            this.trangThaiXoaCheckBox.Checked = false;
            this.gcNhanVien.Enabled = false;
            this.panelNhapLieu.Enabled = true;
        }

        /**********************************************************************
         * moi lan nhan btnHOANTAC thi nen nhan them btnLAMMOI de 
         * tranh bi loi khi an btnTHEM lan nua
         * statement: chua cau y nghia chuc nang ngay truoc khi an btnHOANTAC.
         * Vi du: statement = INSERT | DELETE
         * bdsNhanVien.CancelEdit() - phuc hoi lai du lieu neu chua an btnGHI
         *********************************************************************/

        private void btnHOANTAC_ItemClick(object sender, ItemClickEventArgs e)
        {
            /* Step 0 : trường hợp đã ấn btnTHEM nhưng chưa ấn btnGHI*/
            if (dangThemMoi == true && this.btnTHEM.Enabled == false)
            {
                dangThemMoi = false;
                this.txtMANV.Enabled = false;
                this.btnTHEM.Enabled = true;
                this.btnXOA.Enabled = true;
                this.btnGHI.Enabled = true;
                this.btnHOANTAC.Enabled = false;
                this.btnLAMMOI.Enabled = true;
                this.trangThaiXoaCheckBox.Checked = false;
                this.gcNhanVien.Enabled = true;
                this.panelNhapLieu.Enabled = true;

                bdsNhanVien.CancelEdit();
                /*xoa dong hien tai*/
                bdsNhanVien.RemoveCurrent();
                /* trở về lúc đầu con trỏ đang đứng*/
                bdsNhanVien.Position = viTri;
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
            bdsNhanVien.CancelEdit();
            String cauTruyVanHoanTac = undoList.Pop().ToString();

            if (Program.KetNoi() == 0)
            {
                return;
            }
            int n = Program.ExecSqlNonQuery(cauTruyVanHoanTac);

            this.nhanVienTableAdapter.Fill(this.dataSet.NhanVien);
        }

        private void btnLAMMOI_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                // do du lieu moi tu dataSet vao gridControl NHANVIEN
                this.nhanVienTableAdapter.Fill(this.dataSet.NhanVien);
                this.gcNhanVien.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi Làm mới" + ex.Message, "Thông báo", MessageBoxButtons.OK);
                return;
            }
        }

        /***************************************************************************
         * Step 1: tu biding source kiem tra xem nhan vien nay da lap don hang - 
         * phieu nhap - phieu xuat chua ?
         *          Neu co thi thong bao la khong the xoa va ket thuc
         *          Neu khong thi bat dau xoa
         * Step 2: Neu chon OK thi tien hanh xoa
         * Step 3: Lay ma nhan vien bi xoa roi luu lai trong manv
         * Step 4: Truong hop xoa nhan vien bi loi thi quay lai dung vi tri manv bi loi
         ***************************************************************************/

        private void btnXOA_ItemClick(object sender, ItemClickEventArgs e)
        {
            String tenNV = ((DataRowView)bdsNhanVien[bdsNhanVien.Position])["MANV"].ToString();
            /*Step 1*/

            // khong cho xoa nguoi dang dang nhap ke ca nguoi do khong co don hang - phieu nhap - phieu xuat
            if (tenNV == Program.userName)
            {
                MessageBox.Show("Không thể xóa chính tài khoản đang đăng nhập", "Thông báo", MessageBoxButtons.OK);
                return;
            }

            if (bdsNhanVien.Count == 0)
            {
                btnXOA.Enabled = false;
            }

            if (bdsDatHang.Count > 0)
            {
                MessageBox.Show("Không thể xóa nhân viên này vì đã lập đơn đặt hàng", "Thông báo", MessageBoxButtons.OK);
                return;
            }

            if (bdsPhieuNhap.Count > 0)
            {
                MessageBox.Show("Không thể xóa nhân viên này vì đã lập phiếu nhập", "Thông báo", MessageBoxButtons.OK);
                return;
            }

            if (bdsPhieuXuat.Count > 0)
            {
                MessageBox.Show("Không thể xóa nhân viên này vì đã lập phiếu xuất", "Thông báo", MessageBoxButtons.OK);
                return;
            }

            /* Phần này phục vụ tính năng hoàn tác
                    * Đưa câu truy vấn hoàn tác vào undoList 
                    * để nếu chẳng may người dùng ấn hoàn tác thì làm luôn*/
            int trangThai = (trangThaiXoaCheckBox.Checked == true) ? 1 : 0;
            /*Lấy ngày sinh trong grid view*/
            DateTime NGAYSINH = (DateTime)((DataRowView)bdsNhanVien[bdsNhanVien.Position])["NGAYSINH"];


            string cauTruyVanHoanTac =
                string.Format("INSERT INTO DBO.NHANVIEN( MANV,HO,TEN,DIACHI,NGAYSINH,LUONG)" +
            "VALUES({0},'{1}','{2}','{3}',CAST({4} AS DATETIME), {5})", txtMANV.Text, txtHO.Text, txtTEN.Text, txtDIACHI.Text, NGAYSINH.ToString("yyyy-MM-dd"), txtLUONG.Value);

            Console.WriteLine(cauTruyVanHoanTac);
            undoList.Push(cauTruyVanHoanTac);


            /*Step 2*/
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này không ?", "Thông báo",
                MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    /*Step 3*/
                    viTri = bdsNhanVien.Position;
                    bdsNhanVien.RemoveCurrent();

                    this.nhanVienTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.nhanVienTableAdapter.Update(this.dataSet.NhanVien);

                    MessageBox.Show("Xóa thành công ", "Thông báo", MessageBoxButtons.OK);
                    this.btnHOANTAC.Enabled = true;
                }
                catch (Exception ex)
                {
                    /*Step 4*/
                    MessageBox.Show("Lỗi xóa nhân viên. Hãy thử lại\n" + ex.Message, "Thông báo", MessageBoxButtons.OK);
                    this.nhanVienTableAdapter.Fill(this.dataSet.NhanVien);
                    // tro ve vi tri cua nhan vien dang bi loi
                    bdsNhanVien.Position = viTri;
                    return;
                }
            }
            else
            {
                undoList.Pop();
            }
        }

        private bool kiemTraDuLieuDauVao()
        {
            /*kiem tra txtMANV*/
            if (txtMANV.Text == "")
            {
                MessageBox.Show("Không bỏ trống mã nhân viên", "Thông báo", MessageBoxButtons.OK);
                txtMANV.Focus();
                return false;
            }

            if (Regex.IsMatch(txtMANV.Text, @"^[a-zA-Z0-9]+$") == false)
            {
                MessageBox.Show("Mã nhân viên chỉ chấp nhận số", "Thông báo", MessageBoxButtons.OK);
                txtMANV.Focus();
                return false;
            }
            /*kiem tra txtHO*/
            if (txtHO.Text == "")
            {
                MessageBox.Show("Không bỏ trống họ và tên", "Thông báo", MessageBoxButtons.OK);
                txtHO.Focus();
                return false;
            }
            if (txtHO.Text.Length > 40)
            {
                MessageBox.Show("Họ không thể lớn hơn 40 kí tự", "Thông báo", MessageBoxButtons.OK);
                txtHO.Focus();
                return false;
            }
            /*kiem tra txtTEN*/
            if (txtTEN.Text == "")
            {
                MessageBox.Show("Không bỏ trống họ và tên", "Thông báo", MessageBoxButtons.OK);
                txtTEN.Focus();
                return false;
            }

            if (txtTEN.Text.Length > 10)
            {
                MessageBox.Show("Tên không thể lớn hơn 10 kí tự", "Thông báo", MessageBoxButtons.OK);
                txtTEN.Focus();
                return false;
            }
            /*kiem tra txtDIACHI*/
            if (txtDIACHI.Text == "")
            {
                MessageBox.Show("Không bỏ trống địa chỉ", "Thông báo", MessageBoxButtons.OK);
                txtDIACHI.Focus();
                return false;
            }

            if (txtDIACHI.Text.Length > 100)
            {
                MessageBox.Show("Địa chỉ không thể lớn hơn 100 kí tự", "Thông báo", MessageBoxButtons.OK);
                txtDIACHI.Focus();
                return false;
            }
            /*kiem tra dteNGAYSINH va txtLUONG*/
            if (CalculateAge(dteNGAYSINH.DateTime) < 18)
            {
                MessageBox.Show("Nhân viên chưa đủ 18 tuổi", "Thông báo", MessageBoxButtons.OK);
                dteNGAYSINH.Focus();
                return false;
            }

            if (txtLUONG.Value < 4000000 || txtLUONG.Value == 0)
            {
                MessageBox.Show("Mức lương không thể bỏ trống & tối thiểu 4.000.000 đồng", "Thông báo", MessageBoxButtons.OK);
                txtLUONG.Focus();
                return false;
            }
            return true;
        }

        /**
         * viTriConTro: vi tri con tro chuot dang dung
         * viTriMaNhanVien: vi tri cua ma nhan vien voi btnTHEM or hanh dong sua du lieu
         * SP_TRACUU_KIEMTRAMANHANVIEN tra ve 0 neu khong ton tai
         *                                    1 neu ton tai
         *                                    
         * Step 0 : Kiem tra du lieu dau vao
         * Step 1 : Dung stored procedure sp_TRACUU_KIEMTRAMANHANVIEN de kiem tra txtMANV
         * Step 2 : Ket hop ket qua tu Step 1 & vi tri cua txtMANV co 4 truong hop xay ra
         * + TH0: ketQua = 1 && viTriConTro != viTriMaNhanVien -> them moi nhung MANV da ton tai
         * + TH1: ketQua = 1 && viTriConTro == viTriMaNhanVien -> sua nhan vien cu
         * + TH2: ketQua = 0 && viTriConTro == viTriMaNhanVien -> co the them moi nhan vien
         * + TH3: ketQua = 0 && viTriConTro != viTriMaNhanVien -> co the them moi nhan vien
         *          
         * Step 3 : Neu khong phai TH0 thi cac TH1 - TH2 - TH3 deu hop le 
         */

        private void btnGHI_ItemClick(object sender, ItemClickEventArgs e)
        {
            /* Step 0 */
            bool ketQua = kiemTraDuLieuDauVao();
            if (ketQua == false)
                return;



            /*Step 1*/
            /*Lay du lieu truoc khi chon btnGHI - phuc vu btnHOANTAC - sau khi OK thi da la du lieu moi*/
            String maNhanVien = txtMANV.Text.Trim();// Trim() de loai bo khoang trang thua
            DataRowView drv = ((DataRowView)bdsNhanVien[bdsNhanVien.Position]);
            String ho = drv["HO"].ToString();
            String ten = drv["TEN"].ToString();
            String diaChi = drv["DIACHI"].ToString();
            DateTime ngaySinh = ((DateTime)drv["NGAYSINH"]);
            Console.WriteLine(ngaySinh);
            int luong = int.Parse(drv["LUONG"].ToString());
            int trangThai = (trangThaiXoaCheckBox.Checked == true) ? 1 : 0;
            String cauTruyVan =
                    "DECLARE	@result int " +
                    "EXEC @result = [dbo].[sp_TraCuu_KiemTraMaNhanVien] '" +
                    maNhanVien + "' " +
                    "SELECT 'Value' = @result"; ;
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



            /*Step 2*/
            int viTriConTro = bdsNhanVien.Position;
            int viTriMaNhanVien = bdsNhanVien.Find("MANV", txtMANV.Text);

            if (result == 1 && viTriConTro != viTriMaNhanVien)
            {
                MessageBox.Show("Mã nhân viên này đã được sử dụng !", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            else/*them moi | sua nhan vien*/
            {
                DialogResult dr = MessageBox.Show("Bạn có chắc muốn ghi dữ liệu vào cơ sở dữ liệu ?", "Thông báo",
                        MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {
                    try
                    {
                        /*bật các nút về ban đầu*/
                        btnTHEM.Enabled = true;
                        btnXOA.Enabled = true;
                        btnGHI.Enabled = true;
                        btnHOANTAC.Enabled = true;
                        btnLAMMOI.Enabled = true;
                        this.txtMANV.Enabled = false;
                        this.bdsNhanVien.EndEdit();
                        this.nhanVienTableAdapter.Update(this.dataSet.NhanVien);
                        this.gcNhanVien.Enabled = true;

                        /*lưu 1 câu truy vấn để hoàn tác yêu cầu*/
                        String cauTruyVanHoanTac = "";
                        /*trước khi ấn btnGHI là btnTHEM*/
                        if (dangThemMoi == true)
                        {
                            cauTruyVanHoanTac = "" +
                                "DELETE DBO.NHANVIEN " +
                                "WHERE MANV = " + txtMANV.Text.Trim();
                        }
                        /*trước khi ấn btnGHI là sửa thông tin nhân viên*/
                        else
                        {


                            cauTruyVanHoanTac =
                                "UPDATE DBO.NhanVien " +
                                "SET " +
                                "HO = '" + ho + "'," +
                                "TEN = '" + ten + "'," +
                                "DIACHI = '" + diaChi + "'," +
                                "NGAYSINH = CAST('" + ngaySinh.ToString("yyyy-MM-dd") + "' AS DATETIME)," +
                                "LUONG = '" + luong + "'," +
                                "TrangThaiXoa = " + trangThai + " " +
                                "WHERE MANV = '" + maNhanVien + "'";
                        }
                        Console.WriteLine(cauTruyVanHoanTac);

                        /*Đưa câu truy vấn hoàn tác vào undoList 
                         * để nếu chẳng may người dùng ấn hoàn tác thì làm luôn*/
                        undoList.Push(cauTruyVanHoanTac);
                        /*cập nhật lại trạng thái thêm mới cho chắc*/
                        dangThemMoi = false;
                        MessageBox.Show("Ghi thành công", "Thông báo", MessageBoxButtons.OK);
                    }
                    catch (Exception ex)
                    {

                        bdsNhanVien.RemoveCurrent();
                        MessageBox.Show("Thất bại. Vui lòng kiểm tra lại!\n" + ex.Message, "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}