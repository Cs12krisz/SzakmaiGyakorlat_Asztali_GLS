using GLS_CLI;
using GLS_CLI.Models;
using Microsoft.Win32;
using System.IO;
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

        private void dtgTabla_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var elem = dtgTabla.SelectedItem as AutoAdatok;
            if (elem != null) {
                tbxNev.Text = elem.SoforNeve;
                tbxCsomagokSzama.Text = elem.KezbesitettCsomagokSzama.ToString();
                tbxDatum.Text = elem.Datum.ToString();
                tbxKm.Text = elem.NapiKilometer.ToString();
                tbxLiterFogyasztas.Text = elem.NapiFogyasztasLiterben.ToString();
            }


        }

        private void btnModositas_Click(object sender, RoutedEventArgs e)
        {
            if (Validator())
            {
                Program.autoAdatokKollekcio[dtgTabla.SelectedIndex].Modositjuk(new AutoAdatok($"{tbxDatum.Text};{tbxNev.Text};{tbxKm.Text};{tbxCsomagokSzama.Text};{tbxLiterFogyasztas.Text}"));
                dtgTabla.Items.Refresh();
            }
            else
            {
                MessageBox.Show("Már rögzített adatok a kiválasztott dátumra", "", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        private bool Validator()
        {
            DateTime datum;
            int literFogyasztas, kmNapi;
            if (tbxNev.Text == "" || (tbxDatum.Text == "" && !DateTime.TryParse(tbxDatum.Text, out datum)) || (tbxLiterFogyasztas.Text == "" && !int.TryParse(tbxLiterFogyasztas.Text, out literFogyasztas) && literFogyasztas <= 0) || (tbxKm.Text == "" && !int.TryParse(tbxKm.Text, out kmNapi) && kmNapi <= 0))
            {
                MessageBox.Show("Már rözített adatok, vagy hibás adatok");
                return false;
            }
            return true;
        }

        private void btnMentés_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = "gls.txt";
                if (sfd.ShowDialog() == true)
                {
                    StreamWriter sw = new StreamWriter(sfd.FileName);
                    foreach (var item in Program.autoAdatokKollekcio)
                    {
                        sw.WriteLine($"{item.Datum};{item.SoforNeve};{item.NapiKilometer};{item.KezbesitettCsomagokSzama};{item.NapiFogyasztasLiterben}");
                    }
                    sw.Close();
                    MessageBox.Show("Sikeres mentés");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}