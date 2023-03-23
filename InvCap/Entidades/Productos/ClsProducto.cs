using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Productos
{
    public class ClsProducto
    {
        #region Atributos privados
        private int _id;
        private string _descripcion;
        private string _categoria;
        private string _almacen;
        private float _precioCompra;
        private float _predcioVenta;
        private int _stock;

        //Atributos de manejo de la base de datos
        private string _mensajeError, _valorScalar;
        //Guardar: msjError devueltos de la database, _valorScalar en caso de que la database me lo pida.

        private DataTable _dtResultados;
        #endregion

        #region Atributos publicos
        public int Id { get => _id; set => _id = value; }
        public string Descripcion { get => _descripcion; set => _descripcion = value; }
        public string Categoria { get => _categoria; set => _categoria = value; }
        public string Almacen { get => _almacen; set => _almacen = value; }
        public float PrecioCompra { get => _precioCompra; set => _precioCompra = value; }
        public float PredcioVenta { get => _predcioVenta; set => _predcioVenta = value; }
        public int Stock { get => _stock; set => _stock = value; }
        //Guardar: los resultados que me devuelva la database en caso de solicitarlos.
        #endregion
    }
}
