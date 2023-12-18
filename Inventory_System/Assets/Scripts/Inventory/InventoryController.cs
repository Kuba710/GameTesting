using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public struct SortingItemData
{
    public Item Item;
    public int Quantity;

    public SortingItemData(Item item, int quantity)
    {
        Item = item;
        Quantity = quantity;
    }
}
public class InventoryController : Singleton<InventoryController>
{
    [SerializeField] private RectTransform inventoryPanel;
    [SerializeField] private ItemInfoUI itemInfoUI;
    [SerializeField] private List<InventorySlot> inventorySlots = new List<InventorySlot>();
    [SerializeField] private Vector2 infoPanelPivot;
    [SerializeField] private Pickable pickableItemPrefab;
    [SerializeField] private float moveItemDistanceFromSlot = 2f;
    private bool isOpened;

    public List<InventorySlot> InventorySlots { get => inventorySlots; set => inventorySlots = value; }
    public RectTransform InventoryPanel { get => inventoryPanel; set => inventoryPanel = value; }
    public ItemInfoUI ItemInfoUI { get => itemInfoUI; set => itemInfoUI = value; }

    public Action<InventorySlot, InventorySlot> onDragTo;
    private void OnEnable()
    {
        foreach (var inventorySlot in inventorySlots)
        {
            inventorySlot.OnPointerEntered += ShowItemInfoPanel;
            inventorySlot.OnPointerExited += CloseItemInfoPanel;
        }
    }
    private void OnDisable()
    {
        foreach (var inventorySlot in inventorySlots)
        {
            inventorySlot.OnPointerEntered -= ShowItemInfoPanel;
            inventorySlot.OnPointerExited -= CloseItemInfoPanel;
        }
    }
    public bool IsInventoryFull()
    {
        foreach (var slot in inventorySlots)
        {
            if (slot.Item == null)
                return false;
        }
        return true;
    }
    public void DropItem(Item item)
    {
        RemoveItem(item, 1);
        ItemSpawner.Instance.SpawnItem(item);
    }
    public void SortItemsByName(bool ascending = true)
    {
        List<SortingItemData> sortedItems = GetSortingData();
        sortedItems.Sort((x_item, y_item) =>
        {
            int result = string.Compare(x_item.Item?.ItemName, y_item.Item?.ItemName);
            return ascending ? result : -result;
        });
        UpdateItemSlotsAfterSort(sortedItems);
    }

    public void SortItemsByPrice(bool ascending = true)
    {
        List<SortingItemData> sortedItems = GetSortingData();
        sortedItems.Sort((x_item, y_item) =>
        {
            int result = x_item.Item?.Price.CompareTo(y_item.Item?.Price) ?? 0;
            return ascending ? result : -result;
        });
        UpdateItemSlotsAfterSort(sortedItems);
    }

    public void SortItemsByWeight(bool ascending = true)
    {
        List<SortingItemData> sortedItems = GetSortingData();
        sortedItems.Sort((x_item, y_item) =>
        {
            int result = x_item.Item?.Weight.CompareTo(y_item.Item?.Weight) ?? 0;
            return ascending ? result : -result;
        });
        UpdateItemSlotsAfterSort(sortedItems);
    }
    private List<SortingItemData> GetSortingData()
    {
        List<SortingItemData> sortedItems = new List<SortingItemData>();

        foreach (var slot in inventorySlots)
        {
            if (slot.Item != null)
            {
                sortedItems.Add(new SortingItemData(slot.Item, slot.Quantity));
                slot.Item = null;
            }
        }
        return sortedItems;
    }

    private void UpdateItemSlotsAfterSort(List<SortingItemData> sortedItems)
    {
        for (int i = 0; i < Mathf.Min(sortedItems.Count, inventorySlots.Count); i++)
        {
            inventorySlots[i].Item = sortedItems[i].Item;
            inventorySlots[i].Quantity = sortedItems[i].Quantity;
        }

        UpdateInventoryUI();
    }

    private void CloseItemInfoPanel()
    {
        itemInfoUI.ClosePanel();
    }

    public void ShowItemInfoPanel(Item item, InventorySlot invSlot = default)
    {
        Vector3 position = Vector3.zero;
        if (invSlot != null)
            position = invSlot.transform.position;
        itemInfoUI.ShowInfoPanel(item, infoPanelPivot, position);
    }

    [Button]
    public void AddItem(Item itemAdded, int quantityAdded)
    {
        if (itemAdded.Stackable)
        {
            foreach (var slot in inventorySlots)
            {
                if (slot.Item == itemAdded)
                {
                    slot.Quantity += quantityAdded;
                    UpdateInventoryUI();
                    return;
                }
                else if (slot.Item == null)
                {
                    slot.Item = itemAdded;
                    slot.Quantity = 1;
                    UpdateInventoryUI();
                    return;
                }
            }
        }
        else
        {
            foreach (var slot in inventorySlots)
            {
                if (slot.Item == null)
                {
                    slot.Item = itemAdded;
                    slot.Quantity = 1;
                    UpdateInventoryUI();
                    return;
                }
            }
        }

    }

    public void ToggleActivity()
    {
        isOpened = !isOpened;
        inventoryPanel.gameObject.SetActive(isOpened);
    }

    public void RemoveItem(Item itemRemoved, int quantityRemoved)
    {
        if (itemRemoved.Stackable)
        {
            foreach (var slot in inventorySlots)
            {
                if (slot.Item == itemRemoved)
                {
                    var quant = Mathf.Clamp(slot.Quantity -= quantityRemoved, 0, 99);
                    slot.Quantity = quant;

                    if (quant == 0)
                        slot.Item = null;
                }
            }
        }
        else
        {
            foreach (var slot in inventorySlots)
            {
                if (slot.Item == itemRemoved)
                {
                    slot.Item = null;
                }
            }
        }
        UpdateInventoryUI();
    }

    public void UpdateInventoryUI()
    {
        foreach (InventorySlot slot in inventorySlots)
        {
            slot.UpdateSlot();
        }
    }

    public void MoveItemBetweenSlots(InventorySlot sourceSlot, InventorySlot targetSlot)
    {
        InventorySlot tempFirstSlot = new InventorySlot();
        InventorySlot tempSecondSlot = new InventorySlot();
        if (targetSlot.Item != null)
        {
            tempFirstSlot.Item = targetSlot.Item;
            tempFirstSlot.Quantity = targetSlot.Quantity;
        }
        if (sourceSlot.Item != null)
        {
            tempSecondSlot.Item = sourceSlot.Item;
            tempSecondSlot.Quantity = sourceSlot.Quantity;
        }


        sourceSlot.Item = tempFirstSlot.Item;
        sourceSlot.Quantity = tempFirstSlot.Quantity;

        targetSlot.Item = tempSecondSlot.Item;
        targetSlot.Quantity = tempSecondSlot.Quantity;
        UpdateInventoryUI();
    }
    public InventorySlot GetNearestActive(Vector2 anchor_pos, InventorySlot grabbedSlot = default)
    {
        InventorySlot nearest = null;
        float min_dist = moveItemDistanceFromSlot;
        foreach (InventorySlot slot in inventorySlots)
        {
            Vector2 canvas_pos = slot.transform.position;
            float dist = (canvas_pos - anchor_pos).magnitude;
            if (dist < min_dist && slot.gameObject.activeInHierarchy)
            {
                min_dist = dist;
                if (nearest != grabbedSlot || grabbedSlot == null)
                    nearest = slot;
            }
        }
        return nearest;
    }

}
