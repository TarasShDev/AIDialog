using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIDialog.Entities
{
    public class AnswerWithCat
    {
        public string Category { get; set; }
        public List<string> Words { get; set; }
        public string Answer { get; set; }
    }
}
