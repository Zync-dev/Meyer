using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    public class Player
    {
        public float health = 6;
    }

    public Player player1 = new Player();
    public Player player2 = new Player();


    
    public TMP_Text healthAmount;

    public void DamagePlayer(int health, int player)
    {
        if (player == 1)
        {
            player1.health = player1.health -= 1;
            healthAmount.text = player1.health + "/6";
            print("P1 Health: " + player1.health);
        }
        else if (player == 2)
        {
            player2.health = player2.health -= 1;
            healthAmount.text = player2.health + "/6";
            print("P2 Health: " + player2.health);
        }
        else
        {
            print("ERROR");
        }
    }
}
