using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Permissions;

namespace UOAmarking.Models
{
    public class MarkerCoordinator
    {
        [Key] public int Id { get; set; }

        [Required] public string email { get; set; }

        public string name { get; set; }


    }
}
