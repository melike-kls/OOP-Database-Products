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

namespace Demo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //List<Turler> lst = new List<Turler>();
            //var snf = new Turler();

            //snf.TurAdi = "Plastik";
            //snf.TurId = 1;

            //lst.Add(snf);
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            SqlConnection cn = null;
            try
            {
                cn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Demo.Db;Integrated Security=true;");
                cn.Open();
                SqlCommand cmd = new SqlCommand($"Insert into tblUrunler (UrunAdi,UrunSayisi,UrunFiyat,TurId) values('{txtAd.Text}','{txtSayi.Text}','{txtFiyat.Text}',{cmbTur.SelectedValue})", cn);
                //cmd.ExecuteNonQuery();
                 
                int sonuc = cmd.ExecuteNonQuery();
                MessageBox.Show(sonuc > 0 ? "İşlem Başarılı " : "Başarısız");
                cn.Close();
            }
            catch (Exception)
            {                
                throw;
            }
            finally
            { 
                if (cn != null && cn.State != ConnectionState.Closed)//Null Check
                {
                    cn.Close();
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SqlConnection cn = null;
            try
            {
                cn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Demo.Db;Integrated Security=true;");
                cn.Open();
                SqlCommand cmd = new SqlCommand($"Select TurId,TurAdi from tblTurler", cn);
                //cmd.ExecuteNonQuery();


                SqlDataReader dr = cmd.ExecuteReader();

                var lst = new List<Turler>();

                while (dr.Read())
                {
                    var snf = new Turler();
                
                    snf.TurAdi = dr["TurAdi"].ToString();
                    snf.TurId = Convert.ToInt32(dr["TurId"]);
                    lst.Add(snf);
                }

                dr.Close();
                cmbTur.DisplayMember = "TurAdi";
                cmbTur.ValueMember = "TurId";//Burası hatalı idi Melike
                cmbTur.DataSource = lst;
            }
            catch (SqlException)
            {
                MessageBox.Show("Bu numara Kayıtlı");
            }

            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (cn != null && cn.State != ConnectionState.Closed)//Null Check
                {
                    cn.Close();
                }
            }
           
        }
        private void cmbTur_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
