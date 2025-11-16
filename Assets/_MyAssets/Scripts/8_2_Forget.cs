using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Section8_2
{
    public sealed class Forget : MonoBehaviour
    {
        private async UniTaskVoid Start()
        {
            DoAsync().Forget();
        }

        private async UniTask DoAsync()
        {
            await UniTask.Delay(1000);
            Debug.Log("Done");
        }
    }
}
