using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LTTQ1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            refreshDataGridView();
            loadcombobox();

        }
        public void loadcombobox()
        {
            myDatabase db = new myDatabase();
            cbLoaiSach.DataSource = db.getData("Select * from LoaiSach");
            cbLoaiSach.DisplayMember = "TenLoai";
            cbLoaiSach.ValueMember = "MaLoaiSach";
        }
        private void refreshDataGridView()
        {
            myDatabase db = new myDatabase();
            dgvSach.DataSource = db.getData("Select * from Sach");
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            CapNhat cn = new CapNhat();
            cn.Show();
        }

        private void btnQLDG_Click(object sender, EventArgs e)
        {
            Quanlydocgia dg = new Quanlydocgia();
            dg.Show();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dl = MessageBox.Show("Bạn có muốn đóng chương trình không?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dl == DialogResult.No)
                e.Cancel = true;
        }

        private void cbLoaiSach_SelectedIndexChanged(object sender, EventArgs e)
        {
            myDatabase db = new myDatabase();
            String maloai = cbLoaiSach.SelectedValue.ToString();
            String sql = String.Format("Select * from Sach where MaLoaiSach='{0}'", maloai);
            dgvSach.DataSource = db.getData(sql);

        }

        private void dgvSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaSach.Text = dgvSach.Rows[e.RowIndex].Cells["MaSach"].Value.ToString();
            txtMaLoaiSach.Text = dgvSach.Rows[e.RowIndex].Cells["MaLoaiSach"].Value.ToString();
            txtMaTG.Text = dgvSach.Rows[e.RowIndex].Cells["MaTG"].Value.ToString();
            txtSoLuong.Text = dgvSach.Rows[e.RowIndex].Cells["SoLuong"].Value.ToString();
            txtTenSach.Text = dgvSach.Rows[e.RowIndex].Cells["TenSach"].Value.ToString();

        }

        private void btnQLTG_Click(object sender, EventArgs e)
        {
            Quanlytacgia qltg = new Quanlytacgia();
            qltg.Show();
        }

        private void btnQLMT_Click(object sender, EventArgs e)
        {
            Quanlymuontra qlmt = new Quanlymuontra();
            qlmt.Show();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            Timkiem tk = new Timkiem();
            tk.Show();

        }

        private void btnKetThuc_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
