using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXPSAE.Modelo
{
    class ComprasModelDAO:IComprasModel
    {
        Compras movimiento;
        List<Compras> listaCompras;
        DataTable data1, data2, data3;
        StringBuilder sqlCompras;
        String sqlComprasDetalle;
        DataRow movAux;
        FbCommand fbCommandAux;
        FbDataAdapter fbDataAdapterAux;
        DataTable dataAux;
        String saldoCompra;

        FbCommand fbCommand;
        FbDataAdapter comprasAdapter1;
        FbDataAdapter comprasAdapter2;
        FbDataAdapter comprasAdapter3;
        public ComprasModelDAO()
        {
            data1 = new DataTable();
            data2 = new DataTable();
            data3 = new DataTable(); 
            

            sqlCompras = new StringBuilder();
            sqlCompras.Append("select pag.no_factura,pag.num_cpto,pag.docto,pag.fecha_apli, pag.importe, from paga_m0  ");
        }

        private void ReiniciaValores()
        {
            data1.Clear();
            data2.Clear();
            data2.Clear();
        }

        public List<Compras> GetCompraPorProveedor(string ClaveProveedor,string empresa)
        {
            listaCompras = new List<Compras>();
            
            comprasAdapter1 = new FbDataAdapter(GetCommand(empresa, ClaveProveedor, sqlCompras.ToString()+3));
            comprasAdapter2 = new FbDataAdapter(GetCommand(empresa, ClaveProveedor, sqlCompras.ToString() + 4));
            comprasAdapter3 = new FbDataAdapter(GetCommand(empresa, ClaveProveedor, sqlCompras.ToString() + 5));
            //Factura(0),num_concepto(1),documento(2),fecha_apli(3),importe(4)
            comprasAdapter1.Fill(data1);
            comprasAdapter2.Fill(data2);
            comprasAdapter3.Fill(data3);

            GetComprasMovimientos(data1, "Matriz");
            GetComprasMovimientos(data2, "Ejidal");
            GetComprasMovimientos(data3, "Poza Rica");
            //movimiento = new Compras("factura","concepto","documento","fechaApli",500,500,"empresa");

           return listaCompras;
            
        }

        private void GetComprasMovimientos(DataTable datos, String empresa)
        {
            String tablaBD = "";
            String dataBase = "";
            switch (empresa)
            {
                case "Matriz":
                    tablaBD = "paga_det03";
                    dataBase = "1";
                    break;
                case "Ejidal":
                    tablaBD = "paga_det04";
                    dataBase = "2";
                    break;
                case "Poza Rica":
                    tablaBD = "paga_det05";
                    dataBase = "3";
                    break;

            }
            for (int i = 0; i < datos.Rows.Count; i++)
            {
                movAux = datos.Rows[i];
                sqlComprasDetalle = "SELECT SUM(DET.IMPORTE) FROM " + tablaBD + " DET WHERE DET.refer = '" + movAux[2] +
                    "'";
                fbCommandAux = new FbCommand(sqlComprasDetalle, ConexionBD.GetInstance().GetConnection(dataBase));
                fbDataAdapterAux = new FbDataAdapter(fbCommand);
                dataAux = new DataTable();
                fbDataAdapterAux.Fill(dataAux);
                saldoCompra = dataAux.Rows[0][0].ToString() ?? "";

                if (!saldoCompra.Equals("0"))
                {
                    movimiento = new Compras(movAux[0].ToString(), movAux[1].ToString(), movAux[2].ToString(),
                            movAux[3].ToString(), float.Parse(movAux[4].ToString()), float.Parse(saldoCompra), empresa);

                    listaCompras.Add(movimiento);
                }
                
            }
           
        }

        private FbCommand GetCommand(string empresa, string claveProveedor, string sql)
        {
            //Obtener la conexión
            Console.WriteLine("Empresa: " + empresa);
            sqlCompras.Append(sql);
            sqlCompras.Append(" pag where pag.cve_prov = '");
            sqlCompras.Append(claveProveedor);
            sqlCompras.Append("'");
            fbCommand = new FbCommand(sqlCompras.ToString(), ConexionBD.GetInstance().GetConnection(empresa));

            return fbCommand;
            
        }
    }
}
