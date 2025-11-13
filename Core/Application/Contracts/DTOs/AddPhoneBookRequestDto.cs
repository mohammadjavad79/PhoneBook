using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.DTOs
{
    public class AddPhoneBookRequestDto
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
    }
}
