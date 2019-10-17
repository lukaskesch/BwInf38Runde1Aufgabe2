﻿using System;
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
        int BestNumberOfZeros = 10000000;
        private void ButtonRechnen_Click(object sender, RoutedEventArgs e)
        {
            try
            {




            }
            catch (Exception)
            {

                throw;
            }
            string Input = TextboxEingabe.Text;
            int[] DigitsLeft = new int[Input.Length * 2];
            int[] DigitsChosen = new int[Input.Length * 2];
            DeclareArrays(DigitsLeft, DigitsChosen, Input);

            Recursion(DigitsLeft, DigitsChosen, 0);
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
            //Wenn nur noch eine Zahlen vorhanden ist
            else if (DigitsLeft[1] == -2)
            {
                return;
            }
            else
            {
                //Choose - Gehe um Schritt nach vorne
                GeheSchritte(ref DigitsLeft, ref DigitsChosen, NumberOfSteps);

                //Check - Schaue ob Zwischenergebnis ein unnötiger Pfad ist
                if (!CheckResult(DigitsChosen, false))
                {
                    return;
                }

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
            if (GetNumberOfZeros(DigitsChosen) > BestNumberOfZeros)
            {
                return false;
            }
            if (Finished)
            {
                int NumberOfZeros = GetNumberOfZeros(DigitsChosen);
                if (NumberOfZeros < BestNumberOfZeros)
                {
                    BestNumberOfZeros = NumberOfZeros;
                    SetAusgabe(DigitsChosen, BestNumberOfZeros);
                }
            }
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
        private int GetNumberOfZeros(int[] DigitsChosen)
        {
            int NumberOfZeros = 0;
            for (int i = 0; i < DigitsChosen.Length - 1; i++)
            {
                if (DigitsChosen[i] == -1 && DigitsChosen[i + 1] == 0)
                {
                    NumberOfZeros++;
                }
            }
            return NumberOfZeros;
        }
        private void SetAusgabe(int[] DigitsChosen, int BestNumberOfZeros)
        {
            string Ausgabe = string.Empty;
            for (int i = 0; i < DigitsChosen.Length; i++)
            {
                if (DigitsChosen[i] == -2)
                {
                    break;
                }
                if (DigitsChosen[i] == -1)
                {
                    Ausgabe += "-";
                    continue;
                }
                Ausgabe += DigitsChosen[i];
            }
            TextboxAusgabe.Text = Ausgabe;
            MessageBox.Show(BestNumberOfZeros.ToString() + " Zahlenbloecke beginnen mit einer Null");
        }
        private void GeheSchritte(ref int[] DigitsLeft, ref int[] DigitsChosen, int NumberOfSteps)
        {
            int Index = GetFirstFreeIndex(DigitsChosen);

            //Übertrage die Zahlen von "Zahlen" nach "Chosen"
            if (NumberOfSteps == 0)
            {
                return;
            }
            else if (NumberOfSteps >= 2)
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
            for (int i = 0; i < DigitsLeft.Length - NumberOfSteps; i++)
            {
                DigitsLeft[i] = DigitsLeft[i + NumberOfSteps];
            }

            //Lösche Ende von Zahlen
            Index = GetFirstFreeIndex(DigitsLeft) - 1;
            if (Index < 0)
            {
                return;
            }
            for (int i = Index; i > Index - NumberOfSteps; i--)
            {
                DigitsLeft[i] = -2;
            }

        }
        private void GeheSchritteZurueck(ref int[] DigitsLeft, ref int[] DigitsChosen, int NumberOfSteps)
        {
            //Zahlenarray um Schritte nach hinten verschieben
            for (int i = DigitsLeft.Length - 1; i >= 0 + NumberOfSteps; i--)
            {
                DigitsLeft[i] = DigitsLeft[i - NumberOfSteps];
            }

            //Ende von Chosen in Zahlenarray einfügen
            int Index = GetFirstFreeIndex(DigitsChosen);
            if (NumberOfSteps == 0) { return; }
            if (NumberOfSteps == 4)
            {
                DigitsLeft[3] = DigitsChosen[Index - 1];
                DigitsLeft[2] = DigitsChosen[Index - 2];
                DigitsLeft[1] = DigitsChosen[Index - 3];
                DigitsLeft[0] = DigitsChosen[Index - 4];

                DigitsChosen[Index - 4] = -2;
                DigitsChosen[Index - 3] = -2;
            }
            else if (NumberOfSteps == 3)
            {
                DigitsLeft[2] = DigitsChosen[Index - 1];
                DigitsLeft[1] = DigitsChosen[Index - 2];
                DigitsLeft[0] = DigitsChosen[Index - 3];

                DigitsChosen[Index - 3] = -2;
            }
            else if (NumberOfSteps == 2)
            {
                if (Index - 2 < 0) { return; }

                DigitsLeft[1] = DigitsChosen[Index - 1];
                DigitsLeft[0] = DigitsChosen[Index - 2];

            }
            DigitsChosen[Index - 1] = -2;
            DigitsChosen[Index - 2] = -2;
        }
    }
}
