using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Section8_2
{
    public sealed class Forget : MonoBehaviour
    {
        private void Start()
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
