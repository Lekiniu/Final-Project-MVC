using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace davaleba.Models
{
    public class UsersDataProvider
    {
        ProjectDbEntities _db = new ProjectDbEntities();


        public bool Exist(UserCustomClass user)
        {
            return _db.Users.FirstOrDefault(e => e.Mail == user.Mail) == null ? false : true;
        }

        public User GetUserById(int id)
        {
            return _db.Users.FirstOrDefault(e => e.Id == id);
        }

        //Create
        public void CreateUser(UserCustomClass user)
        {
            if (!Exist(user))
            {
                _db.Users.Add(new User()
                {
                   First_Name= user.First_Name,
                   Last_Name=user.Last_Name,
                   Mail=user.Mail,
                   Password=user.Password,
                   IsActive=user.IsActive,
                   CategoryId=user.CategoryId,

                });

            }
            _db.SaveChanges();
        }

        //Edit 
        public void EditUser(UserCustomClass user)
        {
            var result = _db.Users.FirstOrDefault(e => e.Id == user.Id);
            if (!Exist(user) || result.First_Name == user.First_Name)
            {
                result.First_Name = user.First_Name;
                result.Last_Name = user.Last_Name;
                result.Mail = user.Mail;
                result.Password = user.Password;
                result.IsActive = user.IsActive;
                result.CategoryId = user.CategoryId;

            }
            _db.SaveChanges();
        }
        //Delete
        //public void deleteUser(Users user)
        //{
        //    var result = _db.Users.FirstOrDefault(e => e.Id == user.Id);
        //    result.IsActive = false;
        //    _db.SaveChanges();
        //}


        public void FullDeleteUser(User user)
        {   var deleteImage = _db.Images.Where(e => e.Product.UserId == user.Id);
            var deleteProductCategory = _db.Products_Categories.Where(e => e.Product.UserId == user.Id);
            
            var deleteProduct = _db.Products.Where(e => e.UserId == user.Id);
            var deleteOrders= _db.Orders.Where(e => e.UserId == user.Id);
            var deleteUsers = _db.Users.Where(e => e.Id == user.Id);

            
            _db.Images.RemoveRange(deleteImage);
            _db.Products_Categories.RemoveRange(deleteProductCategory);
            
            _db.Orders.RemoveRange(deleteOrders);
            _db.Products.RemoveRange(deleteProduct);
            _db.Users.RemoveRange(deleteUsers);
            _db.SaveChanges();
        }

        //search 
        public IEnumerable<User> GetUsers(string name,string surname, string mail)
        {

            return _db.Users.Where(e => e.First_Name.Contains(name) && e.Last_Name.Contains(surname) && e.Mail.Contains(mail));
        }


        public IEnumerable<User> AllUsers()
        {
            return _db.Users;
        }
    }
}