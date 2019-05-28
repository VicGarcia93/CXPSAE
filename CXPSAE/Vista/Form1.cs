using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CXPSAE.Controlador;
using CXPSAE.Vista;
using System.Threading;
using CXPSAE.Modelo;
using System.Collections.Generic;

namespace CXPSAE
{
    public partial class Form1 : Form, IProveedoresView
    {
        IProveedoresController controller;
        IComprasController controllerCompras;
        private bool HeavyProcessStopped;
        // Expone el contexto de sincronización en la clase entera 
        private readonly SynchronizationContext SyncContext;
        private DataTable listProveedores;
        // Crear los 2 contenedores de callbacks
        public event EventHandler Callback1;
        // public event EventHandler Callback2;
        private const string PLACEHOLDER = "Búsqueda rápida";
        String[] parametros;
        public Form1(IProveedoresController controller)
        {
            this.controller = controller;
            SyncContext = AsyncOperationManager.SynchronizationContext;

            InitializeComponent();

            cmbEmpresa.SelectedIndex = 0;
            cmbEstatus.SelectedIndex = 1;
        }

        //******************SETTERS & GETTERS*****************************************************************
        public void SetListaProveedores(DataTable proveedores)
        {
            this.listProveedores = proveedores;
        }

        public void SetProveedoresController(IProveedoresController controller)
        {
            this.controller = controller;
        }


        //*******************EVENTOS*****************************************************************

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
          /*  e.Graphics.DrawRectangle(Pens.Red, e.ClipRectangle.Left, e.ClipRectangle.Top, e.ClipRectangle.Right, e.ClipRectangle.Bottom);
            base.OnPaint(e);  */
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            doConsultaProveedores();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
           // this.Location = new Point(0, 0); //sobra si tienes la posición en el diseño
           // this.Size = new Size(Screen.PrimaryScreen.WorkingArea.Size.Width, Screen.PrimaryScreen.WorkingArea.Size.Height);
            txtBuscar.GotFocus += new EventHandler(this.ObtieneFoco);
            txtBuscar.LostFocus += new EventHandler(this.SaleFoco);
            txtBuscar.Text = PLACEHOLDER;
            txtBuscar.ForeColor = Color.LightGray;
            // Puedes crear multiples callbacks o solo uno
            Callback1 += CallbackChangeDataGridView;
            Start(0);
        }
        private void CallbackChangeDataGridView(object sender, EventArgs e)
        {
            // Acceder al dataGridView desde el hilo principal :)
            Console.WriteLine("Vista: Tamaño de lista " + listProveedores.Rows.Count);
            dgvProveedores.DataSource = null;
            dgvProveedores.AutoGenerateColumns = false;
            dgvProveedores.DataSource = listProveedores;

        }

        private void ObtieneFoco(object sender, EventArgs e)
        {
            if (txtBuscar.Text.Equals("Búsqueda rápida") && txtBuscar.ForeColor == Color.LightGray)
            {
                txtBuscar.Text = "";
                txtBuscar.ForeColor = Color.Black;
            }
        }

        private void SaleFoco(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtBuscar.Text))
            {
                txtBuscar.Text = PLACEHOLDER;
                txtBuscar.ForeColor = Color.LightGray;
            }
        }

        //**********************************MÉTODOS********************************************************
        public void doConsultaProveedores()
        {
            if (txtBuscar.ForeColor == Color.LightGray)
                txtBuscar.Text = "";
             parametros = new string[] { txtBuscar.Text, cmbEmpresa.SelectedIndex.ToString(),
                                    cmbEstatus.SelectedItem.ToString().Substring(0,1) };
            Start(1);
        }

        // Método para iniciar el proceso de obtención de proveedores
        public void Start(int opcion)
        {
            Thread thread;
            if (opcion == 0)
                thread = new Thread(RunInicio) { IsBackground = true };
            else if(opcion == 1)
                thread = new Thread(RunBusqueda) { IsBackground = true };
            else 
            {
                thread = new Thread(RunGetCompras) { IsBackground = true };
            }

            thread.Start();
        }
 
        // Método donde la lógica principal de tu tarea se ejecuta
        private void RunInicio()
        {
            while (!HeavyProcessStopped)
            {
                controller.GetListaProveedores(this);
                // Ejecuta el primer callback desde el proceso de fondo al hilo principal (el de la interfaz gráfica)
                // El primer callback activa el primer boton !

                SyncContext.Post(e => TriggerCallback1(), null);
                // Esperar otros 2 segundos para más tareas pesadas.
                Thread.Sleep(2000);
                // La tarea heavy task finaliza, así que hay que detenerla.
                Stop();
            }
        }
        private void RunBusqueda()
        {
            HeavyProcessStopped = false;
            while (!HeavyProcessStopped)
            {
               
                controller.GetListaProveedoresConFiltro(this, parametros);
                
                // Ejecuta el primer callback desde el proceso de fondo al hilo principal (el de la interfaz gráfica)
                // El primer callback activa el primer boton !

                SyncContext.Post(e => TriggerCallback1(), null);
                // Esperar otro segundo para más tareas pesadas.
                Thread.Sleep(1000);
                // La tarea heavy task finaliza, así que hay que detenerla.
                Stop();
            }
        }
        private void RunGetCompras()
        {
            HeavyProcessStopped = false;
            while (!HeavyProcessStopped)
            {

                controllerCompras.GetCompras(this);
                
                // Ejecuta el primer callback desde el proceso de fondo al hilo principal (el de la interfaz gráfica)
                // El primer callback activa el primer boton !

                SyncContext.Post(e => TriggerCallback1(), null);
                // Esperar otro segundo para más tareas pesadas.
                Thread.Sleep(1000);
                // La tarea heavy task finaliza, así que hay que detenerla.
                Stop();
            }
        }

        // Método para detener el proceso
        private void Stop()
        {
            HeavyProcessStopped = true;
        }

        // Métodos que ejecutan los callback si y solo si fueron declarados durante la instanciación de la clase HeavyTask
        private void TriggerCallback1()
        {
            // Si el primer callback existe, ejecutarlo con la información dada
            Callback1?.Invoke(this, EventArgs.Empty);
        }

        public void SetComprasController(IComprasController controllerCompras)
        {
            this.controllerCompras = controllerCompras;
        }

        public void DoConsultaCompras()
        {
            Start(2);
        }

        public void SetListaCompras(List<Compras> compras)
        {
            throw new NotImplementedException();
        }
    }
}
