using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class EnemyDisplay : MonoBehaviour
{
    [Header("StastBoss")]
    [SerializeField] Image hpBar;
    [SerializeField] Image hpBarEspejo;
    [SerializeField] TextMeshProUGUI armour;
    [SerializeField] TextMeshProUGUI mrArmour;
    [SerializeField] Image enemyPoison;
    [SerializeField] TextMeshProUGUI poisonTurns;
    [Header("StastDefault")]
    [SerializeField] Image hpBarDef;
    [SerializeField] TextMeshProUGUI armourDef;
    [SerializeField] TextMeshProUGUI mrArmourDef;
    [SerializeField] Image enemyPoisonDef;
    [SerializeField] TextMeshProUGUI poisonTurnsDef;
    //se van a updatear tanto la default como la boss para despues poder apagarlas y prenderlas segun el enemy
    public void UpdateDisplay()
    {  //Set stast boss
        Enemy enemy = CombatManager.instance.GetEnemy();
        armour.text = enemy.armour.ToString();
        mrArmour.text = enemy.mArmour.ToString();
        hpBar.fillAmount = (float) enemy.currentHp / enemy.hp;
        if(hpBarEspejo != null)
        {
            hpBarEspejo.fillAmount = hpBar.fillAmount;
        }
        //Debug.Log("Enemy Hp at: " + enemy.currentHp);

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
        //set stast default;
        armourDef.text = enemy.armour.ToString();
        mrArmourDef.text = enemy.mArmour.ToString();
        hpBarDef.fillAmount = (float)enemy.currentHp / enemy.hp;
  
        // Agregar check para ver en player si esta envenenado y mostrar el gameobject si lo esta
        if (enemy.envenenado)
        {
            enemyPoisonDef.gameObject.SetActive(true);
            // Actualizar el texto de turnos.
            poisonTurnsDef.text = enemy.poisonedTime.ToString();
            // Si es el ultimo tick lo apagamos.
            if (enemy.poisonedTime == 0)
            {
                enemyPoisonDef.gameObject.SetActive(false);
            }
        }
        else
        {
            enemyPoisonDef.gameObject.SetActive(false);
        }
    }
    


}
