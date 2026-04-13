using DG.Tweening;
using System;

namespace Feature.UI.Shared
{
    public class Animator : IAnimator
    {
        public void Animate(float from, float to, Action<float> onUpdate, float duration = 1f)
        {
            DOTween.To(() => from, x => from = x, to, duration)
                   .OnUpdate(() =>
                   {
                       try
                       {
                           onUpdate(from);
                       }
                       catch (NullReferenceException)
                       {
                           DOTween.Kill(onUpdate);
                       }
                   });
        }
    }
}