using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using LittleStars.Domain.Entities;
using LittleStars.Infrastructure.Catalog;
using LittleStars.Infrastructure.Uow;
using Mweb.Foundation.Practices.Logging;

namespace LittleStars.Domain.Logic
{
 public class CoreLogic
 {
  private static readonly ILogger Log = LogManager.GetLogger(typeof(CoreLogic));

  public string CreateProfile(ProfileEntity newProfile)
  {
   string methodName = MethodBase.GetCurrentMethod().Name;
   var rnd = new Random();
   var profilePin = rnd.Next(1, 9999);
   try
   {
    using (var uow = new Uow<Profile>())
    {
     if (!Utils.IsProfileRegistered(newProfile.Email))
     {
     var profile = uow.Repository.Insert(new Profile
      {
       Email = newProfile.Email,
       Cell = newProfile.Cell,
       Surname = newProfile.Surname,
       Name = newProfile.Name,
       Pin = profilePin
      });

     uow.Save();
     return profile.ProfileId.ToString(CultureInfo.InvariantCulture) + "|" + profilePin;
     }
    }
   }
   catch (Exception ex)
   {
     Log.Info(methodName, string.Format("Create Profile Exception Occured: {0}", ex.Message));
   }
   return string.Empty;
  }

  public int CreateStudent(StudentEntity newStudent)
  {

   string methodName = MethodBase.GetCurrentMethod().Name;
   
   try
   {
    using (var uow = new Uow<Student>())
    {

     if (!Utils.IsStudentRegistered(newStudent.Name, newStudent.ProfileId))
     {
      var student = uow.Repository.Insert(new Student
      {
       ProfileId = newStudent.ProfileId,
       Birthday = newStudent.Birthday,
       Name = newStudent.Name,
       Image = newStudent.ImageUrl
      
      });
      uow.Save();
      return student.StudentId;
     }
    }
    }
   
   catch (Exception ex)
   {
    Log.Info(methodName, string.Format("Create Studentr Exception Occured: {0}", ex.Message));
   }
   return 0;

  }

  public int CreateSession(SessionEntity newSession)
  {
   string methodName = MethodBase.GetCurrentMethod().Name;
   try
   {
    using (var uow = new Uow<Session>())
    {
    
      var session = uow.Repository.Insert(new Session
      {
       Alias = newSession.Alias,
       Duration = newSession.Duration,
       StartDate = newSession.StartDate,
       StartTime = newSession.StartTime,
       Status = "Open"
      });

      uow.Save();
      return session.SessionId;
     
    }
   }
   catch (Exception ex)
   {
    Log.Info(methodName, string.Format("Create Session Exception Occured: {0}", ex.Message));
   }
   return 0;
  }

  public int RegisterStudent(RegisterEntity newRegister)
  {
   string methodName = MethodBase.GetCurrentMethod().Name;
   try
   {
    using (var uow = new Uow<Register>())
    {

     var register = uow.Repository.Insert(new Register
     {
      SessionId = newRegister.SessionId,
      Payment = newRegister.Payment,
      StudentId = newRegister.StudentId
     });

     uow.Save();
     return register.SessionId;

    }
   }
   catch (Exception ex)
   {
    Log.Info(methodName, string.Format("Create Register Exception Occured: {0}", ex.Message));
   }
   return 0;
  }

  public bool SaveImageUrl(int studentId, string imageUrl)
  {
     string methodName = MethodBase.GetCurrentMethod().Name;
   
   try
   {
    using (var uow = new Uow<Student>())
    {

     var studentQuery = (from allStudents in uow.Repository.Find(a => a.StudentId == studentId)
      select allStudents).First();

     var student = new Student
     {
      ProfileId = studentQuery.ProfileId,
      Birthday = studentQuery.Birthday,
      Name = studentQuery.Name,
      Image = imageUrl
     
    };

    
     uow.Repository.Update(student);
     uow.Save();
     return true;
     }
    
    }
   
   catch (Exception ex)
   {
    Log.Info(methodName, string.Format("Create Studentr Exception Occured: {0}", ex.Message));
    return false;
   }


  }
  
  public ProfileEntity GetProfileOverview(string email)
  {

   string methodName = MethodBase.GetCurrentMethod().Name;
   try
   {
    using (var uow = new Uow<Profile>())
    {

     var profileQuery = (from allUsers in uow.Repository.Find(a => a.Email.ToLower() == email.ToLower())
                         select allUsers).Single();

     var profileOverview = new ProfileEntity
     {
      Cell = profileQuery.Cell,
      Email = email,
      Name = profileQuery.Name,
      ProfileId = profileQuery.ProfileId,
      Surname = profileQuery.Surname,
      Students = GetStudents(profileQuery.Students),

     };

     return profileOverview;
    }

   }
   catch (Exception ex)
   {
    Log.Info(methodName, string.Format("Profile Overview Exception Occured: {0}", ex.Message));
   }
   return null;
  }

  public List<SessionEntity> GetOpenSessions()
  {
   string methodName = MethodBase.GetCurrentMethod().Name;
   try
   {
    using (var uow = new Uow<Session>())
    {

     var sessionQuery = (from allSessions in uow.Repository.Find(a => a.Status == "Open")
                         select allSessions);

     var openSessions = sessionQuery.Select(session => new SessionEntity
     {
      Alias = session.Alias, Duration = session.Duration, Status = session.Status, SessionId = session.SessionId, StartDate = session.StartDate, StartTime = session.StartTime
     }).ToList();


     return openSessions;
    }

   }
   catch (Exception ex)
   {
    Log.Info(methodName, string.Format("Open Sessions Exception Occured: {0}", ex.Message));
   }
   return null;
  } 

  private static List<StudentEntity> GetStudents(IEnumerable<Student> students)
  {
   return students.Select(child => new StudentEntity
   {
    Name = child.Name,
    Birthday = child.Birthday,
    StudentId = child.StudentId,
    ImageUrl = System.Configuration.ConfigurationManager.AppSettings["ImageURL"] + "/" + child.Image
    
   }).ToList();
  } 
 
 }


 }

