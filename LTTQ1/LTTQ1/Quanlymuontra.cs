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
    public partial class Quanlymuontra : Form
    {
        public Quanlymuontra()
        {
            InitializeComponent();
        }
        

        private void Quanlymuontra_Load(object sender, EventArgs e)
        {
            refreshDataGridView();
            Loadcombox();
        }
        private void refreshDataGridView()
        {
            myDatabase db = new myDatabase();
            dgvMuontra.DataSource = db.getData("Select * from MuonTraSach");
            dgvTraSach.DataSource = db.getData("Select * from MuonTraSach");
        }
        private void Loadcombox()
        {
            myDatabase db = new myDatabase();
            cbMaSach.DataSource = db.getData("Select * From Sach");
            cbMaSach.DisplayMember = "TenSach";
            cbMaSach.ValueMember = "MaSach";

            cbDocGia.DataSource = db.getData("Select * from NguoiMuon");
            cbDocGia.DisplayMember = "MaDG";
            cbDocGia.ValueMember = "MaDG";

            cbMaDG_TraSach.DataSource = db.getData("Select * from MuonTraSach");
            cbMaDG_TraSach.DisplayMember = "MaDG";
            cbMaDG_TraSach.ValueMember = "MaDG";

        }

        private void cbMaSach_SelectedIndexChanged(object sender, EventArgs e)
        {
             String conSt = @"Data Source=ADMIN\SQLEXPRESS;Initial Catalog=QLTVS;Integrated Security=True";
             SqlConnection myConnection = new SqlConnection(conSt);
            String masach = cbMaSach.SelectedValue.ToString();
            String sql = String.Format("Select MaSach, MaLoaiSach, SoLuong, MaTG From Sach where MaSach=N'{0}'", masach);
            SqlDataAdapter da = new SqlDataAdapter(sql, myConnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            foreach (DataRow drw in dt.Rows)
            {
                txtMaSach1.Text = drw["MaSach"].ToString();
                labMaLoai.Text = drw["MaLoaiSach"].ToString();
                labSoLuong.Text = drw["SoLuong"].ToString();
                lbMaTG.Text = drw["MaTG"].ToString();
            }

           
        }

        private void Quanlymuontra_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dl = MessageBox.Show("Bạn có muốn đóng chương trình không?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dl == DialogResult.No)
                e.Cancel = true;
        }

        private void btnKetThuc_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnChoMuon_Click(object sender, EventArgs e)
        {
            myDatabase db = new myDatabase();
            SqlConnection con = new SqlConnection(db.conSt);
            String madg = cbDocGia.SelectedValue.ToString();
            String masach = txtMaSach.Text;
            int soluong = Convert.ToInt32(txtSoLuong.Text);
            String ngaymuon = Convert.ToDateTime(dtMuon.Text).ToShortDateString();
            String ngayhentra = Convert.ToDateTime(dtTra.Text).ToShortDateString();
            String sql = String.Format("Insert into  MuonTraSach(MaDG,MaSach,SoLuong,NgayMuon,NgayHenTra) values('{0}','{1}',{2},'{3}','{4}')", madg, masach, soluong, ngaymuon, ngayhentra);
            db.getData(sql);
            refreshDataGridView();
        }
        //Trả sách
        private void btnTraSach_Click(object sender, EventArgs e)
        {
            DateTime ngayhentra = dtNgayHenTra_TraSach.Value;
            DateTime ngaytra = dtNgayTra.Value;
            if (ngaytra <= ngayhentra)
            {
                lblTinhTrangTraSach.Text = "Đúng hạn";
            }
            else
            {
                lblTinhTrangTraSach.Text = "Quá hạn";
            }
        }

        private void dgvTraSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cbMaDG_TraSach.Text = dgvTraSach.Rows[e.RowIndex].Cells["MaDG"].Value.ToString();
            txtMaSach_TraSach.Text = dgvTraSach.Rows[e.RowIndex].Cells["MaSach"].Value.ToString();
            txtSoLuong_TraSach.Text = dgvTraSach.Rows[e.RowIndex].Cells["SoLuong"].Value.ToString();
            dtNgayMuon_TraSach.Text = dgvTraSach.Rows[e.RowIndex].Cells["NgayMuon"].Value.ToString();
            dtNgayHenTra_TraSach.Text = dgvTraSach.Rows[e.RowIndex].Cells["NgayHenTra"].Value.ToString();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            myDatabase db = new myDatabase();
            SqlConnection con = new SqlConnection(db.conSt);
            String MaDG = cbMaDG_TraSach.SelectedValue.ToString();
            String Masach = txtMaSach_TraSach.Text;
            String ngaytra = Convert.ToDateTime(dtNgayTra.Text).ToShortDateString();
            String sql = String.Format("UPDATE MuonTraSach SET NgayTra='{0}' WHERE MaDG='{1}'AND MaSach='{2}'",ngaytra, MaDG,Masach);
            db.getData(sql);
            refreshDataGridView();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            cbDocGia.Text = "";
            txtMaSach.Text = "";
            dtMuon.Text = "";
            dtTra.Text = "";
            txtSoLuong.Text = "";
        }
    }
}
