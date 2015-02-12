using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Web.Hosting;
using LittleStars.Domain.Entities;
using LittleStars.Domain.Logic;
using LittleStars.Service.Wrappers;

namespace LittleStars.Service
{

 [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
 public class RestService : IRestService
 {

  private readonly CoreLogic _manager;

  public RestService()
  {
   _manager = new CoreLogic();
  }

  public ProfileOverviewResponse GetProfileOverview(string email)
  {
   var profileOverview = _manager.GetProfileOverview(email);
   if (profileOverview != null)
   {
    return new ProfileOverviewResponse
    {
     Message = "success",
     Status = "success",
     Profile = profileOverview
    };
   }
   else
   {
    return new ProfileOverviewResponse
    {
     Status = "failed",
     Message = "Error fetching profile overview.",
     Profile = null
    };
   }

  }

  public SessionsResponse GetOpenSessions()
  {
   var sessionsList = _manager.GetOpenSessions();
   if (sessionsList != null)
   {
    return new SessionsResponse
    {
     Message = "success",
     Status = "success",
     Sessions = sessionsList
    };
   }
   else
   {
    return new SessionsResponse
    {
     Status = "failed",
     Message = "Error fetching open sessions.",
     Sessions = null
    };

   }
  }

  public AbstractResponse CreateProfile(ProfileEntity profile)
  {
   var newProfile = _manager.CreateProfile(profile);

   if (newProfile != string.Empty)
   {
    return new AbstractResponse
    {
     Message = newProfile.ToString(CultureInfo.InvariantCulture),
     Status = "success"
    };
   }

   return new AbstractResponse
   {
    Message = "failed",
    Status = "failed"
   };


  }

  public AbstractResponse CreateStudent(StudentEntity student)
  {

   var newStudent = _manager.CreateStudent(student);

   if (newStudent != 0)
   {
    return new AbstractResponse
    {
     Message = newStudent.ToString(CultureInfo.InvariantCulture),
     Status = "success"
    };
   }

   return new AbstractResponse
   {
    Message = "failed",
    Status = "failed"
   };
  }

  public AbstractResponse CreateSession(SessionEntity session)
  {
   var newSession = _manager.CreateSession(session);

   if (newSession != 0)
   {
    return new AbstractResponse
    {
     Message = newSession.ToString(CultureInfo.InvariantCulture),
     Status = "success"
    };
   }

   return new AbstractResponse
   {
    Message = "failed",
    Status = "failed"
   };
  }

  public AbstractResponse RegisterStudent(RegisterEntity register)
  {
   var newRegisterSession = _manager.RegisterStudent(register);

   if (newRegisterSession != 0)
   {
    return new AbstractResponse
    {
     Message = newRegisterSession.ToString(CultureInfo.InvariantCulture),
     Status = "success"
    };
   }

   return new AbstractResponse
   {
    Message = "failed",
    Status = "failed"
   };
  }

  public AbstractResponse UploadCustomFile(string fileName, Stream stream)
  {

   var studentData = fileName.Split('_');
   var profileId = studentData[1];
   var name = studentData[2];
   var birthday = studentData[3];
   var saveFileName = Guid.NewGuid() + studentData[0];

  
    string filePath = Path.Combine(HostingEnvironment.MapPath("~/_upload"), saveFileName);

    using (var writer = new FileStream(filePath, FileMode.Create))
    {
     int readCount;
     var buffer = new byte[8192];
     while ((readCount = stream.Read(buffer, 0, buffer.Length)) != 0)
     {
      writer.Write(buffer, 0, readCount);
     }
    }

    var studentId = _manager.CreateStudent(new StudentEntity
    {
     Name = name,
     Birthday = birthday,
     ImageUrl = saveFileName,
     ProfileId = int.Parse(profileId)
    });

    if (studentId != 0)
    {
     return new AbstractResponse
     {
      Message = studentId.ToString(CultureInfo.InvariantCulture),
      Status = "success"
     };
    }
   
    return new AbstractResponse
    {
     Message = "failed",
     Status = "failed"
    };
    
   }

  }
  

  }
 
 

 