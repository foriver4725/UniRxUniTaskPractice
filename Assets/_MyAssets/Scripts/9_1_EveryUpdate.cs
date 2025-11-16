using UnityEngine;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;

namespace Section9_1
{
    public sealed class EveryUpdate : MonoBehaviour
    {
        private async UniTaskVoid Start()
        {
            await foreach (var _ in UniTaskAsyncEnumerable.EveryUpdate())
            {
                await UniTask.Delay(1000);
                Debug.Log(Time.time);
            }
        }
    }
}
