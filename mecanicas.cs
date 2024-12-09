using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDI_Tarea7
{
    public class Mecanicas
    {
        //clase donde se definen distintas mecanicas y herramientas de juego
       
        //metodo que evalua cuantos dados se recogen dependiendo del valor que tenga la barra de progreso
        //del personaje correspondiente en el momento que el jugador pulse su boton.
        //recibe un personaje y sus rangos de valor de ProgressBar para anadir dados
        //Devuelve el numero de dados activados
        internal int calcularTotalDados(Persona p, int rango1, int rango2)
        {
            if (p.Bar.Value < rango1)//si no llega al nivel minimo
            {
                return 0;
            }
            else if (p.Bar.Value >= rango1 && p.Bar.Value < rango2)//cuando se encuentra entre los 2 rangos definidos
            {
                return 1;
            }
            else if (p.Bar.Value >= rango2 && p.Bar.Value < p.Bar.Maximum)//cuando sobrepasa el rango superior, pero no llega a valor maximo
            {
                return 2;
            }
            else //si la barra alcanza en valor maximo, devuelve el numero de dados maximo definido para cada persona
            {
                if (p is Enfermera)
                { 
                    Enfermera enf = p as Enfermera;
                    return enf.IntCuidados;
                }
                if (p is Doctor)
                { 
                    Doctor doctor = p as Doctor;
                    return doctor.IntDiagnosis;
                }
                if (p is Psicologo)
                { 
                    Psicologo psicologo = p as Psicologo;
                    return psicologo.IntTerapias;
                }
                return 0;
            }
        }

        /// <summary>
        /// Metodo que recibe los botones pulsadores de dados de salud y el numero de ellos que hay que activar
        /// y mediante un bucle for, recorre y activa en caso de estar desactivados de uno en uno hasta alcanzar el numero 
        /// de dados a activar recibido
        /// </summary>
        internal void HabilitarDadosSalud(int totaldados, Button btn1, Button btn2, Button btn3, Button btn4, Button btn5, Button btn6)
        { 
            for (int i = 0;i < totaldados; i++)
            {
                if (!btn1.Enabled) 
                {
                    btn1.Enabled = true;
                    btn1.BackgroundImage = Properties.Resources.dadoactivoverde;
                    btn1.BackgroundImageLayout = ImageLayout.Stretch;
                    continue;
                }
                if (!btn2.Enabled) 
                { 
                    btn2.Enabled = true;
                    btn2.BackgroundImage = Properties.Resources.dadoactivoverde;
                    btn2.BackgroundImageLayout = ImageLayout.Stretch;
                    continue;
                }
                if (!btn3.Enabled)
                {
                    btn3.Enabled = true;
                    btn3.BackgroundImage = Properties.Resources.dadoactivoverde;
                    btn3.BackgroundImageLayout = ImageLayout.Stretch; //break;
                    continue;
                }
                if (!btn4.Enabled)
                {
                    btn4.Enabled = true;
                    btn4.BackgroundImage = Properties.Resources.dadoactivoverde;
                    btn4.BackgroundImageLayout = ImageLayout.Stretch;
                    continue;
                }
                if (!btn5.Enabled)
                {
                    btn5.Enabled = true;
                    btn5.BackgroundImage = Properties.Resources.dadoactivoverde;
                    btn5.BackgroundImageLayout = ImageLayout.Stretch;
                    continue;
                }
                if (!btn6.Enabled)
                {
                    btn6.Enabled = true;
                    btn6.BackgroundImage = Properties.Resources.dadoactivoverde;
                    btn6.BackgroundImageLayout = ImageLayout.Stretch;
                    continue;
                }
            }
        }

        /// <summary>
        /// Metodo analogo al anterior, para los dados de moral
        /// </summary>
        internal void HabilitarDadosMoral(int totaldados, Button btn1, Button btn2, Button btn3, Button btn4, Button btn5, Button btn6)
        {
            for (int i = 0; i < totaldados; i++)
            {
                if (!btn1.Enabled)
                {
                    btn1.Enabled = true;
                    btn1.BackgroundImage = Properties.Resources.dadoactivomagenta;
                    btn1.BackgroundImageLayout = ImageLayout.Stretch;
                    continue;
                }
                if (!btn2.Enabled)
                {
                    btn2.Enabled = true;
                    btn2.BackgroundImage = Properties.Resources.dadoactivomagenta;
                    btn2.BackgroundImageLayout = ImageLayout.Stretch;
                    continue;
                }
                if (!btn3.Enabled)
                {
                    btn3.Enabled = true;
                    btn3.BackgroundImage = Properties.Resources.dadoactivomagenta;
                    btn3.BackgroundImageLayout = ImageLayout.Stretch;
                    continue;

                }
                if (!btn4.Enabled)
                {
                    btn4.Enabled = true;
                    btn4.BackgroundImage = Properties.Resources.dadoactivomagenta;
                    btn4.BackgroundImageLayout = ImageLayout.Stretch;
                    continue;
                }
                if (!btn5.Enabled)
                {
                    btn5.Enabled = true;
                    btn5.BackgroundImage = Properties.Resources.dadoactivomagenta;
                    btn5.BackgroundImageLayout = ImageLayout.Stretch;
                    continue;
                }
                if (!btn6.Enabled)
                {
                    btn6.Enabled = true;
                    btn6.BackgroundImage = Properties.Resources.dadoactivomagenta;
                    btn6.BackgroundImageLayout = ImageLayout.Stretch;
                    continue;
                }

            }
        }

        /// <summary>
        /// Metodo que se llama desde el evento click de famila, cuando la barra de progreso de familia est'a lo bastante cargada
        /// (mas del 40%), muestra un mensaje de carino aleatorio y realiza un incremento aleatorio en las barras de vida y moral
        /// del paciente y reduce el valor de la barra de familia en 40 %.
        /// El metodo controla que el aumento de valor de las barras del paciente no sobrepase 100, en cuyo cayo le da valor maximo
        /// </summary>
        internal void RecibirAfecto(Familiar f, Paciente pac, Label lb, List<CarinoFamilia> carino)
        {
            if(f.Bar.Value <= 40) 
            {
                return;
            }
            else 
            { 
                f.Bar.Value -= 40;

                lb.Visible = true;
                Random random = new Random();
                Random random1 = new Random();
                int incremento = random.Next(30, 50);
                lb.Text = carino[random1.Next(carino.Count)].StrDescripcion;

                if (pac.Bar.Value + incremento < 99)
                {
                    pac.Bar.Value += incremento;
                }
                else 
                {
                    pac.Bar.Value = 100;
                }

                if(pac.PrbMoral.Value + incremento < 99) 
                {
                    pac.PrbMoral.Value += incremento;
                }
                else 
                {
                    pac.PrbMoral.Value = 100;
                }
            }
        }

        //Clase pensada para una mecánica de calculo de refuerzo familiar distinta a la implementada,
        //Se mantiene porque en este motor de juego es  la base de donde se extraen los textos de apoyo que se
        //muestran al pulsar el boton de cariño familiar
        internal class CarinoFamilia
        {
            string _strDescripcion;
            int _intDadosSalud, _intDadosMoral;

            public CarinoFamilia(string strDescripcion, int intDadosSalud, int intDadosMoral)
            {
                _strDescripcion = strDescripcion;
                _intDadosSalud = intDadosSalud;
                _intDadosMoral = intDadosMoral;
            }

            public string StrDescripcion { get => _strDescripcion; set => _strDescripcion = value; }
            public int IntDadosSalud { get => _intDadosSalud; set => _intDadosSalud = value; }
            public int IntDadosMoral { get => _intDadosMoral; set => _intDadosMoral = value; }

        }

        //clase que define el objeto AtaqueSalud, que posee atributos de nombre y dano que realiza a la salud del paciente
        internal class AtaqueSalud
        {
            string _strDescripcion;
            int _intDanoSalud;

            public AtaqueSalud(string strDescripcion, int intDanoSalud)
            {
                _strDescripcion = strDescripcion;
                _intDanoSalud = intDanoSalud;
            }

            public string StrDescripcion { get => _strDescripcion; set => _strDescripcion = value; }
            public int IntDanoSalud { get => _intDanoSalud; set => _intDanoSalud = value; }
        }

        //clase que define el objeto AtaqueMoral, que posee atributos de nombre y dano que hace a la moral del paciente
        internal class AtaqueMoral
        {
            string _strDescripcion;
            int _intDanoMoral;

            public AtaqueMoral(string strDescripcion, int intDanoMoral)
            {
                _strDescripcion = strDescripcion;
                _intDanoMoral = intDanoMoral;
            }

            public string StrDescripcion { get => _strDescripcion; set => _strDescripcion = value; }
            public int IntDanoMoral { get => _intDanoMoral; set => _intDanoMoral = value; }

        }

    } 

    

    

    
}
