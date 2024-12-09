
using System.Media;
using static DDI_Tarea7.Mecanicas;
using Label = System.Windows.Forms.Label;

namespace DDI_Tarea7
{
    /// <summary>
    /// Pantalla principal del juego, donde transcurre y se gestionan la mayoria de eventos y calculos
    /// </summary>
    public partial class Juego : Form
    {
        #region InstanciasPersonajes
        private Persona[] personas;
        private Enfermera primeraEnfermera;
        private Enfermera segundaEnfermera;
        private Doctor doctor;
        private Psicologo psicologo;
        private Familiar familia;
        private Paciente paciente;
        #endregion

        //istancias para objetos de hilos y soundplayer para reproducir audio en segundo plano
        private Thread musicaFondo;
        private SoundPlayer sonido;

        #region VariablesInteger
        private int velocidadAmenazas = 300;//tiempo de refresco del evento que desplaza los mensajes negativos
        private int intervaloDificultad = 128000;//timer que marca el momento de incremento de dificultad
        private int posicionInicial;//guarda la posicion original de las labels de amenazas
        private int tipoMal;//mediante un random, guarda el tipo de amenaza que mostraran las labels
        private int def = 1;
        private int valorAtaqueSalud = 0;
        private int valorAtaqueMoral = 0;

        #endregion

        #region InstanciasListas
        private List<Label> labels = new List<Label>();//labels donde se muestean las amenazas
        private List<AtaqueMoral> danoMoral = new List<AtaqueMoral>();//almacena los objetos que causan dano moral
        private List<AtaqueSalud> danoSalud = new List<AtaqueSalud>();//almacena los objetos que causan dano fisico
        private List<CarinoFamilia> apoyoFamilia = new List<CarinoFamilia>();//almacena mensajes de apoyo
        private List<Image> dadosBotonSalud = new List<Image>();//almacena llas imagenes de dados  verdes
        private List<Image> dadosBotonMoral = new List<Image>();//almacena las imagenes de dados magenta
        #endregion

        #region InstanciasObjetosMecanicas
        private Mecanicas mecanicas;
        private AtaqueMoral ataqueMoral;
        private AtaqueSalud ataqueSalud;
        #endregion

        private Label seleccionada;//varialbe que guarda la label de amenaza seleccionada

        //immagen de dado por defecto para boton deshabilitado
        private Image dadoDeshabilitado = Properties.Resources.perspective_dice_six_faces_four__4_;

        public Juego()
        {
            InitializeComponent();

            mecanicas = new Mecanicas();//objeto de clase mecanicas para poder usar sus metodos

            RellenarListLabels();
            IniciarBarras();

            //inicio de musica de fondo en el hilo correspondiente
            musicaFondo = new Thread(PlayBackground);
            musicaFondo.IsBackground = true;
            musicaFondo.Start();

            IniciarTemporizadores();

            RellenarCarino();
            RellenarDanoMoral();
            RellenarDanoSalud();
            RellenarDadosBotones();
            IniciarNuevoLabel();

            #region Personajes

            //instancias de objetos personas con sus atributos
            primeraEnfermera = new Enfermera("Alicia", prbEnfermera1, 3, 4);
            segundaEnfermera = new Enfermera("Salvia", prbEnfermera2, 3, 3);
            doctor = new Doctor("Fabiano", prbDoctor, 5, 1);
            psicologo = new Psicologo("Karl", prbPsicologo, 5, 1);
            familia = new Familiar("familia", prbFamilia, 10, 1);
            paciente = new Paciente("Paciente", prbVIda, 0, 100, 100, prbMoral);

            //creacion de array con los objetos generados
            personas = new Persona[] { primeraEnfermera, segundaEnfermera, doctor, psicologo, familia };

            #endregion

        }

        #region CargaElementos

        //definicion de valores inciales de las progressbars
        private void IniciarBarras()
        {
            prbEnfermera1.Minimum = 0;
            prbEnfermera1.Maximum = 100;
            prbEnfermera2.Minimum = 0;
            prbEnfermera2.Maximum = 100;
            prbDoctor.Minimum = 0;
            prbDoctor.Maximum = 100;
            prbPsicologo.Minimum = 0;
            prbPsicologo.Maximum = 100;
            prbFamilia.Minimum = 0;
            prbFamilia.Maximum = 100;
            prbVIda.Minimum = 0;
            prbVIda.Maximum = 100;
            prbVIda.Value = 50;
            prbMoral.Minimum = 0;
            prbMoral.Maximum = 100;
            prbMoral.Value = 50;
        }

