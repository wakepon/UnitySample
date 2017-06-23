using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace WK { 
namespace Tween { 

public static class TweenExtentions
{
    public static TweenUnit TwWait( this CanvasGroup canvas_group, float duration ) { return waitImpl( duration ); }
    public static TweenUnit TwWait( this Image image, float duration ) { return waitImpl( duration ); }
    public static TweenUnit TwWait( this Text text, float duration ) { return waitImpl( duration ); }
    public static TweenUnit TwWait( this Transform transform, float duration ) { return waitImpl( duration ); }

    //------------------------------------------------------------------------------
    private static TweenUnit waitImpl( float duration )
    {
        TweenUnit unit = new TweenUnit();
        TweenManager.Instance.StartCoroutine( unit.WaitCoroutine( duration ) );
        return unit;
    }

    //------------------------------------------------------------------------------
    public static TweenUnit TwFade( this CanvasGroup canvas_group, float to, float duration )
    {
        TweenUnit unit = new TweenUnit();
        TweenManager.Instance.StartCoroutine( unit.FadeCoroutine( canvas_group, to, duration ) );
        return unit;
    }

    //------------------------------------------------------------------------------
    public static TweenUnit TwFade( this Image image, float to, float duration )
    {
        TweenUnit unit = new TweenUnit();
        TweenManager.Instance.StartCoroutine( unit.FadeCoroutine( image, to, duration ) );
        return unit;
    }

    //------------------------------------------------------------------------------
    public static TweenUnit TwFade( this Text text, float to, float duration )
    {
        TweenUnit unit = new TweenUnit();
        TweenManager.Instance.StartCoroutine( unit.FadeCoroutine( text, to, duration ) );
        return unit;
    }

    //------------------------------------------------------------------------------
    public static TweenUnit TwScale( this Text text, Vector3 to_scale, float duration )
    {
        TweenUnit unit = TwScale( text.transform, to_scale, duration );
        return unit;
    }

    //------------------------------------------------------------------------------
    public static TweenUnit TwScale( this Text text, Vector3 from_scale, Vector3 to_scale, float duration )
    {
        TweenUnit unit = TwScale( text.transform, from_scale, to_scale, duration );
        return unit;
    }

    //------------------------------------------------------------------------------
    public static TweenUnit TwScale( this Transform transform, Vector3 from_scale, Vector3 to, float duration )
    {
        transform.localScale = from_scale;
        return TwScale( transform, to, duration );
    }

    //------------------------------------------------------------------------------
    public static TweenUnit TwScale( this Transform transform, Vector3 to_scale, float duration )
    {
        TweenUnit unit = new TweenUnit();
        TweenManager.Instance.StartCoroutine( unit.ScaleCoroutine( transform, to_scale, duration ) );
        return unit;
    }

    //------------------------------------------------------------------------------
    public static TweenUnit TwRotate( this Transform transform, Vector3 from_angles, Vector3 to_angles, float duration )
    {
        transform.localRotation = Quaternion.Euler( from_angles );
        return TwScale( transform, to_angles, duration );
    }

    //------------------------------------------------------------------------------
    public static TweenUnit TwRotate( this Transform transform, Vector3 to_angles, float duration )
    {
        TweenUnit unit = new TweenUnit();
        TweenManager.Instance.StartCoroutine( unit.RotateCoroutine( transform, to_angles, duration ) );
        return unit;
    }

    //------------------------------------------------------------------------------
    public static TweenUnit TwMove( this Transform transform, Vector3 from_pos, Vector3 to_pos, float duration )
    {
        transform.localPosition = from_pos;
        return TwMove( transform, to_pos, duration );
    }

    //------------------------------------------------------------------------------
    public static TweenUnit TwMove( this Transform transform, Vector3 to_pos, float duration )
    {
        TweenUnit unit = new TweenUnit();
        TweenManager.Instance.StartCoroutine( unit.MoveCoroutine( transform, to_pos, duration ) );
        return unit;
    }
}   

}
}
