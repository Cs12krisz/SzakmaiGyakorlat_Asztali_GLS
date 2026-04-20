using GLS_CLI;
using GLS_CLI.Models;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GLS_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Program.Beolvasas();
            dtgTabla.ItemsSource = Program.autoAdatokKollekcio;
        }

        private void btnFelvitel_Click(object sender, RoutedEventArgs e)
        {
            int csomagokSzama, napiKilometer, napiFogyasztasLiterben;
            DateTime datum;
            if (!int.TryParse(tbxKm.Text, out napiKilometer) || !int.TryParse(tbxCsomagokSzama.Text, out csomagokSzama) || !int.TryParse(tbxLiterFogyasztas.Text, out napiFogyasztasLiterben) || !DateTime.TryParse(tbxDatum.Text, out datum))
            {
                MessageBox.Show("Töltse ki jól a mezőket!","Hiba");
                return;
            }

            if (Program.autoAdatokKollekcio.Exists(a => a.Datum == datum))
            {
                MessageBox.Show("Már rögzítettek adatot a kiválasztott dátumra!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            AutoAdatok ujAutoAdat = new AutoAdatok(
                   datum,
                   tbxNev.Text,
                   napiKilometer,
                   csomagokSzama,
                   napiFogyasztasLiterben
             );
            Program.autoAdatokKollekcio.Add(ujAutoAdat);
            dtgTabla.Items.Refresh();

        }
    }
}