using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyDamage : MonoBehaviour {

    public float damage;
    public float damageRate;
    public float pushBackForce;

    float nextDamage;

	// Use this for initialization
	void Start () {
        nextDamage = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player" && nextDamage < Time.time)
        {
            playerHealth thePlayerHealth = other.gameObject.GetComponent<playerHealth>(); // going to give us reference to the player health script
            thePlayerHealth.addDamage(damage);
            nextDamage = Time.time + damageRate; // let a little ofset thet the player will not take tons of damage in the same time

            pushBack(other.transform);
        }
    }

    void pushBack(Transform pushedObject)
    {
        Vector2 pushDirection = new Vector2(0, (pushedObject.position.y - transform.position.y)).normalized; //push the player away abit up in the y posision 
        pushDirection *= pushBackForce; // multyply the vector to give it a force greater then 1
        Rigidbody2D pushRB = pushedObject.gameObject.GetComponent<Rigidbody2D>(); // find the rigied boudy of the pushe object
        pushRB.velocity = Vector2.zero; // set the forces that are made to the charecture are now 0
        pushRB.AddForce(pushDirection, ForceMode2D.Impulse); //add a force to the player now
    }

}
