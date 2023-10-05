using System;
using System.Collections;
using System.Collections.Generic;
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
        if (!CombatManager.instance.finalBattle) // Si no es la batalla final funciona la carta
        {
            if (target == null)
            {
                // LO QUE PASA SI NO EXISTE TARGET , DUELO FINAL
                return;
            }
            target.TomarDaño(dado.currentValue);
        }

    }
    public void UsarCartaAmagico(Enemy target, Dice dado)
    {
        if (!CombatManager.instance.finalBattle)
        {
            target.TomarDañoMagico(dado.currentValue);
        }
    }
    public void UsarCartaCurar(Player player, Dice dado)
    {
        player.Cura(dado.currentValue);
    }
    public void UsarCartaArmadura(Player player, Dice dado)
    {
        player.armour += dado.currentValue;
        player.armour = Mathf.Clamp(player.armour, 0, player.maxArmour);
    }
    public void UsarCartaMr(Player player, Dice dado)
    {
        player.mArmour += dado.currentValue;
        player.mArmour = Mathf.Clamp(player.mArmour, 0, player.maxMArmour);
    }
    public void UsarCartaBreak(Enemy target, Dice dado)
    {
        if (!CombatManager.instance.finalBattle)
        {
            target.armour -= dado.currentValue;
            target.mArmour -= dado.currentValue;
        }
    }
    public void UsarCartaParryAtaque(Player player)
    {
        player.parryFisico = true;
    }
    public void UsarCartaParryAMagico(Player player)
    {
        player.parryMagico = true;
    }
    public void UsarCartaVeneno(Enemy target, Dice dado)
    {
        if (!CombatManager.instance.finalBattle)
        {
            target.ChanceEnvenenado(dado.currentValue);
        }
    }
    public void UsarCartaCurax2(Player player1, Player player2, Dice dado)
    {
        UsarCartaCurar(player1, dado);
        UsarCartaCurar(player2, dado);
    }
    public void UsarCartaPotenciador()
    {

    }
    public void UsarCartaArmadurax2(Player player1, Player player2, Dice dado)
    {
        UsarCartaArmadura(player1, dado);
        UsarCartaArmadura(player2, dado);
    }
    public void UsarCartaBloqueador()
    {

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
    }
    public void UsarCartaVenenoRomp(Player player, Dice dado)
    {
        player.ChanceEnvenenado(dado.currentValue);
    }

    public void RunLogic(Card card, Dice dado)
    {
        Enemy enemigo = CombatManager.instance.GetEnemy();
        Player actualPlayer = CombatManager.instance.GetPlayerN(CombatManager.instance.currentTurn);
        Player allyPlayer = CombatManager.instance.GetAlly(CombatManager.instance.currentTurn);

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
                UsarCartaPotenciador();
                break;
            case CardEffects.ARMADURAX2:
                UsarCartaArmadurax2(actualPlayer, allyPlayer, dado);
                break;
            case CardEffects.BLOQUEADOR:
                UsarCartaBloqueador();
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
    }
}
