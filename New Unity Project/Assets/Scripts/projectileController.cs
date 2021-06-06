using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileController : MonoBehaviour {

    public float rocketSpeed;

    Rigidbody2D myRB;

	// Use this for initialization
	void Awake () {
        myRB = GetComponent<Rigidbody2D>(); // get my rigidbody

        if(transform.localRotation.z > 0) //the z is changing cus of the player sprite between -1 and 1
            myRB.AddForce(new Vector2(-1, 0) * rocketSpeed, ForceMode2D.Impulse); //Force to the left
        
        else
            myRB.AddForce(new Vector2(1, 0) * rocketSpeed, ForceMode2D.Impulse); //Force to the rhite

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void removeForce()
    {
        myRB.velocity = new Vector2(0, 0);
    }
}
