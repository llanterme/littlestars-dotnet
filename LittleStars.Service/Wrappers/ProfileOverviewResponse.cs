using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LittleStars.Domain.Entities;

namespace LittleStars.Service.Wrappers
{
 public class ProfileOverviewResponse :AbstractResponse
 {
  public ProfileEntity Profile { get; set; }
 }
}