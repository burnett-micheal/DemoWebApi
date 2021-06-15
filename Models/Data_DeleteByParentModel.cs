using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestWebApi.Models
{
    public class Data_DeleteByParentModel : Data_IdModel
    {
        [Required]
        public bool deleteParent { get; set; }
    }
}
