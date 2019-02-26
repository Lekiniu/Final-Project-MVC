using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using davaleba.Helpers;
using static davaleba.Helpers.PasswordHelper;

namespace davaleba.Models
{
    public class UserAccountDataProvider
    {   
        ProjectDbEntities _db = new ProjectDbEntities();
        public void AddUser(User user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();
        }

        public bool ValidLogin(LoginViewModel user)
        {

            var pass = SHA.GenerateSHA512String(user.Password);
            var result = _db.Users.FirstOrDefault(e => e.Mail == user.Email && e.Password == pass);


            //LoginHelper.CreateUser(new User()
            //{
            //    Password = user.Password,
            //    Mail = user.Email,
            //    //CategoryId=user.CategoryId
            //});
            return result == null ? false : true;
        }


    }
}