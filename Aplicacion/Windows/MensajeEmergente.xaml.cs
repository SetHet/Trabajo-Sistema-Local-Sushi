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

namespace Aplicacion
{
    /// <summary>
    /// Lógica de interacción para MensajeEmergente.xaml
    /// </summary>
    public partial class MensajeEmergente : Window
    {
        public string mensaje = "";
        
        public MensajeEmergente(string msg = "")
        {
            InitializeComponent();
            ActualizarMensaje(msg);
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        public void ActualizarMensaje(string msg = null)
        {
            if (msg != null) mensaje = msg;

            //Actualizar ahora algun bloque de texto
            TxtBlockMensaje.Text = mensaje;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
