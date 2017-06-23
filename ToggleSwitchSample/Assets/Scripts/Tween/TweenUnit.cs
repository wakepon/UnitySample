using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

namespace WK {

public class TweenUnit {
    private Transform cachedTransform;
    private Action completeCallback = null;

    //------------------------------------------------------------------------------
    public TweenUnit OnComplete(Action callback)
    {
        completeCallback = callback;
        return this;
    }

    //------------------------------------------------------------------------------
    public IEnumerator WaitCoroutine( float duration )
    {
        yield return new WaitForSeconds( duration );
        if(completeCallback != null)
        {
            completeCallback();
            completeCallback= null;
        }
    }

    //------------------------------------------------------------------------------
    public IEnumerator FadeCoroutine( CanvasGroup canvas_group, float to, float duration)
    {
        /* int times = (int)( duration / Time.deltaTime ); */
        /* int times = duration; */
        /* float speed = ( to - CanvasGroup.alpha ) / times; */
        float speed = ( to - canvas_group.alpha ) / duration;
        while( ( duration = duration - Time.deltaTime ) > 0 )
        {
            canvas_group.alpha += speed * Time.deltaTime;
            yield return null;
        }
        canvas_group.alpha = to;
        if(completeCallback != null)
        {
            completeCallback();
            completeCallback= null;
        }
    }

    //------------------------------------------------------------------------------
    public IEnumerator FadeCoroutine( Image image, float to, float duration)
    {
        float speed = ( to - image.color.a ) / duration;
        Color col = image.color;
        while( ( duration = duration - Time.deltaTime ) > 0 )
        {
            col.a += speed * Time.deltaTime;
            image.color = col;
            yield return null;
        }
        col.a = to;
        image.color = col;
        if(completeCallback != null)
        {
            completeCallback();
            completeCallback= null;
        }
    }

    //------------------------------------------------------------------------------
    public IEnumerator FadeCoroutine( Text text, float to, float duration)
    {
        float speed = ( to - text.color.a ) / duration;
        Color col = text.color;
        while( ( duration = duration - Time.deltaTime ) > 0 )
        {
            col.a += speed * Time.deltaTime;
            text.color = col;
            yield return null;
        }
        col.a = to;
        text.color = col;
        if(completeCallback != null)
        {
            completeCallback();
            completeCallback= null;
        }
    }

    //------------------------------------------------------------------------------
    public IEnumerator MoveCoroutine( Transform transform, Vector3 to_pos, float duration )
    {
        cachedTransform = transform;
        Vector3 speed = ( to_pos - cachedTransform.localPosition ) / duration;
        while( ( duration = duration - Time.deltaTime ) > 0 )
        {
            cachedTransform.localPosition += speed * Time.deltaTime;
            yield return null;
        }
        cachedTransform.localPosition = to_pos;
        if(completeCallback != null)
        {
            completeCallback();
            completeCallback= null;
        }
    }

    //------------------------------------------------------------------------------
    public IEnumerator ScaleCoroutine( Transform transform, Vector3 to, float duration )
    {
        cachedTransform = transform;
        Vector3 speed = ( to - cachedTransform.localScale ) / duration;
        while( ( duration = duration - Time.deltaTime ) > 0 )
        {
            cachedTransform.localScale += speed * Time.deltaTime;
            yield return null;
        }
        cachedTransform.localScale = to;
        if(completeCallback != null)
        {
            completeCallback();
            completeCallback= null;
        }
    }

    //------------------------------------------------------------------------------
    public IEnumerator RotateCoroutine( Transform transform, Vector3 to_angles, float duration )
    {
        cachedTransform = transform;
        Vector3 angles = cachedTransform.localRotation.eulerAngles;
        Vector3 speed = ( to_angles - angles ) / duration;
        while( ( duration = duration - Time.deltaTime ) > 0 )
        {
            angles += speed * Time.deltaTime;
            cachedTransform.localRotation = Quaternion.Euler( angles );
            yield return null;
        }
        cachedTransform.localRotation = Quaternion.Euler( to_angles );
        if(completeCallback != null)
        {
            completeCallback();
            completeCallback= null;
        }
    }

}

}
