using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CXPSAE.Controlador;
using CXPSAE.Modelo;
using CXPSAE.Vista;

namespace CXPSAE.Main
{
    class ModelViewController
    {
        public static void Main(String[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            IProveedoresModel model = new ProveedoresModelDAO();
            IComprasModel modelCompras = new ComprasModelDAO();
            IProveedoresController controller = new ProveedoresControllerImp(model);
            IComprasController controllerCompras = new ComprasControllerImp(modelCompras);
            IProveedoresView view = new Form1(controller);
            view.SetComprasController(controllerCompras);
           

            Application.Run((Form1)view);
        }
    }
}
