using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

[Serializable]
public class Player
{
    public string name;
    public Deck currentDeck;
    public int MaxHp;
    public int currentHp;
    public int armour;
    public int mArmour;
    public int maxArmour = 15;
    public int maxMArmour = 15;
    public bool parryFisico;
    public bool parryMagico;
    public bool envenenado;
    public int poisonedTime;
    public bool bloqueador;
    public int potenciado;
    public Sprite sprite;
    public Sprite spriteFront;
    public Sprite miniSprite;
    public List<Dice> dices;
    public PopUpManager.POPUPTARGET identifier;


    public Player(string name, Deck startingDeck, int MaxHp, int armour, int mArmour, List<Dice> dices, Sprite img = null,Sprite imgFront = null, PopUpManager.POPUPTARGET identifier = PopUpManager.POPUPTARGET.PLAYER1,Sprite miniSprite = null)
    {
        this.name = name;
        this.currentDeck = startingDeck;
        this.MaxHp = MaxHp;
        this.armour = armour;
        this.mArmour = mArmour;
        this.currentHp = MaxHp;
        this.sprite = img;
        this.spriteFront = imgFront;
        this.miniSprite = miniSprite;
        this.dices = dices;
        this.identifier = identifier;
    }

    public void TomarDaño(int valor, Enemy enemigo = null)
    {
        // Si es el combate final y bloqueador esta activo.
        if(FinalBattleManager.instance != null && bloqueador)
        {
            PopUpManager.instance.GeneratePopUp("Daño Bloqueado !", this.identifier);
            bloqueador = false;
            return;
        }
        if (parryFisico)
        {
            if (FinalBattleManager.instance != null)
            {
                parryFisico = false;
                Player player = FinalBattleManager.instance.GetPlayerN(FinalBattleManager.instance.currentTurn);
                PopUpManager.instance.GeneratePopUp("Parry Exitoso!", this.identifier);
                //Llamar daño al enemigo
                player.TomarDaño(valor * 2);
                return;
            }
            else
            {
                enemigo = CombatManager.instance.GetEnemy();
                PopUpManager.instance.GeneratePopUp("Parry Exitoso!", this.identifier);
                //Llamar daño al enemigo
                enemigo.TomarDaño(valor * 2);
                parryFisico = false;
                return;
            }
        }

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
        if (dañoPasa == 0)
        {
            PopUpManager.instance.GeneratePopUp("Daño bloqueado !", this.identifier);
        }
        else
        {
            PopUpManager.instance.GeneratePopUp(this.name + " recibio " + dañoPasa + " de daño", this.identifier);
        }

        // Updateamos el display
        UpdateDisplayPlayers();
    }

    public void TomarDañoMagico(int valor, Enemy enemigo = null)
    {
        // Si es el combate final y bloqueador esta activo.
        if(FinalBattleManager.instance != null && bloqueador)
        {
            PopUpManager.instance.GeneratePopUp("Daño Bloqueado !", this.identifier);
            bloqueador = false;
            return;
        }
        if (parryMagico)
        {
            if (FinalBattleManager.instance != null)
            {
                parryMagico = false;
                Player player = FinalBattleManager.instance.GetPlayerN(FinalBattleManager.instance.currentTurn);
                PopUpManager.instance.GeneratePopUp("Parry Magico Exitoso!", this.identifier);
                //Llamar daño al enemigo
                player.TomarDañoMagico(valor * 2);
                return;
            }
            else
            {
                enemigo = CombatManager.instance.GetEnemy();
                PopUpManager.instance.GeneratePopUp("Parry Magico Exitoso!", this.identifier);
                //Llamar daño al enemigo
                enemigo.TomarDañoMagico(valor * 2);
                parryMagico = false;
                return;
            }
        }
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
        if (dañoPasa == 0)
        {
            PopUpManager.instance.GeneratePopUp("Daño bloqueado !", this.identifier);
        }
        else
        {
            PopUpManager.instance.GeneratePopUp(this.name + " recibio " + dañoPasa + " de daño", this.identifier);
        }

        // Updateamos el display
        UpdateDisplayPlayers();
    }

    public void Cura(int valor)
    {
        //Cura
        currentHp += valor;
        currentHp = Mathf.Clamp(currentHp, 0, MaxHp);
        UpdateDisplayPlayers();
    }

    public void ChanceEnvenenado(int valor)
    {
        //Chance de envenenar
        int chance = Random.Range(0, 2);
        if (chance == 0)
        {
            envenenado = true;
            // Mostramos el pop up
            PopUpManager.instance.GeneratePopUp(this.name + " ha sido envenenado!", this.identifier);
            poisonedTime += valor;
        }
        else
        {
            PopUpManager.instance.GeneratePopUp(" Fallo el veneno!", this.identifier);
        }
    }

    public void Envenenado()
    {
        //Daño veneno
        if (envenenado)
        {
            if (poisonedTime > 0)
            {
                if (FinalBattleManager.instance != null)
                {
                    currentHp -= FinalBattleManager.instance.poisonDotValue;
                    PopUpManager.instance.GeneratePopUp(this.name + " sufrio " + FinalBattleManager.instance.poisonDotValue + " puntos por el veneno", this.identifier);
                    EffectManager.instance.GenerateEffect(EffectManager.EFFECT_NAME.POISON_TICK, EffectManager.instance.GetCurrentPlayerTarget(FinalBattleManager.instance.GetPlayerN(FinalBattleManager.instance.currentTurn)));
                    poisonedTime -= 1;
                }
                else
                {
                    currentHp -= CombatManager.instance.poisonDotValue;
                    PopUpManager.instance.GeneratePopUp(this.name + " sufrio " + CombatManager.instance.poisonDotValue + " puntos por el veneno", this.identifier);
                    EffectManager.instance.GenerateEffect(EffectManager.EFFECT_NAME.POISON_TICK, EffectManager.instance.GetCurrentPlayerTarget(CombatManager.instance.GetPlayerN(CombatManager.instance.currentTurn)));
                    poisonedTime -= 1;
                }
            }
            else
            {
                envenenado = false;
            }
        }
    }
    public void UpdateDisplayPlayers()
    {
        if (FinalBattleManager.instance != null)
        {
            FinalBattleManager.instance.p1Display.UpdateDisplay();
            FinalBattleManager.instance.p2Display.UpdateDisplay();
        }
        else
        {
            CombatManager.instance.p1Display.UpdateDisplay();
            CombatManager.instance.p2Display.UpdateDisplay();
        }
    }
}