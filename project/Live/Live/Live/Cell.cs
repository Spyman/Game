using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Live
{
    class Cell
    {
       public short Type;
       public int CodeOfEll;
       public Cell(short Type, int CodeOfEll)
       {
           this.Type = Type;
           this.CodeOfEll = CodeOfEll; 
       }
       public Cell()
       {
       }
    }
}
