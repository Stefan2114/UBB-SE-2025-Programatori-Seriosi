using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Team3.Domain;
using Team3.Models;
//Mi a trebuit mie clasa si am creato eu cu gpt dar nu e taskul meu sherdy

namespace Team3.ModelViews
{

    public class RoomModelView
    {
        // Attributes
        public ObservableCollection<Room> Rooms { get; private set; }
        public ObservableCollection<Room> RoomsInfo { get; private set; }
        private readonly RoomModel _roomModel;

        // Constructor
        public RoomModelView()
        {
            _roomModel = RoomModel.Instance;
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
                        Debug.WriteLine($"Room: {room.RoomId}, Name: {room.RoomName}, Department ID: {room.DepartmentId}");
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
        public List<Room> GetRoomsByDepartmentId(int departmentId)
        {
            try
            {
                var roomList = _roomModel.GetRooms();
                var filteredRooms = roomList.FindAll(r => r.DepartmentId == departmentId);

                if (filteredRooms.Count == 0)
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
    }
}
