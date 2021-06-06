using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour {

    public float fullHealth;
    public GameObject deathFX;
    public AudioClip playerHurt;

    public restartGame theGameManager;

    float currentHealth;

    characterController controlMovment;
    
    //player audio source
    AudioSource playerAS;
    public AudioClip playerDeathSound;

    //HUD Variables
    public Slider healthSlider;
    public Text gameOverScreen;
    public Text winGameScreen;

    //flash the damage to the player screen by image
    public Image damageScreen;
    bool damaged = false;
    Color damagedColour = new Color(0f , 0f , 0f , 0.5f);
    float smoothColour = 5f; // fade it slowly and show it slowly

	// Use this for initialization
	void Start () {
        currentHealth = fullHealth;
        controlMovment = GetComponent<characterController>();

        //HUD Initilization
        healthSlider.maxValue = fullHealth;
        healthSlider.value = fullHealth;

        playerAS = GetComponent<AudioSource>(); //reference to ower audio source
	}
	
	// Update is called once per frame
	void Update () {
		if(damaged)
        {
            damageScreen.color = damagedColour;
        }
        else
        {
            damageScreen.color = Color.Lerp(damageScreen.color, Color.clear, smoothColour*Time.deltaTime); // change the color from damage color to normal color clear
        }
        damaged = false;

	}

    public void addDamage(float damage)
    {
        if (damage <= 0) return;
        currentHealth -= damage;

        //play the sound that we want
        playerAS.clip = playerHurt;
        playerAS.Play();
        

        healthSlider.value = currentHealth; //change the slider fill

        damaged = true;
        
        if(currentHealth <= 0)
        {
            makeDead();
        }
    }

    public void addHealth(float healthAmount)
    {
        currentHealth += healthAmount;
        if(currentHealth > fullHealth)
        {
            currentHealth = fullHealth;
        }
        healthSlider.value = currentHealth;
    }

    public void makeDead()
    {
        Instantiate(deathFX, transform.position, transform.rotation);
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(playerDeathSound, transform.position);//play death sound in the player position
        damageScreen.color = damagedColour;

        Animator gameOverAnimator = gameOverScreen.GetComponent<Animator>(); //get accest to game animator
        gameOverAnimator.SetTrigger("gameOver");
        theGameManager.restartTheGame();
    }

    public void winGame()
    {
        Destroy(gameObject);
        theGameManager.restartTheGame(); // change it to another level
        Animator winGameAnimator = winGameScreen.GetComponent<Animator>(); //get the animator
        winGameAnimator.SetTrigger("gameOver");
    }
}
