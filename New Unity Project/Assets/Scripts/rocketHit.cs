using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocketHit : MonoBehaviour {

    public float weaponDamage;

    projectileController myPC;

    public GameObject exlosionEffect;

	// Use this for initialization
	void Awake () {
        myPC = GetComponentInParent<projectileController>(); //find my projectile this parent
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Shootable")) //if the object is on the same shootable layer we want somthing to happen
        {
            myPC.removeForce(); // go to projectileController script and stop the rocket movment
            Instantiate(exlosionEffect, transform.position , transform.rotation); //start the explosion effect
            Destroy(gameObject); //destroying the rocket but leave the projectile to play

            //looking for an enemy to do damage to him
            if(other.tag == "Enemy")
            {
                enemyHealth hurtEnemy = other.gameObject.GetComponent<enemyHealth>(); //looking for the enemy health script of the specific enemy that we are colided white
                hurtEnemy.addDamage(weaponDamage); //do damage to enemy
            }
        }
    }

    void OnTriggerStay2D(Collider2D other) //making sure if ites going to fast we will steel get it
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Shootable")) //if the object is on the same shootable layer we want somthing to happen
        {
            myPC.removeForce(); // go to projectileController script and stop the rocket movment
            Instantiate(exlosionEffect, transform.position, transform.rotation); //start the explosion effect
            Destroy(gameObject); //destroying the rocket but leave the projectile to play

            //looking for an enemy to do damage to him
            if (other.tag == "Enemy")
            {
                enemyHealth hurtEnemy = other.gameObject.GetComponent<enemyHealth>(); //looking for the enemy health script of the specific enemy that we are colided white
                hurtEnemy.addDamage(weaponDamage); //do damage to enemy
            }
        }

    }
}
