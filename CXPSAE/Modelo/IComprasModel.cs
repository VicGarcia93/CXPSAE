using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXPSAE.Modelo
{
    public interface IComprasModel
    {
        List<Compras> GetCompraPorProveedor(String ClaveProveedor,String empresa);
    }
}
