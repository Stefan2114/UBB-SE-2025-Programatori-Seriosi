using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Team3.Entities;
using Team3.Models;

namespace Team3.ModelViews
{
    public class RoomModelView
    {
        // Attributes
        public ObservableCollection<Room> Rooms { get; private set; }
        public ObservableCollection<Room> RoomsInfo { get; private set; }
        private readonly RoomModel _roomModel;
        private readonly EquipmentModel _equipmentModel;
        private readonly HospitalizationModel _hospitalizationModel;

        // Constructor
        public RoomModelView()
        {
            _roomModel = RoomModel.Instance;
            _equipmentModel = new EquipmentModel();
            _hospitalizationModel = new HospitalizationModel();
            Rooms = new ObservableCollection<Room>();
            RoomsInfo = new ObservableCollection<Room>();
            LoadRooms();
        }

        // Load all rooms
        private void LoadRooms()
        {
            try
            {
                var roomList = _roomModel.GetRooms();
                if (roomList != null && roomList.Count > 0)
                {
                    foreach (var room in roomList)
                    {
                        Debug.WriteLine($"Room: {room.Id}, Department ID: {room.DepartmentId}");
                        Rooms.Add(room);
                        RoomsInfo.Add(room);
                    }
                }
                else
                {
                    Debug.WriteLine("No rooms found.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading rooms: {ex.Message}");
            }
        }

        // Filter rooms by department ID
        public ObservableCollection<Room> GetRoomsByDepartmentId(int departmentId)
        {
            try
            {
                var filteredRooms = new ObservableCollection<Room>(
                    _roomModel.GetRooms().Where(r => r.DepartmentId == departmentId)
                );

                if (!filteredRooms.Any())
                {
                    Debug.WriteLine($"No rooms found for Department ID: {departmentId}");
                }

                return filteredRooms;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error filtering rooms by Department ID: {ex.Message}");
                throw;
            }
        }

        // Get equipment by Department ID and date range
        public List<Equipment> GetEquipmentsByDepartmentId(int departmentId, DateTime startDate, DateTime endDate)
        {
            try
            {
                var departmentRooms = _roomModel.GetRooms().Where(r => r.DepartmentId == departmentId).ToList();

                if (!departmentRooms.Any())
                {
                    Debug.WriteLine($"No rooms found for Department ID: {departmentId}");
                    return new List<Equipment>();
                }

                var roomIds = departmentRooms.Select(r => r.Id).ToList();
                var equipmentList = _equipmentModel.GetEquipmentsByRoomsAndDate(roomIds, startDate, endDate);

                if (!equipmentList.Any())
                {
                    Debug.WriteLine($"No equipment found for Department ID: {departmentId} between {startDate} and {endDate}");
                }

                return equipmentList;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error retrieving equipment: {ex.Message}");
                throw;
            }
        }

        // Get hospitalizations by Department ID and date range
        public List<Hospitalization> GetHospitalizationByDepartmentID(int departmentId, DateTime startDate, DateTime endDate)
        {
            try
            {
                var roomList = _roomModel.GetRooms();
                var departmentRooms = roomList.Where(r => r.DepartmentId == departmentId).Select(r => r.Id).ToList();

                if (!departmentRooms.Any())
                {
                    Debug.WriteLine($"No rooms found for Department ID: {departmentId}");
                    return new List<Hospitalization>();
                }

                var hospitalizations = _hospitalizationModel.GetHospitalizations()
                    .Where(h => departmentRooms.Contains(h.RoomId) &&
                                h.StartDate >= startDate &&
                                h.EndDate <= endDate)
                    .ToList();

                if (!hospitalizations.Any())
                {
                    Debug.WriteLine($"No hospitalizations found for Department ID: {departmentId} between {startDate} and {endDate}");
                }

                return hospitalizations;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error retrieving hospitalizations: {ex.Message}");
                throw;
            }
        }
    }
}
