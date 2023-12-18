using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusUI : MonoBehaviour
{
    public Text statusText;
    public void UpdateStatusUI(List<PlayerStats> statName, List<float> totalValue, List<float> value)
    {
        statusText.text = "Player Stats:\n";
        for (int i = 0; i < statName.Count; i++)
        {
            if (totalValue[i] != value[i])
            {

                if (totalValue[i] > value[i])
                {

                    statusText.text = statusText.text + statName[i] + ": " + totalValue[i] + " (" + value[i] + " + " + (totalValue[i] - value[i]) + ")\n";
                }
                else
                {
                    statusText.text = statusText.text + statName[i] + ": " + totalValue[i] + " (" + value[i] + " - " + Mathf.Abs(totalValue[i] - value[i]) + ")\n";
                }
            }
            else
            {
                statusText.text = statusText.text + statName[i] + ": " + totalValue[i] + "\n";
            }
        }



    }
}
