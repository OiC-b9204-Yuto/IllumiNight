using Cysharp.Threading.Tasks;

namespace IllumiNight.Interface
{
    public interface IAnimation
    {
        UniTask AnimationStart();

        void AnimationSkip();
    }
}