using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryController : Singleton<InventoryController>
{
    [SerializeField] private RectTransform inventoryPanel;
    [SerializeField] private ItemInfoUI itemInfoUI;
    [SerializeField] private List<InventorySlot> slotList;
    [SerializeField] private List<Item> itemList = new List<Item>();
    public List<int> quantityList = new List<int>();
    [SerializeField] private Vector2 infoPanelPivot;
    [SerializeField] private Pickable pickableItemPrefab;
    private bool isOpened;

    public List<InventorySlot> SlotList { get => slotList; set => slotList = value; }
    public List<Item> ItemList { get => itemList; set => itemList = value; }

    public bool IsInventoryFull => itemList.Count == slotList.Count;

    public RectTransform InventoryPanel { get => inventoryPanel; set => inventoryPanel = value; }

    public void DropItem(Item item)
    {
        RemoveItem(item, 1);
        ItemSpawner.Instance.SpawnItem(item);
    }

    private void OnEnable()
    {
        foreach (var inventorySlot in slotList)
        {
            inventorySlot.OnPointerEntered += ShowItemInfoPanel;
            inventorySlot.OnPointerExited += CloseItemInfoPanel;
        }
    }
    private void OnDisable()
    {
        foreach (var inventorySlot in slotList)
        {
            inventorySlot.OnPointerEntered -= ShowItemInfoPanel;
            inventorySlot.OnPointerExited -= CloseItemInfoPanel;
        }
    }
    private void CloseItemInfoPanel()
    {
        itemInfoUI.ClosePanel();
    }

    private void ShowItemInfoPanel(Item item, InventorySlot invSlot)
    {
        itemInfoUI.ShowInfoPanel(item, infoPanelPivot, invSlot.transform.position);
    }

    [Button]
    public void AddItem(Item itemAdded, int quantityAdded)
    {
        if (itemAdded.Stackable)
        {
            if (itemList.Contains(itemAdded))
            {
                quantityList[itemList.IndexOf(itemAdded)] = quantityList[itemList.IndexOf(itemAdded)] + quantityAdded;
            }
            else
            {

                if (itemList.Count < slotList.Count)
                {
                    itemList.Add(itemAdded);
                    quantityList.Add(quantityAdded);
                }
                else { }
            }

        }
        else
        {
            for (int i = 0; i < quantityAdded; i++)
            {
                if (itemList.Count < slotList.Count)
                {
                    itemList.Add(itemAdded);
                    quantityList.Add(1);
                }
                else { }

            }

        }
        UpdateInventoryUI();
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
            if (itemList.Contains(itemRemoved))
            {
                quantityList[itemList.IndexOf(itemRemoved)] = quantityList[itemList.IndexOf(itemRemoved)] - quantityRemoved;

                if (quantityList[itemList.IndexOf(itemRemoved)] <= 0)
                {
                    quantityList.RemoveAt(itemList.IndexOf(itemRemoved));
                    itemList.RemoveAt(itemList.IndexOf(itemRemoved));
                }
            }

        }
        else
        {
            for (int i = 0; i < quantityRemoved; i++)
            {
                quantityList.RemoveAt(itemList.IndexOf(itemRemoved));
                itemList.RemoveAt(itemList.IndexOf(itemRemoved));

            }
        }
        UpdateInventoryUI();
    }

    public void UpdateInventoryUI()
    {
        int ind = 0;
        foreach (InventorySlot slot in slotList)
        {

            if (itemList.Count != 0)
            {

                if (ind < itemList.Count)
                {
                    slot.UpdateSlot(itemList[ind], quantityList[ind]);
                    ind = ind + 1;
                }
                else
                {
                    slot.UpdateSlot(null, 0);
                }
            }
            else
            {
                slot.UpdateSlot(null, 0);
            }

        }

        if (EquipmentController.Instance.EquipmentPanel.gameObject.activeInHierarchy)
        {
            EquipmentController.Instance.StatusUI.OnEnable();
        }

    }


}
