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
    [SerializeField] Image enemyPoison;
    [SerializeField] TextMeshProUGUI poisonTurns;

    public void UpdateDisplay()
    {
        Enemy enemy = CombatManager.instance.GetEnemy();
        armour.text = enemy.armour.ToString();
        mrArmour.text = enemy.mArmour.ToString();
        //Debug.Log("CHP: " + enemy.currentHp + " MHP: " + enemy.hp);
        //Debug.Log(((1f* enemy.currentHp) / (1f *enemy.hp)).ToString());
        hpBar.fillAmount = (float) enemy.currentHp / enemy.hp;
        Debug.Log("Enemy Hp at: " + enemy.currentHp);

        // Agregar check para ver en player si esta envenenado y mostrar el gameobject si lo esta
        if(enemy.envenenado)
        {
            enemyPoison.gameObject.SetActive(true);
            // Actualizar el texto de turnos.
            poisonTurns.text = enemy.poisonedTime.ToString();
            // Si es el ultimo tick lo apagamos.
            if(enemy.poisonedTime == 0)
            {
                enemyPoison.gameObject.SetActive(false);
            }
        } else{
            enemyPoison.gameObject.SetActive(false);
        }
    }
}
