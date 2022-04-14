using System;
using System.ComponentModel.DataAnnotations;

namespace CRUDelicious
{
    public class Dish
    {
        [Key]
        public int DishId {get;set;}
        [Required]
        public string ChefName {get;set;}
        [Required]
        public string DishName {get;set;}
        [Required]
        [Range(0,double.PositiveInfinity)]
        public int Calories {get;set;}
        [Required]
        public string Tastiness {get;set;}
        [Required]
        public string Description {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}