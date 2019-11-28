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

namespace LTTQ1
{
    public partial class Timkiem : Form
    {
        public Timkiem()
        {
            InitializeComponent();
        }
         String conSt = @"Data Source=ADMIN\SQLEXPRESS;Initial Catalog=QLTVS;Integrated Security=True";
        
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if(btnTimKiem.Text=="Tìm Kiếm")
            {
                if (txtTimSach.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập thông tin cần tìm", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtTimSach.Focus();
                }
                else
                {
                    if(radMasach.Checked==false && radTensach.Checked== false)
                    {
                        MessageBox.Show("Vui lòng lựa chọn tìm kiếm theo mã sách hoặc tên sách", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        btnTimKiem.Text = "Thử lại";
                        if (radMasach.Checked == true)
                        {
                            SqlConnection myConnection = new SqlConnection(conSt);
                            String timkiem = txtTimSach.Text;
                            String sql = String.Format("Select * from Sach where MaSach='{0}'", timkiem);
                            SqlDataAdapter da = new SqlDataAdapter(sql, myConnection);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            foreach (DataRow drw in dt.Rows)
                            {
                                txtMaSach.Text = drw["MaSach"].ToString();
                                txtTenSach.Text = drw["TenSach"].ToString();
                                txtSoLuong.Text = drw["SoLuong"].ToString();
                                txtMaTacGia.Text = drw["MaTG"].ToString();
                                txtMaLoai.Text = drw["MaLoaiSach"].ToString();
                            }
                        }
                        else if(radTensach.Checked==true)
                        {
                            SqlConnection myConnection = new SqlConnection(conSt);
                            String timkiem = txtTimSach.Text;
                            String sql = String.Format("Select * from Sach where TenSach=N'{0}'", timkiem);
                            SqlDataAdapter da = new SqlDataAdapter(sql, myConnection);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            foreach (DataRow drw in dt.Rows)
                            {
                                txtMaSach.Text = drw["MaSach"].ToString();
                                txtTenSach.Text = drw["TenSach"].ToString();
                                txtSoLuong.Text = drw["SoLuong"].ToString();
                                txtMaTacGia.Text = drw["MaTG"].ToString();
                                txtMaLoai.Text = drw["MaLoaiSach"].ToString();
                            }
                        }
                    }
                }
            }
            else
            {
                btnTimKiem.Text = "Tìm Kiếm";
                txtTimSach.Enabled = true;
                txtTimSach.Text = "";
                txtTimSach.Focus();

                txtMaSach.Text = "";
                txtTenSach.Text = "";
                txtSoLuong.Text = "";
                txtMaTacGia.Text = "";
                txtMaLoai.Text = "";
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Timkiem_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dl = MessageBox.Show("Bạn có muốn đóng chương trình không?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dl == DialogResult.No)
                e.Cancel = true;
        }
    }
}
