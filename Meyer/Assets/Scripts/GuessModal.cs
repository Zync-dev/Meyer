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

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

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

        if (meyer.bluffing == true)
        {
            hScript.DamagePlayer(1, otherplayer);
            meyer.NewGameShort("CORRECT. THE OTHER PLAYER HAS LOST A POINT. THE NUMBER IS NOW 0 AGAIN.");
        } else
        {
            hScript.DamagePlayer(1, thisplayer);
            meyer.NewGameShort("WRONG. YOU HAVE LOST A POINT. THE NUMBER IS NOW 0 AGAIN");
        }
        StartCoroutine(DeleteOBJ());
    }

    public void TellingTruthBTN()
    {
        meyer.PlayerTurn();
        StartCoroutine(DeleteOBJ());
    }

    public IEnumerator DeleteOBJ()
    {
        animator.Play("GuessModalExit");
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }
}
