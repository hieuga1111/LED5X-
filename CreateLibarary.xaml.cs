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
    /// Interaction logic for CreateLibarary.xaml
    /// </summary>
    public partial class CreateLibarary : Window
    {
        private One Led;

        public CreateLibarary()
        {
            InitializeComponent();
            GolobalValue.index = 0;
              Led = new One();
            m_UserControl.Content = Led;

            Character.Text = GolobalValue.led[0].c;
            
        }

        private void SaveLEDStates_Click(object sender, RoutedEventArgs e)
        {
            Led.SaveLEDStates(Character.Text);
        }

        private void LoadPre_Click(object sender, RoutedEventArgs e)
        {
            GolobalValue.index--;
            if (GolobalValue.index < 0 )
            {
                GolobalValue.index = GolobalValue.MaxNumCharactoer-1;
            }
            Led = new One();
            m_UserControl.Content = Led;

            //  Led = new One();
            Character.Text = GolobalValue.led[GolobalValue.index].c;

        }

        private void LoadNext_Click(object sender, RoutedEventArgs e)
        {

            GolobalValue.index++;
            if (GolobalValue.index == GolobalValue.MaxNumCharactoer)
            {
                GolobalValue.index = 0;
            }
            Led = new One();
            m_UserControl.Content = Led;

            // Led = new One();
            Character.Text = GolobalValue.led[GolobalValue.index].c;

        }
    }
}
