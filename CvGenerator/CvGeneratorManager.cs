using CvGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvGenerator
{
    public class CvGeneratorManager
    {
        ICvGenerator generator;

        public CvGeneratorManager(ICvGenerator generator)
        {
            this.generator = generator;
        }
        public bool CreateCv(Cv cv)
        {
            return generator.Generate(cv);
        }
       
    }
}
