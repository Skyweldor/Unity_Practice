using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacterStateMachine : MonoBehaviour
{
    public BasePlayerCharacter PlayerCharacter;
    private Vector3 startPosition;
    private float currentCooldown;
    private float maxCooldown = 5.0f;
    private float calculateCooldown;
    public Image ProgressBar;

    private BattleStateMachine battleMachine;

    //Action Variables
    private bool actionStarted = false;
    public GameObject npcToInteract;
    private float animSpeed = 10f;

    public enum TurnState
    {
        CALCULATINGTURN,
        ADDTOLIST,
        WAIT,
        ACTION,
        DEFEAT
    }

    public TurnState currentState;

    void Start()
    {
        currentState = TurnState.CALCULATINGTURN;
        battleMachine = GameObject.Find("BattleManager").GetComponent<BattleStateMachine>();
        startPosition = transform.position;
    }

    void Update()
    {
        switch (currentState)
        {
            case (TurnState.CALCULATINGTURN):
                UpgradeProgessBar();
                break;
            case (TurnState.ADDTOLIST):
                ChooseAction();
                break;
            case (TurnState.WAIT):
                //Idle State
                break;
            case (TurnState.ACTION):
                StartCoroutine(ActionCoroutine());
                break;
            case (TurnState.DEFEAT):
                break;
        }
    }

    // Upgrades progress bar, adds to performList
    void UpgradeProgessBar()
    {
        currentCooldown = currentCooldown + (PlayerCharacter.speed * Time.deltaTime);
        calculateCooldown = currentCooldown / maxCooldown;
        ProgressBar.transform.localScale = new Vector3
            (Mathf.Clamp(calculateCooldown, 0, 1), ProgressBar.transform.localScale.y, ProgressBar.transform.localScale.z);
        if (currentCooldown >= maxCooldown)
        {
            currentState = TurnState.ADDTOLIST;
        }
    }

    void ChooseAction()
    {
        //battleMachine.playersToManage.Add(this.gameObject);

        
        TurnHandler myAction = new TurnHandler();
        myAction.activistName = PlayerCharacter.name;
        myAction.characterType = "Player";
        myAction.activistObject = this.gameObject;
        battleMachine.CollectActions(myAction);
        
        
        currentState = TurnState.WAIT;
    }

    private IEnumerator ActionCoroutine()
    {
        if(actionStarted)
        {
            yield break;
        }

        actionStarted = true;

        //Animate towards focus
        Vector3 focusPosition = new Vector3
            (npcToInteract.transform.position.x + 1.5f, npcToInteract.transform.position.y, npcToInteract.transform.position.z);
        while (MoveTowardsFocus(focusPosition))
        {
            yield return null;
        }

        //Wait
        yield return new WaitForSeconds(0.5f);

        //Calculate Action (TODO)

        //Animate back to start postion
        while (MoveTowardsFocus(startPosition))
        {
            yield return null;
        }

        //Remove from performList
        battleMachine.performList.RemoveAt(0);

        //Reset BattleStateMachine
        battleMachine.currentHandlerState = BattleStateMachine.BattleHandlerState.WAIT;

        //End Coroutine
        actionStarted = false;

        //Reset PlayerCharacterStateMachine
        currentCooldown = 0f;
        currentState = TurnState.CALCULATINGTURN;
    }

    private bool MoveTowardsFocus(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, animSpeed * Time.deltaTime));
    }
}
