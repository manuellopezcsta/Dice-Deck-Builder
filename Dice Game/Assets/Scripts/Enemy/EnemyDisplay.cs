using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EnemyDisplay : MonoBehaviour
{
    [SerializeField] Image hpBar;
    [SerializeField] TextMeshProUGUI armour;
    [SerializeField] TextMeshProUGUI mrArmour;

    public void UpdateDisplay()
    {
        Enemy enemy = CombatManager.instance.GetEnemy();
        armour.text = enemy.armour.ToString();
        mrArmour.text = enemy.mArmour.ToString();
        hpBar.fillAmount = ((enemy.currentHp * 100f) /enemy.hp) / 100f;
        Debug.Log("Enemy Hp at: " + enemy.currentHp);
    }
}
