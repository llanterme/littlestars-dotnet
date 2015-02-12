using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleStars.Domain.Entities
{
public class SessionEntity
 {
 public int SessionId { get; set; }
 public string StartDate { get; set; }
 public string StartTime { get; set; }
 public string Duration { get; set; }
 public string Status { get; set; }
 public string Alias { get; set; }
 }
}
