using System.Data;

namespace Entidades.Usuarios //Esta capa seria un reflejo de la database.
{
    public class ClsUsuario
    {
        #region Atributos privados
        private int _idUsuario;
        private string _nombreCompleto;
        private string _nombreUsuario;
        private string _clave;
        private int _idPermisos;
        private bool _estado;

        //Atributos de manejo de la base de datos
        private string _mensajeError, _valorScalar;
        //Guardar: msjError devueltos de la database, _valorScalar en caso de que la database me lo pida.
        
        private DataTable _dtResultados;
        //Guardar: los resultados que me devuelva la database en caso de solicitarlos.

        #endregion

        #region Atributos publicos
        public int IdUsuario { get => _idUsuario; set => _idUsuario = value; }
        public string NombreCompleto { get => _nombreCompleto; set => _nombreCompleto = value; }
        public string NombreUsuario { get => _nombreUsuario; set => _nombreUsuario = value; }
        public string Clave { get => _clave; set => _clave = value; }
        public int IdPermisos { get => _idPermisos; set => _idPermisos = value; }
        public bool Estado { get => _estado; set => _estado = value; }
        public string MensajeError { get => _mensajeError; set => _mensajeError = value; }
        public string ValorScalar { get => _valorScalar; set => _valorScalar = value; }
        public DataTable DtResultados { get => _dtResultados; set => _dtResultados = value; }
        #endregion

    }
}
