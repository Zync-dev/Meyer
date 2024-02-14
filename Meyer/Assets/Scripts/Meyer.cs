using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Meyer : MonoBehaviour
{
    [SerializeField]
    List<GameObject> p1_List;
    [SerializeField]
    List<GameObject> p2_List;
    int playerTurn = 0;

    [SerializeField]
    GameObject backgroundPanel;

    public GameObject BluffModal;

    void Start()
    {
        PlayerTurn(2);
    }

    // Start Player Turn. (Argument: Player ID. 1 or 2)
    public void PlayerTurn(int player)
    {
        // Declare variable
        List<Button> buttons = new List<Button>();

        // Check: Is argument valid?
        if (player == 1)
        {
            // Set playerTurn variable. Used in Roll Dice function.
            playerTurn = 1;

            // Gathers list of all buttons from the p1_List gameobject list.
            foreach (GameObject p in p1_List)
            {
                if(p.tag == "RollDiceBtn")
                {
                    Button button = p.GetComponent<Button>();
                    if (button != null)
                    {
                        buttons.Add(button);
                    }
                }
            }

            // Sets all the found button-elements to be interactable.
            foreach (Button button in buttons)
            {
                button.interactable = true;
            }
        } else if(player == 2)
        {
            // Set playerTurn variable. Used in Roll Dice function.
            playerTurn = 2;

            // Gathers list of all buttons from the p2_List gameobject list.
            foreach (GameObject p in p2_List)
            {
                if (p.tag == "RollDiceBtn")
                {
                    Button button = p.GetComponent<Button>();
                    if (button != null)
                    {
                        buttons.Add(button);
                    }
                }
            }

            // Sets all the found button-elements to be interactable.
            foreach (Button button in buttons)
            {
                button.interactable = true;
            }
        } else
        {
            print("ERROR. PLAYER MUST BE 1 or 2!");
        }
    }

    // Declare DiceRoll() variables
    int previousNum = 0;
    int id;
    int output;

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

        // Declare the wanted numbers
        int[] nums = { 32, 41, 42, 43, 51, 52, 53, 54, 61, 62, 63, 64, 65, 11, 22, 33, 44, 55, 66, 31, 21 };

        // Assign an ID corresponding to the number. This is to figure out, which number is valued the most, and which is valued the least.
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

            foreach (GameObject p in p1_List)
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
            foreach (GameObject p in p2_List)
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

    public void Bluff()
    {
        GameObject modal = Instantiate(BluffModal);

        modal.transform.SetParent(backgroundPanel.transform);

        modal.transform.position = backgroundPanel.transform.position;
    }

    public void TellTruth()
    {

    }
}
