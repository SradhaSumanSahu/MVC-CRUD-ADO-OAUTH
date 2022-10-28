using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MVC_CRUD_ADO_OAUTH.Models
{
    public class Products
    {
        [Key]
        public int ProductID { get; set; }
        [Required]
        [DisplayName("Product Name")]
        public string ProductName { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Qty { get; set; }
        public string Remark { get; set; }
    }
}