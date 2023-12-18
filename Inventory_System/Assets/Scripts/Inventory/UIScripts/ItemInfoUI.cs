using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoUI : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private Text nameText, descriptionText;
    [SerializeField] private Image icon;

    public void ShowInfoPanel(Item itemInfo, Vector2 rectPivot, Vector3 targetPosition)
    {
        if (itemInfo != null)
        {
            rectTransform.pivot = rectPivot;
            rectTransform.position = targetPosition;
            infoPanel.SetActive(true);

            nameText.text = itemInfo.ItemName;
            icon.sprite = itemInfo.ItemIcon;

            if (itemInfo.GetType() == typeof(Armour))
            {
                Armour armour = itemInfo as Armour;

                descriptionText.text = armour.Description + "\n";
                for (int i = 0; i < armour.StatsToModify.Count; i++)
                {

                    if (armour.ModifyValue[i].Contains("%"))
                    {

                        string[] stgArray = armour.ModifyValue[i].Split(char.Parse("%"));
                        if (float.Parse(stgArray[0]) > 0)
                        {
                            descriptionText.text = descriptionText.text + "+" + armour.ModifyValue[i] + " " + armour.StatsToModify[i] + "\n";
                        }
                        else
                        {
                            descriptionText.text = descriptionText.text + "- " + Mathf.Abs(float.Parse(stgArray[0])) + "% " + armour.StatsToModify[i] + "\n";
                        }
                    }
                    else
                    {

                        if (float.Parse(armour.ModifyValue[i]) > 0) { descriptionText.text = descriptionText.text + "+" + armour.ModifyValue[i] + " " + armour.StatsToModify[i] + "\n"; }
                        else

                        { descriptionText.text = descriptionText.text + "- " + Mathf.Abs(float.Parse(armour.ModifyValue[i])) + " " + armour.StatsToModify[i] + "\n"; }
                    }




                }

            }
            else if (itemInfo.GetType() == typeof(Weapon))
            {
                Weapon weapon = itemInfo as Weapon;

                descriptionText.text = weapon.WeaponType + "\n" + weapon.WeaponSize + "\n";
                for (int i = 0; i < weapon.StatsToModify.Count; i++)
                {
                    if (weapon.ModifyValue[i].Contains("%"))
                    {

                        string[] stgArray = weapon.ModifyValue[i].Split(char.Parse("%"));
                        if (float.Parse(stgArray[0]) > 0)
                        {
                            descriptionText.text = descriptionText.text + "+" + weapon.ModifyValue[i] + " " + weapon.StatsToModify[i] + "\n";
                        }
                        else
                        {
                            descriptionText.text = descriptionText.text + "- " + Mathf.Abs(float.Parse(stgArray[0])) + "% " + weapon.StatsToModify[i] + "\n";
                        }
                    }
                    else
                    {

                        if (float.Parse(weapon.ModifyValue[i]) > 0) { descriptionText.text = descriptionText.text + "+" + weapon.ModifyValue[i] + " " + weapon.StatsToModify[i] + "\n"; }
                        else

                        { descriptionText.text = descriptionText.text + "- " + Mathf.Abs(float.Parse(weapon.ModifyValue[i])) + " " + weapon.StatsToModify[i] + "\n"; }
                    }
                }
            }
            else
            {
                descriptionText.text = "";
            }



        }
        else
        {
            infoPanel.SetActive(false);
        }
    }

    public void ClosePanel()
    {
        infoPanel.SetActive(false);
    }




}
