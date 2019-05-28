using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CXPSAE.Modelo;
using CXPSAE.Vista;

namespace CXPSAE.Controlador
{
    class ComprasControllerImp : IComprasController
    {
        private IComprasModel modelCompras;

        public ComprasControllerImp(IComprasModel model)
        {
            this.modelCompras = model;
        }
        public void GetCompras(IProveedoresView view)
        {
            
        }

        public void SetComprasModel(IComprasModel model)
        {
            this.modelCompras = model;
        }
    }
}
