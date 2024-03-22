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

namespace LED5X7
{
    /// <summary>
    /// Interaction logic for MultiCharacter.xaml
    /// </summary>
    public partial class MultiCharacter : Window
    {
        private Multi Multi;

        public MultiCharacter()
        {
            InitializeComponent();
            GolobalValue.index = 0;
            Character.Text = GolobalValue.multichar[0].c;
            this.Width = 30 * Character.Text.Length * 5;

            Multi = new Multi(Character.Text.Length * 5, Character.Text);
            m_UserControl.Content = Multi;
        }
        private void SaveLEDStates_Click(object sender, RoutedEventArgs e)
        {
            Multi.SaveLEDStates(Character.Text);
        }

        private void LoadPre_Click(object sender, RoutedEventArgs e)
        {
            GolobalValue.index--;
            if (GolobalValue.index < 0)
            {
                GolobalValue.index = GolobalValue.MaxNumMulChar - 1;
            }
           
            Character.Text = GolobalValue.multichar[GolobalValue.index].c;
            this.Width = 30 * Character.Text.Length * 5;

            Multi = new Multi(Character.Text.Length * 5, Character.Text);
            m_UserControl.Content = Multi;

        }

        private void LoadNext_Click(object sender, RoutedEventArgs e)
        {

            GolobalValue.index++;
            if (GolobalValue.index == GolobalValue.MaxNumMulChar)
            {
                GolobalValue.index = 0;
            }
           
            Character.Text = GolobalValue.multichar[GolobalValue.index].c;
            this.Width = 30 * Character.Text.Length * 5;

            Multi = new Multi(Character.Text.Length * 5, Character.Text);
            m_UserControl.Content = Multi;
        }

      
        private void Character_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            int len = Character.Text.Length * 5;
            if(len == 0) len = 5;
            this.Width = 30 * Character.Text.Length * 5;

            Multi = new Multi(len, Character.Text);
            m_UserControl.Content = Multi;
        }
    }
}