        //rellenado de list de labels con las 5 posiciones para amenazas disponibles
        private void RellenarListLabels()
        {
            labels.Add(lblAtaque1);
            labels.Add(lblAtaque2);
            labels.Add(lblAtaque3);
            labels.Add(lblAtaque4);
            labels.Add(lblAtaque5);
        }

        //rellenado de List de amenazas a la psyque, con sus nombres y valor de daño
        public void RellenarDanoMoral()
        {
            danoMoral.Add(new AtaqueMoral("Para que seguir sufriendo?", 5));
            danoMoral.Add(new AtaqueMoral("Mejor me voy y que retomen su vida", 10));
            danoMoral.Add(new AtaqueMoral("En que me he convertido?", 15));
            danoMoral.Add(new AtaqueMoral("Apatia y hastio", 10));
            danoMoral.Add(new AtaqueMoral("Esto ya no tiene sentido", 5));
            danoMoral.Add(new AtaqueMoral("Nunca voy a salir de esta cama", 5));
            danoMoral.Add(new AtaqueMoral("Frustracion", 5));
            danoMoral.Add(new AtaqueMoral("Angustia", 10));
        }

        //rellenado de List de amenazas fisicas ocn sus nombres y valor de daño
        public void RellenarDanoSalud()
        {
            danoSalud.Add(new AtaqueSalud("Presion arterial demasiado alta", 5));
            danoSalud.Add(new AtaqueSalud("Falta de respiracion", 15));
            danoSalud.Add(new AtaqueSalud("Pulso debil", 10));
            danoSalud.Add(new AtaqueSalud("Presion en el pecho", 5));
            danoSalud.Add(new AtaqueSalud("Dolor de cabeza intenso", 10));
            danoSalud.Add(new AtaqueSalud("baja saturacion oxigeno", 5));
            danoSalud.Add(new AtaqueSalud("Infección interna", 10));
            danoSalud.Add(new AtaqueSalud("Hemorragia interna", 5));
        }

        //rellenado de List de mensajes de apoyo. Solo se utilizan los nombres, el resto de atributos
        //no tienen uno en esta version de mecanicas de juego
        public void RellenarCarino()
        {
            apoyoFamilia.Add(new CarinoFamilia("..te coge la mano...", 1, 1));
            apoyoFamilia.Add(new CarinoFamilia("...\"Tus hijos han venido a verte\"...", 1, 1));
            apoyoFamilia.Add(new CarinoFamilia("...os abrazais...", 2, 1));
            apoyoFamilia.Add(new CarinoFamilia("...apoya su cabeza en tu pecho...", 1, 2));
            apoyoFamilia.Add(new CarinoFamilia("...sientes una acaricia en la cabeza...", 1, 1));
            apoyoFamilia.Add(new CarinoFamilia("...\"Te he traido escondido un Phoskito ;)\"...", 1, 3));
            apoyoFamilia.Add(new CarinoFamilia("...\"Te acuerdas aquel verano que...?\"...", 1, 1));
            apoyoFamilia.Add(new CarinoFamilia("...tengo que volver a casa....", 2, 1));
            apoyoFamilia.Add(new CarinoFamilia("...\"Aun nos queda ir a Japon, mi amor:)\"...", 1, 1));
            apoyoFamilia.Add(new CarinoFamilia("...volver a hacer el tonto juntos por Paris...", 1, 2));

        }

        #endregion

        #region LabelsMoviles

        //metodo que recibe un label de amenaza y lo desplaza un pixel hacia abajo
        private void MoverLabel(Label label)
        {
            if (label.Bottom >= panel2.Top + 350)//si el label baja hasta el limite fijado 
            {
                if (tipoMal == 1)//si el tipo de daño es psiquico
                {
                    //se reduce la barra de moral del paciente en 30, o lo deja a 0 si la resta lo dejara en negativo
                    prbMoral.Value = Math.Max(0, prbMoral.Value - 30);
                    lblTextoDepresion.Text += label.Text;//anade al clipboard correspondiente
                    label.Visible = false;//esconde el label
                    IniciarNuevoLabel();//inicia uno nuevo
                }
                else
                {
                    //lo mismo para dano de salud
                    prbVIda.Value = Math.Max(0, prbVIda.Value - 30);
                    lblTextoEnfermedad.Text += label.Text;
                    label.Visible = false;
                    IniciarNuevoLabel();
                }
            }
            else { label.Top++; }//si no ha llegado al limite mmarcado, baja el label un pixel su posicion
        }

