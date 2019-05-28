using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CXPSAE.Modelo;
using CXPSAE.Vista;

namespace CXPSAE.Controlador
{
    public interface IProveedoresController
    {
        void GetListaProveedores(IProveedoresView view);
        void GetListaProveedoresConFiltro(IProveedoresView view,String[] param);
        void SetProveedoresModel(IProveedoresModel model);
    }
}
