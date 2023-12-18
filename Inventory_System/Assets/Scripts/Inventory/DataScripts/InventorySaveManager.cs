using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class InventorySaveManager : MonoBehaviour
{
    public List<Item> itemLibrary = new List<Item>();
    string inventoryString = "", equipmentString = "";
    public void TransformDataToString()
    {
        inventoryString = "";
        equipmentString = "";

        foreach (InventorySlot slot in InventoryController.Instance.InventorySlots)
        {
            Item item = slot.Item;
            inventoryString = inventoryString + item.Guid + ":" + slot.Quantity + "/";
        }

        foreach (EquipmentSlot equipSlot in EquipmentController.Instance.EquipmentSlots)
        {
            if (equipSlot.ItemEquipped != null)
            {
                equipmentString = equipmentString + equipSlot.ItemEquipped.Guid + "/";


            }
            else
            {
                equipmentString = equipmentString + "-1/";
            }
        }
    }

    public void SaveInventory()
    {

        TransformDataToString();
        string destination = Application.persistentDataPath + "/save.dat";
        FileStream file;

        if (File.Exists(destination)) file = File.OpenWrite(destination);
        else file = File.Create(destination);

        InventoryData data = new InventoryData(inventoryString, equipmentString);


        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();
    }

    public void LoadInventory()
    {
        string destination = Application.persistentDataPath + "/save.dat";
        FileStream file;

        if (File.Exists(destination)) file = File.OpenRead(destination);
        else
        {
            Debug.LogError("File not found");
            return;
        }

        BinaryFormatter bf = new BinaryFormatter();
        InventoryData data = (InventoryData)bf.Deserialize(file);
        file.Close();

        ReadInventoryData(data.inventoryString, data.equipmentString);
        InventoryController.Instance.UpdateInventoryUI();

    }

    public void ReadInventoryData(string data, string data2)
    {
        string[] splitData = data.Split(char.Parse("/"));
        int index = 0;
        foreach (string stg in splitData)
        {
            string[] splitID = stg.Split(char.Parse(":"));

            if (splitID.Length >= 2)
            {
                InventoryController.Instance.InventorySlots[index].Item = itemLibrary[int.Parse(splitID[0])];
                InventoryController.Instance.InventorySlots[index].Quantity = int.Parse(splitID[1]);
            }
        }

        string[] splitDataTwo = data2.Split(char.Parse("/"));

        for (int i = 0; i < EquipmentController.Instance.EquipmentSlots.Count; i++)
        {

            if (int.Parse(splitDataTwo[i]) >= 0)
            {
                EquipmentController.Instance.EquipmentSlots[i].ItemEquipped = itemLibrary[int.Parse(splitDataTwo[i])];
                EquipmentController.Instance.EquipmentSlots[i].Icon.enabled = true;
                EquipmentController.Instance.EquipmentSlots[i].Icon.sprite = EquipmentController.Instance.EquipmentSlots[i].ItemEquipped.ItemIcon;
            }
            else
            {
                EquipmentController.Instance.EquipmentSlots[i].ItemEquipped = null;
                EquipmentController.Instance.EquipmentSlots[i].Icon.enabled = false;
            }

        }

        EquipmentController.Instance.UpdateEquippedValues();

    }



}
