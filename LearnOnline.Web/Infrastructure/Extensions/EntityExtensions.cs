using LearnOnline.Model.Models;
using LearnOnline.Web.Models;

namespace LearnOnline.Web.Infrastructure.Extensions
{
    public static class EntityExtensions
    {
        public static void UpdateUser(this User user, UserViewModel userVM)
        {
            user.ID = userVM.ID;
            user.Avatar = userVM.Avatar;
            user.FullName = userVM.FullName;
            user.Address = userVM.Address;
            user.NgaySinh = userVM.NgaySinh;
            user.Sex = userVM.Sex;
            user.SSN = userVM.SSN;
            user.Phone = userVM.Phone;
            user.Email = userVM.Email;
            user.ProvincesID = userVM.ProvincesID;
            user.DistrictID = userVM.DistrictID;
            user.UnitName = userVM.UnitName;
            user.Status = userVM.Status;
            user.UserName = userVM.UserName;
            user.Password = userVM.Password;
            user.UserGroupID = userVM.UserGroupID;
        }
    }
}