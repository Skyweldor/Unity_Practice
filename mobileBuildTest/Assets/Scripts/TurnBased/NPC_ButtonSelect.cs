using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_ButtonSelect : MonoBehaviour
{
    public GameObject NPC_Prefab;
    public void SelectNPC()
    {
        GameObject.Find("BattleManager").GetComponent<BattleStateMachine>().Action_2(NPC_Prefab);
    }
}
