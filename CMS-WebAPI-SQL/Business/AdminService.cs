using CMS_WebAPI_SQL.Models;
using Microsoft.EntityFrameworkCore;

namespace CMS_WebAPI_SQL.Business
{
    public class AdminService : IAdminService
    {
        private readonly AdminContext _adminContext;
        public AdminService(AdminContext adminContext)
        {
            _adminContext = adminContext;
        }

        public bool DeleteAdmin(int id)
        {
            var admin = _adminContext.Admins.Find(id);
            if (admin == null)
            {
                return false;
            }

            _adminContext.Admins.Remove(admin);
            _adminContext.SaveChanges();

            return true;
        }

        public bool DeleteAllAdmins()
        {
            _adminContext.Admins.RemoveRange(_adminContext.Admins);
            _adminContext.SaveChanges();

            return true;
        }

        public Admin LoginAdmin(string username, string password)
        {
            var admin = _adminContext.Admins.FirstOrDefault(a => a.Email == username && a.Password == password);
            return admin;
        }

        public void RegisterAdmin(Admin admin)
        {
            if (_adminContext.Admins.Any(a => a.Email == admin.Email))
            {
                throw new ArgumentException("User with this email already exists");
            }

            if (!PasswordIsStrong(admin.Password))
            {
                throw new ArgumentException("Password must contain at least one digit, one uppercase and one lowercase letter");
            }

            _adminContext.Admins.Add(admin);
            _adminContext.SaveChanges();
        }

        private bool PasswordIsStrong(string password)
        {
            return password.Any(char.IsDigit) && password.Any(char.IsUpper) && password.Any(char.IsLower);
        }



        public void UpdateAdmin(int id, Admin admin)
        {
            if (id != admin.ID)
            {
                throw new ArgumentException("Invalid ID");
            }

            _adminContext.Entry(admin).State = EntityState.Modified;
            _adminContext.SaveChanges();
        }
    }
}
