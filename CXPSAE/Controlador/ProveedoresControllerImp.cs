using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CXPSAE.Modelo;
using CXPSAE.Vista;

namespace CXPSAE.Controlador
{
    class ProveedoresControllerImp : IProveedoresController
    {
        private IProveedoresModel model;

        public ProveedoresControllerImp(IProveedoresModel model)
        {
            this.model = model;
        }

        public void GetListaProveedores(IProveedoresView view)
        {
            DataTable proveedores = model.GetProveedores();
            view.SetListaProveedores(proveedores);
            Console.WriteLine("Controlador: Tamaño de lista " + proveedores.Rows.Count);
        }

       
        public void GetListaProveedoresConFiltro(IProveedoresView view, string[] param)
        {
          
            DataTable proveedores = model.GetProveedores(param[0], param[1], param[2]);
            view.SetListaProveedores(proveedores);
            Console.WriteLine("Controlador: Tamaño de lista " + proveedores.Rows.Count);
        }

        public void SetProveedoresModel(IProveedoresModel model)
        {
            this.model = model;
        }
    }
}
