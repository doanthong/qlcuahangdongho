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
    public partial class FormChonDongHo : DevExpress.XtraEditors.XtraForm
    {
        public FormChonDongHo()
        {
            InitializeComponent();
        }

        private void donghoBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bdsDongHo.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dataSet);

        }

        private void FormChonDongHo_Load(object sender, EventArgs e)
        {
            /*không kiểm tra khóa ngoại nữa*/
            dataSet.EnforceConstraints = false;
            this.donghoTableAdapter.Connection.ConnectionString = Program.connstr;
            this.donghoTableAdapter.Fill(this.dataSet.Dongho);
        }

        private void btnCHON_Click(object sender, EventArgs e)
        {
            string maDongHo = ((DataRowView)bdsDongHo.Current)["MADH"].ToString();
            int soLuongDongHo = int.Parse(((DataRowView)bdsDongHo.Current)["SOLUONGTON"].ToString());
            Program.maDongHoDuocChon = maDongHo;
            Program.soLuongDongHo = soLuongDongHo;
            this.Close();
        }

        private void btnTHOAT_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}