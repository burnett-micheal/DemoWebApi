using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestWebApi.Models
{
    public class Data_IdModel
    {
        [Required]
        [Range(1, Int32.MaxValue)]
        public int id { get; set; }
    }
}
