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
    /// Lógica de interacción para GuiConfiguracion.xaml
    /// </summary>
    public partial class GuiConfiguracion : Window
    {
        BD.Configuracion config;
        Utilidades.FileControlDicc control;

        public GuiConfiguracion()
        {
            InitializeComponent();
            Start();
        }

        void Start()
        {
            config = new BD.Configuracion();
            control = new Utilidades.FileControlDicc("configuracion_BD.txt");
            config.dicc = control.FileToDicc();

            TxtBox_DataSource.Text = config.dicc["DATA SOURCE"];
            TxtBox_UserID.Text = config.dicc["USER ID"];
            TxtBox_Password.Text = config.dicc["PASSWORD"];
        }

        #region Botones
        private void Btn_Save_Click(object sender, RoutedEventArgs e)
        {
            config.dicc["DATA SOURCE"] = TxtBox_DataSource.Text;
            config.dicc["USER ID"] = TxtBox_UserID.Text;
            config.dicc["PASSWORD"] = TxtBox_Password.Text;

            control.DiccToFile(config.dicc);

            BD.Access.CloseConnection();
            BD.Access.Connect(config);

            this.Close();
        }

        private void Btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
