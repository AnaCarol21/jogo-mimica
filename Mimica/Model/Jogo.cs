﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Mimica.Model
{
    public class Jogo
    {
        public Grupo Grupo1 { get; set; }
        public Grupo Grupo2 { get; set; }

        public int NivelNumerico { get; set; }
        public string Nivel { get; set; }
        public short Tempo { get; set; }
        public short Rodadas { get; set; }
    }
}
