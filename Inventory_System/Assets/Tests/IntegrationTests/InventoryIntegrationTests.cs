using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class InventoryIntegrationTests
{
    [UnityTest]
    public IEnumerator TestPickUpItem()
    {
        //Loading gameplay scene
        SceneManager.LoadScene("Gameplay");
        yield return null;

        //Creating test item
        Consumable drinkItem = ScriptableObject.CreateInstance<Consumable>();
        drinkItem.Guid = System.Guid.NewGuid().ToString();
        drinkItem.ItemIcon = null;
        drinkItem.ItemName = "drink";
        drinkItem.Price = 10;
        drinkItem.Weight = 5;
        drinkItem.Stackable = true;
        drinkItem.Description = "test drink";
        drinkItem.ConsumableType = ConsumableType.Drink;
        drinkItem.PointsRecover = 5;

        //Adding item
        InventoryController.Instance.AddItem(drinkItem, 1);

        yield return null;

        //Assert
        Assert.IsNotNull(InventoryController.Instance.InventorySlots[0].Item);
    }

    [UnityTest]
    public IEnumerator TestMoveItemInInventory()
    {
        //Loading gameplay scene
        SceneManager.LoadScene("Gameplay");
        yield return null;

        //Creating test item
        Consumable drinkItem = ScriptableObject.CreateInstance<Consumable>();
        drinkItem.Guid = System.Guid.NewGuid().ToString();
        drinkItem.ItemIcon = null;
        drinkItem.ItemName = "drink";
        drinkItem.Price = 10;
        drinkItem.Weight = 5;
        drinkItem.Stackable = true;
        drinkItem.Description = "test drink";
        drinkItem.ConsumableType = ConsumableType.Drink;
        drinkItem.PointsRecover = 5;

        InventoryController invController = InventoryController.Instance;
        //Adding item to first slot
        invController.AddItem(drinkItem, 1);

        //Moving item from first to second slot
        invController.MoveItemBetweenSlots(invController.InventorySlots[0], invController.InventorySlots[1]);
        yield return null;

        //Assert if item on second slot isnt null
        Assert.IsNotNull(InventoryController.Instance.InventorySlots[1].Item);
    }

    [UnityTest]
    public IEnumerator TestSortItemsByName()
    {
        //Loading gameplay scene
        SceneManager.LoadScene("Gameplay");
        yield return null;

        //Adding two test items
        Item firstItem = ScriptableObject.CreateInstance<Item>();
        Item secondItem = ScriptableObject.CreateInstance<Item>();
        firstItem.ItemName = "Sword";
        secondItem.ItemName = "Armor";

        InventoryController.Instance.AddItem(firstItem, 1);
        InventoryController.Instance.AddItem(secondItem, 1);

        //Sorting
        InventoryController.Instance.SortItemsByName();
        yield return null;

        //Assert
        Assert.AreEqual("Armor", InventoryController.Instance.InventorySlots[0].Item.ItemName);
        Assert.AreEqual("Sword", InventoryController.Instance.InventorySlots[1].Item.ItemName);
    }

    [UnityTest]
    public IEnumerator TestSortItemsByPrice()
    {
        //Loading gameplay scene
        SceneManager.LoadScene("Gameplay");
        yield return null;
        //Adding two test items
        Item firstItem = ScriptableObject.CreateInstance<Item>();
        Item secondItem = ScriptableObject.CreateInstance<Item>();
        firstItem.Price = 15;
        secondItem.Price = 5;
        InventoryController.Instance.AddItem(firstItem, 1);
        InventoryController.Instance.AddItem(secondItem, 1);
        //Sorting
        InventoryController.Instance.SortItemsByPrice();
        yield return null;

        //Assert
        Assert.AreEqual(5, InventoryController.Instance.InventorySlots[0].Item.Price);
        Assert.AreEqual(15, InventoryController.Instance.InventorySlots[1].Item.Price);
    }

    [UnityTest]
    public IEnumerator TestSortItemsByWeight()
    {
        //Loading gameplay scene
        SceneManager.LoadScene("Gameplay");
        yield return null;

        //Adding two test items
        Item firstItem = ScriptableObject.CreateInstance<Item>();
        Item secondItem = ScriptableObject.CreateInstance<Item>();
        firstItem.Weight = 3;
        secondItem.Weight = 5;
        InventoryController.Instance.AddItem(firstItem, 1);
        InventoryController.Instance.AddItem(secondItem, 1);

        //Sorting
        InventoryController.Instance.SortItemsByWeight();
        yield return null;

        //Assert
        Assert.AreEqual(3, InventoryController.Instance.InventorySlots[0].Item.Weight);
        Assert.AreEqual(5, InventoryController.Instance.InventorySlots[1].Item.Weight);
    }

    [UnityTest]
    public IEnumerator TestDropItemFromInventory()
    {
        //Loading gameplay scene
        SceneManager.LoadScene("Gameplay");
        yield return null;

        //Creating test item
        Consumable drinkItem = ScriptableObject.CreateInstance<Consumable>();
        drinkItem.Guid = System.Guid.NewGuid().ToString();
        drinkItem.ItemIcon = null;
        drinkItem.ItemName = "drink";
        drinkItem.Price = 10;
        drinkItem.Weight = 5;
        drinkItem.Stackable = true;
        drinkItem.Description = "test drink";
        drinkItem.ConsumableType = ConsumableType.Drink;
        drinkItem.PointsRecover = 5;

        InventoryController invController = InventoryController.Instance;
        //Adding item to first slot
        invController.AddItem(drinkItem, 1);

        yield return null;
        Assert.IsNotNull(InventoryController.Instance.InventorySlots[0].Item);

        //Dropping item from inventory
        invController.DropItem(drinkItem);

        yield return null;
        //Assert
        Assert.IsNull(InventoryController.Instance.InventorySlots[0].Item);
    }

    [UnityTest]
    public IEnumerator TestEquipItem()
    {
        //Loading gameplay scene
        SceneManager.LoadScene("Gameplay");
        yield return null;

        //Adding test two swords 
        Weapon swordItem = ScriptableObject.CreateInstance<Weapon>();
        swordItem.SlotType = SlotType.Weapon;
        swordItem.RareLevel = RareLevel.Common;
        swordItem.Damage = 5f;
        swordItem.WeaponSize = WeaponSlotSpace.OnedHanded;
        swordItem.WeaponType = WeaponType.Sword;

        Weapon secondSword = ScriptableObject.CreateInstance<Weapon>();
        secondSword.SlotType = SlotType.Weapon;
        secondSword.RareLevel = RareLevel.Common;
        secondSword.Damage = 5f;
        secondSword.WeaponSize = WeaponSlotSpace.OnedHanded;
        secondSword.WeaponType = WeaponType.Sword;

        InventoryController.Instance.AddItem(swordItem, 1);
        InventoryController.Instance.AddItem(secondSword, 1);
        //Equiping item
        InventoryController.Instance.InventorySlots[0].UseItem();
        InventoryController.Instance.InventorySlots[1].UseItem();
        yield return null;
        //Assert
        EquipmentSlot weaponSlot = new EquipmentSlot();
        foreach (var slot in EquipmentController.Instance.EquipmentSlots)
        {
            if (slot.SlotSpaceType == SlotType.Weapon)
                weaponSlot = slot;
        }
        Assert.IsNotNull(weaponSlot.ItemEquipped);
    }

    [UnityTest]
    public IEnumerator TestUnequipItem()
    {
        //Loading gameplay scene
        SceneManager.LoadScene("Gameplay");
        yield return null;

        //Adding test two swords 
        Weapon swordItem = ScriptableObject.CreateInstance<Weapon>();
        swordItem.SlotType = SlotType.Weapon;
        swordItem.RareLevel = RareLevel.Common;
        swordItem.Damage = 5f;
        swordItem.WeaponSize = WeaponSlotSpace.OnedHanded;
        swordItem.WeaponType = WeaponType.Sword;

        Weapon secondSword = ScriptableObject.CreateInstance<Weapon>();
        secondSword.SlotType = SlotType.Weapon;
        secondSword.RareLevel = RareLevel.Common;
        secondSword.Damage = 5f;
        secondSword.WeaponSize = WeaponSlotSpace.OnedHanded;
        secondSword.WeaponType = WeaponType.Sword;

        InventoryController.Instance.AddItem(swordItem, 1);
        InventoryController.Instance.AddItem(secondSword, 1);
        //Equiping item
        InventoryController.Instance.InventorySlots[0].UseItem();
        InventoryController.Instance.InventorySlots[1].UseItem();
        yield return null;

        //Assert if sword is in sword slot
        EquipmentSlot weaponSlot = new EquipmentSlot();
        foreach (var slot in EquipmentController.Instance.EquipmentSlots)
        {
            if (slot.SlotSpaceType == SlotType.Weapon)
                weaponSlot = slot;
        }
        Assert.IsNotNull(weaponSlot.ItemEquipped);

        weaponSlot.ReturnItemToInventory();

        //checking if item was returned
        Assert.IsNull(weaponSlot.ItemEquipped);

    }

    [UnityTest]
    public IEnumerator TestToggleInventoryActivity()
    {
        //Loading gameplay scene
        SceneManager.LoadScene("Gameplay");
        yield return null;

        //Toggling and testing
        InventoryController.Instance.ToggleActivity();
        yield return null;
        Assert.IsTrue(InventoryController.Instance.InventoryPanel.gameObject.activeSelf);
        InventoryController.Instance.ToggleActivity();
        yield return null;
        Assert.IsFalse(InventoryController.Instance.InventoryPanel.gameObject.activeSelf);

    }

    [UnityTest]
    public IEnumerator TestRemoveItemFromInventory()
    {
        //Loading gameplay scene
        SceneManager.LoadScene("Gameplay");
        yield return null;

        //Creating test item
        Consumable drinkItem = ScriptableObject.CreateInstance<Consumable>();
        drinkItem.Guid = System.Guid.NewGuid().ToString();
        drinkItem.ItemIcon = null;
        drinkItem.ItemName = "drink";
        drinkItem.Price = 10;
        drinkItem.Weight = 5;
        drinkItem.Stackable = true;
        drinkItem.Description = "test drink";
        drinkItem.ConsumableType = ConsumableType.Drink;
        drinkItem.PointsRecover = 5;

        InventoryController invController = InventoryController.Instance;
        //Adding item to first slot
        invController.AddItem(drinkItem, 1);
        yield return null;
        //Assert if not null
        Assert.IsNotNull(InventoryController.Instance.InventorySlots[0].Item);

        invController.DropItem(drinkItem);
        yield return null;
        //Assert if null
        Assert.IsNull(InventoryController.Instance.InventorySlots[0].Item);
    }

    [UnityTest]
    public IEnumerator TestItemInfoPopup()
    {
        //Loading gameplay scene
        SceneManager.LoadScene("Gameplay");
        yield return null;
        //Creating test item
        Consumable drinkItem = ScriptableObject.CreateInstance<Consumable>();
        drinkItem.Guid = System.Guid.NewGuid().ToString();
        drinkItem.ItemIcon = null;
        drinkItem.ItemName = "drink";
        drinkItem.Price = 10;
        drinkItem.Weight = 5;
        drinkItem.Stackable = true;
        drinkItem.Description = "test drink";
        drinkItem.ConsumableType = ConsumableType.Drink;
        drinkItem.PointsRecover = 5;

        InventoryController invController = InventoryController.Instance;
        //Adding item to first slot
        invController.AddItem(drinkItem, 1);

        //Enabling popup
        invController.ShowItemInfoPanel(drinkItem);

        yield return null;
        //Assert
        Assert.IsTrue(InventoryController.Instance.ItemInfoUI.gameObject.activeInHierarchy);
    }
}
