using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallTrough : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Physics2D.GetIgnoreLayerCollision(LayerMask.NameToLayer("Shootable"), LayerMask.NameToLayer("Shootable"));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
