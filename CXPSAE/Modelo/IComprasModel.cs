using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXPSAE.Modelo
{
    interface IComprasModel
    {
        Compras GetCompraPorProveedor(String ClaveProveedor);
    }
}
