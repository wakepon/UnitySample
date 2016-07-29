using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
	private static T instance;
	public static T Instance {
		get {
            FindInstanceExplicitly();
			return instance;
		}
	}

	public static void FindInstanceExplicitly () {
        if ( instance == null ) {
            instance = ( T )FindObjectOfType( typeof( T ) );
            if ( instance == null ) {
                Debug.LogError ( typeof( T ) + "is nothing" );
            }
        }
	}

	void OnDestroy () {
		instance = null;
	}
}

