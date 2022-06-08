using DevExpress.XtraEditors;
using System;
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
    public partial class FormDangNhap : DevExpress.XtraEditors.XtraForm
    {

        private SqlConnection connPublisher = new SqlConnection();

        public FormDangNhap()
        {
            InitializeComponent();
        }

        /******************************************************************
         * mở kết nối tới server 
         * @return trả về 1 nếu thành công
         *         trả về 0 nếu thất bại
         ******************************************************************/

        private int KetNoiDatabaseGoc()
        {
            if (connPublisher != null && connPublisher.State == ConnectionState.Open)
                connPublisher.Close();
            try
            {
                connPublisher.ConnectionString = Program.connstrPublisher;
                connPublisher.Open();
                return 1;
            }

            catch (Exception e)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu.\nBạn xem lại user name và password.\n " + e.Message, "", MessageBoxButtons.OK);
                return 0;
            }
        }

        private void FormDangNhap_Load(object sender, EventArgs e)
        {
            txtTAIKHOAN.Text = "NV1";// CUA HANG
            txtMATKHAU.Text = "123";
            if (KetNoiDatabaseGoc() == 0)
                return;
        }

        private void btnDANGNHAP_Click(object sender, EventArgs e)
        {
            /* Step 1: Kiểm tra tài khoản & mật khẩu xem có bị trống không ?*/
            if (txtTAIKHOAN.Text.Trim() == "" || txtMATKHAU.Text.Trim() == "")
            {
                MessageBox.Show("Tài khoản & mật khẩu không thể bỏ trống", "Thông Báo", MessageBoxButtons.OK);
                return;
            }

            /* Step 2: gán loginName & loginPassword với tài khoản mật khẩu được nhập, loginName và loginPassword dùng để đăng nhập vào phân mảnh này*/
            Program.loginName = txtTAIKHOAN.Text.Trim();
            Program.loginPassword = txtMATKHAU.Text.Trim();
            if (Program.KetNoi() == 0)
                return;

            /* Step 3: chạy stored procedure DANG NHAP de lay thong tin nguoi dung*/
            String strLenh = "EXEC SP_DangNhap '" + Program.loginName + "'";
            Program.myReader = Program.ExecSqlDataReader(strLenh);
            if (Program.myReader == null)
                return;
            // đọc một dòng của myReader - điều này là hiển nhiên vì kết quả chỉ có 1 dùng duy nhất
            Program.myReader.Read();

            /* Step 4: gán giá trị Mã nhân viên - họ tên - vai trò ở góc màn hình*/
            Program.userName = Program.myReader.GetString(0);// lấy userName
            if (Convert.IsDBNull(Program.userName))
            {
                MessageBox.Show("Tài khoản này không có quyền truy cập \n Hãy thử tài khoản khác", "Thông Báo", MessageBoxButtons.OK);
            }

            Program.staff = Program.myReader.GetString(1);
            Program.role = Program.myReader.GetString(2);

            Program.myReader.Close();
            Program.conn.Close();

            Program.formChinh.MANHANVIEN.Text = "MÃ NHÂN VIÊN: " + Program.userName;
            Program.formChinh.HOTEN.Text = "HỌ TÊN: " + Program.staff;
            Program.formChinh.NHOM.Text = "VAI TRÒ: " + Program.role;

            /* Step 5: ẩn form hiện tại & hiện các nút chức năng còn lại*/
            this.Visible = false;
            Program.formChinh.enableButtons();
        }

        private void btnTHOAT_Click(object sender, EventArgs e)
        {
            this.Close();
            Program.formChinh.Close();
        }
    }
}