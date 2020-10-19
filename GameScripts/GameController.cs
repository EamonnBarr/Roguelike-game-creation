using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public static GameController instance;
    //Integers set for use later, these values are our player stats.
    private static float health = 6;
    private static int maxHealth = 6;
    private static float moveSpeed = 5f;
    private static float fireRate = 0.5f;
    private static float bulletSize = 0.5f;

    //Set to false, when they are both collected, it creates a synergy.
    private bool bootCollected = false;
    private bool screwCollected = false;

    public List<string> collectedNames = new List<string>();

    //These allow the stats to be changed via unity editor.
    public static float Health { get => health; set => health = value; }
    public static int MaxHealth { get => maxHealth; set => maxHealth = value; }
    public static float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public static float FireRate { get => fireRate; set => fireRate = value; }
    public static float BulletSize { get => bulletSize; set => bulletSize = value; }
    
    public Text healthText;
    //Showed the Helath text, this was taken out of the UI.

    // Invoked when Monobehaviour is created, checks if this is instantiated, if not then it creates an instance.
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    //Shows Health Text.
    void Update()
    {
        healthText.text = "Health: " + health; 
    }

    //Takes damage variable value from player health.
    //If the health is below 1 then the player is killed
    public static void DamagePlayer(int damage) 
    {
        health -= damage;


        if(Health <= 0) 
        {
            Destroy(GameObject.Find("Player"));
            KillPlayer(); 
        }
    }


    //ITEMS


    //Potion Item uses this, Makes it heal the player.
    public static void HealPlayer(float healAmount)
    {  
        health = Mathf.Min(maxHealth, health + healAmount);
    }

    //Boot Item uses this, Makes playerspeed increase.
    public static void MoveSpeedChange(float speed)
    {  
        moveSpeed += speed;
    }

    //Screw Uses this, Makes Fire Rate increase. 
    public static void FireRateChange(float rate) 
    {
        fireRate -= rate;
    }

    //Screw Uses this, Makes Bullet size increase. 
    public static void BulletSizeChange(float size) 
    {
        bulletSize += size;
    }
    //Check for what Item has been collected. 
    //Checks if both the Boot and the Screw have been Collected, Sets them to true if so.
    public void UpdateCollectedItems(CollectionController item) 
    {
        collectedNames.Add(item.item.name);

        foreach(string i in collectedNames) 
        {
            switch(i)
            {
                case "Boot":
                    bootCollected = true;
                break;
                case "Screw":
                    screwCollected = true;
                break;
            }
        }
        //Adds a synergy of 0.25f fire rate speed when both boot and screw are collected.
        if (bootCollected && screwCollected) 
        {
            FireRateChange(0.25f);
        }
    }

    //This Loads the Menu Scene and Makes the Player Health value revert back to Max.
    private static void KillPlayer() 
    {
        SceneManager.LoadScene("Menu");
        Health = maxHealth;
    }
}
