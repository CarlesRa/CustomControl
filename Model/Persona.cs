using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Persona
    {
        private string _nombre;
        private string _passWdPrivada;
        private string _passWdPublica;

        public Persona(string nombre, string clave)
        {
            this._nombre = nombre;
            this._passWdPrivada = clave;
            _passWdPublica = crearHash(nombre, clave);
        }


        public string Nombre
        {
            get
            {
                return _nombre;
            }
            set
            {
                _nombre = value;
            }
        }

        public string PassWdPublica
        {
            get
            {
                return _passWdPublica;
            }
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
    }
}
