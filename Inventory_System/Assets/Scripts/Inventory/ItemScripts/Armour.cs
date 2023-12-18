
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Armour", menuName = "Item/Equipment/Armour")]
public class Armour : Equipment
{
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

                        if (slot.Icon != null)
                        {
                            slot.Icon.enabled = true;
                            slot.Icon.sprite = slot.ItemEquipped.ItemIcon;
                        }

                        InventoryController.Instance.RemoveItem(slot.ItemEquipped, 1);
                        InventoryController.Instance.AddItem(itemBefore, 1);

                    }

                }
                else
                {
                    if (slot.Icon != null)
                    {
                        slot.Icon.enabled = true;
                        slot.Icon.sprite = slot.ItemEquipped.ItemIcon;
                    }
                    slot.ItemEquipped = this as Item;

                    InventoryController.Instance.RemoveItem(slot.ItemEquipped, 1);

                }
            }
        }

        EquipmentController.Instance.UpdateEquippedValues();
    }


}