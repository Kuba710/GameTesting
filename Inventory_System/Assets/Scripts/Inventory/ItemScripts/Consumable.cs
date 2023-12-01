using UnityEngine;
[CreateAssetMenu(fileName = "Consumable", menuName = "Item/Consumable")]
public class Consumable : Item
{
    public ConsumableType consumableType;
 
    public int PointsRecover;

    public override void Use()
    {
        base.Use();
        InventoryController.Instance.RemoveItem(this, 1);
    }

    public enum ConsumableType { Drink, Food, Potion }
   
}
