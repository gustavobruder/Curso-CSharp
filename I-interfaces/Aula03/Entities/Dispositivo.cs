﻿namespace I_interfaces.Aula03.Entities
{
    public abstract class Dispositivo
    {
        public int Numero { get; set; }

        public abstract void ProcessarDocumento(string doc);
    }
}