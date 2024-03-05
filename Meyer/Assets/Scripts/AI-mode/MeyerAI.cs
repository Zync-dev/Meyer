using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class MeyerAI : MonoBehaviour
{

    int id;
    int output;
    int actualNumber;
    int inputId;
    int actualNumId;
    bool bluffing = false;
    int[] nums = { 32, 41, 42, 43, 51, 52, 53, 54, 61, 62, 63, 64, 65, 11, 22, 33, 44, 55, 66, 31, 21 };

    [SerializeField]
    List<GameObject> obj_List;
    [SerializeField]
    GameObject backgroundPanel;
    [SerializeField]
    GameObject diceRollObj;
    [SerializeField]
    GameObject bluffModal;
    [SerializeField]
    GameObject aiTurnModal;

    private void Start()
    {
        PlayerTurn();
    }

    public void DiceRoll()
    {
        GameObject DiceRollInstance = Instantiate(diceRollObj, backgroundPanel.transform);
        DiceRollInstance.transform.position = backgroundPanel.transform.position;

        DiceRoll DiceRollScript = DiceRollInstance.GetComponent<DiceRoll>();

        int[] returnValues = DiceRollScript.RollDice();

        id = returnValues[1];
        output = returnValues[0];

        foreach (GameObject p in obj_List)
        {
            if (p.tag == "BluffBtn" || p.tag == "TellTruthBtn")
            {
                p.GetComponent<Button>().interactable = true;
            }
        }
    }

    public void PlayerTurn()
    {
        List<Button> buttons = new List<Button>();

        foreach (GameObject p in obj_List)
        {
            if (p.tag == "RollDiceBtn")
            {
                p.GetComponent<Button>().interactable = true;
            }

            if (p.tag == "BluffBtn" || p.tag == "TellTruthBtn")
            {
                p.GetComponent<Button>().interactable = false;
            }
        }
    }

    GameObject aiTurnInstance;

    public void AITurn()
    {
        aiTurnInstance = Instantiate(aiTurnModal, backgroundPanel.transform);
        aiTurnInstance.transform.position = backgroundPanel.transform.position;


        // Guess if bluffing or telling truth



        // If not lost health, roll own dice.
        // If dice roll under currentNum, then bluff

    }

    public void AITurnAnim()
    {
        aiTurnInstance.GetComponent<Animator>().Play("ModalHideAnim");
    }

    public void TellTruth()
    {
        actualNumber = output;

        AITurn();
    }

    public void Bluff()
    {
        GameObject bluffModalInstance = Instantiate(bluffModal, backgroundPanel.transform);
        bluffModalInstance.transform.position = backgroundPanel.transform.position;
    }

    public void BluffConfirmed(int input)
    {
        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] == input)
            {
                inputId = i;
                print("ID: " + inputId.ToString());
            }
            else if (nums[i] == actualNumber)
            {
                actualNumId = i;
                print("ID: " + actualNumId.ToString());
            }
        }

        print(actualNumId + " - " + inputId);
        bluffing = true;

        AITurn();
    }
}
