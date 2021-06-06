using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootSpore : MonoBehaviour {

    public GameObject theprojectile;
    public GameObject leafProjectile1;
    public GameObject leafProjectile2;
    public GameObject leafProjectile3;
    public GameObject leafProjectile4;
    public float shootTime;
    public int chanceShoot;
    public Transform shootFrom; //the location we want it to shot from
    public Transform shootLeafsFrom;

    float nextShootTime; //when the next time he can shoot
    Animator cannonAnim;

	// Use this for initialization
	void Start () {
        cannonAnim = GetComponentInChildren<Animator>(); // going to the first child he see 
        nextShootTime = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player" && nextShootTime < Time.time)
        {
            nextShootTime = Time.time + shootTime; //give the enemy time for his next shot
            if(Random.Range(0 , 10) >=  chanceShoot) // give a chance to get out the projectile
            {
                Instantiate(theprojectile, shootFrom.position, Quaternion.identity); //shooting the projectile
                Instantiate(leafProjectile1, shootLeafsFrom.position, Quaternion.identity);
                Instantiate(leafProjectile2, shootLeafsFrom.position, Quaternion.identity);
                Instantiate(leafProjectile3, shootLeafsFrom.position, Quaternion.identity);
                Instantiate(leafProjectile4, shootLeafsFrom.position, Quaternion.identity);
                cannonAnim.SetTrigger("cannonShoot");
            }
        }
    }
}
