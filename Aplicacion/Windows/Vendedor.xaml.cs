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
    /// Lógica de interacción para Vendedor.xaml
    /// </summary>
    public partial class Vendedor : Window
    {
        //Variables
        List<Registro_ProductoPedido> list_ProductoPedidos;


        //Metodos

        public Vendedor()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Limpiar();
        }



        void Limpiar()
        {
            list_ProductoPedidos = new List<Registro_ProductoPedido>();
            Txt_RutCliente.Text = "";
            Txt_NombreCliente.Text = "";
            Txt_ApellidoCliente.Text = "";
            Txt_EspecificacionPedido.Text = "";
            Txt_Telefono.Text = "";
            Txt_direccion.Text = "";
            ListBox_Pedidos.Items.Clear();
        }

        public void Agregar(Registro_ProductoPedido rpp, int index = -1)
        {
            if (index < 0)
            {
                list_ProductoPedidos.Add(rpp);
                ListBox_Pedidos.Items.Add(rpp.nombre_producto + " Cant: " + rpp.cantidad + " Cost: " + rpp.cantidad * rpp.costo);
            }
            else
            {
                list_ProductoPedidos[index] = rpp;
                ListBox_Pedidos.Items[index] = rpp.nombre_producto + " Cant: " + rpp.cantidad + " Cost: " + rpp.cantidad * rpp.costo;
            }
        }

        #region Buttons
        private void Btn_BuscarRut_Click(object sender, RoutedEventArgs e)
        {
            string consulta = "Select nombre, apellido from cliente where rut = '" + Txt_RutCliente.Text + "'";
            List<object[]> query = BD.Access.QuerySelect(consulta);
            if (query.Count > 0)
            {
                Txt_NombreCliente.Text = Convert.ToString(query[0][0]);
                Txt_ApellidoCliente.Text = Convert.ToString(query[0][1]);
            }
        }
        
        private void Btn_Salir_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        private void Btn_Enviar_Click(object sender, RoutedEventArgs e)
        {
            if (Txt_RutCliente.Text == "") return;
            //preparativos
            int telefono = 0;
            int total = 0;
            int.TryParse(Txt_Telefono.Text, out telefono);
            foreach (var item in list_ProductoPedidos) total += item.cantidad * item.costo;


            //
            List<object[]> query;
            string consulta;
            //Existe cliente ??
            consulta = "Select nombre, apellido from cliente where rut = '" + Txt_RutCliente.Text + "'";
            query = BD.Access.QuerySelect(consulta);
            if (query.Count > 0) //Existe
            {
                consulta = "Update cliente set nombre = '" + Txt_NombreCliente.Text + "' , apellido = '" + Txt_ApellidoCliente.Text + "' where rut = '" + Txt_RutCliente.Text + "'";
                BD.Access.QueryUpdate(consulta);
            }
            else
            {
                consulta = "Insert into cliente values ('"+Txt_RutCliente.Text+"', '"+Txt_NombreCliente.Text+"', '" + Txt_ApellidoCliente.Text+"', 0, '')";
                BD.Access.QueryInsert(consulta);
            }

            //Crear Pedido
            consulta = "Insert into pedido Values (seq_nroPedido.nextval, '" + Txt_direccion.Text +
                "' , " +  telefono +
                ", " + total +
                ", '" + Txt_EspecificacionPedido.Text +
                "', sysdate, 0, '" + Txt_RutCliente.Text +
                "')";
            BD.Access.QueryInsert(consulta);

            //Crear pedido_producto
            foreach (var pp in list_ProductoPedidos)
            {
                consulta = "INSERT INTO producto_pedido VALUES (" +
                    "seq_idProductoPedido.nextval, " + pp.cantidad + ", '" + pp.descripcion + "', seq_nroPedido.currval," + pp.id_producto
                    +")";

                BD.Access.QueryInsert(consulta);
            }

            //listo
            Limpiar();
        }

        private void Btn_Limpiar_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void Btn_Editar_Click(object sender, RoutedEventArgs e)
        {
            if (ListBox_Pedidos.SelectedIndex < 0) return;
            Pedido newPedido = new Pedido(this,list_ProductoPedidos[ListBox_Pedidos.SelectedIndex],ListBox_Pedidos.SelectedIndex);
            newPedido.Show();
            this.Hide();
        }

        private void Btn_Quitar_Click(object sender, RoutedEventArgs e)
        {
            int index = ListBox_Pedidos.SelectedIndex;
            if (index == -1) return;
            ListBox_Pedidos.Items.RemoveAt(index);
            list_ProductoPedidos.RemoveAt(index);
        }

        private void Btn_Agregar_Click(object sender, RoutedEventArgs e)
        {
            Pedido newPedido = new Pedido(this);
            newPedido.Show();
            this.Hide();
        }
        #endregion
    }
}
