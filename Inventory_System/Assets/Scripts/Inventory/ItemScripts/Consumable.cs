using UnityEngine;
public enum ConsumableType { Drink, Food, Potion }

[CreateAssetMenu(fileName = "Consumable", menuName = "Item/Consumable")]
public class Consumable : Item
{
    public ConsumableType ConsumableType;
 
    public int PointsRecover;

    public override void Use()
    {
        base.Use();
        InventoryController.Instance.RemoveItem(this, 1);
    }
   
}
