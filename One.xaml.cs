﻿using System;
using System.Collections.Generic;
using System.IO;
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

namespace LED5X7
{
    /// <summary>
    /// Interaction logic for One.xaml
    /// </summary>
    public partial class One : UserControl
    {
        private bool[,] ledState = new bool[GolobalValue.row, GolobalValue.col]; // State of LEDs

        //public One()
        //{
        //    InitializeComponent();

        //    for (int row = 0; row < GolobalValue.row; row++)
        //    {
        //        for (int col = 0; col < GolobalValue.col; col++)
        //        {
        //            ledState[row, col] = false; // Initially all LEDs are off
        //            DrawLED(row, col, Brushes.Black); // Draw LED
        //        }
        //    }
        //}

        public One()
        {
            ledState = GolobalValue.led[GolobalValue.index].led;
         
            InitializeComponent();
          //  ledGrid.Children.Clear();

            for (int row = 0; row < GolobalValue.row; row++)
            {
                for (int col = 0; col < GolobalValue.col; col++)
                {
                    // ledState[row, col] = false; // Initially all LEDs are off
                    if (ledState[row, col])
                    {
                    DrawLED(row, col, Brushes.Red); // Draw LED

                    }
                    else
                    {
                        DrawLED(row, col, Brushes.Black); // Draw LED
                    }



                }
            }
        }

        private void DrawLED(int row, int col, Brush color)
        {
            Rectangle led = new Rectangle
            {
                Fill = color,
                Width = 30,
                Height = 30,
                Stroke = Brushes.Gray, // Define the stroke color
                StrokeThickness = 1   // Define the stroke thickness

            };
            Grid.SetRow(led, row);
            Grid.SetColumn(led, col);
            led.MouseLeftButtonDown += (sender, e) => ChangeLEDColor(sender as Rectangle);
            ledGrid.Children.Add(led);
        }

        private void ChangeLEDColor(Rectangle led)
        {
            int row = Grid.GetRow(led);
            int col = Grid.GetColumn(led);
            led.Fill = led.Fill == Brushes.Black ? Brushes.Red : Brushes.Black;
            ledState[row, col] = led.Fill == Brushes.Red ? true : false;
        }
        public void SetLED(int row, int col, bool state)
        {
            if (row >= 0 && row < GolobalValue.row && col >= 0 && col < GolobalValue.col)
            {
                ledState[row, col] = state;
                DrawLED(row, col, state ? Brushes.Red : Brushes.Black);
            }
        }

        public bool GetLED(int row, int col)
        {
            if (row >= 0 && row < GolobalValue.row && col >= 0 && col < GolobalValue.col)
            {
                return ledState[row, col];
            }
            return false;
        }


        public void SaveLEDStates( string _character)
        {
            StringBuilder hexCode = new StringBuilder();
            hexCode.AppendLine(_character);
            character temp = GolobalValue.led.FirstOrDefault(s => s.c == _character);
            if (!temp.Equals(default(character)))
            {

                for (int col = 0; col < GolobalValue.col; col++)
                {
                    for (int row = 0; row < GolobalValue.row; row++)
                    {
                        bool state = ledState[row, col];
                        temp.led[row, col] = state;
                    }

                }

            }

            if (_character != null )
            {
                try
                {
                    string filePath = "LEDStates.txt";
                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        for(int i = 0; i < GolobalValue.MaxNumCharactoer;  i++)
                        {
                            writer.WriteLine(GolobalValue.led[i].c);

                            for (int col = 0; col < GolobalValue.col; col++)
                            {
                                StringBuilder columnCode = new StringBuilder();
                                for (int row = 0; row < GolobalValue.row; row++)
                                {
                                    columnCode.Append(GolobalValue.led[i].led[row, col] ? "1" : "0");
                                }

                                columnCode.Append("1");
                                byte resultByte = Convert.ToByte(columnCode.ToString(), 2);
                                if (col == 0)
                                {
                                    writer.Write("0x" + resultByte.ToString("X2"));
                                }
                                else
                                {
                                    writer.Write(" 0x" + resultByte.ToString("X2"));
                                }
                            }
                            writer.WriteLine();
                        }
                        MessageBox.Show(hexCode.ToString(), "Codes");

                    }
                }
                catch (IOException ex)
                {
                    MessageBox.Show("Error saving LED states to file: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            
        }



    }
}
