using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
using Unity.VisualScripting;
using UnityEngine.Windows;
using JetBrains.Annotations;

public class Meyer : MonoBehaviour
{
    [SerializeField]
    List<GameObject> obj_List;

    [SerializeField]
    GameObject backgroundPanel;

    [SerializeField]
    TMP_Text currentNumberTxt;

    public GameObject BluffModal;
    public int playerTurn;
    public int actualNumber;

    HealthScript hScript;

    [SerializeField]
    GameObject FadeInScreen;
    [SerializeField]
    GameObject NewGameModal;
    [SerializeField]
    GameObject NewGameModal2;

    public bool bluffing = false;

    private void Awake()
    {
        hScript = GameObject.Find("HealthDisplay").GetComponent<HealthScript>();
    }

    void Start()
    {
        GuessModal();
    }

    GameObject fadein;
    public void GuessModal()
    {
        switch (playerTurn)
        {
            case 1:
                playerTurn = 2;
                fadein = Instantiate(FadeInScreen, backgroundPanel.transform);
                fadein.transform.position = backgroundPanel.transform.position;
                break;
            case 2:
                playerTurn = 1;
                fadein = Instantiate(FadeInScreen, backgroundPanel.transform);
                fadein.transform.position = backgroundPanel.transform.position;
                break;
            default:
                playerTurn = 1;
                PlayerTurn();
                break;
        }
    }

    GameObject gamemodal;
    public void NewGame()
    {
        actualNumber = 0;

        gamemodal = Instantiate(NewGameModal, backgroundPanel.transform);
        gamemodal.transform.position = backgroundPanel.transform.position;

        PlayerTurn();
    }

    public void NewGameShort(string text)
    {
        actualNumber = 0;

        StartCoroutine(NewGameShortDelay(text));

        PlayerTurn();
    }

    public IEnumerator NewGameShortDelay(string text)
    {
        yield return new WaitForSeconds(1.1f);

        gamemodal = Instantiate(NewGameModal2, backgroundPanel.transform);
        gamemodal.transform.position = backgroundPanel.transform.position;
        gamemodal.GetComponentInChildren<TMP_Text>().text = text;
    }

    // Start Player Turn. (Argument: Player ID. 1 or 2)
    public void PlayerTurn()
    {
        // Declare variable
        List<Button> buttons = new List<Button>();

        // Check: Is function argument valid?
        if (playerTurn == 1)
        {
            hScript.healthAmount.text = hScript.player1.health.ToString() + "/6";

            // Gathers list of all buttons from the obj_List gameobject list.
            foreach (GameObject p in obj_List)
            {
                if (p.tag == "RollDiceBtn")
                {
                    Button button = p.GetComponent<Button>();
                    if (button != null)
                    {
                        buttons.Add(button);
                    }
                }
                else if (p.tag == "PlayerHeaderBG")
                {
                    p.gameObject.GetComponent<Image>().color = Color.blue;
                    p.gameObject.GetComponentInChildren<TMP_Text>().text = "PLAYER 1";
                }

                if (p.tag == "BluffBtn" || p.tag == "TellTruthBtn")
                {
                    p.GetComponent<Button>().interactable = false;
                }
                else if (p.tag == "BluffBtn" || p.tag == "TellTruthBtn")
                {
                    p.GetComponent<Button>().interactable = false;
                }
                else if (p.tag == "RolledText")
                {
                    TMP_Text TMTxt = p.GetComponent<TMP_Text>();
                    if (TMTxt != null)
                    {
                        TMTxt.text = "";
                    }
                }
            }

            // Sets all the found button-elements to be interactable.
            foreach (Button button in buttons)
            {
                button.interactable = true;
            }

        }
        else if (playerTurn == 2)
        {
            hScript.healthAmount.text = hScript.player2.health.ToString() + "/6";

            // Gathers list of all buttons from the obj_List gameobject list.
            foreach (GameObject p in obj_List)
            {
                if (p.tag == "RollDiceBtn")
                {
                    Button button = p.GetComponent<Button>();
                    if (button != null)
                    {
                        buttons.Add(button);
                    }
                }
                else if (p.tag == "PlayerHeaderBG")
                {
                    p.gameObject.GetComponent<Image>().color = Color.red;
                    p.gameObject.GetComponentInChildren<TMP_Text>().text = "PLAYER 2";
                }
                else if (p.tag == "BluffBtn" || p.tag == "TellTruthBtn")
                {
                    p.GetComponent<Button>().interactable = false;
                }
                else if (p.tag == "RolledText")
                {
                    TMP_Text TMTxt = p.GetComponent<TMP_Text>();
                    if (TMTxt != null)
                    {
                        TMTxt.text = "";
                    }
                }
            }

            // Sets all the found button-elements to be interactable.
            foreach (Button button in buttons)
            {
                button.interactable = true;
            }
        }
        else
        {
            print("ERROR. PLAYER MUST BE 1 or 2!");
        }
    }

