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
    /// Lógica de interacción para Cocinero.xaml
    /// </summary>
    public partial class Cocinero : Window
    {
        //List<object[]> pedidos;
        int pedidoSeleccionado = 0;
        List<PedidoPendiente> pedidos;


        public Cocinero()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Pedido_DataGrid_Tabla.IsReadOnly = true;
            Update();
        }

        void Update()
        {
            string consulta = "select c.rut, c.nombre || ' ' || c.apellido, p.nro_pedido" +
                    " from cliente c join pedido p on(c.rut = p.rut_cliente)" +
                    " where p.id_estado = 0";
            List<object[]> query = BD.Access.QuerySelect(consulta);
            pedidos = new List<PedidoPendiente>(query.Count);
            Console.WriteLine(query.Count);
            foreach (var item in query)
            {
                PedidoPendiente aux = new PedidoPendiente();
                aux.rutCliente = Convert.ToString(item[0]);
                aux.nombreCompleto = Convert.ToString(item[1]);
                aux.nroPedido = Convert.ToInt32(item[2]);
                aux.BuscarProductos();
                pedidos.Add(aux);
            }
            //mover a la izquierda el selector
            MoverIzquierdaSeleccion();
            //Desplegar datos
            if (pedidos.Count > 0)
            {
                Pedido_Lb_Rut.Content = pedidos[pedidoSeleccionado].rutCliente;
                Pedido_Lb_Nombre.Content = pedidos[pedidoSeleccionado].nombreCompleto;
                Pedido_DataGrid_Tabla.ItemsSource = pedidos[pedidoSeleccionado].pp;
            }
        }

        void MoverIzquierdaSeleccion()
        {
            pedidoSeleccionado--;
            ActualizarPaginaPedido();
        }

        void MoverDerechaSeleccion()
        {
            pedidoSeleccionado++;
            ActualizarPaginaPedido();
        }

        void ActualizarPaginaPedido()
        {
            //Correccion de pagina
            if (pedidos.Count <= pedidoSeleccionado)
                pedidoSeleccionado = pedidos.Count - 1;
            if (pedidoSeleccionado < 0) pedidoSeleccionado = 0;
            //numeros
            Pedido_Lb_Nro_Actual.Content = (pedidoSeleccionado + 1) + "/" + pedidos.Count;
            Pedido_Lb_Nro_Anterior.Content = "+" + pedidoSeleccionado;
            Pedido_Lb_Nro_Siguiente.Content = "+" + (pedidos.Count - (pedidoSeleccionado + 1));
            //botones abilitados
            if (pedidoSeleccionado + 1 == pedidos.Count)
                Btn_Siguiente.IsEnabled = false;
            else Btn_Siguiente.IsEnabled = true;
            if (pedidoSeleccionado == 0)
                Btn_Anterior.IsEnabled = false;
            else Btn_Anterior.IsEnabled = true;
            //visibilidad
            if (pedidos.Count > 0)
            {
                Grid_Pedido.Visibility = Visibility.Visible;
                Lb_NoPedidos.Visibility = Visibility.Hidden;
            }
            else
            {
                Grid_Pedido.Visibility = Visibility.Hidden;
                Lb_NoPedidos.Visibility = Visibility.Visible;
            }
            if (pedidos.Count > 0)
            {
                Pedido_Lb_Rut.Content = pedidos[pedidoSeleccionado].rutCliente;
                Pedido_Lb_Nombre.Content = pedidos[pedidoSeleccionado].nombreCompleto;
                Pedido_DataGrid_Tabla.ItemsSource = pedidos[pedidoSeleccionado].pp;
            }
        }

        public class Producto_Pedido
        {
            public int id { get; set; }
            public string nombre { get; set; }
            public int cantidad { get; set; }
            public string descripcion { get; set; }
        }

        public class PedidoPendiente
        {
            public List<Producto_Pedido> pp;

            public string rutCliente;
            public string nombreCompleto;
            public int nroPedido;
            
            public void BuscarProductos()
            {
                string consulta = "select p.id_producto, p.nombre_producto, r.cantidad, r.descripcion" +
                    " from producto p join producto_pedido r on(p.id_producto = r.id_producto)" +
                    " where r.nro_pedido = " + nroPedido;

                List<object[]> pp_objects = BD.Access.QuerySelect(consulta);
                pp = new List<Producto_Pedido>(pp_objects.Count);

                foreach (object[] item in pp_objects)
                {
                    Producto_Pedido p = new Producto_Pedido();
                    p.id = Convert.ToInt32(item[0]);
                    p.nombre = Convert.ToString(item[1]);
                    p.cantidad = Convert.ToInt32(item[2]);
                    p.descripcion = Convert.ToString(item[3]);

                    pp.Add(p);
                }
            }

            public void PedidoListo()
            {
                string peticion = "UPDATE pedido SET id_estado = 1 WHERE nro_pedido = " + nroPedido;

                BD.Access.QueryUpdate(peticion);
            }
        }


        #region Revisar
        /*
        void ActualizarPedidos()
        {

            pedidos = BD.Access.QuerySelect("select * from producto_pedido");
            pp = new List<Producto_Pedido>();

            Console.WriteLine("Rows: " + pedidos.Count);

            foreach (object[] item in pedidos)
            {
                Console.WriteLine("Columns: " + item.Length);
                Producto_Pedido p = new Producto_Pedido();
                p.id = Convert.ToInt32(item[0]);
                p.cantidad = Convert.ToInt32(item[1]);
                p.descripcion = Convert.ToString(item[2]);

                pp.Add(p);
            }

            Pedido_DataGrid_Tabla.ItemsSource = pp;
            Pedido_DataGrid_Tabla.UpdateLayout();
        }
        */
        #endregion

        private void Pedido_Btn_Listo_Click(object sender, RoutedEventArgs e)
        {
            if (pedidos.Count > 0) pedidos[pedidoSeleccionado].PedidoListo();
            Update();
        }

        private void Btn_Siguiente_Click(object sender, RoutedEventArgs e)
        {
            MoverDerechaSeleccion();
        }

        private void Btn_Anterior_Click(object sender, RoutedEventArgs e)
        {
            MoverIzquierdaSeleccion();
        }

        private void Btn_Volver_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        private void Btn_Actualizar_Click(object sender, RoutedEventArgs e)
        {
            Update();
        }
    }
}