        //metodo que escoge aleatoriamente una posicion para alojar labels de amenaza entreclas 5 existentes,
        //elige aleatorioamente el tipo de amenaza y su texto y lo aplica al label seleccionado
        private void IniciarNuevoLabel()
        {
            Random posicion = new Random();//indice del List de labels
            Random contenido = new Random();//indice del List daños correspondiente
            Random tipo = new Random();//tipo de daño
            tipoMal = tipo.Next(0, 2);
            seleccionada = labels[posicion.Next(labels.Count)];
            posicionInicial = seleccionada.Top;//guarda la posicion inicial para recuperarla la siguiente iteracion 

            if (tipoMal == 1)
            {
                ataqueMoral = danoMoral[contenido.Next(danoMoral.Count)];//se escoge un dano del tipo moral del list
                seleccionada.Text = ataqueMoral.StrDescripcion;//se pinta su descripcion en la label
            }
            else
            {
                ataqueSalud = danoSalud[contenido.Next(danoSalud.Count)];
                seleccionada.Text = ataqueSalud.StrDescripcion;
            }
            seleccionada.Top = posicionInicial;
            seleccionada.Visible = true;
        }

        //evento de boton de click en amenaza
        private void lblAtaque2_Click(object sender, EventArgs e)
        {
            Label lb = sender as Label;//convierte el sender que recoge el evento en un label

            if (lb != null && lb == seleccionada)//si este no es nulo y coincide con la seleccionada
            {
                seleccionada.Visible = false;//se oculta
                seleccionada.Top = posicionInicial;//recupera su posicion inicial, guardada al inciarla

                if (tipoMal == 1)
                {
                    //se anade el texto del label al clipboard correspondiente, de dano moral
                    //se anade su valor de dano al contador de dano acumulado sin tratar
                    //y se actualiza el marcador del clipboard
                    lblTextoDepresion.Text += ataqueMoral.StrDescripcion + "\n";
                    valorAtaqueMoral += ataqueMoral.IntDanoMoral;
                    lblCantidadDanoMoral.Text = valorAtaqueMoral.ToString();
                }
                else
                {
                    //lo mismo que arriba, con caso de dano de salud
                    lblTextoEnfermedad.Text += ataqueSalud.StrDescripcion + "\n";
                    valorAtaqueSalud += ataqueSalud.IntDanoSalud;
                    lblCantidadDanoSalud.Text = valorAtaqueSalud.ToString();
                }
                IniciarNuevoLabel();//se llama al metodo que muestra un nuevo label
            }
        }

        #endregion

        #region PulsadoresCargaDados

        //eventos de pulsacion de boton para recogida de dados. llama al evento calcularTotalDados
        //que calcula el numero de dados a activar, y posteriormente le pasa ese numero al metodo que 
        //habilitara el numero de botoes correspondiente.
        //Tras ello, pondr'a el valor de la barra del personaje llamado a 0
        //En el caso de enfermeras, activara dados de ambos tipos de amenaza

        private void btnEnfermera1_Click(object sender, EventArgs e)
        {
            int totalDados = mecanicas.calcularTotalDados(primeraEnfermera, 15, 40);

            mecanicas.HabilitarDadosSalud(totalDados, btnSalud1, btnSalud2, btnSalud3, btnSalud4, btnSalud5, btnSalud6);
            mecanicas.HabilitarDadosMoral(totalDados, btnMoral1, btnMoral2, btnMoral3, btnMoral4, btnMoral5, btnMoral6);

            primeraEnfermera.Bar.Value = 0;

        }

        private void btnEnfermera2_Click(object sender, EventArgs e)
        {
            int totalDados = mecanicas.calcularTotalDados(segundaEnfermera, 25, 60);

            mecanicas.HabilitarDadosSalud(totalDados, btnSalud1, btnSalud2, btnSalud3, btnSalud4, btnSalud5, btnSalud6);
            mecanicas.HabilitarDadosMoral(totalDados, btnMoral1, btnMoral2, btnMoral3, btnMoral4, btnMoral5, btnMoral6);

            segundaEnfermera.Bar.Value = 0;
        }

