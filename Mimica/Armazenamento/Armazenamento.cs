using Mimica.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mimica.Armazenamento
{
    public class Armazenamento
    {
        public static Jogo Jogo { get; set; }
        public static short Atual { get; set; }

        public static string[][] Palavras = { 
            
            //fáceis
            new string [] {"Olho", "Língua", "Chinelo"},
            //media
            new string [] {"Carpinteiro", "Amarelo", "Abacaxi", "Limão", "Abelha"},
            //dificeis
            new string [] {"Cisterna", "Procurando nemo", "Monitor"},

        };

    }
}
