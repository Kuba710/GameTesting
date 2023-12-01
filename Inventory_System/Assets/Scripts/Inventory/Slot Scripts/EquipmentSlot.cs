using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour, IDropHandler, IPointerEnterHandler
{
    [SerializeField] private Item itemEquipped;
    [SerializeField] private Image icon;
    [SerializeField] private SlotType slotSpaceType;
    [SerializeField] private DragUI dragComponent;

    public Action<Item, EquipmentSlot> OnPointerEntered;
    public Action OnPointerExited;

    public Item ItemEquipped { get => itemEquipped; set => itemEquipped = value; }
    public Image Icon { get => icon; set => icon = value; }
    public SlotType SlotSpaceType { get => slotSpaceType; set => slotSpaceType = value; }
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
        //EquipmentController.Instance.DropItem(itemEquipped, 1);
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            if (eventData.pointerDrag.GetComponentInParent<InventorySlot>() != null)
            {

                if (eventData.pointerDrag.GetComponentInParent<InventorySlot>().Item != itemEquipped)
                {

                    if (eventData.pointerDrag.GetComponentInParent<InventorySlot>().Item != null)
                    {
                        eventData.pointerDrag.GetComponentInParent<InventorySlot>().Item.Use();
                    }
                }
            }
        }
    }

    public void ReturnItemToInventory()
    {
        InventoryController.Instance.AddItem(itemEquipped, 1);
        itemEquipped = null;
        icon.enabled = false;
        EquipmentController.Instance.UpdateEquippedValues();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        OnPointerEntered?.Invoke(itemEquipped, this);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        OnPointerExited?.Invoke();
    }

}
