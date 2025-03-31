using System;

namespace Team3.ModelViews
{
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using Team3.Entities;
    using Team3.Models;

    public class UserModelView
    {
        private readonly UserModel _userModel;
        public ObservableCollection<User> Users { get; private set; }


        public UserModelView()
        {
            _userModel = UserModel.Instance;
            Users = new ObservableCollection<User>();
            LoadUsers();
        }


        private void LoadUsers()
        {
            try
            {
                var userList = _userModel.GetUsers();
                if (userList != null && userList.Any())
                {
                    foreach (var user in userList)
                    {
                        Debug.WriteLine(user.ToString());

                        Users.Add(user);
                    }
                }
                else
                {
                    Debug.WriteLine("No users returned.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }


        public string getUserNameById(int id)
        {
            return _userModel.GetUser(id).Name;
        }
    }

}
