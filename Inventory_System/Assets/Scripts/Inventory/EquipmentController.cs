using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PlayerStats
{ Speed, Attack, Defense, MaxHealth }

public class EquipmentController : Singleton<EquipmentController>
{
    [SerializeField] private RectTransform equipmentPanel;
    [SerializeField] private StatusUI statusUI;
    [SerializeField] private ItemInfoUI itemInfoUI;
    [SerializeField] private List<EquipmentSlot> equipmentSlots = new List<EquipmentSlot>();
    [SerializeField] private List<PlayerStats> playerStats;
    [SerializeField] private Vector2 infoPanelPivot;
    private bool isOpened;

    public List<EquipmentSlot> EquipmentSlots { get => equipmentSlots; set => equipmentSlots = value; }
    public RectTransform EquipmentPanel { get => equipmentPanel; set => equipmentPanel = value; }
    public StatusUI StatusUI { get => statusUI; set => statusUI = value; }
    private void OnEnable()
    {
        foreach (var eqSlot in equipmentSlots)
        {
            eqSlot.OnPointerEntered += ShowItemInfoPanel;
            eqSlot.OnPointerExited += CloseItemInfoPanel;
        }
    }

    private void OnDisable()
    {
        foreach (var eqSlot in equipmentSlots)
        {
            eqSlot.OnPointerEntered -= ShowItemInfoPanel;
            eqSlot.OnPointerExited -= CloseItemInfoPanel;
        }
    }
    private void CloseItemInfoPanel()
    {
        itemInfoUI.ClosePanel();
    }

    private void ShowItemInfoPanel(Item item, EquipmentSlot eqSlot)
    {
        itemInfoUI.ShowInfoPanel(item, infoPanelPivot, eqSlot.transform.position);
    }
    public void ToggleActivity()
    {
        isOpened = !isOpened;
        equipmentPanel.gameObject.SetActive(isOpened);
    }

    public void UpdateEquippedValues()
    {
        foreach (EquipmentSlot slot in equipmentSlots)
        {
            if (slot.ItemEquipped != null)
            {
                if (slot.ItemEquipped.GetType() == typeof(Armour))
                {
                    Armour armour = slot.ItemEquipped as Armour;
                    for (int i = 0; i < armour.StatsToModify.Count; i++)
                    {

                        foreach (PlayerStats stat in playerStats)
                        {
                            if (armour.StatsToModify[i] == stat)
                            {
                                if (armour.ModifyValue[i].Contains("%"))
                                {

                                    string[] stgArray = armour.ModifyValue[i].Split(char.Parse("%"));

                                    float percentage = float.Parse(stgArray[0]) / 100;
                                }
                                else
                                {

                                }
                            }
                        }
                    }
                }
                else if (slot.ItemEquipped.GetType() == typeof(Weapon))
                {
                    Weapon weapon = slot.ItemEquipped as Weapon;
                    for (int i = 0; i < weapon.StatsToModify.Count; i++)
                    {

                        foreach (PlayerStats stat in playerStats)
                        {
                            if (weapon.StatsToModify[i] == stat)
                            {
                                if (weapon.ModifyValue[i].Contains("%"))
                                {

                                    string[] stgArray = weapon.ModifyValue[i].Split(char.Parse("%"));

                                    float percentage = float.Parse(stgArray[0]) / 100;

                                }
                                else
                                {

                                }



                            }
                        }
                    }
                }
            }
        }

        //statusUI.UpdateStatusUI(playerStats, statsTotalValue, statsValue);
    }
    public void DropItem(Item itemEquipped)
    {
        foreach (var slot in equipmentSlots)
        {
            if (slot.ItemEquipped = itemEquipped)
            {
                slot.ItemEquipped = null;
                slot.Icon.enabled = false;
                UpdateEquippedValues();
                //statusUI.UpdateStatusUI(playerStats, statsTotalValue, statsValue);
                ItemSpawner.Instance.SpawnItem(itemEquipped);
                return;
            }
        }
    }
}
