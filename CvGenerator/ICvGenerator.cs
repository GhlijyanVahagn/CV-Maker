using CvGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvGenerator
{
    public interface ICvGenerator
    {
        bool Generate(Cv cv );
    }
}
