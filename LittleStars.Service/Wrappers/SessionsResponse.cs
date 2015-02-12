using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LittleStars.Domain.Entities;

namespace LittleStars.Service.Wrappers
{
 public class SessionsResponse : AbstractResponse
 {
  public List<SessionEntity> Sessions { get; set; }
 }
}