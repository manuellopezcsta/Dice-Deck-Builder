using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;

public class EffectManager : MonoBehaviour
{
    public static EffectManager instance = null;
    [SerializeField] Transform enemyAsTarget; // donde spawnean
    [SerializeField] Transform p1AsTarget;
    [SerializeField] Transform p2AsTarget;
    [SerializeField] List<GameObject> efectsLists; // Lista de efectos
    
    [SerializeField] List<GameObject> queuedEffects = new List<GameObject>();
    public float timeBetweenPopUps;

    void Awake()
    {
        //If this script does not exit already, use this current instance
        if (instance == null)
            instance = this;

        //If this script already exit, DESTROY this current instance
        else if (instance != this)
            Destroy(gameObject);
    }

    void Update()
    {
        if (instantiateObjects == null && queuedEffects.Count != 0)
        {
            instantiateObjects = StartCoroutine(InstantiateObjects());
        }
    }

    void StopPopUps()
    {
        instantiateObjects = null;
        queuedEffects = new List<GameObject>();
    }

    public enum EFFECT_NAME
    {
        ATAQUE,
        MATAQUE,
        ARMADURA,
        MR,
        VENENO,
        ROMPER,
        POTENCIAR,
        CURAR,
        PARRY,
        BLOQUADOR,
        POISON_TICK
    }

    public enum EFFECT_TARGET
    {
        P1,
        P2,
        ENEMY
    }

    public EFFECT_TARGET GetCurrentPlayerTarget(Player player)
    {
        EFFECT_TARGET result = 0;

        if(FinalBattleManager.instance == null) // Caso comun
        {
            if(player == CombatManager.instance.GetPlayerN(1))
            {
                result = EFFECT_TARGET.P1;
            } else {
                result = EFFECT_TARGET.P2;
            }
        } else { // Batalla final
            if(player == FinalBattleManager.instance.GetPlayerN(1))
            {
                result = EFFECT_TARGET.P1;
            } else {
                result = EFFECT_TARGET.P2;
            }
        }
        return result;
    }

    
    public void GenerateEffect(EFFECT_NAME name, EFFECT_TARGET target)
    {
        GameObject popUp = null;
        Transform where = null;
        // switch para ver donde se spawnea.
        switch(target)
        {
            case EFFECT_TARGET.P1:
                where = p1AsTarget;
                break;
            case EFFECT_TARGET.P2:
                where = p2AsTarget;
                break;
            case EFFECT_TARGET.ENEMY:
                where = enemyAsTarget;
                break;
        }

        // Switch de ver cual genero. 
        switch (name)
        {
            case EFFECT_NAME.ATAQUE:
                popUp = Instantiate(efectsLists[0], where);
                break;
            case EFFECT_NAME.MATAQUE:
                popUp = Instantiate(efectsLists[1], where);
                break;
            case EFFECT_NAME.ARMADURA:
                popUp = Instantiate(efectsLists[2], where);
                break;
            case EFFECT_NAME.MR:
                popUp = Instantiate(efectsLists[3], where);
                break;
            case EFFECT_NAME.VENENO:
                popUp = Instantiate(efectsLists[4], where);
                break;
            case EFFECT_NAME.ROMPER:
                popUp = Instantiate(efectsLists[5], where);
                break;
            case EFFECT_NAME.POTENCIAR:
                popUp = Instantiate(efectsLists[6], where);
                break;
            case EFFECT_NAME.CURAR:
                popUp = Instantiate(efectsLists[7], where);
                break;
            case EFFECT_NAME.PARRY:
                popUp = Instantiate(efectsLists[8], where);
                break;
            case EFFECT_NAME.BLOQUADOR:
                popUp = Instantiate(efectsLists[9], where);
                break;
        }
        // Le doy el valor del texto.
        popUp.SetActive(false);
        queuedEffects.Add(popUp);
    }

    Coroutine instantiateObjects = null;
    private IEnumerator InstantiateObjects()
    {
        List<GameObject> clone = queuedEffects.ToList(); ;
        foreach (GameObject obj in clone)
        {
            // Si se termino el combate
            if (!CombatManager.instance.FightingAnEnemy()  && FinalBattleManager.instance == null)
            {
                StopPopUps();
                yield break;
            }

            // Comportamiento comun de la rutina.
            obj.SetActive(true);
            queuedEffects.RemoveAt(0);
            yield return new WaitForSeconds(timeBetweenPopUps);
        }
        instantiateObjects = null;
    }
}