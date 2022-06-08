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
    public partial class FormDongHo : DevExpress.XtraEditors.XtraForm
    {

        int viTri = 0;

        bool dangThemMoi = false;

        Stack undoList = new Stack();

        public FormDongHo()
        {
            InitializeComponent();
        }

        private void donghoBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bdsDongHo.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dataSet);

        }

        private void FormDongHo_Load(object sender, EventArgs e)
        {
            /*không kiểm tra khóa ngoại nữa*/
            dataSet.EnforceConstraints = false;

            this.cTDDHTableAdapter.Connection.ConnectionString = Program.connstr;
            this.cTDDHTableAdapter.Fill(this.dataSet.CTDDH);

            this.cTPNTableAdapter.Connection.ConnectionString = Program.connstr;
            this.cTPNTableAdapter.Fill(this.dataSet.CTPN);

            this.cTPXTableAdapter.Connection.ConnectionString = Program.connstr;
            this.cTPXTableAdapter.Fill(this.dataSet.CTPX);

            this.donghoTableAdapter.Connection.ConnectionString = Program.connstr;
            this.donghoTableAdapter.Fill(this.dataSet.Dongho);

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

            /* CUA HANG co the xem - xoa - sua du lieu*/
            if (Program.role == "CUAHANG")
            {
                this.btnTHEM.Enabled = true;
                this.btnXOA.Enabled = true;
                this.btnGHI.Enabled = true;
                this.btnHOANTAC.Enabled = false;
                this.btnLAMMOI.Enabled = true;
                this.panelNhapLieu.Enabled = true;
                this.txtMADH.Enabled = false;
            }
        }

        private void btnTHEM_ItemClick(object sender, ItemClickEventArgs e)
        {
            /*lấy vị trí hiện tại của con trỏ*/
            viTri = bdsDongHo.Position;
            this.panelNhapLieu.Enabled = true;
            dangThemMoi = true;

            /*AddNew tự động nhảy xuống cuối thêm 1 dòng mới*/
            bdsDongHo.AddNew();
            txtSOLUONGTON.Value = 1;

            this.txtMADH.Enabled = true;
            this.btnTHEM.Enabled = false;
            this.btnXOA.Enabled = false;
            this.btnGHI.Enabled = true;
            this.btnHOANTAC.Enabled = true;
            this.btnLAMMOI.Enabled = false;
            this.gcDongHo.Enabled = false;
            this.panelNhapLieu.Enabled = true;
        }

        /**********************************************************************
         * moi lan nhan btnHOANTAC thi nen nhan them btnLAMMOI de 
         * tranh bi loi khi an btnTHEM lan nua
         * bdsNhanVien.CancelEdit() - phuc hoi lai du lieu neu chua an btnGHI
         * Step 0: trường hợp đã ấn btnTHEM nhưng chưa ấn btnGHI
         * Step 1: kiểm tra undoList có trông hay không ?
         * Step 2: Neu undoList khong trống thì lấy ra khôi phục
         *********************************************************************/

        private void btnHOANTAC_ItemClick(object sender, ItemClickEventArgs e)
        {
            /* Step 0 */
            if (dangThemMoi == true && this.btnTHEM.Enabled == false)
            {
                dangThemMoi = false;

                this.txtMADH.Enabled = false;
                this.btnTHEM.Enabled = true;
                this.btnXOA.Enabled = true;
                this.btnGHI.Enabled = true;

                this.btnHOANTAC.Enabled = false;
                this.btnLAMMOI.Enabled = true;
                this.gcDongHo.Enabled = true;
                this.panelNhapLieu.Enabled = true;

                bdsDongHo.CancelEdit();
                /*xoa dong hien tai*/
                bdsDongHo.RemoveCurrent();
                /* trở về lúc đầu con trỏ đang đứng*/
                bdsDongHo.Position = viTri;
                return;
            }

            /*Step 1*/
            if (undoList.Count == 0)
            {
                MessageBox.Show("Không còn thao tác nào để khôi phục", "Thông báo", MessageBoxButtons.OK);
                btnHOANTAC.Enabled = false;
                return;
            }

            /*Step 2*/
            bdsDongHo.CancelEdit();
            String cauTruyVanHoanTac = undoList.Pop().ToString();
            Console.WriteLine(cauTruyVanHoanTac);
            int n = Program.ExecSqlNonQuery(cauTruyVanHoanTac);
            this.donghoTableAdapter.Fill(this.dataSet.Dongho);
        }

        private bool kiemTraDuLieuDauVao()
        {
            /*Kiem tra txtMADH*/
            if (txtMADH.Text == "")
            {
                MessageBox.Show("Không bỏ trống mã đồng hồ", "Thông báo", MessageBoxButtons.OK);
                txtMADH.Focus();
                return false;
            }

            if (Regex.IsMatch(txtMADH.Text, @"^[a-zA-Z0-9]+$") == false)
            {
                MessageBox.Show("Mã đồng hồ chỉ có chữ cái và số", "Thông báo", MessageBoxButtons.OK);
                txtMADH.Focus();
                return false;
            }

            if (txtMADH.Text.Length > 4)
            {
                MessageBox.Show("Mã đồng hồ không quá 4 kí tự", "Thông báo", MessageBoxButtons.OK);
                txtMADH.Focus();
                return false;
            }
            /*Kiem tra txtTENDH*/
            if (txtTENDH.Text == "")
            {
                MessageBox.Show("Không bỏ trống tên đồng hồ", "Thông báo", MessageBoxButtons.OK);
                txtTENDH.Focus();
                return false;
            }

            if (txtTENDH.Text.Length > 30)
            {
                MessageBox.Show("Tên đồng hồ không quá 30 kí tự", "Thông báo", MessageBoxButtons.OK);
                txtTENDH.Focus();
                return false;
            }
            /*Kiem tra txtDVT*/
            if (txtDVT.Text == "")
            {
                MessageBox.Show("Không bỏ trống đơn vị tính", "Thông báo", MessageBoxButtons.OK);
                txtDVT.Focus();
                return false;
            }

            if (txtDVT.Text.Length > 15)
            {
                MessageBox.Show("Đơn vị vật tự không quá 15 kí tự", "Thông báo", MessageBoxButtons.OK);
                txtDVT.Focus();
                return false;
            }

            /*Kiem tra txtSOLUONGTON*/
            if (txtSOLUONGTON.Value < 0)
            {
                MessageBox.Show("Sô lượng tồn phải ít nhất bằng 0", "Thông báo", MessageBoxButtons.OK);
                txtSOLUONGTON.Focus();
                return false;
            }

            return true;
        }

        /***********************************************************************
         * viTriConTro: vi tri con tro chuot dang dung
         * viTriMaDongHo: vi tri cua ma dong ho voi btnTHEM or hanh dong sua du lieu
         * SP_KiemTraMaDongHo tra ve 0 neu khong ton tai
         *                          1 neu ton tai
         *                                    
         * Step 0 : Kiem tra du lieu dau vao
         * Step 1 : Dung stored procedure SP_KiemTraMaDongHo de kiem tra txtMADH
         * Step 2 : Ket hop ket qua tu Step 1 & vi tri cua txtMADH co 4 truong hop xay ra
         * + TH0: ketQua = 1 && viTriConTro != viTriMaDongHo -> them moi nhung MADH da ton tai
         * + TH1: ketQua = 1 && viTriConTro == viTriMaDongHo -> sua dong ho cu
         * + TH2: ketQua = 0 && viTriConTro == viTriMaDongHo -> co the them moi dong ho
         * + TH3: ketQua = 0 && viTriConTro != viTriMaDongHo -> co the them moi dong ho
         *          
         * Step 3 : Neu khong phai TH0 thi cac TH1 - TH2 - TH3 deu hop le 
         ***********************************************************************/

        private void btnGHI_ItemClick(object sender, ItemClickEventArgs e)
        {
            /* Step 0 */
            bool ketQua = kiemTraDuLieuDauVao();
            if (ketQua == false)
                return;


            /*Step 1*/
            /*Lay du lieu truoc khi chon btnGHI - phuc vu btnHOANTAC*/
            String maDongHo = txtMADH.Text.Trim();// Trim() de loai bo khoang trang thua
            DataRowView drv = ((DataRowView)bdsDongHo[bdsDongHo.Position]);
            String tenDongHo = drv["TENDH"].ToString();
            String donViTinh = drv["DVT"].ToString();
            String soLuongTon = (drv["SOLUONGTON"].ToString());

            String cauTruyVan =
                    "DECLARE	@result int " +
                    "EXEC @result = SP_KiemTraMaDongHo '" +
                    maDongHo + "' " +
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
            int viTriConTro = bdsDongHo.Position;
            int viTriMaDongHo = bdsDongHo.Find("MADH", txtMADH.Text);

            if (result == 1 && viTriConTro != viTriMaDongHo)
            {
                MessageBox.Show("Mã đồng hồ này đã được sử dụng !", "Thông báo", MessageBoxButtons.OK);
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
                        /*bật các nút về ban đầu*/
                        btnTHEM.Enabled = true;
                        btnXOA.Enabled = true;
                        btnGHI.Enabled = true;
                        btnHOANTAC.Enabled = true;
                        btnLAMMOI.Enabled = true;
                        this.txtMADH.Enabled = false;
                        this.bdsDongHo.EndEdit();
                        this.donghoTableAdapter.Update(this.dataSet.Dongho);
                        this.gcDongHo.Enabled = true;

                        /*lưu 1 câu truy vấn để hoàn tác yêu cầu*/
                        String cauTruyVanHoanTac = "";
                        /*trước khi ấn btnGHI là btnTHEM*/
                        if (dangThemMoi == true)
                        {
                            cauTruyVanHoanTac = "" +
                                "DELETE DBO.DONGHO " +
                                "WHERE MADH = '" + txtMADH.Text.Trim() + "'";
                        }
                        /*trước khi ấn btnGHI là sửa thông tin nhân viên*/
                        else
                        {
                            cauTruyVanHoanTac =
                                "UPDATE DBO.DONGHO " +
                                "SET " +
                                "TENDH = '" + tenDongHo + "'," +
                                "DVT = '" + donViTinh + "'," +
                                "SOLUONGTON = " + soLuongTon + " " +
                                "WHERE MADH = '" + maDongHo + "'";
                        }
                        Console.WriteLine(cauTruyVanHoanTac);
                        /*Đưa câu truy vấn hoàn tác vào undoList 
                         * để nếu chẳng may người dùng ấn hoàn tác thì quất luôn*/
                        undoList.Push(cauTruyVanHoanTac);

                        this.bdsDongHo.EndEdit();
                        this.donghoTableAdapter.Update(this.dataSet.Dongho);
                        /*cập nhật lại trạng thái thêm mới cho chắc*/
                        dangThemMoi = false;
                        MessageBox.Show("Ghi thành công", "Thông báo", MessageBoxButtons.OK);
                    }
                    catch (Exception ex)
                    {
                        bdsDongHo.RemoveCurrent();
                        MessageBox.Show("Thất bại. Vui lòng kiểm tra lại!\n" + ex.Message, "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnLAMMOI_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                // do du lieu moi tu dataSet vao gridControl NHANVIEN
                this.donghoTableAdapter.Fill(this.dataSet.Dongho);
                this.gcDongHo.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi Làm mới" + ex.Message, "Thông báo", MessageBoxButtons.OK);
                return;
            }
        }

        private void btnXOA_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (bdsDongHo.Count == 0)
            {
                btnXOA.Enabled = false;
            }

            if (bdsCTDDH.Count > 0)
            {
                MessageBox.Show("Không thể xóa đồng hồ này vì đã lập đơn đặt hàng", "Thông báo", MessageBoxButtons.OK);
                return;
            }

            if (bdsCTPN.Count > 0)
            {
                MessageBox.Show("Không thể xóa đồng hồ này vì đã lập phiếu nhập", "Thông báo", MessageBoxButtons.OK);
                return;
            }

            if (bdsCTPX.Count > 0)
            {
                MessageBox.Show("Không thể xóa đồng hồ này vì đã lập phiếu xuất", "Thông báo", MessageBoxButtons.OK);
                return;
            }

            /* Phần này phục vụ tính năng hoàn tác
            * Đưa câu truy vấn hoàn tác vào undoList 
            * để nếu chẳng may người dùng ấn hoàn tác thì làm luôn*/


            string cauTruyVanHoanTac =
            "INSERT INTO DBO.DONGHO( MADH,TENDH,DVT,SOLUONGTON) " +
            " VALUES( '" + txtMADH.Text + "','" +
                        txtTENDH.Text + "','" +
                        txtDVT.Text + "', " +
                        txtSOLUONGTON.Value + " ) ";

            Console.WriteLine(cauTruyVanHoanTac);
            undoList.Push(cauTruyVanHoanTac);

            /*Step 2*/
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không ?", "Thông báo",
                MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    /*Step 3*/
                    viTri = bdsDongHo.Position;
                    bdsDongHo.RemoveCurrent();

                    this.donghoTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.donghoTableAdapter.Update(this.dataSet.Dongho);

                    MessageBox.Show("Xóa thành công ", "Thông báo", MessageBoxButtons.OK);
                    this.btnHOANTAC.Enabled = true;
                }
                catch (Exception ex)
                {
                    /*Step 4*/
                    MessageBox.Show("Lỗi xóa đồng hồ. Hãy thử lại\n" + ex.Message, "Thông báo", MessageBoxButtons.OK);
                    this.donghoTableAdapter.Fill(this.dataSet.Dongho);
                    // tro ve vi tri cua dong ho dang bi loi
                    bdsDongHo.Position = viTri;
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