using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

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
    public bool bloqueador;
    public int potenciado;
    public Sprite sprite;
    public List<Dice> dices;
    public PopUpManager.POPUPTARGET identifier;


    public Player(string name, Deck startingDeck, int MaxHp, int armour, int mArmour, List<Dice> dices, Sprite img = null, PopUpManager.POPUPTARGET identifier = PopUpManager.POPUPTARGET.PLAYER1)
    {
        this.name = name;
        this.currentDeck = startingDeck;
        this.MaxHp = MaxHp;
        this.armour = armour;
        this.mArmour = mArmour;
        this.currentHp = MaxHp;
        this.sprite = img;
        this.dices = dices;
        this.identifier = identifier;
    }

    public void TomarDaño(int valor, Enemy enemigo = null)
    {
        if (parryFisico)
        {
            enemigo = CombatManager.instance.GetEnemy();
            PopUpManager.instance.GeneratePopUp("Parry Exitoso!", this.identifier);
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

        // Mostramos el pop up
        PopUpManager.instance.GeneratePopUp(this.name + " recibio " + dañoPasa + " de daño", this.identifier);
                
        // Updateamos el display
        CombatManager.instance.p1Display.UpdateDisplay();
        CombatManager.instance.p2Display.UpdateDisplay();
    }

    public void TomarDañoMagico(int valor, Enemy enemigo = null)
    {
        if (parryMagico)
        {
            enemigo = CombatManager.instance.GetEnemy();
            PopUpManager.instance.GeneratePopUp("Parry Magico Exitoso!", this.identifier);
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

        // Mostramos el pop up
        PopUpManager.instance.GeneratePopUp(this.name + " recibio " + dañoPasa + " de daño", this.identifier);
                
        // Updateamos el display
        CombatManager.instance.p1Display.UpdateDisplay();
        CombatManager.instance.p2Display.UpdateDisplay();
    }

    public void Cura(int valor){
        //Cura
        currentHp += valor;
        currentHp = Mathf.Clamp(currentHp,0,MaxHp);

        // Mostramos el pop up
        PopUpManager.instance.GeneratePopUp(this.name + " se curo " + valor + " puntos", this.identifier);
                
        // Updateamos el display
        CombatManager.instance.p1Display.UpdateDisplay();
        CombatManager.instance.p2Display.UpdateDisplay();       
    }

    public void ChanceEnvenenado(int valor){
        //Chance de envenenar
        int chance = Random.Range(0,2);
        if(chance == 0){
            envenenado = true;
            // Mostramos el pop up
            PopUpManager.instance.GeneratePopUp(this.name + " ha sido envenenado!", this.identifier);
            poisonedTime += valor;
        } else {
            PopUpManager.instance.GeneratePopUp(" Fallo el veneno!", this.identifier);
        }
    }

    public void Envenenado(){
        //Daño veneno
         if(envenenado){
            if(poisonedTime < 0){
                currentHp -= CombatManager.instance.poisonDotValue;
                PopUpManager.instance.GeneratePopUp(this.name + " sufrio " + CombatManager.instance.poisonDotValue + " puntos por el veneno", this.identifier);
                poisonedTime -= 1;
            }
            else{
                envenenado = false;
            }
        }
    }    
}