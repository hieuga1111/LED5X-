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

namespace LED5X7
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            GolobalValue.led = new character [50];
            GolobalValue.multichar = new character[50];

            for (int i = 0; i < 50; i++)
            {
                GolobalValue.led[i].led = new bool[7, 5];
                GolobalValue.multichar[i].led = new bool[7, 140];

            }
            // LoadLEDStatesFromFile();
            int h = 0;
        }
        private void LoadLEDStatesFromFile()
        {
            try
            {
                int i = 0;
                int j = -1;
                string filePath = "LEDStates.txt";
                using (StreamReader reader = new StreamReader(filePath))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        if (line == "" || line == null) continue;

                        if (i%2 ==0)
                        {
                            j++;
                            GolobalValue.led[j].c = line;
                            GolobalValue.MaxNumCharactoer = j+1;

                        }
                        else
                        {
                            byte[] bytes = new byte[5];

                            for (int k = 0; k < 5; k++)
                            {
                                string[] temp = line.Split(' ');
                                bytes[k] = byte.Parse(temp[k].Substring(2), System.Globalization.NumberStyles.HexNumber);
                                string binaryString = Convert.ToString(bytes[k], 2).PadLeft(8, '0');

                                for (int col = 0; col < 7; col++)
                                {
                                    bool state = binaryString[col] == '1';
                                    GolobalValue.led[j].led[col, k] = state;
                                    
                                    // DrawLED(row, col, state ? Brushes.Red : Brushes.Black);
                                }
                            }
                            
                        }
                         i++;
                    }
                 
                }

                MessageBox.Show("LED states loaded from file: " + filePath, "Load Successful", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (IOException ex)
            {
                MessageBox.Show("Error loading LED states from file: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void LoadMultiLEDStatesFromFile()
        {
            try
            {
                int i = 0;
                int j = -1;
                string filePath = "MultiLEDStates.txt";
                using (StreamReader reader = new StreamReader(filePath))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        if (line == "" || line == null) continue;

                        if (i % 2 == 0)
                        {
                            j++;
                            GolobalValue.multichar[j].c = line;
                            GolobalValue.MaxNumMulChar = j + 1;

                        }
                        else
                        {
                            byte[] bytes = new byte[28];
                            int row = 0;

                            for (int k = 0; k < 28; k++)
                            {
                                string[] temp = line.Split(' ');
                                string data = (k < temp.Length) ? temp[k] : "0x01";
                                bytes[k] = byte.Parse(data.Substring(2), System.Globalization.NumberStyles.HexNumber);
                                string binaryString = Convert.ToString(bytes[k], 2).PadLeft(8, '0');

                                for (int col = 0; col < 7; col++)
                                {
                                    bool state = binaryString[col] == '1';
                                    GolobalValue.multichar[j].led[col, k] = state;

                                    // DrawLED(row, col, state ? Brushes.Red : Brushes.Black);
                                }
                            }

                        }
                        i++;
                    }

                }

                MessageBox.Show("LED states loaded from file: " + filePath, "Load Successful", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (IOException ex)
            {
                MessageBox.Show("Error loading LED states from file: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void openLibrary_Clcik(object sender, RoutedEventArgs e)
        {
            LoadLEDStatesFromFile();
            CreateLibarary a = new CreateLibarary();
            a.ShowDialog();
        }

        private void openMultiChar_Click(object sender, RoutedEventArgs e)
        {
            LoadLEDStatesFromFile();
            LoadMultiLEDStatesFromFile();
            MultiCharacter a = new MultiCharacter();
            a.ShowDialog();
        }
    }
}