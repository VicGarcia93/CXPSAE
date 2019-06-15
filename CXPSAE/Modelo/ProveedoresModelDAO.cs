using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirebirdSql.Data.FirebirdClient;

namespace CXPSAE.Modelo
{
    class ProveedoresModelDAO : IProveedoresModel
    { 
        List<Proveedor> proveedores; 
        DataRow  dRProveedor;
        StringBuilder sqlProveedor;
        FbCommand fbCommand;
        FbDataAdapter adapter1,adapter2,adapter3;
        String sqlProveedorCompleta;
        DataTable data1,data2,data3,dataResult;
        
        const string EMPRESA1 = "1";
        const string EMPRESA2 = "2";
        const string EMPRESA3 = "3";    
        
        public ProveedoresModelDAO()
        {
            proveedores = new List<Proveedor>();
            sqlProveedor = new StringBuilder();
            data1 = new DataTable();
            data2 = new DataTable();
            data3 = new DataTable();
            InicializaDataResult();

        }

        private void InicializaDataResult()
        {
            dataResult = new DataTable("proveedores");
            dataResult.Columns.Add("cve_1");
            dataResult.Columns.Add("cve_2");
            dataResult.Columns.Add("cve_3");
            dataResult.Columns.Add("Nombre");
            dataResult.Columns.Add("Estatus");
            dataResult.Columns.Add("Saldo");
            dataResult.Columns.Add("Empresa");

            data1.Clear();
            data2.Clear();
            data3.Clear();
        }

        //Obtener los proveedores filtrado por clave, empresa o estatus
        public DataTable GetProveedores(string clave,string empresa,string status)
        {
            InicializaDataResult();
           
            switch (empresa)
            {
                case EMPRESA1:
                    sqlProveedorCompleta = "SELECT pro.clave, pro.nombre, pro.status, pro.saldo FROM prov03";
                    adapter1 = new FbDataAdapter(GetCommand(clave, empresa, status, sqlProveedorCompleta));
                    adapter1.Fill(data1);
                    Console.WriteLine("Tamaño de data1: " + data1.Rows.Count);
                    for (int i = 0; i < data1.Rows.Count; i++)
                    {
                        //proveedor = new Proveedor(data1.Rows[i][0].ToString(), status, float.Parse(data1.Rows[i][1].ToString()), "Matriz");
                        // proveedores.Add(proveedor);
                        dataResult.Rows.Add(new Object[] { data1.Rows[i][0].ToString(),"","" , data1.Rows[i][1].ToString(), data1.Rows[i][2] ,float.Parse(data1.Rows[i][3].ToString()), "Matriz" });
                    }
                    
                    break;
                case EMPRESA2:
                    sqlProveedorCompleta = "SELECT pro.clave ,pro.nombre, pro.status, pro.saldo FROM prov04";
                    adapter2 = new FbDataAdapter(GetCommand(clave, empresa, status, sqlProveedorCompleta));
                    adapter2.Fill(data2);
                    for (int i = 0; i < data2.Rows.Count; i++)
                    {
                        //proveedor = new Proveedor(data2.Rows[i][0].ToString(), status, float.Parse(data2.Rows[i][1].ToString()), "Ejidal");
                        //proveedores.Add(proveedor);
                        dataResult.Rows.Add(new Object[] { "" ,data2.Rows[i][0].ToString(), "" , data2.Rows[i][1].ToString() ,data2.Rows[i][2].ToString(),float.Parse(data2.Rows[i][3].ToString()), "Ejidal" });
                    }

                    break;
                case EMPRESA3:
                    sqlProveedorCompleta = "SELECT pro.clave, pro.nombre, pro.status, pro.saldo FROM prov05";
                    adapter3 = new FbDataAdapter(GetCommand(clave, empresa, status, sqlProveedorCompleta));
                    adapter3.Fill(data3);
                    for (int i = 0; i < data3.Rows.Count; i++)
                    {
                        // proveedor = new Proveedor(data3.Rows[i][0].ToString(), status, float.Parse(data3.Rows[i][1].ToString()), "Poza Rica");
                        // proveedores.Add(proveedor);
                        dataResult.Rows.Add(new Object[] { "" , "" , data3.Rows[i][0].ToString(), data3.Rows[i][1].ToString(), data3.Rows[i][2].ToString() ,float.Parse(data3.Rows[i][3].ToString()), "Poza Rica" });

                    }
                    break;
               default:
                    sqlProveedorCompleta = "SELECT pro.clave ,pro.nombre, pro.status, pro.saldo FROM prov0";
                    adapter1 = new FbDataAdapter(GetCommand(clave, "1", status, sqlProveedorCompleta + 3));
                    adapter2 = new FbDataAdapter(GetCommand(clave, "2", status, sqlProveedorCompleta + 4));
                    adapter3 = new FbDataAdapter(GetCommand(clave, "3", status, sqlProveedorCompleta + 5));

                    adapter1.Fill(data1);
                    adapter2.Fill(data2);
                    adapter3.Fill(data3);

                    return GetData();
                    

                    
            }
            return dataResult;

        }
        //Obtener todos los proveedores
        public DataTable GetProveedores()
        {
            dataResult.Clear();
            sqlProveedorCompleta = "SELECT pro.clave ,pro.nombre, pro.status, pro.saldo FROM prov0";
            adapter1 = new FbDataAdapter(GetCommand(EMPRESA1, sqlProveedorCompleta + 3));
            adapter2 = new FbDataAdapter(GetCommand(EMPRESA2, sqlProveedorCompleta + 4));
            adapter3 = new FbDataAdapter(GetCommand(EMPRESA3, sqlProveedorCompleta + 5));

            adapter1.Fill(data1);
            adapter2.Fill(data2);
            adapter3.Fill(data3);
            
            return GetData();
        }
        //Obtener la conexión filtrado por clave, empresa y status
        private FbCommand GetCommand(string clave,string empresa,string status,string sql)
        {
            sqlProveedor.Clear();
            sqlProveedor.Append(sql);
            sqlProveedor.Append(" pro WHERE (pro.clave like '%");
            sqlProveedor.Append(clave + "%'");
            sqlProveedor.Append(" OR pro.nombre like '%");
            sqlProveedor.Append(clave + "%')");
            if (!status.Equals("T"))
            {
                sqlProveedor.Append(" AND status = '");
                sqlProveedor.Append(status);
                sqlProveedor.Append("'");
            }
           

            Console.WriteLine(sqlProveedor.ToString());

            fbCommand = new FbCommand(sqlProveedor.ToString(), ConexionBD.GetInstance().GetConnection(empresa));

            return fbCommand;
        }
        //Obtener la conexión
        private FbCommand GetCommand(string empresa, string sql)
        {
            Console.WriteLine("Empresa: " + empresa);
            sqlProveedor.Clear();
            sqlProveedor.Append(sql);
            sqlProveedor.Append(" pro WHERE pro.status = 'A'");
            fbCommand = new FbCommand(sqlProveedor.ToString(), ConexionBD.GetInstance().GetConnection(empresa));
            
            return fbCommand;
        }

