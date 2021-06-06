using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovmentController : MonoBehaviour {

    public float enemySpeed;

    Animator enemyAnimator;

    //facing
    public GameObject enemyGraphic;
    bool canFlip = true; //if he is charging he cant flip
    bool facingRight = false; //his looking left at the start
    float flipTime = 5f; //evey 5 seconds he can flip
    float nextFlipChance = 0f; // he can flip rhite away for the next flip

    //attacking
    public float chargeTime; //give the carecture a time to preper for his charge
    float startChargeTime; //what time ites going to charge exsectly
    bool charging; //his charging
    Rigidbody2D enemyRB; 

	// Use this for initialization
	void Start () {
        enemyAnimator = GetComponentInChildren<Animator>(); // the animator is in the chaild of this object so we will look for childreds
        enemyRB = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time > nextFlipChance) //the time he can flip 
        {
            if (Random.Range(0, 10) >= 5) // every 5 seconds there is a posibility that we are going to flip
                flipFacing();
            nextFlipChance = Time.time + flipTime;
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if (facingRight && other.transform.position.x < transform.position.x)
            {
                flipFacing();
            }
            else if(!facingRight && other.transform.position.x > transform.position.x)
            {
                flipFacing();
            }
            canFlip = false;
            charging = true;
            startChargeTime = Time.time + chargeTime;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if(startChargeTime < Time.time)
            {
                if (!facingRight) enemyRB.AddForce(new Vector2(-1, 0) * enemySpeed);
                else enemyRB.AddForce(new Vector2(1, 0) * enemySpeed);

                enemyAnimator.SetBool("isCharging", charging);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canFlip = true;
            charging = false;
            enemyRB.velocity = new Vector2(0f, 0f);
            enemyAnimator.SetBool("isCharging", charging);
        }
    }

    void flipFacing()
    {
        if (!canFlip) return; // take a look if he is not charging
        float facingX = enemyGraphic.transform.localScale.x; //which diraction ites facing
        facingX *= -1f; // we want to change the posiotion here 
        enemyGraphic.transform.localScale = new Vector3(facingX, enemyGraphic.transform.localScale.y, enemyGraphic.transform.localScale.z);
        facingRight = !facingRight;
        
    }
}
