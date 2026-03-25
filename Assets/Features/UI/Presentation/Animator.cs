using DG.Tweening;
using Feature.MainMenu.Presentation;
using System;

namespace Feature.UI.Presentation
{
    /// <summary>
    /// Provides functionality to animate a numeric value from a starting point to an ending point over a specified
    /// duration.
    /// </summary>
    /// <remarks>The Animator class uses the DOTween library to perform value interpolation and invoke a
    /// callback on each update. This class is intended for animating UI or other float-based properties in a
    /// frame-by-frame manner. If the update callback throws a NullReferenceException, the animation is stopped to
    /// prevent further errors.</remarks>
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