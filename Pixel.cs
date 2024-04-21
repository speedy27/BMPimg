using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pbscientfique
{
    public class Pixel
    {
        private byte rouge;
        private byte vert;
        private byte bleu;

        public Pixel(byte rouge, byte vert, byte bleu)
        {
            this.rouge = rouge;
            this.bleu = bleu;
            this.vert = vert;
        }

        public Pixel(Pixel c)
        {
            this.rouge = c.rouge;
            this.vert = c.vert;
            this.bleu = c.bleu;
        }


        public string toString()
        {
            return "" +this.rouge + " " + this.vert + " " + this.bleu + "";
        }


        public byte Rouge
        {
            get { return this.rouge; }
        }
        public byte Vert
        {
            get { return this.vert; }
        }
        public byte Bleu
        {
            get { return this.bleu; }
        }

    }
}
