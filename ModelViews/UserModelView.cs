using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Team3.Models;

namespace Team3.ModelViews
{
    public class UserModelView
    {
        // Attributes
        public ObservableCollection<User> Users { get; private set; }
        private readonly UserModel _userModel;

        // Constructor
        public UserModelView()
        {
            _userModel = UserModel.Instance;
            Users = new ObservableCollection<User>();
            LoadUsers();
        }

        // Load all users
        public void LoadUsers()
        {
            try
            {
                var userList = _userModel.GetUsers();
                if (userList != null && userList.Count > 0)
                {
                    Users.Clear();
                    foreach (var user in userList)
                    {
                        Users.Add(user);
                        Debug.WriteLine($"Loaded User: ID = {user.UserId}, Name = {user.Name}");
                    }
                }
                else
                {
                    Debug.WriteLine("No users found.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading users: {ex.Message}");
            }
        }

        // Get all users
        public List<User> GetUsers()
        {
            try
            {
                return _userModel.GetUsers();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error retrieving users: {ex.Message}");
                return new List<User>();
            }
        }

        // Get a single user by ID
        public User? GetUser(int userId)
        {
            try
            {
                return _userModel.GetUsers().FirstOrDefault(u => u.UserId == userId);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error retrieving user: {ex.Message}");
                return null;
            }
        }

        // Handles user selection
        public void UserClickedHandler(int userId)
        {
            try
            {
                var selectedUser = GetUser(userId);
                if (selectedUser != null)
                {
                    Debug.WriteLine($"User clicked: {selectedUser.Name} (ID: {selectedUser.UserId})");
                }
                else
                {
                    Debug.WriteLine("User not found.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error handling user click: {ex.Message}");
            }
        }
    }
}
