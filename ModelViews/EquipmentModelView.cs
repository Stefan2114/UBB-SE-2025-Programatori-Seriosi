using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Team3.Entities;
using Team3.Models;

namespace Team3.ModelViews
{
    public class EquipmentModelView
    {
        // Attributes
        public ObservableCollection<Equipment> EquipmentsInfo { get; private set; }
        private readonly EquipmentModel _equipmentModel;

        // Constructor
        public EquipmentModelView()
        {
            _equipmentModel = new EquipmentModel();
            EquipmentsInfo = new ObservableCollection<Equipment>();
            LoadEquipmentsInfo();
        }

        // Load detailed equipment information
        public void LoadEquipmentsInfo()
        {
            try
            {
                var equipmentList = _equipmentModel.getEquipments();

                if (equipmentList != null && equipmentList.Count > 0)
                {
                    EquipmentsInfo.Clear(); // Clear old data
                    foreach (var equipment in equipmentList)
                    {
                        EquipmentsInfo.Add(equipment);
                        Debug.WriteLine($"Loaded Equipment: ID = {equipment.EquipmentId}, Name = {equipment.EquipmentName}");
                    }
                }
                else
                {
                    Debug.WriteLine("No equipment found in the database.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading equipment details: {ex.Message}");
            }
        }

        // Get equipment by room ID
        public List<Equipment> GetEquipmentsByRoomID(int roomId)
        {
            try
            {
                var equipmentList = _equipmentModel.getEquipments();
                var filteredEquipments = equipmentList.FindAll(e => e.RoomId == roomId);

                if (filteredEquipments.Count == 0)
                {
                    Debug.WriteLine($"No equipment found for Room ID {roomId}.");
                }

                return filteredEquipments;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error filtering equipment: {ex.Message}");
                throw;
            }
        }
    }
}
