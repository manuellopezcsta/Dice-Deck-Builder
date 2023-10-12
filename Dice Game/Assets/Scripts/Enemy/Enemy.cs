using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // Si tira error el inspector es xq estas viendo el Enemy Manager, borrar esto. y listo, no se va a ver la lista pero no ta el error.
public class Enemy : CombatesYEventos
{
    public string name;
    public int hp;
    public int currentHp;
    public int armour;
    public int mArmour;
    public bool envenenado;
    public int poisonedTime;
    public Sprite img;
    //public Deck deck;  x si le damos inteligencia y mazos luego.

    public Enemy(Sprite img, string name, int hp, int armour = 0, int mArmour = 0)
    {
        this.name = name;
        this.hp = hp;
        this.currentHp = hp;
        this.armour = armour;
        this.mArmour = mArmour;
        this.img = img;
    }

    public Sprite GetSprite()
    {
        return img;
    }

    public void DebugInfo()
    {
        Debug.Log("NAME: " + this.name + " HP: " + this.hp + " CURRENTHP: " + this.currentHp + " ARMOUR:" + this.armour + " MR: " + this.mArmour);
    }

    public void TomarDaño(int valor)
    {
        int dañoPasa = 999;
        // Si tiene armadura
        if (armour > 0)
        {
            // Si tengo mas  armadura que el ataque
            if (armour > valor)
            {
                dañoPasa = 0;
                armour -= valor;
            }
            else
            {
                dañoPasa = valor - armour;
                armour = 0;
            }
        }
        else
        {
            // SI no tengo armadura
            dañoPasa = valor;
        }
        // Igualamos
        currentHp -= dañoPasa;

        // Mostramos el pop up
        if(dañoPasa == 0)
        {
            PopUpManager.instance.GeneratePopUp("Daño bloqueado !", PopUpManager.POPUPTARGET.ENEMY);
        } else {
            PopUpManager.instance.GeneratePopUp(this.name + " recibio " + dañoPasa + " de daño", PopUpManager.POPUPTARGET.ENEMY);
        }

        // Updateamos el display
        CombatManager.instance.enemyDisplay.UpdateDisplay();
    }

    public void TomarDañoMagico(int valor)
    {
        int dañoPasa = 999;
        // Si tiene armadura
        if (mArmour > 0)
        {
            // Si tengo mas  armadura que el ataque
            if (mArmour > valor)
            {
                dañoPasa = 0;
                mArmour -= valor;
            }
            else
            {
                dañoPasa = valor - mArmour;
                mArmour = 0;
            }
        }
        else
        {
            // SI no tengo armadura
            dañoPasa = valor;
        }
        // Igualamos
        currentHp -= dañoPasa;

        // Mostramos el pop up
        if(dañoPasa == 0)
        {
            PopUpManager.instance.GeneratePopUp("Daño bloqueado !", PopUpManager.POPUPTARGET.ENEMY);
        } else {
            PopUpManager.instance.GeneratePopUp(this.name + " recibio " + dañoPasa + " de daño magico", PopUpManager.POPUPTARGET.ENEMY);
        }
                
        // Updateamos el display
        CombatManager.instance.enemyDisplay.UpdateDisplay();
    }

    // IA DEL ENEMIGO.
    public void TomarAccion(Player player1, Player player2)
    {
        // Para el bloqueador que redirecciona el da;o de un jugador a otro
        if(player1.bloqueador){
            PopUpManager.instance.GeneratePopUp(player1.name + " tanqueo el daño!", PopUpManager.POPUPTARGET.PLAYER1);
            player2 = player1;
        }
        if(player2.bloqueador){
            PopUpManager.instance.GeneratePopUp(player2.name + " tanqueo el daño!", PopUpManager.POPUPTARGET.PLAYER2);
            player1 = player2;
        }

        int valor = Random.Range(0, 7);
        int daño = Random.Range(0, 6);
        switch (valor)
        {
            case 0:
                player1.TomarDaño(daño);
                break;
            case 1:
                player2.TomarDaño(daño);
                break;
            case 2:
                player1.TomarDaño(daño);
                player2.TomarDaño(daño);
                break;
            case 3:
                player1.TomarDañoMagico(daño);
                break;
            case 4:
                player2.TomarDañoMagico(daño);
                break;
            case 5:
                player1.TomarDañoMagico(daño);
                player2.TomarDañoMagico(daño);
                break;
            case 6:
                currentHp += daño;
                currentHp = Mathf.Clamp(currentHp, 0, hp);
                PopUpManager.instance.GeneratePopUp(this.name + " se curo " + valor + " puntos", PopUpManager.POPUPTARGET.ENEMY);
                break;

        }
        // Desactivamos el bloqueador al terminar el turno del enemigo.
        player1.bloqueador = false;
        player2.bloqueador = false;
    }

    public void ChanceEnvenenado(int dado)
    {
        //Chance de envenenar
        int chance = Random.Range(0, 2);
        if (chance == 0)
        {
            poisonedTime += dado; // Si esta muy roto cambiar esto.
            envenenado = true;

            // Mostramos el pop up
            PopUpManager.instance.GeneratePopUp(this.name + " ha sido envenenado!", PopUpManager.POPUPTARGET.ENEMY);
            // Updateamos si sale bien el veneno.
            CombatManager.instance.enemyDisplay.UpdateDisplay();
        } else {
            PopUpManager.instance.GeneratePopUp(" Fallo el veneno!", PopUpManager.POPUPTARGET.ENEMY);
        }
    }

    public void Envenenado()
    {
        //Daño veneno
        if (envenenado)
        {
            if (poisonedTime > 0)
            {
                currentHp -= CombatManager.instance.poisonDotValue;
                PopUpManager.instance.GeneratePopUp(this.name + " sufrio " + CombatManager.instance.poisonDotValue + " puntos por el veneno", PopUpManager.POPUPTARGET.ENEMY);
                poisonedTime -= 1;
            }
            else
            {
                envenenado = false;
            }
        }
    }
}
