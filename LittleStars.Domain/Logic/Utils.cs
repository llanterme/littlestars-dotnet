using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LittleStars.Infrastructure.Catalog;
using LittleStars.Infrastructure.Uow;
using Mweb.Foundation.Practices.Logging;

namespace LittleStars.Domain.Logic
{
 public static class Utils
 {
  private static readonly ILogger Log = LogManager.GetLogger(typeof(Utils));

  internal static bool IsProfileRegistered(string emailAddress)
  {
   using (var uow = new Uow<Profile>())
   {
    var emailQuery = (from allUsers in uow.Repository.Find(a => a.Email.ToLower() == emailAddress.ToLower() )
     select allUsers).SingleOrDefault();
     if (emailQuery == null)
    {
     return false;
    }
    return true;
   }
  }

  internal static bool IsStudentRegistered(string name, int profileId)
  {
   using (var uow = new Uow<Student>())
   {
    var emailQuery = (from allUsers in uow.Repository.Find(a => a.Name.ToLower() == name.ToLower() && a.ProfileId == profileId)
                      select allUsers).SingleOrDefault();
    if (emailQuery == null)
    {
     return false;
    }
    return true;
   }
  }
 


 }
}
