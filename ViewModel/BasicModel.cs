using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AribTask.ViewModel
{
	public class BasicModel
	{
    [Key]
    public int Id { get; set; }
    [Display(Name = "Code")]
    [Required(ErrorMessage = "CodeRequired")]
    public int Code { get; set; }
    public DateTime? CreationDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public int? CreationUserId { get; set; }
    public int? UpdatedUserId { get; set; }
  }
}
