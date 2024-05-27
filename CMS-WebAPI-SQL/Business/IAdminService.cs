using CMS_WebAPI_SQL.Models;

namespace CMS_WebAPI_SQL.Business
{
    public interface IAdminService
    { 
        void RegisterAdmin(Admin admin);
        Admin LoginAdmin(string username, string password);
        void UpdateAdmin(int id, Admin admin);
        bool DeleteAdmin(int id);
        bool DeleteAllAdmins();
    }
}