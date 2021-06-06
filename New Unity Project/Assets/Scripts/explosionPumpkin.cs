using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosionPumpkin : MonoBehaviour {
    public float damage;
    public float damageRate;
    public AudioClip explosionSound;

    public GameObject enemyExlode; //particale system
    float nextDamage;

    // Use this for initialization
    void Start()
    {
        nextDamage = 0f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" && nextDamage < Time.time)
        {
            playerHealth thePlayerHealth = other.gameObject.GetComponent<playerHealth>(); // going to give us reference to the player health script
            thePlayerHealth.addDamage(damage);
            nextDamage = Time.time + damageRate; // let a little ofset thet the player will not take tons of damage in the same time
            makeDead();
        }
    }

    void makeDead()
    {
        Destroy(gameObject.transform.parent.gameObject);
        AudioSource.PlayClipAtPoint(explosionSound, transform.position); //create sound when he dies
        Instantiate(enemyExlode, transform.position, transform.rotation); //create blood in the enemy location
    }


}
