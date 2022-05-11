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
            DataTable theTable = new DataTable();

            //set the connection string
            String connString = @"Data Source = localhost; initial Catalog = DataBaseNew ;Integrated Security = True";

            //set the query
            String query = @"SELECT ID, Nama, Berat, Isi, Kadaluarsa FROM Record";
            
            //fill the set with the data
            using (sqlConnection conn = new sqlConnection(connString))
            {
                //passing the query and connection string
                sqlDataAdapter da = new sqlDataAdapter(query, conn);
                da.Fill(theTable);
            }

            //set the data context
            DataContext = theTable;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
