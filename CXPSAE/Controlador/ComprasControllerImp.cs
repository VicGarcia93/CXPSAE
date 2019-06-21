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
        public void GetCompras(IProveedoresView view, string cve_1, string cve_2, string cve_3)
        {

            view.SetListaCompras(modelCompras.GetCompraPorProveedor(cve_1, cve_2, cve_3));
        }

        public void SetComprasModel(IComprasModel model)
        {
            this.modelCompras = model;
        }
    }
}
