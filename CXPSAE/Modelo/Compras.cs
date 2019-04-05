using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXPSAE.Modelo
{
    class Compras
    {
        private string factura;
        private string documento;
        private string fechaAplicacion;
        private float monto;
        private float saldo;

        public Compras(string factura, string documento,string fechaApli,float monto, float saldo)
        {
            this.factura = factura;
            this.documento = documento;
            this.fechaAplicacion = fechaApli;
            this.monto = monto;
            this.saldo = saldo;
        }

        public void SetFactura(string factura)
        {
            this.factura = factura;
        }
        public string GetFactura()
        {
            return factura;
        }
        public void SetFechaAplicacion(string fecha)
        {
            this.fechaAplicacion = fecha;
        }
        public string GetFechaAplicacion()
        {
            return fechaAplicacion;
        }
        public void SetMonto(float monto)
        {
            this.monto = monto;
        }
        public float GetMonto()
        {
            return monto;
        }
        public void SetSaldo(float saldo)
        {
            this.saldo = saldo;
        }
        public float GetSaldo()
        {
            return saldo;
        }
    }


}
