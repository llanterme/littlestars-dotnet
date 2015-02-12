using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleStars.Domain.Entities
{
 public class ProfileEntity
 {
  public int ProfileId { get; set; }
  public string Surname { get; set; }
  public string Name { get; set; }
  public string Cell { get; set; }
  public string Email { get; set; }
  public List<StudentEntity> Students { get; set; }


 }
}
