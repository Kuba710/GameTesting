using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{
    [SerializeField] private Item item;
    [SerializeField] private Image itemImage;
    [SerializeField] private Text quantityText;
    [SerializeField] private DragUI dragComponent;

    public Action<Item, InventorySlot> OnPointerEntered;
    public Action OnPointerExited;
    private int quantity;
    public DragUI DragComponent { get => dragComponent; set => dragComponent = value; }
    public Item Item { get => item; set => item = value; }

    public void OnEnable()
    {
        dragComponent.OnDropped += DropItem;
    }
    public void OnDisable()
    {
        dragComponent.OnDropped -= DropItem;
    }
    private void DropItem()
    {
        InventoryController.Instance.DropItem(item);
    }

    public void UpdateSlot(Item itemInSlot, int quantityInSlot)
    {
        item = itemInSlot;

        if (itemInSlot != null && quantityInSlot != 0)
        {
            quantity = quantityInSlot;
            itemImage.enabled = true;

            itemImage.sprite = itemInSlot.ItemIcon;

            if (quantity > 1)
            {

                quantityText.enabled = true;
                quantityText.text = quantity.ToString();
            }
            else
            {
                quantityText.enabled = false;

            }

        }
        else
        {
            itemImage.enabled = false;
            quantityText.enabled = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnPointerEntered?.Invoke(item, this);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        OnPointerExited?.Invoke();
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            if (eventData.pointerDrag.GetComponentInParent<EquipmentSlot>() != null && !InventoryController.Instance.IsInventoryFull)
            {
                eventData.pointerDrag.GetComponentInParent<EquipmentSlot>().ReturnItemToInventory();
            }
        }
    }
    public void UseItem()
    {
        if (item != null)
        {

            item.Use();
        }
    }

    public void RemoveItem()
    {
        InventoryController.Instance.RemoveItem(InventoryController.Instance.ItemList[InventoryController.Instance.ItemList.IndexOf(item)], 1);
    }
}
