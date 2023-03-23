using System;
using System.Data;
using System.Data.SqlClient;

namespace AccesoDatos.DataBase
{
    public class ClsDataBase
    {

        #region Variables privadas

        private SqlConnection _objSqlConnection;
        private SqlDataAdapter _objsqlDataAdapter;
        private SqlCommand _objsqlCommand;
        private DataSet _dsResultados;
        private DataTable _dtParametros;
        private string _nombreTabla, _nombreSP, _mensajeErrorDB, _valorScalar, _nombreDB; //ValorScalar es para cuando quiera que me retorne algo
        private bool _Scalar;

        #endregion

        #region Variables publicas

        public SqlConnection ObjSqlConnection { get => _objSqlConnection; set => _objSqlConnection = value; }
        public SqlDataAdapter ObjsqlDataAdapter { get => _objsqlDataAdapter; set => _objsqlDataAdapter = value; }
        public SqlCommand ObjsqlCommand { get => _objsqlCommand; set => _objsqlCommand = value; }
        public DataSet DsResultados { get => _dsResultados; set => _dsResultados = value; }
        public DataTable DtParametros { get => _dtParametros; set => _dtParametros = value; }
        public string NombreTabla { get => _nombreTabla; set => _nombreTabla = value; }
        public string NombreSP { get => _nombreSP; set => _nombreSP = value; }
        public string MensajeErrorDB { get => _mensajeErrorDB; set => _mensajeErrorDB = value; }
        public string ValorScalar { get => _valorScalar; set => _valorScalar = value; }
        public string NombreDB { get => _nombreDB; set => _nombreDB = value; }
        public bool Scalar { get => _Scalar; set => _Scalar = value; }


        #endregion

        #region Constructores
         public ClsDataBase()
        {
            DtParametros = new DataTable("SpParametros");
            DtParametros.Columns.Add("Nombre");
            DtParametros.Columns.Add("TipoDato");
            DtParametros.Columns.Add("Valor");

            NombreDB = "InventarioIlde";
        }

        #endregion

        #region Metodos privados

        private void CrearConexionBaseDatos(ref ClsDataBase ObjDataBase)
        {
            switch (ObjDataBase.NombreDB)
            {
                case "InventarioIlde":
                    ObjDataBase.ObjSqlConnection = new SqlConnection(Properties.Settings.Default.cadenaConexion_InventarioIlde); 
                    //Inicialización y conxion a database
                    break;
                default:
                    break;                                                
            }
        }

        private void ValidarConexionBaseDatos(ref ClsDataBase ObjDataBase)
        {
            if(ObjDataBase.ObjSqlConnection.State == ConnectionState.Closed) //Si la conexion esta cerrada
            {
                ObjDataBase.ObjSqlConnection.Open();//La abre
            }
            else //De lo contrario
            {
                ObjDataBase.ObjSqlConnection.Close(); //Lo cierra
                ObjDataBase.ObjSqlConnection.Dispose(); //Lo quita de memoria
            }
        }

