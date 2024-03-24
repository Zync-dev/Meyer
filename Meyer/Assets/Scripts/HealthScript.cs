using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    public Player player1 = new Player();
    public Player player2 = new Player();

    public TMP_Text healthAmount;
    public Button rerollBtn;
    public GameObject notifyObj;

    public GameObject backgroundPanel;
    public GameObject modalHolder;
    public GameObject deathScreen;

    Meyer meyer;

    private void Start()
    {
        meyer = GameObject.Find("GameManager").GetComponent<Meyer>();
    }

    public class Player
    {
        public float health = 6;
        public bool hasRerolledHealth = false;
    }

    public void RerollHealth()
    {
        int player = meyer.playerTurn;

        switch (player)
        {
            case 1:
                if (player1.hasRerolledHealth == false)
                {
                    player1.health = Random.Range(1, 7);
                    player1.hasRerolledHealth = true;
                    healthAmount.text = player1.health + "/6";
                } 
                else
                {
                    GameObject notify = Instantiate(notifyObj, backgroundPanel.transform);
                    NotificationScript notification = notify.GetComponent<NotificationScript>();
                    notification.Notify("NOTIFICATION", "You have already used your re-roll.");
                }
                break;
            case 2:
                if (player2.hasRerolledHealth == false)
                {
                    player2.health = Random.Range(1, 7);
                    player2.hasRerolledHealth = true;
                    healthAmount.text = player2.health + "/6";
                }
                else
                {
                    GameObject notify = Instantiate(notifyObj, backgroundPanel.transform);
                    NotificationScript notification = notify.GetComponent<NotificationScript>();
                    notification.Notify("NOTIFICATION", "You have already used your re-roll.");
                }
                break;
        }
    }

    public void DamagePlayer(int amount, int player)
    {
        switch (player)
        {
            case 1:
                player1.health = player1.health -= amount;
                healthAmount.text = player1.health + "/6";
                print("P1 Health: " + player1.health);
                break;
            case 2:
                player2.health = player2.health -= amount;
                healthAmount.text = player2.health + "/6";
                print("P2 Health: " + player2.health);
                break;
        }

        if(player1.health <= 0 || player2.health <= 0)
        {
            GameObject deathScreenInstance = Instantiate(deathScreen, modalHolder.transform);
            TMP_Text winnerTxt = deathScreenInstance.GetComponentInChildren<TMP_Text>();

            if (player1.health <= 0)
            {
                winnerTxt.text = "PLAYER 2 HAS WON!";
            } else if (player2.health <= 0)
            {
                winnerTxt.text = "PLAYER 1 HAS WON!";
            }
        }
    }
}