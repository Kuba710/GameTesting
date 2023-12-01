using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    [SerializeField] private Item item;

    public Item Item { get => item; set => item = value; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!InventoryController.Instance.IsInventoryFull)
            {
                InventoryController.Instance.AddItem(item, 1);
                Destroy(gameObject);
            }
        }
    }

    [Button]
    public void SetupSpriteRenderer()
    {
        if (gameObject.TryGetComponent(out SpriteRenderer spriteRenderer))
        {
            spriteRenderer.sprite = item.ItemIcon;
        }
    }
}
