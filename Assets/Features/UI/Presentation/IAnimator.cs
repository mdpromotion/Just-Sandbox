using DG.Tweening;

namespace Feature.UI.Presentation
{
    public interface IAnimator
    {
        void Animate(float from, float to, System.Action<float> onUpdate, float duration = 1f);
    }
}