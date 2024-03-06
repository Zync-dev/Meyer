using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class MeyerAI : MonoBehaviour
{
    int aiType = 1;
    int id;
    int output;
    int currentNumber;
    int inputId;
    int actualNumId;
    bool bluffing = false;
    int[] nums = { 32, 41, 42, 43, 51, 52, 53, 54, 61, 62, 63, 64, 65, 11, 22, 33, 44, 55, 66, 31, 21 };

    [SerializeField]
    List<GameObject> obj_List;

    public GameObject backgroundPanel;
    [SerializeField]
    GameObject diceRollObj;
    [SerializeField]
    GameObject bluffModal;

    public GameObject aiTurnModal;
    [SerializeField]
    TMP_Text currentNumText;

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
    GameObject aiTurnInstance2;

    public void AITurn()
    {
        aiTurnInstance = Instantiate(aiTurnModal, backgroundPanel.transform);
        aiTurnInstance.transform.position = backgroundPanel.transform.position;

        int randomNum = Random.Range(0, 99);

        if(aiType == 1)
        {
            if (randomNum > 50)
            {
                AIThinkBluffing();
            }
            else if (randomNum <= 50)
            {
                AIThinkTellingTruth();
            }
        }


        // Guess if bluffing or telling truth



        // If not lost health, roll own dice.
        // If dice roll under currentNum, then bluff

    }

    public void AIThinkTellingTruth()
    {
        GameObject Header = GameObject.FindGameObjectWithTag("Header");
        Header.GetComponent<TMP_Text>().text = "THE AI BELIEVES THAT YOU'RE TELLING THE TRUTH.";
        PlayerTurn();
    }

    public void AIThinkBluffing()
    {
        GameObject Header = GameObject.FindGameObjectWithTag("Header");
        if (bluffing == true)
        {
            Header.GetComponent<TMP_Text>().text = "THE AI HAS GUESSED THAT YOU WERE BLUFFING. YOU LOST 1 HEALTH POINT.";
            currentNumber = 0;
            PlayerTurn();
        } else if(bluffing == false)
        {
            Header.GetComponent<TMP_Text>().text = "THE AI HAS GUESSED THAT YOU WERE BLUFFING. THE AI LOST 1 HEALTH POINT.";
            currentNumber = 0;
            PlayerTurn();
        }
        // If bluffing
        // Player lost health
        // Reset currentNum

        // If not bluffing
        // AI lost health
        // Reset currentNum
    }

    public void AITurnAnim()
    {
        aiTurnInstance.GetComponent<Animator>().Play("ModalHideAnim");
    }

    public void TellTruth()
    {
        currentNumber = output;
        currentNumText.text = currentNumber.ToString(); 

        AITurn();
    }

    public void NewAIModalInstance()
    {
        aiTurnInstance2 = Instantiate(aiTurnModal, backgroundPanel.transform);
        aiTurnInstance2.transform.position = backgroundPanel.transform.position;

        AIModalScript aiModalScript = aiTurnInstance2.GetComponent<AIModalScript>();

        aiModalScript.id = 1;
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
            else if (nums[i] == currentNumber)
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
