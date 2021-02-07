using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OdeToFood.Core
{
    public class Restaurant
    {        
        public int Id { get; set; }
        [Required(ErrorMessage ="Nome é obrigatório"), StringLength(80)]
        public string Name { get; set; }
        [Required]
        public string Location { get; set; }

        public CuisineType Cuisine { get; set; }
    }
}
