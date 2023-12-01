
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Armour", menuName = "Item/Equipment/Armour")]
public class Armour : Equipment
{
    public List<PlayerStats> statsToModify = new List<PlayerStats>();
    public List<string> modifyValue = new List<string>();

    public override void Use()
    {
        base.Use();
        foreach (EquipmentSlot slot in EquipmentController.Instance.EquipmentSlots)
        {

            if (slot.SlotSpaceType == SlotType)
            {
                if (slot.ItemEquipped != null)
                {
                    if (slot.ItemEquipped != this as Item)
                    {

                        Item itemBefore = slot.ItemEquipped;

                        slot.ItemEquipped = this as Item;

                        if (slot.ItemEquipped.Stackable == false || (slot.ItemEquipped.Stackable && InventoryController.Instance.ItemList.Count < InventoryController.Instance.SlotList.Count))
                        {
                            slot.Icon.enabled = true;
                            slot.Icon.sprite = slot.ItemEquipped.ItemIcon;

                            InventoryController.Instance.RemoveItem(slot.ItemEquipped, 1);
                            InventoryController.Instance.AddItem(itemBefore, 1);
                        }
                        else { slot.ItemEquipped = itemBefore; }
                    }

                }
                else
                {

                    slot.Icon.enabled = true;
                    slot.ItemEquipped = this as Item;
                    slot.Icon.sprite = slot.ItemEquipped.ItemIcon;

                    InventoryController.Instance.RemoveItem(slot.ItemEquipped, 1);

                }
            }
        }

        EquipmentController.Instance.UpdateEquippedValues();
    }


}