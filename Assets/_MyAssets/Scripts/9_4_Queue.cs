using UnityEngine;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;

namespace Section9_4
{
    public sealed class Queue : MonoBehaviour
    {
        private async UniTaskVoid Start()
        {
            await foreach (var count in
                UniTaskAsyncEnumerable.EveryUpdate()
                    .Select(_ => Time.frameCount)
                    .Take(5)
                    .Queue()
            )
            {
                Debug.Log(count);
                await UniTask.DelayFrame(5);
            }
        }
    }
}
