using UnityEngine;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;

namespace Section9_2
{
    public sealed class MyEveryUpdateByCreate : MonoBehaviour
    {
        private async UniTaskVoid Start()
        {
            await foreach (long i in MyEveryUpdate())
            {
                await UniTask.Delay(1000);
                Debug.Log((i, Time.frameCount));
                if (i >= 5)
                {
                    Debug.Log("Breaking the loop");
                    break;
                }
            }
        }

        private IUniTaskAsyncEnumerable<long> MyEveryUpdate()
            => UniTaskAsyncEnumerable.Create<long>(static async (writer, token) =>
            {
                token.ThrowIfCancellationRequested();

                long count = 0;
                while (!token.IsCancellationRequested)
                {
                    await UniTask.Yield(PlayerLoopTiming.Update, token);

                    Debug.Log("YieldAsync called");
                    await writer.YieldAsync(count++);
                    Debug.Log("YieldAsync completed");
                }
            });
    }
}
