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

namespace Aplicacion
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Start();
        }

        void Start()
        {
            //Configuracion BD
            BD.Configuracion configBD = new BD.Configuracion();
            Utilidades.FileControlDicc file = new Utilidades.FileControlDicc("configuracion_BD.txt");
            if (file.Exist) //call file
            {
                configBD.dicc = file.FileToDicc();
            }
            else //if file dont exist, create file
            {
                file.DiccToFile(configBD.dicc);
            }
            BD.Access.Connect(configBD);
        }
        


        #region Test

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BD.Configuracion configuracion = new BD.Configuracion();
            BD.Access.Connect(configuracion);

            Console.WriteLine(BD.Access.QuerySelect("select * from cliente")[0][0].ToString());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Utilidades.FileControlDicc file = new Utilidades.FileControlDicc("configuration.txt");
            if (!file.Exist) file.DiccToFile(new Dictionary<string, string>() { {"USER ID", "csharp" } });
            file.LookDicc(file.FileToDicc());
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Vendedor ven = new Vendedor();
            ven.Show();
            this.Close();
        }
        #endregion

        private void BtnCocinero_Click(object sender, RoutedEventArgs e)
        {
            if (BD.Access.isOpen)
            {
                Cocinero cocinero = new Cocinero();
                cocinero.Show();
                this.Close();
            }
            else
            {
                MensajeEmergente me = new MensajeEmergente("No se puede acceder al GUI del concinero. \nLa Base de datos no esta abierta, la coneccion a fallado.");
                me.Show();
            }
        }

        private void BtnVendedor_Click(object sender, RoutedEventArgs e)
        {
            if (BD.Access.isOpen)
            {
                Vendedor ven = new Vendedor();
                ven.Show();
                this.Close();
            }
            else
            {
                MensajeEmergente me = new MensajeEmergente("No se puede acceder al GUI del vendedor. \nLa Base de datos no esta abierta, la coneccion a fallado.");
                me.Show();
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            BD.Access.QueryUpdate("UPDATE pedido SET id_estado = 0");
        }
        

        private void Btn_Config(object sender, RoutedEventArgs e)
        {
            GuiConfiguracion config = new GuiConfiguracion();
            config.Show();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Utilidades.FileControlDicc file = new Utilidades.FileControlDicc("x.txt");
            //Txt_Filepath.Text = file.filePath;
        }
    }
}
