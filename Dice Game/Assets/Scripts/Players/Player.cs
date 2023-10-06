using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player
{
    public string name;
    public Deck currentDeck;
    public int MaxHp;
    public int currentHp;
    public int armour;
    public int mArmour;
    public int maxArmour = 50;
    public int maxMArmour = 50;
    public bool parryFisico;
    public bool parryMagico;
    public bool envenenado;
    public int poisonedTime;
    public Sprite sprite;
    public List<Dice> dices;


    public Player(string name, Deck startingDeck, int MaxHp, int armour, int mArmour, List<Dice> dices, Sprite img = null)
    {
        this.name = name;
        this.currentDeck = startingDeck;
        this.MaxHp = MaxHp;
        this.armour = armour;
        this.mArmour = mArmour;
        this.currentHp = MaxHp;
        this.sprite = img;
        this.dices = dices;
    }

    public void TomarDaño(int valor, Enemy enemigo = null)
    {
        if (parryFisico)
        {
            enemigo = CombatManager.instance.GetEnemy();
            //Llamar daño al enemigo
            enemigo.TomarDaño(valor * 2);
            parryFisico = false;
            return;
        }

        int dañoPasa = 999;
        // Si tiene armadura
        if(armour > 0)
        {
            // Si tengo mas  armadura que el ataque
            if(armour > valor)
            {
                dañoPasa = 0;
                armour -= valor;
            }
            else{
                dañoPasa = valor - armour;
                armour = 0;
            }
        } else {
            // SI no tengo armadura
            dañoPasa = valor;
        }
        // Igualamos
        currentHp -= dañoPasa;
                
        // Updateamos el display
        CombatManager.instance.p1Display.UpdateDisplay();
        CombatManager.instance.p2Display.UpdateDisplay();
    }

    public void TomarDañoMagico(int valor, Enemy enemigo = null)
    {
        if (parryMagico)
        {
            enemigo = CombatManager.instance.GetEnemy();
            //Llamar daño al enemigo
            enemigo.TomarDañoMagico(valor * 2);
            parryMagico = false;
            return;
        }
        int dañoPasa = 999;
        // Si tiene armadura
        if(mArmour > 0)
        {
            // Si tengo mas  armadura que el ataque
            if(mArmour > valor)
            {
                dañoPasa = 0;
                mArmour -= valor;
            }
            else{
                dañoPasa = valor - mArmour;
                mArmour = 0;
            }
        } else {
            // SI no tengo armadura
            dañoPasa = valor;
        }
        // Igualamos
        currentHp -= dañoPasa;
                
        // Updateamos el display
        CombatManager.instance.p1Display.UpdateDisplay();
        CombatManager.instance.p2Display.UpdateDisplay();
    }

    public void Cura(int valor){
        //Cura
        currentHp += valor;
        currentHp = Mathf.Clamp(currentHp,0,MaxHp);
                
        // Updateamos el display
        CombatManager.instance.p1Display.UpdateDisplay();
        CombatManager.instance.p2Display.UpdateDisplay();       
    }

    public void ChanceEnvenenado(int valor){
        //Chance de envenenar
        int chance = Random.Range(0,2);
        if(chance == 0){
            envenenado = true;
            poisonedTime += valor;
        }
    }

    public void Envenenado(){
        //Daño veneno
         if(envenenado){
            if(poisonedTime < 0){
                currentHp -= CombatManager.instance.poisonDotValue;
                poisonedTime -= 1;
            }
            else{
                envenenado = false;
            }
        }
    }    
}