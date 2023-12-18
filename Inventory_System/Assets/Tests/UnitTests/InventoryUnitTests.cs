using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

[TestFixture]
public class InventoryControllerEditorTests
{
    [Test]
    public void AddStackableItemAndCheckQuantity()
    {
        // Arrange
        InventoryController inventoryController = new GameObject().AddComponent<InventoryController>();
        InventorySlot inventorySlot = new GameObject().AddComponent<InventorySlot>();
        inventoryController.InventorySlots.Add(inventorySlot);
        Item stackableItem = ScriptableObject.CreateInstance<Item>();
        stackableItem.Stackable = true;

        // Act
        inventoryController.AddItem(stackableItem, 1);

        // Assert
        Assert.AreEqual(1, inventoryController.InventorySlots[0].Quantity);
    }

    [Test]
    public void AddItemNonStackableItemToInventory()
    {
        // Arrange
        InventoryController inventoryController = new GameObject().AddComponent<InventoryController>();
        InventorySlot inventorySlot = new GameObject().AddComponent<InventorySlot>();
        inventoryController.InventorySlots.Add(inventorySlot);
        Item nonStackableItem = ScriptableObject.CreateInstance<Item>();
        nonStackableItem.Stackable = false;

        // Act
        inventoryController.AddItem(nonStackableItem, 1);

        // Assert
        Assert.AreEqual(nonStackableItem, inventoryController.InventorySlots[0].Item);
    }

    [Test]
    public void DropItemFromInventory()
    {
        // Arrange
        InventoryController inventoryController = new GameObject().AddComponent<InventoryController>();
        InventorySlot inventorySlot = new GameObject().AddComponent<InventorySlot>();
        inventoryController.InventorySlots.Add(inventorySlot);
        Item itemToDrop = ScriptableObject.CreateInstance<Item>();
        inventoryController.AddItem(itemToDrop, 1);

        // Act
        inventoryController.DropItem(itemToDrop);

        // Assert
        Assert.IsNull(inventoryController.InventorySlots[0].Item);
    }

    [Test]
    public void SortItemsByName()
    {
        // Arrange
        InventoryController inventoryController = new GameObject().AddComponent<InventoryController>();
        InventorySlot firstSlot = new GameObject().AddComponent<InventorySlot>();
        InventorySlot secondSlot = new GameObject().AddComponent<InventorySlot>();
        inventoryController.InventorySlots.Add(firstSlot);
        inventoryController.InventorySlots.Add(secondSlot);
        Item item1 = ScriptableObject.CreateInstance<Item>();
        Item item2 = ScriptableObject.CreateInstance<Item>();
        item1.ItemName = "Sword";
        item2.ItemName = "Armor";
        inventoryController.AddItem(item1, 1);
        inventoryController.AddItem(item2, 1);

        // Act
        inventoryController.SortItemsByName();

        // Assert
        Assert.AreEqual("Armor", inventoryController.InventorySlots[0].Item.ItemName);
        Assert.AreEqual("Sword", inventoryController.InventorySlots[1].Item.ItemName);
    }

    [Test]
    public void SortItemsByPrice()
    {
        // Arrange
        InventoryController inventoryController = new GameObject().AddComponent<InventoryController>();
        InventorySlot firstSlot = new GameObject().AddComponent<InventorySlot>();
        InventorySlot secondSlot = new GameObject().AddComponent<InventorySlot>();
        inventoryController.InventorySlots.Add(firstSlot);
        inventoryController.InventorySlots.Add(secondSlot);
        Item firstItem = ScriptableObject.CreateInstance<Item>();
        Item secondItem = ScriptableObject.CreateInstance<Item>();
        firstItem.Price = 20;
        secondItem.Price = 10;
        inventoryController.AddItem(firstItem, 1);
        inventoryController.AddItem(secondItem, 1);

        // Act
        inventoryController.SortItemsByPrice();

        // Assert
        Assert.AreEqual(10, inventoryController.InventorySlots[0].Item.Price);
        Assert.AreEqual(20, inventoryController.InventorySlots[1].Item.Price);
    }

