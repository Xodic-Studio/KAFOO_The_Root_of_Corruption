using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/GameSystemData", order = 1)]
public class GameSystemData : ScriptableObject
{
    public int level;
    public int unitAmount;
    public float badPlayerStaminaRegen;
    public int goodPlayerHp;
    public float goodPlayerStaminaRegen;
    public float timeSpan;
    public float goodPlayerFireRate;
}

