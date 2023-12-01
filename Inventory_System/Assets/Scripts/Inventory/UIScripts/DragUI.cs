using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragUI : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private CanvasGroup canvasGroup;
    private Vector2 initialPosition;

    public Action OnDropped;
    private void Awake()
    {
        initialPosition = transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.parent.transform.SetAsLastSibling();
        canvasGroup.blocksRaycasts = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        GetComponentInParent<Canvas>().sortingOrder = 1;
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponentInParent<Canvas>().sortingOrder = 0;
        if (!RectTransformUtility.RectangleContainsScreenPoint(InventoryController.Instance.InventoryPanel, Input.mousePosition) && !RectTransformUtility.RectangleContainsScreenPoint(EquipmentController.Instance.EquipmentPanel, Input.mousePosition))
        {
            OnDropped?.Invoke();
        }
        transform.position = initialPosition;
        canvasGroup.blocksRaycasts = true;
    }

}
