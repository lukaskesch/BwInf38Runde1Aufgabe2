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

namespace BwInfAufgabe2
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

        private void ButtonRechnen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string Eingabe = TextboxEingabe.Text;
                int[] Zahl = new int[Eingabe.Length];

                for (int i = 0; i < Zahl.Length; i++)
                {
                    Zahl[i] = int.Parse(Eingabe[i].ToString());
                }


            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
