using Mimica.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace Mimica.ViewModel
{
    public class InicioViewModel : INotifyPropertyChanged
    {
        public Jogo Jogo { get; set; }
        public Command IniciarCommand { get; set; }

        private string _MsgErro;
        public string MsgErro { get { return _MsgErro; } set { _MsgErro = value; OnPropertyChanged("MsgErro"); } }
        
        public InicioViewModel()
        {
            IniciarCommand = new Command(IniciarJogo);
            Jogo = new Jogo();
            Jogo.Grupo1 = new Grupo();
            Jogo.Grupo2 = new Grupo();

            Jogo.Tempo = 120;
            Jogo.Rodadas = 2;
        }
        private void IniciarJogo()
        {
            string error = "";
            if(Jogo.Tempo < 10)
            {
                error += "O tempo mínimo para o tempo é 10 segundos.";
            } 
            if(Jogo.Rodadas < 1)
            {
                error += "\nO valor mínimo de rodadas é 1.";
            }
            if(error.Length > 0)
            {
                MsgErro = error;
            }
            else
            {
                Armazenamento.Armazenamento.Jogo = this.Jogo;
                Armazenamento.Armazenamento.Atual = 1;
                App.Current.MainPage = new View.Jogo(Jogo.Grupo1);
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string NameProperty)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(NameProperty));
            }
        }
    }
}
