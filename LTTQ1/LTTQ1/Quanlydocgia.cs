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
    public partial class Quanlydocgia : Form
    {
        public Quanlydocgia()
        {
            InitializeComponent();
        }
        private void refreshDataGridView()
        {
            myDatabase db = new myDatabase();
            dgvDocGia.DataSource = db.getData("Select * from NguoiMuon");

        }
        private void setbutton()
        {
            btnThem.Text = "Thêm";
            txtMaDG.Text = "";
            txtTenDG.Text = "";
            txtDiaChi.Text = "";
            txtMaDG.Focus();
            btnXoa.Enabled = true;
            btnSua.Enabled = true;

        }


        private void Quanlydocgia_Load(object sender, EventArgs e)
        {
            refreshDataGridView();
            grbChiTietDG.Enabled = false;
            grbChiTietDG.Enabled = true; ;
            grGioiTinh.Enabled = false;

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (btnThem.Text == "Thêm")
            {
                btnThem.Text = "Hủy";
                txtMaDG.Text = "";
                txtTenDG.Text = "";
                txtDiaChi.Text = "";
                txtMaDG.Focus();
                btnXoa.Enabled = false;
                btnSua.Enabled = false;
                grbChiTietDG.Enabled = true;
                grGioiTinh.Enabled = true;
            }
            else
            {
                setbutton();
                grbChiTietDG.Enabled = false;
            }

        }
        private void sualuu()
        {
            myDatabase db = new myDatabase();
            String maDG = txtMaDG.Text;
            String tenDG = txtTenDG.Text;
            String diaChi = txtDiaChi.Text;
            String ngayMuon = Convert.ToDateTime(dtNgayMuon.Text).ToShortDateString();
            String sql = String.Format("Update NguoiMuon Set TenDG=N'{0}',NgayMuon='{1}',DiaChi=N'{2}' Where MaDG='{3}'", tenDG, ngayMuon, diaChi, maDG);
            db.getData(sql);
            refreshDataGridView();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (btnThem.Enabled == true)
            {
                myDatabase db = new myDatabase();
                String maDG = txtMaDG.Text;
                String tenDG = txtTenDG.Text;
                String diaChi = txtDiaChi.Text;

                String ngayMuon = Convert.ToDateTime(dtNgayMuon.Text).ToShortDateString();
                if (radNam.Checked)
                {
                    String gioiTinh = "Nam";
                    String sql = String.Format("Insert into NguoiMuon values('{0}',N'{1}',N'{2}','{3}',N'{4}')", maDG, tenDG, gioiTinh, ngayMuon, diaChi);
                    db.getData(sql);
                }
                else
                {
                    String gioiTinh = "Nữ";
                    String sql = String.Format("Insert into NguoiMuon values('{0}',N'{1}',N'{2}','{3}',N'{4}')", maDG, tenDG, gioiTinh, ngayMuon, diaChi);
                    db.getData(sql);
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
                grbChiTietDG.Enabled = true;
                btnSua.Text = "Hủy";
                txtMaDG.Enabled = false;
                txtTenDG.Text = "";
                //txt.Text = "";
                btnThem.Enabled = false;
                btnXoa.Enabled = false;
                //grbChiTietLoaiSach.Enabled = true;
            }
            else
            {
                btnSua.Text = "Sửa";
                //txtMaLoaiSach.Text = "";
                //txtTenLoaiSach.Text = "";
                //txtKieuSach.Text = "";
                btnThem.Enabled = true;
                btnXoa.Enabled = true;
                // grbChiTietLoaiSach.Enabled = false;
            }

        }

        private void dgvDocGia_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            grbChiTietDG.Enabled = true;
            radNam.Checked = false;
            radNu.Checked = false;
            txtMaDG.Text = dgvDocGia.Rows[e.RowIndex].Cells["MaDG"].Value.ToString();
            txtTenDG.Text = dgvDocGia.Rows[e.RowIndex].Cells["TenDG"].Value.ToString();
            if (dgvDocGia.Rows[e.RowIndex].Cells["GioiTinh"].Value.ToString() == "Nam")
            {
                radNam.Checked = true;
            }
            else
            {
                radNu.Checked = true;
            }
            dtNgayMuon.Text = dgvDocGia.Rows[e.RowIndex].Cells["NgayMuon"].Value.ToString();
            txtDiaChi.Text = dgvDocGia.Rows[e.RowIndex].Cells["DiaChi"].Value.ToString();

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            myDatabase db = new myDatabase();
            String MaDG = txtMaDG.Text;
            String sql = String.Format("Delete NguoiMuon Where MaDG='{0}'", MaDG);
            db.getData(sql);
            refreshDataGridView();


        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Quanlydocgia_FormClosing(object sender, FormClosingEventArgs e)
        {

            DialogResult dl = MessageBox.Show("Bạn có muốn đóng chương trình không?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dl == DialogResult.No)
                e.Cancel = true;

        }
    }
}
