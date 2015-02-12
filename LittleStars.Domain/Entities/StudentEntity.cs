using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleStars.Domain.Entities
{
 public class StudentEntity
 {
  public int StudentId { get; set; }
  public int ProfileId { get; set; }
  public string Name { get; set; }
  public string Birthday { get; set; }
  public String ImageUrl { get; set; } 
 }
}
