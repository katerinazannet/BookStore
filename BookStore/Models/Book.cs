using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class Book
    {
        public int ID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "You must enter a Name.")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "Name must be at least 3 characters and maximum 60.")]
        public string Name { get; set; }

        // αυτα που ακολουθουν λεγονται data annotations
        [Display(Name = "Publication Date")] //με τα brackets κανω decoration
        [DataType(DataType.Date)]
        public DateTime PublicationDate { get; set; }

        [DataType(DataType.Currency)]
        [Range(1, 100, ErrorMessage = "Must be between 1 and 100 Euros.")]
        public decimal Price { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "You must enter a Genre.")]
        public string Genre { get; set; }
    }
}