    // Declare DiceRoll() variables
    int id;
    int output;
    int[] nums = { 32, 41, 42, 43, 51, 52, 53, 54, 61, 62, 63, 64, 65, 11, 22, 33, 44, 55, 66, 31, 21 };

    // Function to roll dice.
    public void DiceRoll()
    {
        // Gets 2 random numbers between 1 and 6.
        int num1 = Random.Range(1, 7);
        int num2 = Random.Range(1, 7);

        // Finds highest of the two numbers, and puts them together to form the highest possible.
        if (num1 >= num2)
        {
            output = int.Parse(num1.ToString() + num2.ToString());

            print("Nummer: " + output.ToString());
        } else
        {
            output = int.Parse(num2.ToString() + num1.ToString());

            print("Nummer: " + output.ToString());
        }

        // Assign an ID corresponding to the number.
        // This is to figure out, which number is valued the most, and which is valued the least.
        for(int i = 0;  i < nums.Length; i++)
        {
            if (nums[i] == output)
            {
                id = i;
                print("ID: " + id.ToString());
                break;
            }
        }

        if (playerTurn == 1)
        {
            // CHANGE TEXT OBJ AND DISABLE/ENABLE BUTTONS FOR PLAYER1

            foreach (GameObject p in obj_List)
            {
                if (p.tag == "RolledText")
                {
                    TMP_Text TMTxt = p.GetComponent<TMP_Text>();
                    if (TMTxt != null)
                    {
                        TMTxt.text = "ROLLED: " + output.ToString();
                    }
                }

                if (p.tag == "BluffBtn" || p.tag == "TellTruthBtn")
                {
                    Button button = p.GetComponent<Button>();
                    if (button != null)
                    {
                        button.interactable = true;
                    }
                }

                if (p.tag == "RollDiceBtn")
                {
                    Button button = p.GetComponent<Button>();
                    if (button != null)
                    {
                        button.interactable = false;
                    }
                }
            }
        } else if(playerTurn == 2)
        {
            // CHANGE TEXT OBJ FOR PLAYER2
            foreach (GameObject p in obj_List)
            {
                if (p.tag == "RolledText")
                {
                    TMP_Text TMTxt = p.GetComponent<TMP_Text>();
                    if (TMTxt != null)
                    {
                        TMTxt.text = "ROLLED: " + output.ToString();
                    }
                }

                if (p.tag == "BluffBtn" || p.tag == "TellTruthBtn")
                {
                    Button button = p.GetComponent<Button>();
                    if (button != null)
                    {
                        button.interactable = true;
                    }
                }

                if (p.tag == "RollDiceBtn")
                {
                    Button button = p.GetComponent<Button>();
                    if (button != null)
                    {
                        button.interactable = false;
                    }
                }
            }
        } else
        {
            print("ERROR. INVALID PLAYER TURN. MUST BE 1 OR 2.");
        }
    }

    // BluffBTN click event
    public void Bluff()
    {
        gamemodal = Instantiate(BluffModal, backgroundPanel.transform);
        gamemodal.transform.position = backgroundPanel.transform.position;
    }

    int inputOrdered;
    int inputId;
    int actualNumId;
    public void BluffConfirmed(int input)
    {
        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] == input)
            {
                inputId = i;
                print("ID: " + inputId.ToString());
            } else if (nums[i] == actualNumber)
            {
                actualNumId = i;
                print("ID: " + actualNumId.ToString());
            }
        }

        print(actualNumId + " - " + inputId);

        bluffing = true;

        if (inputId >= actualNumId)
        {
            actualNumber = input;
            currentNumberTxt.text = actualNumber.ToString();
            GuessModal();
        } else
        {
            hScript.DamagePlayer(1, playerTurn);
            actualNumber = 0;
            currentNumberTxt.text = actualNumber.ToString();
            NewGameShort("THE OTHER PLAYER HAS LOST A POINT. THE NUMBER IS NOW 0 AGAIN. #1");
        }
    }

    // TellTruthBTN click event
    int outputId;
    public void TellTruth()
    {
        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] == output)
            {
                outputId = i;
                print("ID1: " + outputId.ToString());
            }
            else if (nums[i] == actualNumber)
            {
                actualNumId = i;
                print("ID2: " + actualNumId.ToString());
            }
        }

        if (outputId >= actualNumId)
        {
            actualNumber = output;
            currentNumberTxt.text = actualNumber.ToString();

            GuessModal();
        } else
        {
            hScript.DamagePlayer(1, playerTurn);
            actualNumber = 0;
            currentNumberTxt.text = actualNumber.ToString();

            NewGame();
        }
    }
}
