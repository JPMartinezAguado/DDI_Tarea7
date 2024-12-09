using AxWMPLib;
using System.Media;

namespace DDI_Tarea7
{
    public partial class Form1 : Form
    {
        //variables para la generacion del texto mostrado
        #region VariablesTexto
        private string strLabel1;
        private string strLabel2;
        private string strLabel3;
        private string strLabel4;
        private string strLabel5;
        private string strLabel6;
        private string strLabel7;
        private string strLabel8;
        private string strLabel9;
        private string strLabel10;
        private string strLabel11;
        private List<string> textoLabel = new List<string>();
        private int indiceTexto = 0;
        private int index = 0;
        #endregion

        //variables para gestionar el degradado de pantalla incial a video
        #region VariablesVideo
        private List<Image> degradado = new List<Image>();
        private int indiceDegradado = 0;
        private int indexDeg = 0;
        #endregion

        private SoundPlayer sonido;

        public Form1()
        {
            InitializeComponent();

            //bucle de sonido background de la portada
            Stream audioStream = new MemoryStream(Properties.Resources.portada);
            sonido = new SoundPlayer(audioStream);
            sonido.PlayLooping();

            //timer para gestinoar la velocidad de muestra de texto
            timer1.Interval = 80;
            timer1.Tick += new EventHandler(OnTimerTick);
            
            //timer que gestiona la velocidad de cambio de imagenes para hacer efecto video de paso degradado
            timer2.Interval = 250;
            timer2.Tick += new EventHandler(timer2_Tick);
            timer2.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GenerarTextos();
            GenerarDegradado();

            //mediante un elemento Com que permite usar windows media player, reproduce video de fondo en bucle
            axWindowsMediaPlayer2.settings.setMode("loop", true);
            axWindowsMediaPlayer2.URL = Application.StartupPath + @"\fondo.avi";
            axWindowsMediaPlayer2.Ctlcontrols.play();
            axWindowsMediaPlayer2.uiMode = "none";//para esconder los controles de WMP
            axWindowsMediaPlayer2.stretchToFit = true;//aseguramos que cubra todo el formulario
        }

        //evento que sirve para mostrar el siguiente bloque de texto
        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (indiceTexto < textoLabel.Count -1)//mientras haya textos en el List de strings
            {
                index = 0;//indice para posicion del primer caracter del string a leer
                label1.Text = "";
                indiceTexto++;//indice del List de strings al que accederemos 
                timer1.Start();
            }
            else 
            {
                btnSalto_Click(sender, e);
            }
        }

