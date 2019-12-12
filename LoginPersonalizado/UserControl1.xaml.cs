using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

namespace LoginPersonalizado
{
    public class RoutedLoginEventArgs : RoutedEventArgs
    {
        private string user;
        private LoginResult resultadoLogin;

        public RoutedLoginEventArgs(RoutedEvent routed,string user, LoginResult resultadoLogin) : base(routed)
        {
            this.user = user;
            this.resultadoLogin = resultadoLogin;
        }

        public string User
        {
            get
            {
                return user;
            }
        }

        public LoginResult ResultadoLogin
        {
            get
            {
                return resultadoLogin;
            }
        }
    }
    #region Tipos
    public enum LoginResult
    {
        OK,
        Fail,
        Canceled
    }
    #endregion
    /// <summary>
    /// Lógica de interacción para UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        private ArrayList personas;
        public UserControl1()
        {
            InitializeComponent();
            personas = new ArrayList();
            personas.Add(new Persona("Juan", "0000"));
            personas.Add(new Persona("Paco", "1111"));
            personas.Add(new Persona("Luis", "2222"));
            personas.Add(new Persona("Maria", "3333"));
            personas.Add(new Persona("Marta", "4444"));
        }

        #region MetodoValidar
        public void validadr()
        {
            bool validado = false;
            string user = tbLogin.Text;
            string clave = tbPassword.Text;
            string clavePublica = crearHash(user, clave);

            foreach (Persona p in personas)
            {
                if (p.PassWdPublica == clavePublica)
                {
                    validado = true;
                    RaiseLoginEvent(user, LoginResult.OK);  
                }
            }
            if (!validado)
            {
                RaiseLoginEvent(user, LoginResult.Fail);
            }
        }
        #endregion

        #region CreoRoutedEvent
        public static readonly
        RoutedEvent EsLoginCorrectoEvent
        = EventManager.RegisterRoutedEvent(
            "EsLoginCorrecto",
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(UserControl1)
            );
        public event RoutedEventHandler EsLoginCorrecto
        {
            add
            {
                AddHandler(EsLoginCorrectoEvent, value);
            }
            remove
            {
                RemoveHandler(EsLoginCorrectoEvent, value);
            }
        }
        void RaiseLoginEvent(string user, LoginResult loginR)
        {
            RoutedLoginEventArgs loginArgs = new RoutedLoginEventArgs(EsLoginCorrectoEvent, user, loginR);
            RaiseEvent(loginArgs);
        }

        public string crearHash(string nombre, string clave)
        {
            byte[] buffer;
            string resultado;
            StringBuilder nombreMasClave = new StringBuilder();
            nombreMasClave.Append(nombre).Append(clave);

            using (SHA1Managed sha1 = new SHA1Managed())
            {
                buffer = sha1.ComputeHash(Encoding.UTF8.GetBytes(nombreMasClave.ToString()));

            }
            resultado = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
            return resultado;
        }
        #endregion

        private void Login_click(object sender, RoutedEventArgs e)
        {
            validadr();
            btCancel.Visibility = Visibility.Visible;
            btLogin.Visibility = Visibility.Collapsed;
        }

        private void cancel_click(object sender, RoutedEventArgs e)
        {
            string user = tbLogin.Text;
            RaiseLoginEvent(user, LoginResult.Canceled);
        }
    }
}
