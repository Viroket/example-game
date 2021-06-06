using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyHealth : MonoBehaviour {

    public float enemyMaxHealth;

    public GameObject enemyDeathFX; //particale system
    public Slider enemySlider;

    //drops from the monster
    public bool drops; //if drops is true the enemy drops health
    public GameObject theDrop;

    public AudioClip deathKnell;

    float currentHealth;

	// Use this for initialization
	void Start () {
        currentHealth = enemyMaxHealth;
        enemySlider.maxValue = enemyMaxHealth;
        enemySlider.value = currentHealth;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void addDamage (float damage)
    {
        enemySlider.gameObject.SetActive(true);
        currentHealth -= damage;
        enemySlider.value = currentHealth;

        if (currentHealth <= 0)
            makeDead();

    }

    void makeDead()
    {
        Destroy(gameObject.transform.parent.gameObject);
        AudioSource.PlayClipAtPoint(deathKnell, transform.position); //create sound when he dies
        Instantiate(enemyDeathFX, transform.position, transform.rotation); //create blood in the enemy location
        if (drops) Instantiate(theDrop, transform.position, transform.rotation); //drop a health to pick up
    }
}