        //evento que cierra este formulario y abre el siguiente
        private void btnSalto_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            timer2.Stop();
            sonido.Stop();
            this.Hide();
            Juego form2 = new Juego();
            form2.Show();
        }

        //metodo que recoge y rellena el List donde se guardan los strings a mostrar
        private void GenerarTextos()
        {
            strLabel1 = "ATENCION!! Se Recomienda el uso de auriculares o altavoces.\n" +
               "Bienvenido a  *** BEAR WITH ME ANOTHER DAY ***\n" +
               "Pulsa  >>  para continuar o *Saltar Intro* para ir al juego";
            strLabel2 = "Hasta ahora todo va bien. Delirando en un sueño quimico\n" +
               "no hay dolor. Toca despertar y afrontar un nuevo dia\n" +
               "Menos mal que en esta lucha no estás solo...  ";
            strLabel3 = "...tu familia y amigos están a tu lado. Tambien el mejor\n" +
               "equipo medico posible. Un doctor cuida de tu salud fisica,\n" +
               "un psicologo  te ayuda que no te vengas abajo...";
            strLabel4 = "...y las enfermeras, con humanidad y saber hacer, no te dejan caer.\n" +
               "El dia es largo, golpes a tu salud y pensamientos negativos\n" +
               "no dejarán de atacarte.    Todos te repiten lo mismo...";
            strLabel5 = "AGUANTA UN DIA MAS!!       Todos luchais por ello.\n" +
               "Cansancio y frustación les hacen mella y deben descansar,\n" +
               "pero en cuanto se recuperan, vuelven para ayudarte..";
            strLabel6 = "El paciente no necesita nombre.   \"Los mios\" tampoco....\n" +
                "Un accidente,esa enfermedad que golpea de repente..."+
                "...todos tenemos nombres y caras para estos personajes... \n";
            strLabel7 = "MECÁNICA DE JUEGO:\n" +
              "Tras despertarte, te enfrentas a la realidad de nuevo: Tu salud\n" +
              "y ganas de seguir adelante se esfuman poco a poco...";
            strLabel8 = "Recoge dados de cada sanitario cuando estén disponibles.\n" +
              "Con ellos crearás un pool de recursos para recuperarte.\n" +
              "Sin embargo, problemas y pensamientos negativos se acercan a ti... ";
            strLabel9 = "Debes pinchar sobre ellos para tratarlos. Si dejas que te alcancen\n" +
             "va a ser peor y tu salud se comprometerá aun mas.\n" +
             "Segun pase el dia, vendrán con mayor frecuencia y rapidez!!";
            strLabel10 = "También cuentas con  *LOS MIOS*. Cuando los actives,la\n" +
            "mejora total será enorme, y no hay enfermedad o\n" +
            "negatividad por tratar que impida que mejore tu estado... ";
            strLabel11 = "...parafrasenado una famosa serie de TV...\n" +
            "¿que le decimos al Dios de la Muerte?........HOY NO!!   \n" +
            "...pues simplemente de eso va el juego.....";

            textoLabel.Add(strLabel1);
            textoLabel.Add(strLabel2);
            textoLabel.Add(strLabel3);
            textoLabel.Add(strLabel4);
            textoLabel.Add(strLabel5);
            textoLabel.Add(strLabel6);
            textoLabel.Add(strLabel7);
            textoLabel.Add(strLabel8);
            textoLabel.Add(strLabel9);
            textoLabel.Add(strLabel10);
            textoLabel.Add(strLabel11);
        }

        //genercion del List que guardará los frames de la introduccion
        private void GenerarDegradado()
        {
            for (int i = 0; i < 10; i++)
            {
                degradado.Add(Properties.Resources.fondojuego100);
            }
            for (int i = 0; i < 30; i++)
            {
                degradado.Add(Properties.Resources.fondojuego110);
            }
            //degradado.Add(Properties.Resources.fondojuego100);
            //degradado.Add(Properties.Resources.fondojuego090);
            degradado.Add(Properties.Resources.fondojuego085);
            degradado.Add(Properties.Resources.fondojuego080);
            degradado.Add(Properties.Resources.fondojuego075);
            degradado.Add(Properties.Resources.fondojuego070);
            degradado.Add(Properties.Resources.fondojuego065);
            degradado.Add(Properties.Resources.fondojuego060);
            degradado.Add(Properties.Resources.fondojuego055);
            degradado.Add(Properties.Resources.fondojuego050);
            degradado.Add(Properties.Resources.fondojuego045);
            degradado.Add(Properties.Resources.fondojuego040);
            degradado.Add(Properties.Resources.fondojuego035);
            degradado.Add(Properties.Resources.fondojuego030);
            degradado.Add(Properties.Resources.fondojuego025);
            degradado.Add(Properties.Resources.fondojuego020);
            degradado.Add(Properties.Resources.fondojuego015);
            degradado.Add(Properties.Resources.fondojuego010);
            degradado.Add(Properties.Resources.fondojuego005);

        }

        //metodo que recoge el string a mostrar, anade un caracter al texto mostrado
        //y mueve el indice una posicion, para mostrar el siguiente carcter en la proxima iteracion.
        //Cuando alcanza la ultima posicion, detiene el timer
        private void OnTimerTick(object sender, EventArgs e)
        {
            string texto = textoLabel[indiceTexto];
            if (index < texto.Length)
            {
                label1.Text += texto[index];
                index++;
            }
            else
            {
                timer1.Stop();
            }
        }

        //timer que mientras el indice del List seleccionado est'a dentro del total de elemnteos del
        //List, muestra en el picturebox la imagen contenida en ese indice, y avanza el mismo.
        //Cunado alcanza el final de List, esconde el Picturebox y detiene el timer
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (indexDeg < degradado.Count)
            {
                Image imagen = degradado[indexDeg];

                pictureBox1.Image = imagen;

                indexDeg++;
            }
            else
            {
                pictureBox1.Visible = false;
                timer1.Start();
            }
        }

    }
}
