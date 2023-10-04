using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDisplay : MonoBehaviour
{
    [SerializeField] Image hpBar;
    [SerializeField] TextMeshProUGUI armour;
    [SerializeField] TextMeshProUGUI mrArmour;
    [SerializeField] int playerNumber;

    public void UpdateDisplay()
    {
        Player player = CombatManager.instance.GetPlayerN(playerNumber);
        armour.text = player.armour.ToString();
        mrArmour.text = player.mArmour.ToString();
        hpBar.fillAmount = ((player.currentHp * 100f) / player.MaxHp) / 100f;
        Debug.Log("Player" + playerNumber + " Hp at: " + player.currentHp);
    }
}
