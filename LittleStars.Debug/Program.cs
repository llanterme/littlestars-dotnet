using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LittleStars.Domain.Entities;
using LittleStars.Domain.Logic;
using LittleStars.Infrastructure.Catalog;

namespace LittleStars.Debug
{
 class Program
 {
  static void Main(string[] args)
  {

   var manager = new CoreLogic();
   var response = manager.CreateStudent(new StudentEntity
   {
   Name = "Adam",
   Birthday = "04/08/2014",
   ProfileId = 1

   });

   Console.WriteLine(response);
   Console.ReadLine();
  }
 }
}
