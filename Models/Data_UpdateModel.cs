using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestWebApi.Models
{
    public class Data_UpdateModel : Data_IdModel
    {

        [Required]
        public string data { get; set; }

        [Required]
        public string type { get; set; }

        [Required]
        [Range(1, Int32.MaxValue)]
        public int parentId { get; set; }
    }
}
