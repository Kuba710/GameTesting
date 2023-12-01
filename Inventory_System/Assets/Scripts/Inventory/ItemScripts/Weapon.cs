using System.Collections.Generic;
using UnityEngine;

public enum WeaponSlotSpace { TwoHanded, OnedHanded }
public enum WeaponType { Sword }

[CreateAssetMenu(fileName = "Weapon", menuName = "Item/Equipment/Weapon")]
public class Weapon : Equipment
{
    public WeaponSlotSpace weaponSize;
    public WeaponType weaponType;
    public float damage;
    public List<PlayerStats> statsToModify = new List<PlayerStats>();
    public List<string> modifyValue = new List<string>();

    public override void Use()
    {
        base.Use();
        EquipmentSlot weapon1 = null, weapon2 = null;
        foreach (EquipmentSlot slot in EquipmentController.Instance.EquipmentSlots)
        {
            if (slot.SlotSpaceType == SlotType.Weapon)
            {
                if (weapon1 == null)
                {
                    weapon1 = slot;

                }
                if (weapon2 == null && weapon1 != slot)
                {
                    weapon2 = slot;
                }
            }
        }

        if (weapon1.ItemEquipped == null && weapon2.ItemEquipped == null)
        {

            weapon1.Icon.enabled = true;
            weapon1.ItemEquipped = this as Item;
            weapon1.Icon.sprite = weapon1.ItemEquipped.ItemIcon;

            InventoryController.Instance.RemoveItem(weapon1.ItemEquipped, 1);
        }
        else

       if ((weapon1.ItemEquipped != null && weapon2.ItemEquipped == null))
        {

            Weapon other = weapon1.ItemEquipped as Weapon;
            if (weaponSize == WeaponSlotSpace.OnedHanded && other.weaponSize == WeaponSlotSpace.OnedHanded)
            {


                weapon2.Icon.enabled = true;
                weapon2.ItemEquipped = this as Item;
                weapon2.Icon.sprite = weapon2.ItemEquipped.ItemIcon;

                InventoryController.Instance.RemoveItem(weapon2.ItemEquipped, 1);


            }
            else
            {
                weapon1.Icon.enabled = true;

                InventoryController.Instance.AddItem(weapon1.ItemEquipped, 1);
                weapon1.ItemEquipped = this as Item;

                weapon1.Icon.sprite = weapon1.ItemEquipped.ItemIcon;

                InventoryController.Instance.RemoveItem(weapon1.ItemEquipped, 1);
            }
        }
        else

         if (weapon1.ItemEquipped == null && weapon2.ItemEquipped != null)
        {
            Weapon other = weapon2.ItemEquipped as Weapon;
            if (weaponSize == WeaponSlotSpace.OnedHanded && other.weaponSize == WeaponSlotSpace.OnedHanded)
            {


                weapon1.Icon.enabled = true;
                weapon1.ItemEquipped = this as Item;
                weapon1.Icon.sprite = weapon1.ItemEquipped.ItemIcon;

                InventoryController.Instance.RemoveItem(weapon1.ItemEquipped, 1);


            }
            else
            {
                weapon1.Icon.enabled = true;

                InventoryController.Instance.AddItem(weapon2.ItemEquipped, 1);
                weapon1.ItemEquipped = this as Item;
                weapon2.ItemEquipped = null;
                weapon2.Icon.enabled = false;
                weapon1.Icon.sprite = weapon1.ItemEquipped.ItemIcon;

                InventoryController.Instance.RemoveItem(weapon1.ItemEquipped, 1);
            }
        }


        if (weapon1.ItemEquipped != null && weapon2.ItemEquipped != null)
        {


            if (weaponSize == WeaponSlotSpace.OnedHanded)
            {
                weapon1.Icon.enabled = true;

                InventoryController.Instance.AddItem(weapon1.ItemEquipped, 1);
                weapon1.ItemEquipped = this as Item;

                weapon1.Icon.sprite = weapon1.ItemEquipped.ItemIcon;

                InventoryController.Instance.RemoveItem(weapon1.ItemEquipped, 1);
            }
            else
            {
                weapon1.Icon.enabled = true;

                InventoryController.Instance.AddItem(weapon1.ItemEquipped, 1);
                InventoryController.Instance.AddItem(weapon2.ItemEquipped, 1);
                weapon1.ItemEquipped = this as Item;

                weapon1.Icon.sprite = weapon1.ItemEquipped.ItemIcon;
                weapon2.ItemEquipped = null;
                weapon2.Icon.enabled = false;
                InventoryController.Instance.RemoveItem(weapon1.ItemEquipped, 1);
            }
        }

        EquipmentController.Instance.UpdateEquippedValues();

    }


}