        private void AgregarParametros(ref ClsDataBase ObjDataBase)
        {

            if (ObjDataBase.DtParametros != null)
            {
                SqlDbType TipoDatoSQL = new SqlDbType();//Recorre la tabla y le asina el tipo segun

                foreach (DataRow item in ObjDataBase.DtParametros.Rows)
                {
                    switch (item[1])
                    {
                        case "1":
                            TipoDatoSQL = SqlDbType.Bit;//Bit es un booleano
                            break;
                        case "2":
                            TipoDatoSQL = SqlDbType.TinyInt;// 1 byte
                            break;
                        case "3":
                            TipoDatoSQL = SqlDbType.SmallInt;// 2 byte
                            break;
                        case "4":
                            TipoDatoSQL = SqlDbType.Int;// 4 byte
                            break;
                        case "5":
                            TipoDatoSQL = SqlDbType.BigInt;// 8 byte
                            break;
                        case "6":
                            TipoDatoSQL = SqlDbType.Decimal;//
                            break;
                        case "7":
                            TipoDatoSQL = SqlDbType.SmallMoney;//
                            break;
                        case "8":
                            TipoDatoSQL = SqlDbType.Money;//
                            break;
                        case "9":
                            TipoDatoSQL = SqlDbType.Float;//
                            break;
                        case "10":
                            TipoDatoSQL = SqlDbType.Real;//
                            break;
                        case "11":
                            TipoDatoSQL = SqlDbType.Date;//
                            break;
                        case "12":
                            TipoDatoSQL = SqlDbType.Time;//
                            break;
                        case "13":
                            TipoDatoSQL = SqlDbType.SmallDateTime;//
                            break;
                        case "14":
                            TipoDatoSQL = SqlDbType.Date;//
                            break;
                        case "15":
                            TipoDatoSQL = SqlDbType.Char;//
                            break;
                        case "16":
                            TipoDatoSQL = SqlDbType.NChar;//
                            break;
                        case "17":
                            TipoDatoSQL = SqlDbType.VarChar;//
                            break;
                        case "18":
                            TipoDatoSQL = SqlDbType.NVarChar;//
                            break;
                        default:
                            break;
                    }

                    if (ObjDataBase.Scalar)//Scalar Booleano
                    {
                        if (item[2].ToString().Equals(string.Empty))
                        {
                          ObjDataBase.ObjsqlCommand.Parameters.Add(item[0].ToString(), TipoDatoSQL).Value = DBNull.Value;
                        }
                        else
                        {
                            ObjDataBase.ObjsqlCommand.Parameters.Add(item[0].ToString(), TipoDatoSQL).Value = item[2].ToString();
                        }
                    }
                    else
                    {
                        if (item[2].ToString().Equals(string.Empty))
                        {
                            ObjDataBase.ObjsqlDataAdapter.SelectCommand.Parameters.Add(item[0].ToString(), TipoDatoSQL).Value = DBNull.Value;
                        }
                        else
                        {
                            ObjDataBase.ObjsqlDataAdapter.SelectCommand.Parameters.Add(item[0].ToString(), TipoDatoSQL).Value = item[2].ToString();
                        }
                    }
                }

            }

        }

        private void PrepararConexionBaseDatos(ref ClsDataBase ObjDataBase)
        {
            CrearConexionBaseDatos(ref ObjDataBase);
            ValidarConexionBaseDatos(ref ObjDataBase);
        }

        private void EjecutarDataAdapter(ref ClsDataBase ObjDataBase)
        {
            try
            {
                PrepararConexionBaseDatos(ref ObjDataBase);

                ObjDataBase.ObjsqlDataAdapter = new SqlDataAdapter(ObjDataBase.NombreSP, ObjDataBase.ObjSqlConnection);
                ObjDataBase.ObjsqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                AgregarParametros(ref ObjDataBase);
                ObjDataBase.DsResultados = new DataSet();
                ObjDataBase.ObjsqlDataAdapter.Fill(ObjDataBase.DsResultados, ObjDataBase.NombreTabla);


            }
            catch (Exception ex)
            {
                ObjDataBase.MensajeErrorDB = ex.Message.ToString();
            }
            finally
            {
                if (ObjDataBase.ObjSqlConnection.State == ConnectionState.Open)
                {
                    ValidarConexionBaseDatos(ref ObjDataBase);
                }
            }
        }

        private void EjecutarCommand(ref ClsDataBase ObjDataBase)
        {
            try
            {
                PrepararConexionBaseDatos(ref ObjDataBase);
                ObjDataBase.ObjsqlCommand = new SqlCommand(ObjDataBase.NombreSP, ObjDataBase.ObjSqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                AgregarParametros(ref ObjDataBase);

                if (ObjDataBase.Scalar)
                {
                    ObjDataBase.ValorScalar = ObjDataBase.ObjsqlCommand.ExecuteScalar().ToString().Trim();//Quita cualquier espacio en balnco Trim();
                }
                else
                {
                    ObjDataBase.ObjsqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                ObjDataBase.MensajeErrorDB = ex.Message.ToString();
            }
            finally
            {
                ValidarConexionBaseDatos(ref ObjDataBase);
            }
        }


        #endregion

        #region Metodos publicos
        public void CRUD(ref ClsDataBase ObjDataBase)
        {
            if (ObjDataBase.Scalar)
            {
                EjecutarCommand(ref ObjDataBase);
            }
            else
            {
                EjecutarDataAdapter(ref ObjDataBase);
            }
        }

        #endregion

    }
}
