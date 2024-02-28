using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceRoll : MonoBehaviour
{

    public Image Dice1;
    public Image Dice2;

    public Sprite[] Sprites;

    bool StopUpdater = false;

    public IEnumerator ImageUpdater()
    {
        while (StopUpdater == false)
        {
            yield return new WaitForSeconds(0.075f);

            Dice1.sprite = Sprites[Random.Range(1, 6)];
            Dice2.sprite = Sprites[Random.Range(1, 6)];
        }

        Dice1.sprite = Sprites[output / 10 - 1];
        Dice2.sprite = Sprites[output % 10 - 1];

        StopCoroutine(ImageUpdater());
    }

    public IEnumerator ImageUpdateStopper()
    {
        yield return new WaitForSeconds(4f);
        StopUpdater = true;

        StopCoroutine(ImageUpdateStopper());
    }

    int id;
    int output;
    int[] nums = { 32, 41, 42, 43, 51, 52, 53, 54, 61, 62, 63, 64, 65, 11, 22, 33, 44, 55, 66, 31, 21 };

    public int[] RollDice()
    {
        StartCoroutine(ImageUpdater());
        StartCoroutine(ImageUpdateStopper());

        // Gets 2 random numbers between 1 and 6.
        int num1 = Random.Range(1, 7);
        int num2 = Random.Range(1, 7);

        // Finds highest of the two numbers, and puts them together to form the highest possible.
        if (num1 >= num2)
        {
            output = int.Parse(num1.ToString() + num2.ToString());

            print("Nummer: " + output.ToString());
        }
        else
        {
            output = int.Parse(num2.ToString() + num1.ToString());

            print("Nummer: " + output.ToString());
        }

        // Assign an ID corresponding to the number.
        // This is to figure out, which number is valued the most, and which is valued the least.
        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] == output)
            {
                id = i;
                print("ID: " + id.ToString());
                break;
            }
        }

        int[] returnValues = { output, id };

        return returnValues;
    }
}
