using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvGenerator.Models
{
    public class Cv
    {
        public PersonalInfo PersonalInfo { get; set; }
        public List<Experience> Experience { get; set; }
        public string AdditionalInfo { get; set; }
    }
}
