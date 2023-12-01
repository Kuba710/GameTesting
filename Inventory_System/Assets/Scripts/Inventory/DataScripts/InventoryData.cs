using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryData
{
    public string inventoryString;
    public string equipmentString;

    public InventoryData(string invStr, string eqmStr)
    {
        inventoryString = invStr;
        equipmentString = eqmStr;
    }


}
