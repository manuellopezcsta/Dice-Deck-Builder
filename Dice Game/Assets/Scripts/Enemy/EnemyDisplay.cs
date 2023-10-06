using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

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
        //Debug.Log("CHP: " + enemy.currentHp + " MHP: " + enemy.hp);
        //Debug.Log(((1f* enemy.currentHp) / (1f *enemy.hp)).ToString());
        hpBar.fillAmount = (float) enemy.currentHp / enemy.hp;
        Debug.Log("Enemy Hp at: " + enemy.currentHp);
    }
}
