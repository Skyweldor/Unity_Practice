using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleStateMachine : MonoBehaviour
{
    public List<TurnHandler> performList = new List<TurnHandler>();

    public List<GameObject> playerList = new List<GameObject>();
    public List<GameObject> npcList = new List<GameObject>();

    public List<GameObject> playersToManage = new List<GameObject>();

    public Transform Spacer;
    public GameObject npcButton;
    public GameObject actionPanel;
    public GameObject focusPanel;

    private TurnHandler playerSelectedChoice;

    public enum playerChoiceState
    {
        ACTIVATE,
        WAIT,
        DONE
    }

    public enum BattleHandlerState
	{
        WAIT,
        TAKEACTION,
        PERFORMACTION
	}

    // enums
	public BattleHandlerState currentHandlerState;
    public playerChoiceState currentPlayerState;



    // Initialize the scene
    void Start()
    {
        currentHandlerState = BattleHandlerState.WAIT;
        playerList.AddRange(GameObject.FindGameObjectsWithTag("PlayerCharacter"));
        npcList.AddRange(GameObject.FindGameObjectsWithTag("NPC"));

        npcButtons();

        actionPanel.SetActive(false);
        focusPanel.SetActive(false);
    }

    // Every frame...
    void Update()
    {
        // switch_one: a battle state handler
        switch (currentHandlerState)
        {
            case(BattleHandlerState.WAIT):

                if (performList.Count > 0)
                {
                    currentHandlerState = BattleHandlerState.TAKEACTION;
                }

                break;
            case (BattleHandlerState.TAKEACTION):

                GameObject performer = GameObject.Find(performList[0].activistName);

                if (performList[0].characterType == "NPC")
                {
                    NPCStateMachine npcSM = performer.GetComponent<NPCStateMachine>();
                    npcSM.playerToInteract = performList[0].activistFocus;
                    npcSM.currentState = NPCStateMachine.TurnState.ACTION;
                }

                if (performList[0].characterType == "Player")
                {
                    //Add functionality
                    //PlayerCharacterStateMachine pcSM = performer.GetComponent<PlayerCharacterStateMachine>();
                    //pcSM.npcToInteract = performList[0].activistFocus;
                    //pcSM.currentState = PlayerCharacterStateMachine.TurnState.ACTION;

                    playersToManage.Add(performer);   
                }

                currentHandlerState = BattleHandlerState.PERFORMACTION;

                break;
            case (BattleHandlerState.PERFORMACTION):
                break;
        }

        // switch_two: a player choice handler
        switch (currentPlayerState)
        {
            case (playerChoiceState.ACTIVATE):

                // Debug.Log("For example...");

                if (playersToManage.Count > 0)
                {
                    playersToManage[0].transform.Find("Selector").gameObject.SetActive(true);
                    // playerSelectedChoice = new TurnHandler();
                    actionPanel.SetActive(true);
                    currentPlayerState = playerChoiceState.WAIT;
                }
                break;
            case (playerChoiceState.WAIT):
                
                break;
            case (playerChoiceState.DONE):

                Debug.Log("Taking action!");

                PlayerCharacterStateMachine pcSM = playersToManage[0].GetComponent<PlayerCharacterStateMachine>();
                pcSM.npcToInteract = performList[0].activistFocus;
                pcSM.currentState = PlayerCharacterStateMachine.TurnState.ACTION;

                PlayerInputDone();
                break;
        }
            
    }



    // Called to add to performList
    public void CollectActions(TurnHandler input)
    {
        performList.Add(input);
    }

    // Places NPC Buttons into the game world
    void npcButtons()
    {
        foreach(GameObject npc in npcList)
        {
            GameObject npcNewButton = Instantiate(npcButton) as GameObject;
            NPC_ButtonSelect npcButtonSelect = npcNewButton.GetComponent<NPC_ButtonSelect>();
            npcButtonSelect.NPC_Prefab = npc;
            NPCStateMachine currentNPC = npc.GetComponent<NPCStateMachine>();


            Text buttonText = npcNewButton.transform.GetComponentInChildren<Text>();
            buttonText.text = currentNPC.NPC.name;

            npcNewButton.transform.SetParent(Spacer, false);
        }
    }

    // Function that runs when Action_Button_1 is selected
    public void Action_1()
    {
        Debug.Log(playersToManage[0].name);

        /*
        playerSelectedChoice.activistName = playersToManage[0].name;
        playerSelectedChoice.activistObject = playersToManage[0];
        playerSelectedChoice.characterType = "Player";
        */

        actionPanel.SetActive(false);
        focusPanel.SetActive(true);
    }

    // Function that runs when Action_Button_2 is selected
    public void Action_2(GameObject npcSelected)
    {
        //playerSelectedChoice.activistFocus = npcSelected;

        performList[0].activistFocus = npcSelected;

        currentPlayerState = playerChoiceState.DONE;
    }

    // Function that runs when Action_Button_2 is selected
    public void Action_3()
    {
        performList[0].activistFocus = performList[0].activistObject;
        actionPanel.SetActive(false);
        currentPlayerState = playerChoiceState.DONE;
    }

    // Called after Action_1 and Action_2
    void PlayerInputDone()
    {
        //performList.Add(playerSelectedChoice);

        focusPanel.SetActive(false);
        playersToManage[0].transform.Find("Selector").gameObject.SetActive(false);
        playersToManage.RemoveAt(0);
        currentPlayerState = playerChoiceState.ACTIVATE;
    }
}