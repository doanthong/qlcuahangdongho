using DevExpress.Skins;
using DevExpress.UserSkins;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace QLDH
{
    static class Program
    {
        /**********************************************
         * conn: biến để kết nối tới cơ sở dữ liệu
         * connstr: connection String , chuỗi kết nối động
         * dataReader: 
         **********************************************/
        public static SqlConnection conn = new SqlConnection();
        public static String connstr = "";
        public static String connstrPublisher = "Data Source=DESKTOP-MNMQRVG;Initial Catalog=QLDH;Integrated Security=true";
        public static SqlDataReader myReader;



        /**********************************************
         * username: tên username trong database sẽ kết nối.
         * Ví dụ: 1, 3 là username trong mục users của database QLDH
         * 
         * loginName & loginPassword: tài khoản & mật khẩu dùng để 
         * đăng nhập vào server
         ***********************************************/
        public static String userName = "";
        public static String loginName = "";
        public static String loginPassword = "";




        /**********************************************
         *database: cơ sở dữ liệu mà ta muốn làm việc
         ***********************************************/
        public static String database = "QLDH";

        /**********************************************
         * role: tên nhóm quyền đang đăng nhập: CUAHANG - NHANVIEN
         * staff: tên nhân viên đang đăng nhập
         **********************************************/
        public static String role = "";// mGroup
        public static String staff = "";//mHoten



        /**********************************************
         * maDongHoDuocChon biến lưu trữ mã đồng hồ được chọn phục vụ 
         * cho btnChonDongHo trong phần tạo mới đơn đặt hàng
         * 
         * maSoDonDatHangDuocChon luu tru ma don hang duoc chon phuc vu
         * cho btnChonDonDatHang trong phan tao moi phieu nhap
         * soLuongDongHo bien luu tru so luong dong ho duoc chon
         * 
         **********************************************/
        public static string maDongHoDuocChon = "";
        public static int soLuongDongHo = 0;
        public static string maDonDatHangDuocChon = "";
        public static string maDonDatHangDuocChonChiTiet = "";
        public static int donGia = 0;



        /*Cho nay de phuc vu tinh nang HOAT DONG NHAN VIEN*/
        public static string maNhanVienDuocChon = "";

        /*bidSou: BindingSource -> liên kết dữ liệu từ bảng dữ liệu vào chương trình*/
        public static BindingSource bindingSource = new BindingSource();


        /*các form của toàn dữ án cũng được coi như 1 một biến toàn cục*/
        public static FormDangNhap formDangNhap;
        public static FormChinh formChinh;
        public static FormNhanVien formNhanVien;
        public static FormDongHo formDongHo;
        public static FormDonDatHang formDonDatHang;
        public static FormChonDongHo formChonDongHo;
        public static FormPhieuNhap formPhieuNhap;
        public static FormChonDonDatHang formChonDonDatHang;
        public static FormChonChiTietDonHang formChonChiTietDonHang;
        public static FormPhieuXuat formPhieuXuat;
        /*****************************************************
         * mở kết nối tới server 
         * @return trả về 1 nếu thành công
         *         trả về 0 nếu thất bại
         *****************************************************/
        public static int KetNoi()
        {
            if (Program.conn != null && Program.conn.State == ConnectionState.Open)
                Program.conn.Close();
            try
            {
                Program.connstr = "Data Source=" + ";Initial Catalog=" +
                       Program.database + ";User ID=" +
                       Program.loginName + ";password=" + Program.loginPassword;
                Program.conn.ConnectionString = Program.connstr;

                Program.conn.Open();
                return 1;
            }

            catch (Exception e)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu.\nXem lại tài khoản và mật khẩu.\n " + e.Message, "", MessageBoxButtons.OK);
                return 0;
            }
        }


        /**********************************************
         *  ExecSqlDataReader thực hiện câu lệnh mà dữ liệu trả về chỉ dùng
         *  để xem & không thao tác với nó.
         **********************************************/
        public static SqlDataReader ExecSqlDataReader(String strLenh)
        {
            SqlDataReader myreader;
            SqlCommand sqlcmd = new SqlCommand(strLenh, Program.conn);
            sqlcmd.CommandType = CommandType.Text;
            if (Program.conn.State == ConnectionState.Closed)
                Program.conn.Open();
            try
            {
                myreader = sqlcmd.ExecuteReader(); return myreader;

            }
            catch (SqlException ex)
            {
                Program.conn.Close();
                MessageBox.Show(ex.Message);
                return null;
            }
        }


        /**********************************************
         * ExecSqlDataTable thực hiện câu lệnh mà dữ liệu trả về có thể
         * xem - thêm - xóa - sửa tùy ý
         * 
         * Ví dụ: SELECT * FROM dbo.NHANVIEN
         **********************************************/
        public static DataTable ExecSqlDataTable(String cmd)
        {
            DataTable dt = new DataTable();
            if (Program.conn.State == ConnectionState.Closed) Program.conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd, conn);
            da.Fill(dt);
            conn.Close();
            return dt;
        }


        /**********************************************
         * Cập nhật trên một stored procedure và không trả về giá trị
         **********************************************/
        public static int ExecSqlNonQuery(String strlenh)
        {
            SqlCommand Sqlcmd = new SqlCommand(strlenh, conn);
            Sqlcmd.CommandType = CommandType.Text;
            Sqlcmd.CommandTimeout = 600;// 10 phut
            if (conn.State == ConnectionState.Closed) conn.Open();
            try
            {
                Sqlcmd.ExecuteNonQuery(); conn.Close();
                return 0;
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("Error converting data type varchar to int"))
                    MessageBox.Show("Bạn format Cell lại cột \"Ngày Thi\" qua kiểu Number hoặc mở File Excel.");
                else MessageBox.Show(ex.Message);
                conn.Close();
                return ex.State;

            }
        }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Program.formChinh = new FormChinh();
            Application.Run(formChinh);
        }
    }
}