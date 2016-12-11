using UnityEngine;
using System.Collections;

public class ShitaCallbacks : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D ( Collider2D other ) {
        Debug.Log( "Trigger Enter" );
	}

    void OnCollisionEnter2D ( Collision2D other ) {
        Debug.Log( "Collision Enter" );
    }
}
