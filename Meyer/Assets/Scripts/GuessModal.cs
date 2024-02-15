using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GuessModal : MonoBehaviour
{

    public TMP_Text PlayerHitTxt;

    Meyer meyer;

    HealthScript hScript;

    int thisplayer = 1;
    int otherplayer = 2;

    private void Start()
    {
        meyer = GameObject.Find("GameManager").GetComponent<Meyer>();
        hScript = GameObject.Find("HealthDisplay").GetComponent<HealthScript>();

        PlayerHitTxt.text = "The other player hit: " + meyer.actualNumber;
    }

    public void BluffingBTN()
    {
        // Find the Player ID (1 or 2)
        thisplayer = meyer.playerTurn;

        switch (meyer.playerTurn)
        {
            case 1: otherplayer = 2; break;
            case 2: otherplayer = 1; break;
            default: print("ERROR"); break;
        }
        print(thisplayer);
        print(otherplayer);

        if (meyer.bluffing == true)
        {
            hScript.DamagePlayer(1, otherplayer);
        } else
        {
            hScript.DamagePlayer(1, thisplayer);

            meyer.actualNumber = 0;
        }
        meyer.PlayerTurn();
        Destroy(this.gameObject);
    }

    public void TellingTruthBTN()
    {
        meyer.PlayerTurn();
        Destroy(this.gameObject);
    }
}
