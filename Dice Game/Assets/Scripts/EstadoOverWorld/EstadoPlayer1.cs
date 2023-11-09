using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class EstadoPlayer1 : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    
    [SerializeField] Image sprite;
    [SerializeField] Image hpBar;
    [SerializeField] TextMeshProUGUI armour;
    [SerializeField] TextMeshProUGUI mrArmour;
    [SerializeField] GameObject objecthpBar;
    [SerializeField] GameObject objectArmour;
    [SerializeField] GameObject objectmAmour;


    private void Start()
    {
        //carga por primera vez los sprites de los players
        Player player = GuardaRopas.instance.player1;
        sprite.sprite = player.miniSprite;
        Debug.Log(player.name);

    }
    public void OnPointerEnter (PointerEventData eventData)
    {
        UpdatePlayers();
        objecthpBar.SetActive(true);
        objectArmour.SetActive(true);
        objectmAmour.SetActive(true);

    }
    public void OnPointerExit (PointerEventData eventData)
    {
        objecthpBar.SetActive(false);
        objectArmour.SetActive(false);
        objectmAmour.SetActive(false);
    }
    public void UpdatePlayers()
    {
        //player 1
        Player player = GuardaRopas.instance.player1;
        //sprite.sprite = player.miniSprite;
        armour.text = player.armour.ToString();
        mrArmour.text = player.mArmour.ToString();
        hpBar.fillAmount = (float)player.currentHp / player.MaxHp;
    }
}

