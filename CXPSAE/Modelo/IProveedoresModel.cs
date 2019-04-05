using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXPSAE.Modelo
{
    interface IProveedoresModel
    {
        
        List<Proveedor> GetProveedores(string clave,string empresa,string status);
        

    }
}
