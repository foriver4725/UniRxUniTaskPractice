using UnityEngine;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;

namespace Section9_3
{
    public sealed class UniTaskToAsyncEnumerable : MonoBehaviour
    {
        private async UniTaskVoid Start()
        {
            // await foreach (int i in UniTask.FromResult(1).ToUniTaskAsyncEnumerable())
            // {
            //     Debug.Log(i);
            // }

            await foreach (int i in UniTask.RunOnThreadPool(static () => 1).ToUniTaskAsyncEnumerable())
            {
                Debug.Log(i);
            }
        }
    }
}
