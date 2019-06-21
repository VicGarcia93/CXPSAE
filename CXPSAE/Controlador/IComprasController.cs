using CXPSAE.Modelo;
using CXPSAE.Vista;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXPSAE.Controlador
{
    public interface IComprasController
    {
        void GetCompras(IProveedoresView view, string cve_1, string cve_2, string cve_3);
        void SetComprasModel(IComprasModel model);
    }
}
