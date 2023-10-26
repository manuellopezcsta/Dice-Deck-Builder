using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewPlayer", menuName ="PlayerData")]
public class PlayerData : ScriptableObject
{
    public string playerName;
    public int maxHP;
    public int startingArmor;
    public int startingMagicArmor;
    public string statingDeckCode;
    public int[] personalDice;

}
