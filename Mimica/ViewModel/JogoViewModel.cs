using Mimica.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace Mimica.ViewModel
{
    public class JogoViewModel : INotifyPropertyChanged
    {
        public Grupo Grupo { get; set; }
        public string NomeGrupo { get; set; }
        public string NumGrupo { get; set; }

        private string _Palavra;
        public string Palavra { get { return _Palavra; } set { _Palavra = value; OnPropertyChanged("Palavra") ; } }
        
        public string _TextoContagem;
        public string TextoContagem { get{ return _TextoContagem; } set { _TextoContagem = value; OnPropertyChanged("TextoContagem"); } }

        public byte _PalavraPontuacao;
        public byte PalavraPontuacao { get { return _PalavraPontuacao; } set { _PalavraPontuacao = value; OnPropertyChanged("PalavraPontuacao"); } }
        
        private bool _ContainerContagem;
        public bool ContainerContagem { get{ return _ContainerContagem; } set { _ContainerContagem = value; OnPropertyChanged("ContainerContagem"); } }
        
        private bool _ContainerIniciar;
        public bool ContainerIniciar { get { return _ContainerIniciar; } set { _ContainerIniciar = value; OnPropertyChanged("ContainerIniciar"); } }
        
        private bool _BtMostrar;
        public bool BtMostrar { get { return _BtMostrar; } set { _BtMostrar = value; OnPropertyChanged("BtMostrar"); } }

        public Command Mostrar { get; set; }
        public Command Acertou { get; set; }
        public Command Errou { get; set; }
        public Command Iniciar { get; set; }
        

        public JogoViewModel(Grupo grupo)
        {
            Grupo = grupo;
            NomeGrupo = grupo.Nome;

            if(grupo == Armazenamento.Armazenamento.Jogo.Grupo1)
            {
                NumGrupo = "Grupo - 1";
            }
            else
            {
                NumGrupo = "Grupo - 2";
            }

            ContainerContagem = false;
            ContainerIniciar = false;
            BtMostrar = true;

            Palavra = "******";
            
            Mostrar = new Command(MostrarPalavraAction);
            Acertou = new Command(AcertouAction);
            Errou = new Command(ErrouAction);
            Iniciar = new Command(IniciarAction);
        }
        public string Random(int NumNivel)
        {
            Random rd = new Random();
            int ind = rd.Next(0, Armazenamento.Armazenamento.Palavras[NumNivel - 1].Length);
            Palavra = Armazenamento.Armazenamento.Palavras[NumNivel - 1][ind];

            return Palavra;
        }
        private void MostrarPalavraAction()
        {
            Palavra = "Abre a Janela!";
            PalavraPontuacao = 3;
            var NumNivel = Armazenamento.Armazenamento.Jogo.NivelNumerico;

            switch (NumNivel)
            {
                case 0:
                    Random rd = new Random();
                    int niv = rd.Next(0, 3);
                    int ind = rd.Next(0, Armazenamento.Armazenamento.Palavras[niv].Length);
                    Palavra = Armazenamento.Armazenamento.Palavras[niv][ind];
                    PalavraPontuacao = (byte)((niv==0)? 1 : (niv==1) ? 3 : 5);
                    break;
                case 1:
                    Palavra = Random(NumNivel);
                    PalavraPontuacao = 1;
                    break;
                case 2:
                    Palavra = Random(NumNivel);
                    PalavraPontuacao = 3;
                    break;
                case 3:
                    Palavra = Random(NumNivel);
                    PalavraPontuacao = 5;
                    break;
            }

            BtMostrar = false;
            ContainerIniciar = true;
        }
        private void IniciarAction()
        {
            ContainerIniciar = false;
            ContainerContagem = true;


            int i = Armazenamento.Armazenamento.Jogo.Tempo;
            TextoContagem = i.ToString();
            i--;
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                TextoContagem = i.ToString();
                i--;
                if (i < 0)
                {
                    TextoContagem = "Tempo esgotado!";
                }
                return true;
            });
        }

        private void AcertouAction()
        {
            Grupo.Pontuacao += PalavraPontuacao;
            ProximoGrupo();
        }

        private void ErrouAction()
        {
            ProximoGrupo();
        }
        private void ProximoGrupo()
        {
            Grupo grupo;
            if (Armazenamento.Armazenamento.Jogo.Grupo1 == Grupo)
            {
                grupo = Armazenamento.Armazenamento.Jogo.Grupo2;
            }
            else
            {
                grupo = Armazenamento.Armazenamento.Jogo.Grupo1;
                Armazenamento.Armazenamento.Atual++;
            }
            if (Armazenamento.Armazenamento.Atual > Armazenamento.Armazenamento.Jogo.Rodadas)
            {
                App.Current.MainPage = new View.Resultado();
            }
            else
            {
                App.Current.MainPage = new View.Jogo(grupo);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string NameProperty)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(NameProperty));
            }
        }
    }
}
