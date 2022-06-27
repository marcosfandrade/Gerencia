using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gerencia.API.Entities;
using Gerencia.API.Helpers;

namespace Gerencia.API.Services
{
    public interface ILoginHistoricService {
        List<LoginHistoric> GetHistorics(int userId);
        void SaveHistorics(int userId);
    }
 public class LoginHistoricService : ILoginHistoricService
 {
  private readonly DataContext _dataContext;

  public LoginHistoricService(DataContext dataContext)
  {
   _dataContext = dataContext;
  }

  public List<LoginHistoric> GetHistorics(int userId)
  {
   var loginHistorics = _dataContext.LoginHistorics.Where(x => x.UserId.Equals(userId)).ToList();
   if (loginHistorics == null || loginHistorics.Count == 0)
   {
    throw new KeyNotFoundException("Users not found");
   }
   return loginHistorics;
  }

  public void SaveHistorics(int userId)
  {
   var loginHistorics = new LoginHistoric
   {
    UserId = userId,
    LoginTime = DateTime.Now 
   };
   _dataContext.LoginHistorics.Add(loginHistorics);
   _dataContext.SaveChanges();
  }
 }
}