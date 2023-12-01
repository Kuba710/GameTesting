
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using UnityEngine;
public class Item : ScriptableObject
{
    public string Guid;
    [HideLabel, PreviewField(45)]
    [HorizontalGroup("Split", width: 45)]
    public Sprite ItemIcon;
    public string ItemName;
    public int Price;
    public bool Stackable;
    public string Description;

    public virtual void Use()
    {

    }

    [Button]
    private void SetNewGuid()
    {
        Guid = System.Guid.NewGuid().ToString();
    }
}
