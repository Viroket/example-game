using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class characterController : MonoBehaviour {

    public float maxSpeed;
    Rigidbody2D myRB;
    Animator myAnim;
    bool facingRight;

    //Jumping variables
    bool grounded = false;
    int secondJump = 0;
    float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;  //what is ower ground layer .... we made it ground
    public Transform groundCheck;
    public float jumpForce = 400;

    //for shoting
    public Transform gunTip;
    public GameObject bullet;
    float fireRate = 0.5f;
    float nextFire = 0f;

    // Use this for initialization
    void Start () {
        myRB = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();

        facingRight = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        //check if we are grounded - if no then we are falling
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius , groundLayer);
        myAnim.SetBool("isGrounded", grounded);

        myAnim.SetFloat("verticalSpeed" , myRB.velocity.y);


        float move = CrossPlatformInputManager.GetAxis("Horizontal");

        myAnim.SetFloat("speed" , Mathf.Abs(move)); //going to give us a value beetwin 0 and 1 regardles on what key we are pressing

        myRB.velocity = new Vector2(move * maxSpeed, myRB.velocity.y);

        if(move > 0 && !facingRight)
        {
            flip();

        }
        else if(move < 0 && facingRight)
        {
            flip();
        }

    }

    void Update()
    {
      
        if ((grounded && CrossPlatformInputManager.GetButtonDown("Jump")) || (secondJump < 1 && CrossPlatformInputManager.GetButtonDown("Jump")))
        {
            //jump method which is invoked when jump UI button is pressed
            secondJump += 1;
            grounded = false;
            myAnim.SetBool("isGrounded", grounded);
            myRB.AddForce(new Vector2(0, jumpForce), ForceMode2D.Force);
        }
        if(grounded)
        {
            secondJump = 0;
        }
        myAnim.SetTrigger("isIdle");
        //player shooting
        if (CrossPlatformInputManager.GetButtonDown("Fire"))
        {
            
            fireRocket();
        }
    }

    public bool getGrounded()
    {
        //print(grounded);
        return grounded;
    }

    // change the player picture positsion left or right on the map
    void flip()
    {
        facingRight = !facingRight;                 // took my facing and reverce it
        Vector3 theScale = transform.localScale;    // took the scale from my transform and placed it on another scale
        theScale.x *= -1;                           // multiplay it by negative one (but we got a save of this)
        transform.localScale = theScale;            // then i placed it back on the game
    }

    void fireRocket()
    {
        if(Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            if (facingRight)//the facing of the rocked left or rhite
            {
                myAnim.SetTrigger("isShotting");
                Instantiate(bullet, gunTip.position, Quaternion.Euler(new Vector3(0, 0, 0))); //we want to create a bullet and where do we want it and add it a rotation of 0
            }
            else if(!facingRight)
            {
                myAnim.SetTrigger("isShotting");
                Instantiate(bullet, gunTip.position, Quaternion.Euler(new Vector3(0, 0, 180f))); //we want to create a bullet and where do we want it and add it a rotation of 180 (to the left side)
            }
        }
    }
    
}
