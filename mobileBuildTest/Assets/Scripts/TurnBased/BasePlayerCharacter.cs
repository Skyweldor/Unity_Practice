using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BasePlayerCharacter
{
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
