using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleStars.Domain.Entities
{
 public class RegisterEntity
 {
  public int RegisterId { get; set; }
  public int SessionId { get; set; }
  public int StudentId { get; set; }
  public string Payment { get; set; }


 }
}
