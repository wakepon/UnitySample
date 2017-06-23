using UnityEngine;

namespace WK {

public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : SingletonMonoBehaviour<T>
{
	private static T instance;
	public static T Instance {
		get {
            if ( instance == null ) {
                instance = ( T )FindObjectOfType( typeof( T ) );
                if ( instance == null ) {
                    Debug.LogError ( typeof( T ) + "is nothing" );
                }
            }
			return instance;
		}
	}

    //@memo なるべく確実にAwakeでCheckInstanceを呼ぶように
    //呼ばないとバグの元になるし、
    //(SetActive false なオブジェクトにInstanceでアクセスするとFindObjectOfTypeが失敗する)
    //FindObjectOfTypeを呼ぶぶん速度的デメリットも有る
	protected virtual void Awake()
	{
		CheckInstance();
	}
	
	protected bool CheckInstance()
	{
		if( instance == null)
		{
			instance = (T)this;
			return true;
		}else if( Instance == this )
		{
			return true;
		}
		
		Destroy(this);
		return false;
	}

	void OnDestroy () {
		instance = null;
	}
}

}
