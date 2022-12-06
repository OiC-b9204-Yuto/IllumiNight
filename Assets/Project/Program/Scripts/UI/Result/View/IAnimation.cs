using Cysharp.Threading.Tasks;
using System.Threading;

namespace IllumiNight.Interface
{
    public interface IAnimation
    {
        UniTask AnimationStart(CancellationToken token = default(CancellationToken));

        void AnimationSkip();
    }
}