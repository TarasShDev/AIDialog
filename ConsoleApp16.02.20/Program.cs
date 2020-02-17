using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;

namespace AIDialog
{
    class Program
    {
        static AISpeaker Speaker = new AISpeaker();

        static void Main(string[] args)
        {
            Speaker.StartCommunication();
        }
    }
}
