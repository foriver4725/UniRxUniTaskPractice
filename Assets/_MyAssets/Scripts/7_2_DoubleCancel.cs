using System.Threading;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Section7_2
{
    public sealed class DoubleCancel : MonoBehaviour
    {
        private async UniTaskVoid Start()
        {
            CancellationTokenSource cts = new();
            cts.RegisterRaiseCancelOnDestroy(this);

            LogForeverAsync(cts.Token).Forget();
            await UniTask.Delay(2000);
            cts.Cancel();
            cts.Dispose();
        }

        private async UniTaskVoid LogForeverAsync(CancellationToken token)
        {
            while (true)
            {
                Debug.Log("!");
                await UniTask.NextFrame(token);
            }
        }
    }
}
