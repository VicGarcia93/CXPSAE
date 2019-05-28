using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXPSAE.Modelo
{
    public class Compras
    {
        private string factura;
        private string concepto;
        private string documento;
        private string fechaAplicacion;
        private float monto;
        private float saldo;
        private string empresa;
        
        
        public Compras(string factura, string concepto ,string documento,string fechaApli,float monto, float saldo, string empresa)
        {
            this.factura = factura;
            this.concepto = concepto;
            this.documento = documento;
            this.fechaAplicacion = fechaApli;
            this.monto = monto;
            this.saldo = saldo;
        }

        public string Factura
        {
            get { return factura; }
            set { factura = value; }
        }
       public string Concepto
        {
            get { return concepto; }
            set { concepto = value; }
        }
        public string Documento
        {
            get { return documento; }
            set { documento = value; }
        }

        public string FechaAplicacion
        {
            get { return fechaAplicacion; }
            set { fechaAplicacion = value; }
        }

        public float Monto
        {
            get { return monto; }
            set { monto = value; }
        }
        public float Saldo
        {
            get { return saldo; }
            set { saldo = value; }
        }

        public String Empresa
        {
            get { return empresa; }
            set { empresa = value; }
        }
       
    }


}
