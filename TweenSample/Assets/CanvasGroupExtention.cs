using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Extentions
{
    public class AnimUnit {
        public CanvasGroup CanvasGroup{ get; set; }

        public delegate void CompleteCallback();
        public CompleteCallback OnCompleteCallback{ get; set; }

        public AnimUnit OnComplete(CompleteCallback callback)
        {
            OnCompleteCallback = callback;
            return this;
        }

        public IEnumerator FadeCoroutine(float to, float duration)
        {
            float speed = ( to - CanvasGroup.alpha ) / duration;
            while( ( duration = duration - Time.deltaTime ) > 0 )
            {
                CanvasGroup.alpha += speed * Time.deltaTime;
                yield return null;
            }
            CanvasGroup.alpha = to;
            if(OnCompleteCallback != null)
            {
                OnCompleteCallback();
            }
        }
    }

    public static class CanvasGroupExtention 
    {
        public static AnimUnit AnimFade(this CanvasGroup canvas_group, float to, float duration)
        {
            AnimUnit unit = new AnimUnit();
            unit.CanvasGroup = canvas_group;
            AnimationManager.Instance.StartCoroutine( unit.FadeCoroutine( to, duration ) );
            return unit;
        }
    }   
}