        private void btnDoctor_Click(object sender, EventArgs e)
        {
            int totalDados = mecanicas.calcularTotalDados(doctor, 50, 70);

            mecanicas.HabilitarDadosSalud(totalDados, btnSalud1, btnSalud2, btnSalud3, btnSalud4, btnSalud5, btnSalud6);

            doctor.Bar.Value = 0;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int totalDados = mecanicas.calcularTotalDados(psicologo, 50, 70);

            mecanicas.HabilitarDadosMoral(totalDados, btnMoral1, btnMoral2, btnMoral3, btnMoral4, btnMoral5, btnMoral6);

            psicologo.Bar.Value = 0;
        }

        //el caso de la familia es distinto. comprueba que existen la famila, el paciente y la instaacnia de
        //objetos de clase mecanica para llamar al metodo que calcula en ajuste en las baarras de progreso
        //de paciente y faamilia. Despues arranca un timer uqe ser'a encargado de manejar el tiempo 
        //por el que el texto de apoyo se mostraar'a por pantalla
        private void btnFamilia_Click(object sender, EventArgs e)
        {
            if (mecanicas != null && familia != null && paciente != null)
            {
                mecanicas.RecibirAfecto(familia, paciente, lblRecuerdosyAfecto, apoyoFamilia);
                timerMensajePositivo.Start();
            }
        }

        #endregion

        #region BotonesSalud

        //metodo que recibe un boton clicado, genera un random que emula un dado ,cuyo vssalor sera la mejora en el paciente
        //llama al metodo que muestra el valor obtenido en la pantalla, deduce de las amenazas en tratamiento el valor de defensa
        //y aplica el exceso de defensa a mejorar la barra ocrrespondiente del paciente
        private void PulsarBotonSalud(System.Windows.Forms.Button btn)
        {
            Random rnd = new Random();
            def = rnd.Next(1, 7);

            PintarBotonSalud(btn);

            valorAtaqueSalud -= def;

            if (valorAtaqueSalud <= 0)//si el valor de ataque restante es menor de 0
            {
                int barraActualizada = Math.Max(0, def - valorAtaqueSalud);
                paciente.Bar.Value = Math.Min(paciente.Bar.Value + barraActualizada, paciente.Bar.Maximum);//nos aseguramos de no poner mas de 100
                valorAtaqueSalud = 0;
                lblTextoEnfermedad.Text = "";//limpiamos el clipboard de amenazas a tratar

            }
            lblCantidadDanoSalud.Text = valorAtaqueSalud.ToString();//mostramos el valor 0 por pantlla

            btn.Enabled = false;

            if (!btnSalud1.Enabled && !btnSalud2.Enabled && !btnSalud3.Enabled && !btnSalud4.Enabled &&
                !btnSalud5.Enabled && !btnSalud6.Enabled)
            {
                if (paciente.Bar.Value - valorAtaqueSalud <= 0)
                {
                    paciente.Bar.Value = 0;
                }
                else
                {
                    paciente.Bar.Value = Math.Max(0, paciente.Bar.Value - valorAtaqueSalud);
                }
            }
        }

        //llamadas al metodo PulsarBotonSalud pasandole el boton clickado en cada caso
        private void btnSalud1_Click(object sender, EventArgs e)
        {
            PulsarBotonSalud(btnSalud1);
        }
        private void btnSalud2_Click(object sender, EventArgs e)
        {
            PulsarBotonSalud(btnSalud2);
        }
        private void btnSalud3_Click(object sender, EventArgs e)
        {
            PulsarBotonSalud(btnSalud3);
        }
        private void btnSalud4_Click(object sender, EventArgs e)
        {
            PulsarBotonSalud(btnSalud4);
        }
        private void btnSalud5_Click(object sender, EventArgs e)
        {
            PulsarBotonSalud(btnSalud5);
        }
        private void btnSalud6_Click(object sender, EventArgs e)
        {
            PulsarBotonSalud(btnSalud6);
        }

        #endregion

        #region BotonesMoral

