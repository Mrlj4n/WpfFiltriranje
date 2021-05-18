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
using System.Windows.Shapes;

namespace WpfFiltriranje
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private decimal minCena = 0;
        private decimal maxCena = 0;

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            
            decimal min = 0;
            decimal max = 0;
            if (string.IsNullOrWhiteSpace(TextBoxMin.Text))
            {
                min = minCena;
                TextBoxMin.Text = min.ToString();
            }
            else
            {
                if (!decimal.TryParse(TextBoxMin.Text,out min))
                {
                    MessageBox.Show("Unesite min cenu");
                    TextBoxMin.Clear();
                    return;
                }
            }

            if (string.IsNullOrWhiteSpace(TextBoxMax.Text))
            {
                max = maxCena;
                TextBoxMax.Text = max.ToString();
            }
            else
            {
                if (!decimal.TryParse(TextBoxMax.Text, out max))
                {
                    MessageBox.Show("Unesite max cenu");
                    TextBoxMax.Clear();
                    return;
                }
            }

            if (min > max)
            {
                MessageBox.Show("Maksimalna cena mora biti veca od minimalne");
                TextBoxMin.Clear();
                TextBoxMax.Clear();
                return;
            }


            DataGrid1.ItemsSource = ProizvodDal.PrikaziProizvode(min, max);
           
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            minCena = ProizvodDal.NadjiMinimum();
            maxCena = ProizvodDal.NadjiMinimum(0);
        }
    }
}