        private DataTable GetData()
        {
            for (int i = 0; i < data1.Rows.Count; i++)
            {

                dataResult.Rows.Add(new Object[] { data1.Rows[i][0].ToString(),"","", data1.Rows[i][1].ToString(), data1.Rows[i][2].ToString(), float.Parse(data1.Rows[i][3].ToString()), "M" });
                // proveedor = new Proveedor(data1.Rows[i][0].ToString(), status, float.Parse(data1.Rows[i][1].ToString()), "M");
                // proveedores.Add(proveedor);
            }
            for (int i = 0; i < data2.Rows.Count; i++)
            {
                //if ((proveedor = proveedores.Find(x => x.GetNombre().Equals(data2.Rows[i][0].ToString()))) != null)
                try
                {
                    if ((dRProveedor = (dataResult.Select("Nombre = '" + data2.Rows[i][1].ToString() + "'"))[0]) != null)
                    {
                        //Actualiza el saldo
                        dRProveedor[5] = float.Parse(dRProveedor[5].ToString()) + float.Parse(data2.Rows[0][3].ToString());
                        //proveedor.SetSaldo(proveedor.GetSaldo() + float.Parse(data2.Rows[0][1].ToString()));
                        dRProveedor[6] = "M / E";
                        //proveedor.SetEmpresa("M / E");
                    }
                    else
                    {

                        // proveedor = new Proveedor(data2.Rows[i][0].ToString(), status, float.Parse(data2.Rows[i][1].ToString()), "E");
                        // proveedores.Add(proveedor);
                    }

                }
                catch (Exception e)
                {
                    dataResult.Rows.Add(new Object[] { "",data2.Rows[i][0].ToString(), "" , data2.Rows[i][1].ToString(), data2.Rows[i][2].ToString() ,float.Parse(data2.Rows[i][3].ToString()), "E" });
                }


            }
            for (int i = 0; i < data3.Rows.Count; i++)
            {
                try
                {
                    if ((dRProveedor = dataResult.Select("Nombre ='" + data3.Rows[i][1].ToString() + "'")[0]) != null)
                    {
                        //proveedor.SetSaldo(proveedor.GetSaldo() + float.Parse(data3.Rows[0][1].ToString()));
                        dRProveedor[5] = float.Parse(dRProveedor[5].ToString()) + float.Parse(data3.Rows[0][3].ToString());
                        //proveedor.SetEmpresa(proveedor.GetEmpresa() + " / PR");
                        dRProveedor[6] = dRProveedor[6] + " / PR";
                    }
                    else
                    {
                        // proveedor = new Proveedor(data3.Rows[i][0].ToString(), status, float.Parse(data3.Rows[i][1].ToString()), "PR");
                        // proveedores.Add(proveedor);
                    }
                }
                catch (Exception e)
                {
                    dataResult.Rows.Add(new Object[] { "", "" ,data3.Rows[i][0].ToString(), data3.Rows[i][1], data3.Rows[i][2].ToString(), float.Parse(data3.Rows[i][3].ToString()), "PR" });

                }
                //if ((proveedor = proveedores.Find(x => x.GetNombre().Equals(data3.Rows[i][0].ToString()))) != null)


            }
            return dataResult;
        }
    }
}
