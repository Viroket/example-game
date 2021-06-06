using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow2Dplatformer : MonoBehaviour {

    public Transform target; // what the camera is following
    public float smoothing;  // dampening effect
    float zoomSize = -0.1f; // zoom the camera into the player
    public GameObject characture;
    bool isPlayerGrounded;

    Vector3 offset;

    float lowY; //the lowest point that the camera can go

	// Use this for initialization
	void Start () {

        offset = transform.position - target.position;

        lowY = transform.position.y;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 targetCamPos = target.position + offset ;

        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime); //alow us to move from a place to another place in a smoothing


        characterController theCarecture = characture.gameObject.GetComponent<characterController>();
        isPlayerGrounded = theCarecture.getGrounded();

        if (isPlayerGrounded)
        {
            zoomSize = 0f;
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.2f);
        }
        
        if (transform.position.y < lowY)
        {
            transform.position = new Vector3(transform.position.x, lowY, transform.position.z );
        }
      
    }


  


}