        //metodos analogos a los de la region anterior BotonoesSalud
        private void PulsarBotonMoral(System.Windows.Forms.Button btn)
        {
            Random rnd = new Random();
            def = rnd.Next(1, 7);

            PintarBotonMoral(btn);

            valorAtaqueMoral -= def;

            if (valorAtaqueMoral <= 0)
            {
                int barraActualizada = Math.Max(0, def - valorAtaqueMoral);
                paciente.PrbMoral.Value = Math.Min(paciente.PrbMoral.Value + barraActualizada, paciente.PrbMoral.Maximum);
                valorAtaqueMoral = 0;
                lblTextoDepresion.Text = "";

            }
            lblCantidadDanoMoral.Text = valorAtaqueMoral.ToString();
            btn.Enabled = false;

            if (!btnMoral1.Enabled && !btnMoral2.Enabled && !btnMoral3.Enabled && !btnMoral4.Enabled &&
                !btnMoral5.Enabled && !btnMoral6.Enabled)
            {

                if (paciente.PrbMoral.Value - valorAtaqueMoral <= 0)
                {
                    paciente.Bar.Value = 0;
                }
                else
                {
                    paciente.PrbMoral.Value = Math.Max(0, paciente.PrbMoral.Value - valorAtaqueMoral);
                }
            }
        }

        private void btnMoral1_Click(object sender, EventArgs e)
        {
            PulsarBotonMoral(btnMoral1);
        }
        private void btnMoral2_Click(object sender, EventArgs e)
        {
            PulsarBotonMoral(btnMoral2);
        }
        private void btnMoral3_Click(object sender, EventArgs e)
        {
            PulsarBotonMoral(btnMoral3);
        }
        private void btnMoral4_Click(object sender, EventArgs e)
        {
            PulsarBotonMoral(btnMoral4);
        }
        private void btnMoral5_Click(object sender, EventArgs e)
        {
            PulsarBotonMoral(btnMoral5);
        }
        private void btnMoral6_Click(object sender, EventArgs e)
        {
            PulsarBotonMoral(btnMoral6);
        }

        #endregion

        #region Timers

        //metodo llamado durante la carga del formulario que recoge los distintos timers que posee
        private void IniciarTemporizadores()
        {
            //timer de actualizacion de valor de barras de progreso de personajes (aumentan)
            timerBarras.Interval = 2500;
            timerBarras.Tick += new EventHandler(timerBarras_Tick);
            timerBarras.Start();

            //timer de actuaalizacion de bsarras de paciente (se reducen)
            timerpaciente.Interval = 1500;
            timerpaciente.Tick += new EventHandler(timerpaciente_Tick);
            timerpaciente.Start();

            //timer que gestiona el descenso de labels de amenzas (valor varia dependiendo del momento de juego
            timerDesplazamientoLabels.Interval = velocidadAmenazas;
            timerDesplazamientoLabels.Tick += timerDesplazamientoLabels_Tick;
            timerDesplazamientoLabels.Start();

            //tiempo de visualizacion de resultado de dado de salud
            timerdados.Interval = 1000;
            timerdados.Tick += new EventHandler(timerdados_Tick);

            //tiempo de visualizacion de resultado de dado de moral
            timerDados2.Interval = 1000;
            timerDados2.Tick += new EventHandler(timerDados2_Tick);

            //timer que marca el tiempo maxiomo de juego
            timer1.Interval = 229000;
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Start();

            //timer que marca el segundo incremento de dificultad
            timerDificultad2.Interval = 205000;
            timerDificultad2.Tick += new EventHandler(timerDificultad2_Tick);
            timerDificultad2.Start();

            //timer que marca el primer salto de dificultad
            timerDificultad.Interval = intervaloDificultad;
            timerDificultad.Tick += new EventHandler(timerDificultad_Tick);
            timerDificultad.Start();

            //tiempo de visualizacion de mensaje positivo
            timerMensajePositivo.Interval = 2500;
            timerMensajePositivo.Tick += new EventHandler(timerMensajePositivo_Tick);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            sonido.Stop();
            this.Close();
            cierre form = new cierre();
            form.Show();
        }
        private void timerDificultad_Tick(object sender, EventArgs e)
        {
            velocidadAmenazas = 200;
            timerDificultad.Stop();
        }
        private void timerDificultad2_Tick(object sender, EventArgs e)
        {
            velocidadAmenazas = 70;
            timerDificultad2.Stop();
        }
        private void timerdados_Tick(object sender, EventArgs e)
        {
            pcbResultadoSalud.Visible = false;
            timerdados.Stop();
        }
        private void timerDados2_Tick(object sender, EventArgs e)
        {
            pcbResultadoMoral.Visible = false;
            timerDados2.Stop();
        }
        private void timerBarras_Tick(object sender, EventArgs e)
        {
            //A cada personaje le aumenta el valor de su barra de rpogreso conforme a su atributo velocidad de carga
            foreach (Persona p in personas)
            {
                if (p.Bar.Value + p.IntVelocidadCarga < p.Bar.Maximum)
                {
                    p.Bar.Value += p.IntVelocidadCarga;
                }
                else
                {
                    p.Bar.Value = p.Bar.Maximum;
                }
            }
        }
        private void timerpaciente_Tick(object sender, EventArgs e)
        {
            //si el paciente pierdo toda la salud o la desesperanza absoluta, da por finalizado el juego
            if (paciente.Bar.Value == 0 || paciente.PrbMoral.Value == 0)
            {
                timerpaciente.Stop();
                this.Close();
                cierre form = new cierre();
                form.Show();
            }
            else//si no, reduce un punto a cada barra de progreso
            {
                paciente.Bar.Value -= 1;
                paciente.PrbMoral.Value -= 1;
            }
        }
        private void timerDesplazamientoLabels_Tick(object sender, EventArgs e)
        {
            if (seleccionada != null)
            {
                timerDesplazamientoLabels.Interval = velocidadAmenazas;
                MoverLabel(seleccionada);
            }
        }
        private void timerMensajePositivo_Tick(object sender, EventArgs e)
        {
            lblRecuerdosyAfecto.Visible = false;
            timerMensajePositivo.Stop();
        }

