using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CXPSAE.Controlador;
using CXPSAE.Modelo;

namespace CXPSAE.Vista
{
    public interface IProveedoresView
    {
        void doConsultaProveedores();
        void DoConsultaCompras();
        void SetListaProveedores(DataTable proveedores);
        void SetListaCompras(List<Compras> compras);
        void SetProveedoresController(IProveedoresController controller);
        void SetComprasController(IComprasController controllerCompras);
    }
}
