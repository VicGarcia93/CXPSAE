using System;
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
        List<Proveedor> listaProveedores;
        Proveedor proveedor;
        ConexionBD conexion;
        StringBuilder sqlProveedor;
        FbCommand fbCommand;
        FbDataAdapter adapter1,adapter2,adapter3;
        String sqlProveedorCompleta;
        DataTable data1,data2,data3;
        
        public List<Proveedor> GetProveedores(string clave,string empresa,string status)
        {
            sqlProveedor = new StringBuilder();
            List<Proveedor> proveedores = new List<Proveedor>();

            switch (empresa)
            {
                case "Matriz":
                    sqlProveedorCompleta = "SELECT pro.nombre, pro.saldo FROM prov03";
                    adapter1 = new FbDataAdapter(GetCommand(clave, empresa, status, sqlProveedorCompleta));
                    adapter1.Fill(data1);
                    for (int i = 0; i < data1.Rows.Count; i++)
                    {
                        proveedor = new Proveedor(data1.Rows[i][0].ToString(), status, float.Parse(data1.Rows[i][1].ToString()), "Matriz");
                        proveedores.Add(proveedor);
                    }
                    
                    break;
                case "Ejidal":
                    sqlProveedorCompleta = "SELECT pro.nombre, pro.saldo FROM prov04";
                    adapter2 = new FbDataAdapter(GetCommand(clave, empresa, status, sqlProveedorCompleta));
                    adapter2.Fill(data2);
                    for (int i = 0; i < data2.Rows.Count; i++)
                    {
                        proveedor = new Proveedor(data2.Rows[i][0].ToString(), status, float.Parse(data2.Rows[i][1].ToString()), "Ejidal");
                        proveedores.Add(proveedor);
                    }

                    break;
                case "Poza Rica":
                    sqlProveedorCompleta = "SELECT pro.nombre, pro.saldo FROM prov05";
                    adapter3 = new FbDataAdapter(GetCommand(clave, empresa, status, sqlProveedorCompleta));
                    adapter3.Fill(data3);
                    for (int i = 0; i < data3.Rows.Count; i++)
                    {
                        proveedor = new Proveedor(data3.Rows[i][0].ToString(), status, float.Parse(data3.Rows[i][1].ToString()), "Poza Rica");
                        proveedores.Add(proveedor);
                    }
                    break;
                case "Todas":
                    sqlProveedorCompleta = "SELECT pro.nombre, pro.saldo FROM prov0";
                    adapter1 = new FbDataAdapter(GetCommand(clave, empresa, status, sqlProveedorCompleta + 1));
                    adapter2 = new FbDataAdapter(GetCommand(clave, empresa, status, sqlProveedorCompleta + 2));
                    adapter3 = new FbDataAdapter(GetCommand(clave, empresa, status, sqlProveedorCompleta + 3));

                    adapter1.Fill(data1);
                    adapter2.Fill(data2);
                    adapter3.Fill(data3);


                    for (int i = 0; i < data1.Rows.Count; i++)
                    {
                        proveedor = new Proveedor(data1.Rows[i][0].ToString(), status, float.Parse(data1.Rows[i][1].ToString()), "M");
                        proveedores.Add(proveedor);
                    }
                    for (int i = 0; i < data2.Rows.Count; i++)
                    {
                        if ((proveedor = proveedores.Find(x => x.GetNombre().Equals(data2.Rows[i][0].ToString()))) != null)
                        {
                            proveedor.SetSaldo(proveedor.GetSaldo() + float.Parse(data2.Rows[0][1].ToString()));
                            proveedor.SetEmpresa("M / E");
                        }
                        else
                        {
                            proveedor = new Proveedor(data2.Rows[i][0].ToString(), status, float.Parse(data2.Rows[i][1].ToString()), "E");
                            proveedores.Add(proveedor);
                        }
                        
                    }
                    for (int i = 0; i < data3.Rows.Count; i++)
                    {
                        if ((proveedor = proveedores.Find(x => x.GetNombre().Equals(data3.Rows[i][0].ToString()))) != null)
                        {
                            proveedor.SetSaldo(proveedor.GetSaldo() + float.Parse(data3.Rows[0][1].ToString()));
                            proveedor.SetEmpresa(proveedor.GetEmpresa() + " / PR");
                        }
                        else
                        {
                            proveedor = new Proveedor(data3.Rows[i][0].ToString(), status, float.Parse(data3.Rows[i][1].ToString()), "PR");
                            proveedores.Add(proveedor);
                        }

                    }

                    break;
            }
            return proveedores;

        }
        private FbCommand GetCommand(string clave,string empresa,string status,string sql)
        {
            sqlProveedor.Append(empresa);
            sqlProveedor.Append(" WHERE pro.clave like %");
            sqlProveedor.Append(clave + "%");
            sqlProveedor.Append(" OR pro.nombre like %");
            sqlProveedor.Append(clave + "%");
            sqlProveedor.Append(" AND status = ");
            sqlProveedor.Append(status);

            fbCommand = new FbCommand(sqlProveedor.ToString(), ConexionBD.GetInstance().GetConnection(empresa));

            return fbCommand;
        }
    }
}
