using LoginPersonalizado;
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

namespace CustomLogin
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void UserControl1_EsLoginCorrecto(object sender, RoutedEventArgs e)
        {
            RoutedLoginEventArgs loginArgs = e as RoutedLoginEventArgs;
            if (loginArgs != null)
            {
                if (loginArgs.ResultadoLogin.Equals(LoginResult.OK)) 
                {
                    MessageBox.Show("Login: " + loginArgs.ResultadoLogin
                        + " UserName: " + loginArgs.User);
                }
                else if (loginArgs.ResultadoLogin.Equals(LoginResult.Fail))
                {
                    MessageBox.Show("Login: " + loginArgs.ResultadoLogin
                       + " UserName: " + loginArgs.User);
                }
            }
        }
    }
}
