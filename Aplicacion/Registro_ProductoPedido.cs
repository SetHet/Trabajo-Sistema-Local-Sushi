using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion
{
    public class Registro_ProductoPedido
    {
        public int id_producto_pedido;
        public int cantidad = 1;
        public string descripcion;
        public int id_producto;
        public string nombre_producto;
        public int costo = 3;

        public Registro_ProductoPedido()
        {
            id_producto_pedido = 0;
            cantidad = 1;
            descripcion = "";
            id_producto = 0;
            nombre_producto = "";
            costo = 0;
        }

        public Registro_ProductoPedido(Registro_ProductoPedido other)
        {
            id_producto_pedido = other.id_producto_pedido;
            cantidad = other.cantidad;
            descripcion = other.descripcion;
            id_producto = other.id_producto;
            nombre_producto = other.nombre_producto;
            costo = other.costo;
        }
    }
}
