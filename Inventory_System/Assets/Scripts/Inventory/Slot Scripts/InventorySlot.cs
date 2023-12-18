using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using Sirenix.OdinInspector;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Item item;
    [SerializeField, ReadOnly] private int quantity;
    [SerializeField] private Image itemImage;
    [SerializeField] private Text quantityText;
    [SerializeField] private DragUI dragComponent;

    public Action<Item, InventorySlot> OnPointerEntered;
    public Action OnPointerExited;
    public DragUI DragComponent { get => dragComponent; set => dragComponent = value; }
    public Item Item { get => item; set => item = value; }
    public int Quantity { get => quantity; set => quantity = value; }

    public void OnEnable()
    {
        dragComponent.OnDropped += DropItem;
    }
    public void OnDisable()
    {
        dragComponent.OnDropped -= DropItem;
    }
    private void DropItem(PointerEventData eventData)
    {
        if (!RectTransformUtility.RectangleContainsScreenPoint(InventoryController.Instance.InventoryPanel, Input.mousePosition) && !RectTransformUtility.RectangleContainsScreenPoint(EquipmentController.Instance.EquipmentPanel, Input.mousePosition))
        {
            InventoryController.Instance.DropItem(item);
        }
        else
        {
            InventorySlot targetSlot = InventoryController.Instance.GetNearestActive(eventData.pointerEnter.transform.position, this);
            if (targetSlot != null)
            {
                InventoryController.Instance.MoveItemBetweenSlots(this, targetSlot);
            }
        }
    }

    public void UpdateSlot()
    {
        if (itemImage == null || quantityText == null)
            return;

        if (item != null && quantity != 0)
        {
            itemImage.enabled = true;
            itemImage.sprite = item.ItemIcon;

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

    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
        }
    }
}
