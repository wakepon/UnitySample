using UnityEngine;
using System.Collections;
using Extentions;

public class Main : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject canvas = GameObject.Find( "Canvas" );
        canvas.GetComponent<CanvasGroup>().AnimFade( 0.0f, 1.0f ).OnComplete( () => canvas.gameObject.SetActive(false) );
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
