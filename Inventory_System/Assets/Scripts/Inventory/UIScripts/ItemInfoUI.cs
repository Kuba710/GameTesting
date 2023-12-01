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
                for (int i = 0; i < armour.statsToModify.Count; i++)
                {

                    if (armour.modifyValue[i].Contains("%"))
                    {

                        string[] stgArray = armour.modifyValue[i].Split(char.Parse("%"));
                        if (float.Parse(stgArray[0]) > 0)
                        {
                            descriptionText.text = descriptionText.text + "+" + armour.modifyValue[i] + " " + armour.statsToModify[i] + "\n";
                        }
                        else
                        {
                            descriptionText.text = descriptionText.text + "- " + Mathf.Abs(float.Parse(stgArray[0])) + "% " + armour.statsToModify[i] + "\n";
                        }
                    }
                    else
                    {

                        if (float.Parse(armour.modifyValue[i]) > 0) { descriptionText.text = descriptionText.text + "+" + armour.modifyValue[i] + " " + armour.statsToModify[i] + "\n"; }
                        else

                        { descriptionText.text = descriptionText.text + "- " + Mathf.Abs(float.Parse(armour.modifyValue[i])) + " " + armour.statsToModify[i] + "\n"; }
                    }




                }

            }
            else if (itemInfo.GetType() == typeof(Weapon))
            {
                Weapon weapon = itemInfo as Weapon;

                descriptionText.text = weapon.weaponType + "\n" + weapon.weaponSize + "\n";
                for (int i = 0; i < weapon.statsToModify.Count; i++)
                {
                    if (weapon.modifyValue[i].Contains("%"))
                    {

                        string[] stgArray = weapon.modifyValue[i].Split(char.Parse("%"));
                        if (float.Parse(stgArray[0]) > 0)
                        {
                            descriptionText.text = descriptionText.text + "+" + weapon.modifyValue[i] + " " + weapon.statsToModify[i] + "\n";
                        }
                        else
                        {
                            descriptionText.text = descriptionText.text + "- " + Mathf.Abs(float.Parse(stgArray[0])) + "% " + weapon.statsToModify[i] + "\n";
                        }
                    }
                    else
                    {

                        if (float.Parse(weapon.modifyValue[i]) > 0) { descriptionText.text = descriptionText.text + "+" + weapon.modifyValue[i] + " " + weapon.statsToModify[i] + "\n"; }
                        else

                        { descriptionText.text = descriptionText.text + "- " + Mathf.Abs(float.Parse(weapon.modifyValue[i])) + " " + weapon.statsToModify[i] + "\n"; }
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
