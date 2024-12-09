using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDI_Tarea7
{
    /// <summary>
    /// Conjunto de clase que recoge una clase base llamada Persona, asi como 
    /// sus clases heredadas, qque reciben sus atributos y anaden los suyos porpios.
    /// Cada clase incluye sus atributos , su constructor y propiedades
    /// </summary>
    internal class Persona
    {
        private string _strNombre;
        private ProgressBar _bar;//barra de progreso asociada al objeto de clase
        private int _intVelocidadCarga;//cantidad de unidades que aumentara el valor de la barra de progreso en cada iteracion del timer

        public Persona(string strNombre, ProgressBar prgBar, int intVelocidadCarga)
        {
            _strNombre = strNombre;
            _bar = prgBar;
            _intVelocidadCarga = intVelocidadCarga;
        }

        public string StrNombre { get => _strNombre; set => _strNombre = value; }
        public ProgressBar Bar { get => _bar; set => _bar = value; }
        public int IntVelocidadCarga { get => _intVelocidadCarga; set => _intVelocidadCarga = value; }
    }

    internal class Enfermera : Persona
    {
        private int _intCuidados;//numero de dados de cuidados

        public Enfermera(string strNombre, ProgressBar prgbar, int intCuidados, int intVelocidadCarga) : base(strNombre, prgbar, intVelocidadCarga)
        {
            _intCuidados = intCuidados;
        }

        public int IntCuidados { get => _intCuidados; set => _intCuidados = value; }
    }

    internal class Doctor : Persona 
    {
        private int _intDiagnosis;

        public Doctor(string strNombre, ProgressBar prgBar, int intDiagnosis,int intVelocidadCarga): base(strNombre, prgBar, intVelocidadCarga) 
        {
            _intDiagnosis = intDiagnosis;
        }

        public int IntDiagnosis { get => _intDiagnosis; set => _intDiagnosis = value; }
    }

    internal class Psicologo : Persona
    {
        private int _intTerapias;

        public Psicologo(string strNombre, ProgressBar prgBar, int intTerapias, int intVelocidadCarga) : base(strNombre, prgBar, intVelocidadCarga)
        {
            _intTerapias = intTerapias;
        }

        public int IntTerapias { get => _intTerapias; set => _intTerapias = value; }
    }

    internal class Familiar: Persona
    {
        //atributo que esta en desuso para la mecanica de juego finalmente implementada
        private int _intAmor;

        public Familiar(string strNombre, ProgressBar prgBar, int intAmor, int intVelocidadCarga): base(strNombre, prgBar, intVelocidadCarga) 
        { 
            _intAmor = intAmor;
        }

        public int IntAmor { get => _intAmor; set => _intAmor = value; }
    }

    internal class Paciente: Persona 
    {
        private int _intVida;
        private int _intMoral;
        ProgressBar _prbMoral;//barra de progreso que indica el nivel de animo del paciente

        public Paciente(string strNombre,ProgressBar prgBar, int intvelocidadCarga, int intVida, int intMoral, ProgressBar prbMoral): base(strNombre, prgBar, intvelocidadCarga)
        {
            _intVida = intVida; 
            _intMoral = intMoral;
            _prbMoral = prbMoral;
        }

        public int IntVida { get => _intVida; set => _intVida = value; }
        public int IntMoral { get => _intMoral; set => _intMoral = value; }
        public ProgressBar PrbMoral { get => _prbMoral; set => _prbMoral = value; }
    }

}
