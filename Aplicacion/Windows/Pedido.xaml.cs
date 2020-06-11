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
    /// Lógica de interacción para pedido.xaml
    /// </summary>
    public partial class Pedido : Window
    {
        Registro_ProductoPedido rpp;
        int indexPedidos = -1;
        List<Producto> lista_productos;
        Vendedor ven;
        int id_producto_temp = -1;

        public Pedido(Vendedor ven,Registro_ProductoPedido orpp = null, int indexPedidos = -1)
        {
            InitializeComponent();
            this.ven = ven;
            this.indexPedidos = indexPedidos;
            if (orpp != null)
            {
                this.rpp = new Registro_ProductoPedido(orpp);
                id_producto_temp = orpp.id_producto;
            }
            else
            {
                this.rpp = new Registro_ProductoPedido();
            }

            Start();
        }

        void Start()
        {
            //Buscar productos
            string consulta = 
                "select p.id_producto, p.nombre_producto, p.costo, p.descripcion, t.nombre_tipo " +
                "from producto p join tipo_producto t on (p.id_tipo = t.id_tipo_producto) ";
            lista_productos = new List<Producto>();
            List<object[]> query = BD.Access.QuerySelect(consulta);

            foreach (var item in query)
            {
                Producto prod = new Producto();
                prod.id_producto = Convert.ToInt32(item[0]);
                prod.nombre_producto = Convert.ToString(item[1]);
                prod.costo = Convert.ToInt32(item[2]);
                prod.descripcion = Convert.ToString(item[3]);
                prod.tipo = Convert.ToString(item[4]);

                lista_productos.Add(prod);
                ListBox_Productos.Items.Add(prod.nombre_producto + ", $" + prod.costo + ", Desc: " + prod.descripcion);
            }

            ListBox_Productos.SelectedIndex = 0;
            if (id_producto_temp >= 0)
            {
                for (int i = 0; i < lista_productos.Count; i++)
                {
                    if (id_producto_temp == lista_productos[i].id_producto) ListBox_Productos.SelectedIndex = i;
                }
            }
            Txt_Descripcion.Text = rpp.descripcion;
            SetCantidad(0);
        }

        void SetCantidad(int c)
        {
            rpp.cantidad += c;
            if (rpp.cantidad < 1) rpp.cantidad = 1;
            Txt_Cantidad.Text = rpp.cantidad.ToString();
        }

        private void Btn_CantidadMas_Click(object sender, RoutedEventArgs e)
        {
            SetCantidad(1);
        }

        private void Btn_CantidadMenos_Click(object sender, RoutedEventArgs e)
        {
            SetCantidad(-1);
        }

        private void Btn_Guardar_Click(object sender, RoutedEventArgs e)
        {
            rpp.id_producto_pedido = 0;
            //cantidad
            rpp.descripcion = Txt_Descripcion.Text;
            rpp.id_producto = lista_productos[ListBox_Productos.SelectedIndex].id_producto;
            rpp.nombre_producto = lista_productos[ListBox_Productos.SelectedIndex].nombre_producto;
            rpp.costo = lista_productos[ListBox_Productos.SelectedIndex].costo;
            ven.Agregar(rpp, indexPedidos);
            this.Close();
        }

        private void Btn_Salir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            ven.Show();
        }
    }
}
