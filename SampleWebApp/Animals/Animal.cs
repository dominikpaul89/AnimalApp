using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWebApp.Animals
{
    public class Animal
    {
        public string IdAnimal
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public string Description
        {
            get;
            set;
        }
        public string Category
        {
            get;
            set;
        }
        public string Area
        {
            get;
            set;
        }

        public Animal IfValuesAreNull(Animal a, Animal b)
        {
            if (b.Name == null)
            {
                b.Name = a.Name;
            }
            if (b.Description == null)
            {
                b.Description = a.Description;
            }
            if (b.Category == null)
            {
                b.Category = a.Category;
            }
            if (b.Area == null)
            {
                b.Area = a.Area;
            }

            return b;
        }
    }

}