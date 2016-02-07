using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    [Serializable()]
    public class Klasa
    {

        private string Okno;
        public int procent;
  
        public string okno { get; set; }

        public void NaWielkie()
        {
            Okno = Okno.ToUpper();
        }

    }
}
