using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Store.Models
{
    public class ProjectWithCategory
    {
        public int Id { get; set; }
        public string P_Name { get; set; }
        public string P_Desc { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Image { get; set; }
    }
}