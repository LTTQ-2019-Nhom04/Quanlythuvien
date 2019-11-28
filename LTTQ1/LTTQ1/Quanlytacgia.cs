using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace LTTQ1
{
    public partial class Quanlytacgia : Form
    {
        public Quanlytacgia()
        {
            InitializeComponent();
        }

        private void Quanlytacgia_Load(object sender, EventArgs e)
        {
            grbChiTietTG.Enabled = false;
            refreshDataGridView();
        }
        private void refreshDataGridView()
        {
            myDatabase db = new myDatabase();
            dgvTacgia.DataSource = db.getData("Select * from TacGia");
        }
        private void setButton()
        {
            btnThem.Text = "Thêm";
            txtMaTG.Text = "";
            txtTenTG.Text = "";
            txtDiaChi.Text = "";
            txtMaTG.Focus();
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            dgvTacgia.Enabled = true;
        }
         
            private void dgvTacgia_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaTG.Text = dgvTacgia.Rows[e.RowIndex].Cells["MaTG"].Value.ToString();
            txtTenTG.Text = dgvTacgia.Rows[e.RowIndex].Cells["TenTG"].Value.ToString();
            txtDiaChi.Text = dgvTacgia.Rows[e.RowIndex].Cells["DiaChi"].Value.ToString();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (btnThem.Text == "Thêm")
            {
                btnThem.Text = "Hủy";
                txtMaTG.Enabled = false;
                txtTenTG.Text = "";
                txtDiaChi.Text = "";
                txtMaTG.Focus();
                grbChiTietTG.Enabled = true;
                btnXoa.Enabled = false;
                btnSua.Enabled = false;
                dgvTacgia.Enabled = false;
            }
            else
            {
                setButton();
            }
        }
        private void sualuu()
        {
            try
            {
                myDatabase db = new myDatabase();
                SqlConnection con = new SqlConnection(db.conSt);
                String sql = "Update TacGia SET TenTG=N'" + txtTenTG.Text + "',DiaChi=N'" + txtDiaChi.Text + "'where MaTG='"+txtMaTG.Text+"'";
                db.getData(sql);

            }
            catch
            {
                MessageBox.Show("Lỗi không sửa được", "Error");
            }
            refreshDataGridView();
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (btnThem.Enabled == true)
            {
                int count = 0;
                count = dgvTacgia.Rows.Count;
                string chuoi = "";
                int chuoi2 = 0;
                chuoi = Convert.ToString(dgvTacgia.Rows[count - 2].Cells[0].Value);
                chuoi2 = Convert.ToInt32((chuoi.Remove(0, 2)));//Loại bỏ ký tự mã tác giả DG
                if (chuoi2 + 1 < 10)
                {
                    txtMaTG.Text = "TG00" + (chuoi2 + 1).ToString();
                }
                else
                    if (chuoi2 + 1 < 100)
                {
                    txtMaTG.Text = "TG0" + (chuoi2 + 1).ToString();
                }
                try
                {
                    myDatabase db = new myDatabase();
                    SqlConnection con = new SqlConnection(db.conSt);
                    String sql = "INSERT INTO TacGia values('" + txtMaTG.Text + "',N'" + txtTenTG.Text + "',N'" + txtDiaChi.Text + "')";
                    dgvTacgia.DataSource = db.getData(sql);

                }
                catch
                {
                    MessageBox.Show("Lỗi không thêm được", "Error");
                }
                refreshDataGridView();
            }
            if (btnSua.Enabled == true)
            {
                sualuu();
            }
            
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (btnSua.Text == "Sửa")
            {
            btnSua.Text = "Hủy";
            grbChiTietTG.Enabled = true;
            txtMaTG.Focus();
            txtMaTG.SelectAll();
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            txtMaTG.Enabled = false;
            }
            else
            {
                btnSua.Text = "Sửa";
                grbChiTietTG.Enabled = false;
                btnThem.Enabled = true;
                btnXoa.Enabled = true;
            }
            
            
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                myDatabase db = new myDatabase();
                SqlConnection con = new SqlConnection(db.conSt);
                String sql = "Delete TacGia where MaTG='" + txtMaTG.Text + "'";
                db.getData(sql);
                txtMaTG.Text = "";
                txtTenTG.Text = "";
                txtDiaChi.Text = "";
                txtMaTG.Focus();
                refreshDataGridView();
            }
            catch
            {
                MessageBox.Show("Lỗi, Không xóa được");
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Quanlytacgia_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dl = MessageBox.Show("Bạn có muốn đóng chương trình không?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dl == DialogResult.No)
                e.Cancel = true;
        }
    }
}
