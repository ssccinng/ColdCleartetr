using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ColdCleartetr.Models
{
    public class Pet
    {
        public  string _0 { get; set; }
        public string _1 { get; set; }
        public string _2 { get; set; }
        public string _3 { get; set; }
        public string _4 { get; set; }
        public string _5 { get; set; }
        public string _6 { get; set; }

        //[Required]
        //public string Breed { get; set; }

        //public string Name { get; set; }

        //[Required]
        //public PetType PetType { get; set; }
    }

    public enum PetType
    {
        Dog = 0,
        Cat = 1
    }
}