    [Test]
    public void SortItemsByWeight()
    {
        // Arrange
        InventoryController inventoryController = new GameObject().AddComponent<InventoryController>();
        InventorySlot firstSlot = new GameObject().AddComponent<InventorySlot>();
        InventorySlot secondSlot = new GameObject().AddComponent<InventorySlot>();
        inventoryController.InventorySlots.Add(firstSlot);
        inventoryController.InventorySlots.Add(secondSlot);
        Item firstItem = ScriptableObject.CreateInstance<Item>();
        Item secondItem = ScriptableObject.CreateInstance<Item>();
        firstItem.Weight = 15;
        secondItem.Weight = 5;
        inventoryController.AddItem(firstItem, 1);
        inventoryController.AddItem(secondItem, 1);

        // Act
        inventoryController.SortItemsByWeight();

        // Assert
        Assert.AreEqual(5, inventoryController.InventorySlots[0].Item.Weight);
        Assert.AreEqual(15, inventoryController.InventorySlots[1].Item.Weight);
    }

    [Test]
    public void TogglesInventoryPanel()
    {
        // Arrange
        InventoryController inventoryController = new GameObject().AddComponent<InventoryController>();
        RectTransform inventoryPanel = new GameObject().AddComponent<RectTransform>();
        inventoryController.InventoryPanel = inventoryPanel;
        // Act
        inventoryController.ToggleActivity();

        // Assert
        Assert.IsTrue(inventoryController.InventoryPanel.gameObject.activeSelf);

        // Act
        inventoryController.ToggleActivity();

        // Assert
        Assert.IsFalse(inventoryController.InventoryPanel.gameObject.activeSelf);
    }

    [Test]
    public void EquipChestArmorItem()
    {
        // Arrange
        InventoryController inventoryController = new GameObject().AddComponent<InventoryController>();
        EquipmentController eqController = new GameObject().AddComponent<EquipmentController>();
        EquipmentSlot eqSlot = new GameObject().AddComponent<EquipmentSlot>();
        eqController.EquipmentSlots.Add(eqSlot);
        eqSlot.SlotSpaceType = SlotType.Chest;
        InventorySlot inventorySlot = new GameObject().AddComponent<InventorySlot>();
        inventoryController.InventorySlots.Add(inventorySlot);
        Armour chestArmorItem = ScriptableObject.CreateInstance<Armour>();
        chestArmorItem.SlotType = SlotType.Chest;
        chestArmorItem.RareLevel = RareLevel.Common;
        inventoryController.AddItem(chestArmorItem, 1);

        // Act
        inventorySlot.UseItem();

        // Assert
        Assert.IsNotNull(eqSlot.ItemEquipped);
    }

    [Test]
    public void GetNearestActiveSlot()
    {
        // Arrange
        InventoryController inventoryController = new GameObject().AddComponent<InventoryController>();
        InventorySlot firstSlot = new GameObject().AddComponent<InventorySlot>();
        InventorySlot secondSlot = new GameObject().AddComponent<InventorySlot>();
        firstSlot.transform.position = new Vector3(0, 0, 0);
        secondSlot.transform.position = new Vector3(1, 1, 0);
        inventoryController.InventorySlots.Add(firstSlot);
        inventoryController.InventorySlots.Add(secondSlot);

        // Act
        InventorySlot nearestSlot = inventoryController.GetNearestActive(Vector2.zero);

        // Assert
        Assert.AreEqual(firstSlot, nearestSlot);
    }

    [Test]
    public void UpdateInventoryUI()
    {
        // Arrange
        var inventoryController = new GameObject().AddComponent<InventoryController>();
        InventorySlot firstSlot = new GameObject().AddComponent<InventorySlot>();
        InventorySlot secondSlot = new GameObject().AddComponent<InventorySlot>();
        inventoryController.InventorySlots.Add(firstSlot);
        inventoryController.InventorySlots.Add(secondSlot);
        Item firstItem = ScriptableObject.CreateInstance<Item>();
        Item secondItem = ScriptableObject.CreateInstance<Item>();
        inventoryController.AddItem(firstItem, 1);
        inventoryController.AddItem(secondItem, 1);

        // Act
        inventoryController.UpdateInventoryUI();

        // Assert
        Assert.IsTrue(firstSlot.Item != null);  
        Assert.IsTrue(secondSlot.Item != null);
    }

}
