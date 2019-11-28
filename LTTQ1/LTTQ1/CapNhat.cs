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
    public partial class CapNhat : Form
    {
        public CapNhat()
        {
            InitializeComponent();
        }
        private void refreshDataGridView1()
        {
            myDatabase db = new myDatabase();
            dgvSach.DataSource = db.getData("Select * from Sach");
        }
        private void refreshDataGridView()
        {
            myDatabase db = new myDatabase();
            dgvLoaiSach.DataSource = db.getData("Select * from LoaiSach");
            
        }

        private void CapNhat_Load(object sender, EventArgs e)
        {
            refreshDataGridView();
            refreshDataGridView1();
            Loadcombox();
            grbChiTietLoaiSach.Enabled = false;
            grbChiTietSach.Enabled = false;
        }
        private void Loadcombox()
        {
            myDatabase db = new myDatabase();
            cbMaLoaiSach.DataSource = db.getData("Select * From LoaiSach");
            cbMaLoaiSach.DisplayMember = "TenLoai";
            cbMaLoaiSach.ValueMember = "MaLoaiSach";

            cbMaTacGia.DataSource = db.getData("Select * from TacGia");
            cbMaTacGia.DisplayMember = "TenTG";
            cbMaTacGia.ValueMember = "MaTG";
            
        }
        //Xử lý form cập nhật loại sách
        private void setbutton()
        {
            btnThem.Text = "Thêm";
            txtMaLoaiSach.Text = "";
            txtTenLoaiSach.Text = "";
            txtKieuSach.Text = "";
            txtMaLoaiSach.Focus();
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
           
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (btnThem.Text == "Thêm")
            {
                btnThem.Text = "Hủy";
                txtMaLoaiSach.Text = "";
                txtTenLoaiSach.Text = "";
                txtKieuSach.Text = "";
                txtMaLoaiSach.Focus();
                btnXoa.Enabled = false;
                btnSua.Enabled = false;
                grbChiTietLoaiSach.Enabled = true;
            }
            else
            {
                setbutton();
                grbChiTietLoaiSach.Enabled = false;
            }
        }
        private void sualuu()
        {
            myDatabase db = new myDatabase();
            String maLS = txtMaLoaiSach.Text;
            String tenLS = txtTenLoaiSach.Text;
            String Kieusach = txtKieuSach.Text;
            String sql = String.Format("Update LoaiSach Set TenLoai=N'{0}',KieuSach=N'{1}' Where MaLoaiSach='{2}'", tenLS, Kieusach, maLS);
            db.getData(sql);
            refreshDataGridView();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (btnThem.Enabled == true)
            {
                myDatabase db = new myDatabase();
                String maloaisach = txtMaLoaiSach.Text;
                String tenloaisach = txtTenLoaiSach.Text;
                String kieusach = txtKieuSach.Text;
                String sql = String.Format("Insert into LoaiSach values('{0}',N'{1}',N'{2}')", maloaisach, tenloaisach, kieusach);
                db.getData(sql);
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
                txtMaLoaiSach.Enabled=false;
                txtTenLoaiSach.Text = "";
                txtKieuSach.Text = "";
                btnThem.Enabled = false;
                btnXoa.Enabled = false;
                grbChiTietLoaiSach.Enabled = true;
            }
            else
            {
                btnSua.Text = "Sửa";
                txtMaLoaiSach.Text = "";
                txtTenLoaiSach.Text = "";
                txtKieuSach.Text = "";
                btnThem.Enabled = true;
                btnXoa.Enabled = true;
                grbChiTietLoaiSach.Enabled = false;
            }
        }

        private void dgvLoaiSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaLoaiSach.Text = dgvLoaiSach.Rows[e.RowIndex].Cells["MaLoaiSach"].Value.ToString();
            txtTenLoaiSach.Text = dgvLoaiSach.Rows[e.RowIndex].Cells["TenLoai"].Value.ToString();
            txtKieuSach.Text = dgvLoaiSach.Rows[e.RowIndex].Cells["KieuSach"].Value.ToString();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            myDatabase db = new myDatabase();
            String Maloaisach = txtMaLoaiSach.Text;
            String sql = String.Format("Delete LoaiSach Where MaLoaiSach='{0}'", Maloaisach);
            db.getData(sql);
            refreshDataGridView();
        }
        //Xử lý form cập nhật sách
        private void setbutton1()
        {
            btnThemSach.Text = "Thêm";
            txtMaSach.Text = "";
            txtTenSach.Text = "";
            cbMaLoaiSach.Text = "";
            txtSoLuong.Text = "";
            cbMaTacGia.Text = "";
            btnSuaSach.Enabled = true;
            btnXoaSach.Enabled = true;
        }

        private void btnThemSach_Click(object sender, EventArgs e)
        {
            if (btnThemSach.Text == "Thêm")
            {
                btnThemSach.Text = "Hủy";
                txtMaSach.Text = "";
                txtTenSach.Text = "";
                cbMaLoaiSach.Text = "";
                txtSoLuong.Text = "";
                cbMaTacGia.Text = "";
                txtMaSach.Focus();
                btnSuaSach.Enabled = false;
                btnXoaSach.Enabled = false;
                grbChiTietSach.Enabled = true;
            }
            else
            {
                setbutton1();
                grbChiTietSach.Enabled = false;
            }
        }
        private void Suasach()
        {
            myDatabase db = new myDatabase();
            String masach = txtMaSach.Text;
            String tensach = txtTenSach.Text;
            String maloai = cbMaLoaiSach.SelectedValue.ToString();
            int soluong = Convert.ToInt32(txtSoLuong.Text);
            String matg = cbMaTacGia.SelectedValue.ToString();
            String sql = String.Format("Update Sach Set TenSach=N'{0}',MaLoaiSach=N'{1}',SoLuong={2},MaTG=N'{3}' where MaSach='{4}'", tensach, maloai, soluong,matg, masach);
            db.getData(sql);
            refreshDataGridView1();
        }
        private void btnLuuSach_Click(object sender, EventArgs e)
        {
            if (btnThemSach.Enabled==true)
            {
                myDatabase db = new myDatabase();
                String masach = txtMaSach.Text;
                String tensach = txtTenSach.Text;
                String maloai = cbMaLoaiSach.SelectedValue.ToString();
                int soluong =Convert.ToInt32(txtSoLuong.Text);
                String maTg = cbMaTacGia.SelectedValue.ToString();
                String sql = String.Format("Insert into Sach values('{0}',N'{1}','{2}',{3},'{4}')", masach, tensach, maloai, soluong, maTg);
                db.getData(sql);
                refreshDataGridView1();
            }
            if (btnSuaSach.Enabled == true)
            {
                Suasach();
                grbChiTietSach.Enabled = true;
            }
        }

        private void btnXoaSach_Click(object sender, EventArgs e)
        {
            myDatabase db = new myDatabase();
            String Masach = txtMaSach.Text;
            String sql = String.Format("Delete Sach Where MaSach='{0}'", Masach);
            db.getData(sql);
            refreshDataGridView1();
        }

        private void dgvSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaSach.Text = dgvSach.Rows[e.RowIndex].Cells["MaSach"].Value.ToString();
            txtTenSach.Text = dgvSach.Rows[e.RowIndex].Cells["TenSach"].Value.ToString();
            cbMaLoaiSach.Text = dgvSach.Rows[e.RowIndex].Cells["MaLoaiSach"].Value.ToString();
            txtSoLuong.Text = dgvSach.Rows[e.RowIndex].Cells["SoLuong"].Value.ToString();
            cbMaTacGia.Text = dgvSach.Rows[e.RowIndex].Cells["MaTG"].Value.ToString();
        }

        private void btnSuaSach_Click(object sender, EventArgs e)
        {
            if (btnSuaSach.Text == "Sửa")
            {
                btnSuaSach.Text = "Hủy";
                txtMaSach.Enabled = false;
                txtTenSach.Text = "";
                cbMaLoaiSach.Text = "";
                txtSoLuong.Text = "";
                cbMaTacGia.Text = "";
                txtTenSach.Focus();
                btnThemSach.Enabled = false;
                btnXoaSach.Enabled = false;
                grbChiTietSach.Enabled = true;
            }
            else
            {
                btnSuaSach.Text = "Sửa";
                txtMaSach.Enabled = true;
                txtTenSach.Text = "";
                cbMaLoaiSach.Text = "";
                txtSoLuong.Text = "";
                cbMaTacGia.Text = "";
                txtMaSach.Focus();
                btnThemSach.Enabled = true;
                btnXoaSach.Enabled = true;
                grbChiTietSach.Enabled = false;
            }
        }

        private void CapNhat_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dl = MessageBox.Show("Bạn có muốn đóng chương trình không?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dl == DialogResult.No)
                e.Cancel = true;
        }

        private void btnThoatSach_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
