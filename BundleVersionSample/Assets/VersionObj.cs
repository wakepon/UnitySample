using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Text))]
public class VersionObj : MonoBehaviour {
    Text text;
    Color color;

	void Awake () {
        text = GetComponent<Text>();
        text.text = "version " + CurrentBundleVersion.version;
        color = text.color;
        StartCoroutine( fadeOut() );
	}
	
	IEnumerator fadeOut () {
        int waitCounter = 180;
        while( waitCounter > 0 )
        {
            waitCounter--;
            yield return null;
        }

        while( color.a > 0.0f )
        {
            color.a = Mathf.Max( color.a - 0.02f, 0.0f );
            text.color = color;
            yield return null;
        }
        gameObject.SetActive( false );
	}
}
