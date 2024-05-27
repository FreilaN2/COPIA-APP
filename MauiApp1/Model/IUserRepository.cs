using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpinningTrainer.ViewModel;

namespace SpinningTrainer.Model
{
    public interface IUserRepository
    {
        (bool, string, int) AuthenticateUser(string username, string password);
        void Add(UserModel userModel);
        void Update (UserModel userModel);
        (bool,string) UpdatePassword (string username, string password);
        void Delete(UserModel userModel);
        UserModel GetById(int Id);
        UserModel GetByUserName(string username);
        string ValidateUsernameforPasswordChange(string username);
        string ValidateUserEmalforPasswordChange(string email);
        IEnumerable<UserModel> GetAll();
    }
}
