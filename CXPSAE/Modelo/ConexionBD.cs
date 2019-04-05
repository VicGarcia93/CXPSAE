using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirebirdSql.Data.FirebirdClient;

namespace CXPSAE.Modelo
{
    class ConexionBD
    {
        private static ConexionBD conexion;
        private static FbConnection connection1,connection2,connection3;
        private static string bd1,bd2,bd3;


        private ConexionBD()
        {

        }
        public static ConexionBD GetInstance()
        {
            if (conexion == null)
            {
                conexion = new ConexionBD();
                ConectarConBD();
            }
            return conexion;

        }

        private static void ConectarConBD()
        {
            bd1 = CXPSAE.Properties.Settings.Default.Llantas1;
            bd2 = CXPSAE.Properties.Settings.Default.Llantas2;
            bd3 = CXPSAE.Properties.Settings.Default.Llantas3;

            FbConnectionStringBuilder conStrBuil = new FbConnectionStringBuilder();
            conStrBuil.ServerType = FbServerType.Default;
            conStrBuil.Database = bd1;
            conStrBuil.UserID = "SYSDBA";
            conStrBuil.Password = "masterkey";
            connection1 = new FbConnection(conStrBuil.ToString());

            conStrBuil.Database = bd2;
            connection2 = new FbConnection(conStrBuil.ToString());

            conStrBuil.Database = bd3;
            connection3 = new FbConnection(conStrBuil.ToString());
        }


        public FbConnection GetConnection(string empresa)
        {
            
            switch (empresa) {
                case "Matriz":
                    return connection1;
                case "Ejidal":
                    return connection2;
                case "Poza Rica":
                    return connection3;
            }
            return null;
            
        }

    }
}
