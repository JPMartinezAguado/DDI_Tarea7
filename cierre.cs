using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DDI_Tarea7
{
    //Formulario de final de juego, que muestra el texto final y sale de la aplicacion
    public partial class cierre : Form
    {
        private int index = 0;
        private SoundPlayer player;

        public cierre()
        {
            InitializeComponent();

            //musica de fondo
            Stream audioStream = new MemoryStream(Properties.Resources.eclipse);
            player = new SoundPlayer(audioStream);
            player.PlayLooping();

            //timer para la muestra progresiva del texto
            timer1.Interval = 200;
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Start();
        }

        //timer de muestra  de texto. Desde la primera posicion del string, en cada iteracion 
        //muestra el caracter siguiente hasta que llega al final del string y para
        private void timer1_Tick(object sender, EventArgs e)
        {
            string texto = "Has llegado al final de este juego un poco tramposo...\n" +
                           "En realidad da igual como lo hiciste. Esta es una guerra\n" +
                           "que no tiene solución. Lo importante ha sido la batalla,\n" +
                           "la batalla del dia de hoy, en la que se luchó y se hizo\n" +
                           "todo lo posible para seguir otro dia.\n\n" +
                           "Las batallas mas épicas no se llevan a cabo en mundos mágicos\n" +
                           "con seres mitológicos y armas formidables.\n" +
                           "Se llevan a cabo cada dia en casas y hospitales, por gente\n" +
                           "corriente que hace lo extraordinario porque otro igual sigua,\n" +
                           "aunque solo un dia mas, acompañandonos.\n\n" +
                           "Éste es un recordatorio de lo que le debemos a los HÉROES\n" +
                           "de verdad, los de bata y jeringuilla, simplemente TODO\n\n" +
                           "..y a los que desde puestos de poder quieren sabotear el \n" +
                           "carácter universal de su ayuda, desearles que encuentren bajo\n" +
                           "tierra la humanidad que no demuestran sobre ella...\n\n" +
                           "See you on the Dark Side of the Moon...";

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

        //evento de salida
        private void button1_Click(object sender, EventArgs e)
        {
            player.Stop();
            Application.Exit();
        }
    }
}
