using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Team3.Domain;
using Team3.Models;

namespace Team3.ModelViews
{
    public class DepartmentModelView
    {
        // Attributes
        public ObservableCollection<Department> DepartmentsInfo { get; private set; }
        public ObservableCollection<Department> Departments { get; private set; }
        private readonly RoomModelView _roomModelView;
        private readonly DepartmentModel _departmentModel;

        // Constructor
        public DepartmentModelView()
        {
            _roomModelView = new RoomModelView(); // Assuming RoomModelView is implemented elsewhere
            _departmentModel = DepartmentModel.Instance;
            DepartmentsInfo = new ObservableCollection<Department>();
            Departments = new ObservableCollection<Department>();
            LoadDepartments();
        }

        // Load all departments
        private void LoadDepartments()
        {
            try
            {
                var departmentList = _departmentModel.GetDepartments();
                if (departmentList != null && departmentList.Count > 0)
                {
                    foreach (var department in departmentList)
                    {
                        Debug.WriteLine(department.ToString());
                        Departments.Add(department);
                        DepartmentsInfo.Add(department);
                    }
                }
                else
                {
                    Debug.WriteLine("No departments found.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading departments: {ex.Message}");
            }
        }

        // Add additional functionality as needed
        public List<Department> GetDepartmentsByName(string name)
        {
            try
            {
                var departmentList = _departmentModel.GetDepartments();
                var filteredDepartments = departmentList.FindAll(d => d.DepartmentName.Contains(name, StringComparison.OrdinalIgnoreCase));

                if (filteredDepartments.Count == 0)
                {
                    Debug.WriteLine($"No departments found with the name '{name}'.");
                }

                return filteredDepartments;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error filtering departments: {ex.Message}");
                throw;
            }
        }
    }
}
