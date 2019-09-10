using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseNPC
{
    public enum Type
    {
        ANALOG,
        DIGITAL,
        QUANTUM
    }

    public enum Rarity
    {
        COMMON,
        RARE,
        SUPER_RARE
    }

    public Type npcType;
    public Rarity npcRarity;

    public string name;

    public float baseHP;
    public float currentHP;

    public float speed;
    public float evade;

    public float baseStrengthValue;
    public float currentStrengthValue;
    public float baseDefenseValue;
    public float currentDefenseValue;
}
