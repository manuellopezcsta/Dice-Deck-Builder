using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class Card
{
    public string cardName;
    public Sprite image;
    public CardEffects effect;

    public Card(string cardName, Sprite image, CardEffects effect)
    {
        this.cardName = cardName;
        this.image = image;
        this.effect = effect;
    }

    public enum CardEffects
    {
        ATAQUE,
        AMAGICO,
        CURAR,
        ARMADURA,
        MR,
        BREAK,
        PARRYATAQUE,
        PARRYAMAGICO,
        VENENO,
        CURAX2,
        POTENCIADOR,
        ARMADURAX2,
        BLOQUEADOR,
        ATAQUEROMP,
        AMAGICOROMP,
        BREAKROMP,
        VENENOROMP,
    }
    public void UsarCartaAtaque(Enemy target, Dice dado)
    {
        if (FinalBattleManager.instance != null) // Si no es la batalla final funciona la carta
        {
            // LO QUE PASA SI NO EXISTE TARGET , DUELO FINAL
            PopUpManager.instance.GeneratePopUp("Esta carta no sirve en pvp", PopUpManager.POPUPTARGET.ENEMY);
            return;
        }
        else
        {
            target.TomarDaño(dado.currentValue);
        }
    }
    public void UsarCartaAmagico(Enemy target, Dice dado)
    {
        if (FinalBattleManager.instance != null) // Si no es la batalla final funciona la carta
        {
            // LO QUE PASA SI NO EXISTE TARGET , DUELO FINAL
            PopUpManager.instance.GeneratePopUp("Esta carta no sirve en pvp", PopUpManager.POPUPTARGET.ENEMY);
            return;
        }
        else
        {
            target.TomarDañoMagico(dado.currentValue);
        }

    }
    public void UsarCartaCurar(Player player, Dice dado)
    {
        player.Cura(dado.currentValue);
        PopUpManager.instance.GeneratePopUp("+" + dado.currentValue + " HP", player.identifier);
    }
    public void UsarCartaArmadura(Player player, Dice dado)
    {
        player.armour += dado.currentValue;
        player.armour = Mathf.Clamp(player.armour, 0, player.maxArmour);
        PopUpManager.instance.GeneratePopUp("+" + dado.currentValue + " de armadura", player.identifier);
    }
    public void UsarCartaMr(Player player, Dice dado)
    {
        player.mArmour += dado.currentValue;
        player.mArmour = Mathf.Clamp(player.mArmour, 0, player.maxMArmour);
        PopUpManager.instance.GeneratePopUp("+" + dado.currentValue + " de armadura magica", player.identifier);
    }
    public void UsarCartaBreak(Enemy target, Dice dado)
    {
        if (FinalBattleManager.instance != null) // Si no es la batalla final funciona la carta
        {
            // LO QUE PASA SI NO EXISTE TARGET , DUELO FINAL
            PopUpManager.instance.GeneratePopUp("Esta carta no sirve en pvp", PopUpManager.POPUPTARGET.ENEMY);
            return;
        }
        else
        {
            target.armour -= dado.currentValue;
            target.mArmour -= dado.currentValue;
            target.armour = Mathf.Clamp(target.armour, 0, 50); // no tiene max
            target.mArmour = Mathf.Clamp(target.mArmour, 0, 50); // no tiene max
            PopUpManager.instance.GeneratePopUp(target.name + " perdio " + dado.currentValue + " de armadura y mr", PopUpManager.POPUPTARGET.ENEMY);
        }


    }
    public void UsarCartaParryAtaque(Player player)
    {
        player.parryFisico = true;
        PopUpManager.instance.GeneratePopUp("Parry fisico activo", player.identifier);
    }
    public void UsarCartaParryAMagico(Player player)
    {
        player.parryMagico = true;
        PopUpManager.instance.GeneratePopUp("Parry magico activo", player.identifier);
    }
    public void UsarCartaVeneno(Enemy target, Dice dado)
    {
        if (FinalBattleManager.instance != null) // Si no es la batalla final funciona la carta
        {
            // LO QUE PASA SI NO EXISTE TARGET , DUELO FINAL
            PopUpManager.instance.GeneratePopUp("Esta carta no sirve en pvp", PopUpManager.POPUPTARGET.ENEMY);
            return;
        }
        else
        {
            target.ChanceEnvenenado(dado.currentValue);
        }
    }
    public void UsarCartaCurax2(Player player1, Player player2, Dice dado)
    {
        UsarCartaCurar(player1, dado);
        UsarCartaCurar(player2, dado);
    }
    public void UsarCartaPotenciador(Player player, Dice dado)
    {
        // Si se usa con un 1-3 da un +1 a todos los dados del aliado. Si se usa con un 4-6 da un +2.
        if (dado.currentValue <= 3 && dado.currentValue != 0)
        {
            // Potenciado +1
            player.potenciado = 1;
            PopUpManager.instance.GeneratePopUp("Dando +1 a los dados aliados", player.identifier);
        }
        else
        {
            // Potenciado +2
            player.potenciado = 2;
            PopUpManager.instance.GeneratePopUp("Dando +2 a los dados aliados", player.identifier);
        }

    }
    public void UsarCartaArmadurax2(Player player1, Player player2, Dice dado)
    {
        UsarCartaArmadura(player1, dado);
        UsarCartaArmadura(player2, dado);
    }
    public void UsarCartaBloqueador(Player player)
    {
        player.bloqueador = true;
        PopUpManager.instance.GeneratePopUp("Bloqueador activo", player.identifier);
    }
    public void UsarCartaAtaqueRomp(Player player, Dice dado)
    {
        player.TomarDaño(dado.currentValue);
    }
    public void UsarCartaAMagicoRomp(Player player, Dice dado)
    {
        player.TomarDañoMagico(dado.currentValue);
    }
    public void UsarCartaBreakRomp(Player player, Dice dado)
    {
        player.armour -= dado.currentValue;
        player.mArmour -= dado.currentValue;
        player.armour = Mathf.Clamp(player.armour, 0, player.maxArmour);
        player.mArmour = Mathf.Clamp(player.mArmour, 0, player.maxMArmour);
        PopUpManager.instance.GeneratePopUp(player.name + " perdio " + dado.currentValue + " de armadura y mr", player.identifier);
    }
    public void UsarCartaVenenoRomp(Player player, Dice dado)
    {
        player.ChanceEnvenenado(dado.currentValue);
    }

    public void RunLogic(Card card, Dice dado)
    {
        Enemy enemigo;
        Player actualPlayer;
        Player allyPlayer;
        if (FinalBattleManager.instance != null)
        {
            enemigo = null;
            actualPlayer = FinalBattleManager.instance.GetPlayerN(FinalBattleManager.instance.currentTurn);
            allyPlayer = FinalBattleManager.instance.GetAlly(FinalBattleManager.instance.currentTurn);
        }
        else
        {
            enemigo = CombatManager.instance.GetEnemy();
            actualPlayer = CombatManager.instance.GetPlayerN(CombatManager.instance.currentTurn);
            allyPlayer = CombatManager.instance.GetAlly(CombatManager.instance.currentTurn);
        }


        switch (card.effect)
        {
            case CardEffects.ATAQUE:
                UsarCartaAtaque(enemigo, dado);
                break;
            case CardEffects.AMAGICO:
                UsarCartaAmagico(enemigo, dado);
                break;
            case CardEffects.CURAR:
                UsarCartaCurar(actualPlayer, dado);
                break;
            case CardEffects.ARMADURA:
                UsarCartaArmadura(actualPlayer, dado);
                break;
            case CardEffects.MR:
                UsarCartaMr(actualPlayer, dado);
                break;
            case CardEffects.BREAK:
                UsarCartaBreak(enemigo, dado);
                break;
            case CardEffects.PARRYATAQUE:
                UsarCartaParryAtaque(actualPlayer);
                break;
            case CardEffects.PARRYAMAGICO:
                UsarCartaParryAMagico(actualPlayer);
                break;
            case CardEffects.VENENO:
                UsarCartaVeneno(enemigo, dado);
                break;
            case CardEffects.CURAX2:
                UsarCartaCurax2(actualPlayer, allyPlayer, dado);
                break;
            case CardEffects.POTENCIADOR:
                UsarCartaPotenciador(actualPlayer, dado);
                break;
            case CardEffects.ARMADURAX2:
                UsarCartaArmadurax2(actualPlayer, allyPlayer, dado);
                break;
            case CardEffects.BLOQUEADOR:
                UsarCartaBloqueador(actualPlayer);
                break;
            case CardEffects.ATAQUEROMP:
                UsarCartaAtaqueRomp(allyPlayer, dado);
                break;
            case CardEffects.AMAGICOROMP:
                UsarCartaAMagicoRomp(allyPlayer, dado);
                break;
            case CardEffects.BREAKROMP:
                UsarCartaBreakRomp(allyPlayer, dado);
                break;
            case CardEffects.VENENOROMP:
                UsarCartaVenenoRomp(allyPlayer, dado);
                break;
        }
        // Updateamos la UI
        if (FinalBattleManager.instance != null) // Si no es la batalla final funciona la carta
        {
            FinalBattleManager.instance.UIUpdateAfterCardPlayed();
        }
        else
        {
            CombatManager.instance.UIUpdateAfterCardPlayed();
        }
    }
}