        #endregion

        #region Dados

        //metodo que recoge un boton y le aplica la imagen correspondiente al valor de defensa sacado
        //lo muestra por un momento y regresa a mostrar el boton por defecto
        private void PintarBotonSalud(System.Windows.Forms.Button btn)
        {
            pcbResultadoSalud.BackgroundImage = dadosBotonSalud[def - 1];
            pcbResultadoSalud.BackgroundImageLayout = ImageLayout.Stretch;
            pcbResultadoSalud.Visible = true;
            timerdados.Start();
            btn.BackgroundImage = dadoDeshabilitado;

        }

        //Analogo al metodo anterior para dano moral
        private void PintarBotonMoral(System.Windows.Forms.Button btn)
        {
            pcbResultadoMoral.BackgroundImage = dadosBotonMoral[def - 1];
            pcbResultadoMoral.BackgroundImageLayout = ImageLayout.Stretch;
            pcbResultadoMoral.Visible = true;
            timerDados2.Start();
            btn.BackgroundImage = dadoDeshabilitado;
        }

        private void PintarDadoDefault(System.Windows.Forms.Button btn)
        {
            btn.BackgroundImage = dadoDeshabilitado;
            btn.BackgroundImageLayout = ImageLayout.Stretch;
        }

        //metodo ue rellena las listas de imagenes de dados para botones que se usaran durante el juego
        private void RellenarDadosBotones()
        {
            dadosBotonSalud.Add(Properties.Resources.inverted_dice_1);
            dadosBotonSalud.Add(Properties.Resources.inverted_dice_2);
            dadosBotonSalud.Add(Properties.Resources.inverted_dice_3);
            dadosBotonSalud.Add(Properties.Resources.inverted_dice_4);
            dadosBotonSalud.Add(Properties.Resources.inverted_dice_5);
            dadosBotonSalud.Add(Properties.Resources.inverted_dice_6);

            dadosBotonMoral.Add(Properties.Resources.inverted_dice_1__2_);
            dadosBotonMoral.Add(Properties.Resources.inverted_dice_2__2_);
            dadosBotonMoral.Add(Properties.Resources.inverted_dice_3__2_);
            dadosBotonMoral.Add(Properties.Resources.inverted_dice_4__2_);
            dadosBotonMoral.Add(Properties.Resources.inverted_dice_5__1_);
            dadosBotonMoral.Add(Properties.Resources.inverted_dice_6__1_);

        }

        #endregion

        #region Sistema

        //boton de salida del programa
        private void btnSalida_Click(object sender, EventArgs e)
        {
            timerAparicionLabels.Stop();
            timerDificultad.Stop();
            sonido.Stop();
            Application.Exit();
        }

        //metodo de reproduccion de musica
        private void PlayBackground()
        {
            Stream audioStream = new MemoryStream(Properties.Resources.comfortNumb);
            sonido = new SoundPlayer(audioStream);
            sonido.Play();
        }

        #endregion

    }
}
