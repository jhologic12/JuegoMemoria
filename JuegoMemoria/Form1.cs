using System.Reflection;

namespace JuegoMemoria
{
    public partial class Form1 : Form
    {

        List<Icono> _iconos = new List<Icono>(); 
        Random _random = new Random();

        Panel primeraSeleccion, segundaSeleccion;
        Panel primeraCoverSeleccion, segundaCoverSeleccion;

        Dictionary<string, int> asigandoPaneles = new Dictionary<string, int>();
        public Form1()
        {
            InitializeComponent();
            CargarImagenesDesdeArchivo();
            PoblarIconosATablas();
            mostrarCartasInic(true);
        }


        private void mostrarCartasInic(bool mostrarCartas)
        {
            pnlCubrir1.Visible = !mostrarCartas;
            pnlCubrir2.Visible = !mostrarCartas;
            pnlCubrir3.Visible = !mostrarCartas;
            pnlCubrir4.Visible = !mostrarCartas;
            pnlCubrir5.Visible = !mostrarCartas;
            pnlCubrir6.Visible = !mostrarCartas;
            pnlCubrir7.Visible = !mostrarCartas;
            pnlCubrir8.Visible = !mostrarCartas;
            pnlCubrir9.Visible = !mostrarCartas;
            pnlCubrir10.Visible = !mostrarCartas;
            pnlCubrir11.Visible = !mostrarCartas;
            pnlCubrir12.Visible = !mostrarCartas;
            pnlCubrir13.Visible = !mostrarCartas;
            pnlCubrir14.Visible = !mostrarCartas;
            pnlCubrir15.Visible = !mostrarCartas;
            pnlCubrir16.Visible = !mostrarCartas;
            timerInit.Start();

        }

        private void timerInit_Tick(object sender, EventArgs e)
        {

            timerInit.Stop();
            mostrarCartasInic(false);

            timerInit.Dispose();

        }


     
        private void CargarImagenesDesdeArchivo()
        {

            var archivos = Assembly.GetExecutingAssembly().GetManifestResourceNames();

            int id = 1;


            //JuegoMemoria.Resources.cellphone.png
            foreach (var imagen in archivos)
            {
                if (!imagen.EndsWith(".png"))
                    continue;


                var icono = new Icono
                {
                    Id = id,
                    Nombre = imagen.Replace("JuegoMemoria.Resources.", "").Replace(".png", ""),
                    Imagen = new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream(imagen))
                };

                _iconos.Add(icono);
                _iconos.Add(icono);
                id++;
            }

        }
    
    
    
       private void PoblarIconosATablas()
        {
            Panel panel;
            int randomNumero;

            foreach (var item in this.Controls)
            {
                if (item is Panel && !((Panel)item).Name.Contains("pnlCubrir"))
                    panel = (Panel)item;
                else
                    continue;

                randomNumero = _random.Next(0,_iconos.Count);

                panel.BackgroundImage = _iconos[randomNumero].Imagen;

                asigandoPaneles.Add(panel.Name, _iconos[randomNumero].Id);
                _iconos.RemoveAt(randomNumero);
            }
        }

        private void pnlCubrir_Click(object sender, EventArgs e)
        {

            if (primeraSeleccion != null && segundaSeleccion != null)
                return;
            Panel clickedPanel = (Panel)sender;

            if (clickedPanel == null)
                return;

            if (!clickedPanel.Visible)
                return;

            clickedPanel.Visible = false;

            if(primeraSeleccion == null)
            {
                primeraSeleccion = ObtenerIconoPanel(clickedPanel);
                primeraCoverSeleccion = clickedPanel;
                return;
            }


            if (segundaSeleccion == null)
            {
                segundaSeleccion = ObtenerIconoPanel(clickedPanel);
                segundaCoverSeleccion = clickedPanel;
            }
       
        
            if(primeraSeleccion != null && segundaSeleccion != null && comprobarCoincidencia())
            {
                LimpiarSelecccion(true);
            }
            else
            {
                ResetearComprobacion();
            }
        
        
      
        }

        




        private Panel ObtenerIconoPanel(Panel cubrirPanel)
        {

            Panel iconoPanel = null;

            foreach (var item in this.Controls)
            {
                if(item is Panel
                    && !((Panel)item).Name.Contains("pnlCubrir")
                    && ((Panel)item).Tag == cubrirPanel.Tag)
                {

                    iconoPanel = (Panel)item;

                }


            }


            return iconoPanel;
        
        
        }



        private bool comprobarCoincidencia()
        {
            return asigandoPaneles[primeraSeleccion.Name] == asigandoPaneles[segundaSeleccion.Name];
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            LimpiarSelecccion(false);
            timer1.Stop();
            

        }

        private void LimpiarSelecccion(bool comprobar)
        {
            if (!comprobar)
            {
                primeraCoverSeleccion.Visible = true;
                segundaCoverSeleccion.Visible = true;
            }

            primeraSeleccion = null;
            segundaSeleccion = null;
            primeraCoverSeleccion = null;
            segundaCoverSeleccion = null;

        }
    
    
    private void ResetearComprobacion()
        {
            timer1.Start();



        }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    }




    

}