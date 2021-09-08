using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Dtos
{
    public class SiteDto
    {
        [Required]
        public string SiteName { get; set; }
    }
}
