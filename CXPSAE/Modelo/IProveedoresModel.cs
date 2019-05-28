using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXPSAE.Modelo
{
    public interface IProveedoresModel
    {
        
        DataTable GetProveedores(string clave,string empresa,string status);
        DataTable GetProveedores();
        

    }
}
