using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;

namespace HY.Frame.CMD
{
    class Program
    {
        static void Main()
        {
            //IRuner runer = new SvcSelect();
            //IRuner runer = new TemplateEG();
            IRuner runer = new DeflateTest();
            runer.Go();
        }
    }
}



