using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using LittleStars.Domain.Entities;
using LittleStars.Service.Wrappers;

namespace LittleStars.Service
{
 
 [ServiceContract]
 public interface IRestService
 {
  [OperationContract]
  [Description("Gets a overview of a profile")]
  [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
  Method = "GET",
  UriTemplate = "/ProfileOverview/{email}",
  RequestFormat = WebMessageFormat.Json,
  ResponseFormat = WebMessageFormat.Json)]
  ProfileOverviewResponse GetProfileOverview(string email);

  [OperationContract]
  [Description("Gets all open sessions")]
  [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
  Method = "GET",
  UriTemplate = "/OpenSessions",
  RequestFormat = WebMessageFormat.Json,
  ResponseFormat = WebMessageFormat.Json)]
  SessionsResponse GetOpenSessions();


  [OperationContract]
  [Description("create a new profile")]
  [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest,
  Method = "POST",
  UriTemplate = "/CreateProfile",
  RequestFormat = WebMessageFormat.Json,
  ResponseFormat = WebMessageFormat.Json)]
  AbstractResponse CreateProfile(ProfileEntity profile);


  [OperationContract]
  [Description("create a new student")]
  [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest,
  Method = "POST",
  UriTemplate = "/CreateStudent",
  RequestFormat = WebMessageFormat.Json,
  ResponseFormat = WebMessageFormat.Json)]
  AbstractResponse CreateStudent(StudentEntity student);

  [OperationContract]
  [Description("create a new session")]
  [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest,
  Method = "POST",
  UriTemplate = "/CreateSession",
  RequestFormat = WebMessageFormat.Json,
  ResponseFormat = WebMessageFormat.Json)]
  AbstractResponse CreateSession(SessionEntity session);

  [OperationContract]
  [Description("Register a student for a session")]
  [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest,
  Method = "POST",
  UriTemplate = "/RegisterStudent",
  RequestFormat = WebMessageFormat.Json,
  ResponseFormat = WebMessageFormat.Json)]
  AbstractResponse RegisterStudent(RegisterEntity register);


  [OperationContract]
  [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
  UriTemplate = "/UploadFile?fileName={fileName}")]
  AbstractResponse UploadCustomFile(string fileName, Stream stream);


 }


}
