using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client; // version 4.112.2.0, solo funciona en proyecto x64


namespace Aplicacion.BD
{
    /// <summary>
    /// Debes tener en las referencias Oracle.DataAccess 4.112.2.0 y el proyecto en modo x64
    /// </summary>
    public static class Access
    {
        /*string oradb =  "Data Source=(DESCRIPTION="
                        + "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=ORASRVR)(PORT=1521)))"
                        + "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=ORCL)));"
                        + "User Id=csharp;Password=1234;"; //statement de coneccion
        */
        static string oradb = "DATA SOURCE = xe; PASSWORD=123; USER ID = csharp;";
        static OracleConnection conn;
        public static Configuracion config;

        public static bool Connect(Configuracion config = null)
        {
            Access.config = config;
            if (config != null) oradb = config.Generate();

            try
            {
                Console.WriteLine("Conectando con BD");
                conn = new OracleConnection(oradb);
                if (!isOpen) conn.Open();
                Console.WriteLine("Coneccion exitosa con BD");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ha fallado la coneccion");
                Console.WriteLine("Error:" + ex.HResult);
                Console.WriteLine(ex.Message);
                return false;
            }

            return true;
        }
        

        public static void CloseConnection()
        {
            conn.Close();
        }

        public static bool isOpen => conn != null && conn.State == System.Data.ConnectionState.Open;

        #region Query's
        #region Query Select

        /// <summary>
        /// No puedes usar ; al final de la sentencia
        /// </summary>
        /// <param name="query"></param>
        public static List<object[]> QuerySelect(string query)
        {
            List<object[]> salida= new List<object[]>();

            try
            {
                OracleCommand command = new OracleCommand(query, conn);

                OracleDataReader lector = command.ExecuteReader();

                while (lector.Read())
                {
                    object[] values = new object[lector.FieldCount]; //cantidad de columnas
                    lector.GetValues(values);
                    salida.Add(values);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ha fallado la consulta");
                Console.WriteLine("Error:" + ex.HResult);
                Console.WriteLine(ex.Message);
                
            }

            return salida;
        }
        #endregion
        #region Query Insert
        public static void QueryInsert(string query)
        {
            try
            {
                Console.WriteLine("Insertando: " + query);
                OracleCommand command = new OracleCommand(query, conn);
                command.ExecuteNonQuery();
                Console.WriteLine("Insercion correcta");
            }
            catch (Exception ex)
            {
                Console.WriteLine("La insercion a fallado.");
            }
        }

        public static void QueryInsert(string table, string values, string columns = null)
        {
            string query = string.Concat("INSERT INTO ", table);
            if (columns != null)
            {
                columns = columns.Replace("(", "").Replace(")", "");
                columns = "(" + columns + ")";
                query += " " + columns;
            }
            query += " VALUES (" + values.Replace("(", "").Replace(")", "") + ")";

            try
            {
                Console.WriteLine("Insertando: " + query);
                OracleCommand command = new OracleCommand(query, conn);
                command.ExecuteNonQuery();
                Console.WriteLine("Insercion correcta");
            }
            catch (Exception ex)
            {
                Console.WriteLine("La insercion a fallado.");
            }
        }
        #endregion
        #region Query Update
        public static void QueryUpdate(string query)
        {
            try
            {
                Console.WriteLine("Update: " + query);
                OracleCommand command = new OracleCommand(query, conn);
                command.ExecuteNonQuery();
                Console.WriteLine("Update correcta");
            }
            catch (Exception ex)
            {
                Console.WriteLine("El update a fallado.");
            }
        }
        #endregion
        #endregion
    }
}
