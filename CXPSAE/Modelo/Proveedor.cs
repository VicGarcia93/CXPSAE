using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXPSAE.Modelo
{
    class Proveedor
    {
        private string clave;
        private string estatus;
        private string nombre;
        private float saldo;
        private string empresa;

        public Proveedor(string nombre, string status, float saldo,string empresa)
        {
     
            this.estatus = status;
            this.nombre = nombre;
            this.saldo = saldo;
            this.empresa = empresa;
        }

        public void SetClave(string clave)
        {
            throw(new InvalidOperationException ());
        }
       
        public void SetEstatus(string estatus)
        {
            this.estatus = estatus;
        }
        public String GetEstatus()
        {
            return estatus;
        }
        public void SetNombre(string nombre)
        {
            this.nombre = nombre;
        }
        public String GetNombre()
        {
            return nombre;
        }
        public void SetSaldo(float saldo)
        {
            this.saldo = saldo;
        }
        public float GetSaldo()
        {
            return saldo;
        }
        public void SetEmpresa(string empresa)
        {
            this.empresa = empresa;
        }
        public string GetEmpresa()
        {
            return empresa;
        }

    }
}
