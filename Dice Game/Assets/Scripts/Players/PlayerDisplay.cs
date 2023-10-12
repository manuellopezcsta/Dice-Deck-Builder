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
    [SerializeField] Image playerPoison;
    [SerializeField] TextMeshProUGUI poisonTurns;
    public void UpdateDisplay()
    {
        Player player = CombatManager.instance.GetPlayerN(playerNumber);
        armour.text = player.armour.ToString();
        mrArmour.text = player.mArmour.ToString();
        hpBar.fillAmount = (float) player.currentHp / player.MaxHp;
        Debug.Log("Player" + playerNumber + " Hp at: " + player.currentHp);
        // Agregar check para ver en player si esta envenenado y mostrar el gameobject si lo esta
        if(player.envenenado)
        {
            playerPoison.gameObject.SetActive(true);
            // Actualizar el texto de turnos.
            poisonTurns.text = player.poisonedTime.ToString();
            // Si es el ultimo tick lo apagamos.
            if(player.poisonedTime == 0)
            {
                playerPoison.gameObject.SetActive(false);
            }
        } else {
            playerPoison.gameObject.SetActive(false);
        }
        
    }
}
