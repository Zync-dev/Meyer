using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ModalScript : MonoBehaviour
{

    Meyer meyer;

    private void Awake()
    {
        meyer = GameObject.Find("GameManager").GetComponent<Meyer>();
    }

    public void ConfirmBTN()
    {
        GameObject input1 = GameObject.FindGameObjectWithTag("ModalInput1");
        GameObject input2 = GameObject.FindGameObjectWithTag("ModalInput2");

        if(int.Parse(input1.GetComponent<TMP_InputField>().text) >= 7 || int.Parse(input1.GetComponent<TMP_InputField>().text) < 1)
        {
            print("WRONG NUMBER IN INPUT 1");
        } else if(int.Parse(input2.GetComponent<TMP_InputField>().text) >= 7 || int.Parse(input2.GetComponent<TMP_InputField>().text) < 1)
        {
            print("WRONG NUMBER IN INPUT 2");
        } else
        {
            meyer.BluffConfirmed(int.Parse(input1.GetComponent<TMP_InputField>().text), int.Parse(input2.GetComponent<TMP_InputField>().text));
        }

        Destroy(this.gameObject);
    }

    public void CancelBTN()
    {
        Destroy(this.gameObject);
    }
}
