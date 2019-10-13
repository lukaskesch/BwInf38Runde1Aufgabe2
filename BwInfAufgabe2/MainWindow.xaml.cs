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
                string Input = TextboxEingabe.Text;
                int[] DigitsLeft = new int[Input.Length * 2];
                int[] DigitsChosen = new int[Input.Length * 2];

                DeclareArrays(DigitsLeft, DigitsChosen, Input);

                Recursion(DigitsLeft, DigitsChosen, 0);

            }
            catch (Exception)
            {

                throw;
            }

        }
        private void DeclareArrays(int[] DigitsLeft, int[] DigitsChoose, string Input)
        {
            for (int i = 0; i < Input.Length; i++)
            {
                DigitsLeft[i] = int.Parse(Input[i].ToString());
            }

            for (int i = Input.Length; i < DigitsLeft.Length; i++)
            {
                DigitsLeft[i] = -2;
            }

            for (int i = 0; i < DigitsChoose.Length; i++)
            {
                DigitsChoose[i] = -2;
            }
        }
        private void Recursion(int[] DigitsLeft, int[] DigitsChosen, int NumberOfSteps)
        {
            //Wenn keine Zahlen mehr übrig sind
            if (DigitsLeft[0] == -2)
            {
                CheckResult(DigitsChosen, true);
            }
            //Wenn nur noch eine oder 3 Zahlen vorhanden sind
            else if ((DigitsLeft[1] == -1) || (DigitsLeft[3] == -1 && DigitsLeft[2] != -1))
            {
                return;
            }
            else
            {
                //Choose - Gehe um Schritt nach vorne
                GeheSchritte(ref DigitsLeft, ref DigitsChosen, NumberOfSteps);

                //Check - Schaue ob Zwischenergebnis ein unnötiger Pfad ist
                CheckResult(DigitsChosen, false);

                //Explore - Probiere alle möglichen kommenden Schritte aus
                for (int i = 2; i <= 4; i++)
                {
                    Recursion(DigitsLeft, DigitsChosen, i);
                }

                //Un-choose - Gehe diesen Schritt wieder zurück
                GeheSchritteZurueck(ref DigitsLeft, ref DigitsChosen, NumberOfSteps);
            }
        }

        private bool CheckResult(int[] DigitsChosen, bool Finished)
        {
            return true;
        }
        private int GetFirstFreeIndex(int[] Array)
        {
            for (int i = 0; i < Array.Length; i++)
            {
                if (Array[i] == -2)
                {
                    return i;
                }
            }
            return -1;
        }
        private void GeheSchritte(ref int[] DigitsLeft, ref int[] DigitsChosen, int NumberOfSteps)
        {
            int Index = GetFirstFreeIndex(DigitsChosen);

            //Übertrage die Zahlen von "Zahlen" nach "Choosen"
            if (NumberOfSteps >= 2)
            {
                DigitsChosen[Index] = DigitsLeft[0];
                DigitsChosen[Index + 1] = DigitsLeft[1];
            }
            else if (NumberOfSteps >= 3)
            {
                DigitsChosen[Index + 2] = DigitsLeft[2];
            }
            else if (NumberOfSteps == 4)
            {
                DigitsChosen[Index + 3] = DigitsLeft[3];
            }

            //Ziehe Zahl um Schritte nach vorne

        }
        private void GeheSchritteZurueck(ref int[] DigitsLeft, ref int[] DigitsChosen, int NumberOfSteps)
        {
            //Zahlenarray um Schritte nach hinten verschieben

            //Ende von Chosen in Zahlenarray einfügen
        }
    }
}
