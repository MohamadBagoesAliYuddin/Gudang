using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;

namespace Gudang
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            loadGrid();
        }

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-KS47LAS;Initial Catalog=GudangDB;Integrated Security=True");


        //koneksi ke database untuk menampilkan tabel
        private void loadGrid()
        {
            SqlCommand cmd = new SqlCommand("select * from Tbl_Gudang", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            Datagrid.ItemsSource = dt.DefaultView;
        }

        private void myDatagrid_Selectionchanged(object sender, SelectionChangedEventArgs e)
        {   

        }


        //message box yang akan tampil bila belum mengisi kolom yang tersedia
        public bool Valid()
        {
            if (nama_br_txtbx.Text == string.Empty)
            {
                MessageBox.Show("Name item is Requaired", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (berat_br_txtbx.Text == string.Empty)
            {
                MessageBox.Show("Weight item is Requaired", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (isi_br_txtbx.Text == string.Empty)
            {
                MessageBox.Show("Total item is Requaired", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (tempat_br_txtbx.Text == string.Empty)
            {
                MessageBox.Show("Location item is Requaired", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (kadaluarsa_date.SelectedDate == DateTime.Today)
            {
                MessageBox.Show("Expired Date item is Requaired", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }


        //add button untuk memasukan data
        private void add_btn_click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Valid())
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Tbl_Gudang VALUES (@NamaBarang, @Berat, @Isi, @Tempat, @Kadaluarsa)", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@NamaBarang", nama_br_txtbx.Text);
                    cmd.Parameters.AddWithValue("@Berat", berat_br_txtbx.Text);
                    cmd.Parameters.AddWithValue("@Isi", isi_br_txtbx.Text);
                    cmd.Parameters.AddWithValue("@Tempat", tempat_br_txtbx.Text);
                    cmd.Parameters.AddWithValue("@Kadaluarsa", kadaluarsa_date.SelectedDate);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    loadGrid();
                    MessageBox.Show("Successfully add Item", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
                    cleardata();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        //bagian tombol untuk mengupdate data yang telah dimasukan ke dalam tabel
        private void update_btn_click(object sender, RoutedEventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("update Tbl_Gudang Set NamaBarang = '" + nama_br_txtbx.Text + "', Berat = '" + berat_br_txtbx.Text + "', Isi = '" + isi_br_txtbx.Text + "', Tempat = '" + tempat_br_txtbx + "', Kadaluarsa = '" + kadaluarsa_date.SelectedDate + "' where Id = '" + search_id.Text + "' ", con);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data has been updated successfully", "Updated", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
                cleardata();
                loadGrid();
            }

        }


        //bagian tombol untuk menghapus data yang ada didalam tabel database
        private void delete_btn_click(object sender, RoutedEventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from Tbl_Gudang where No = " + search_id.Text + " ", con);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data has been deleted successfully", "Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                con.Close();
                cleardata();
                loadGrid();
                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Not Deleted" + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }


        //bagian clear data untuk menghapus atau mereset kolom input
        private void cleardata()
        {
            nama_br_txtbx.Clear();
            isi_br_txtbx.Clear();
            berat_br_txtbx.Clear();
            search_id.Clear();
            tempat_br_txtbx.Clear();
            kadaluarsa_date.SelectedDate = null;
        }

        private void clear_btn_click(object sender, RoutedEventArgs e)
        {
            cleardata();
        }
    }
}
