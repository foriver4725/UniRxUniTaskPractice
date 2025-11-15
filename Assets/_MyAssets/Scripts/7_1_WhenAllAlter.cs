using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Section7_1
{
    public sealed class WhenAllAlter : MonoBehaviour
    {
        private async UniTaskVoid Start()
        {

            var uniTask1 = UniTask.FromResult(1);
            var uniTask2 = UniTask.FromResult(2);
            var uniTask3 = UniTask.FromResult(3);

            var (r1, r2, r3) = await (uniTask1, uniTask2, uniTask3);
            Debug.Log((r1, r2, r3));

            (r1, r2, r3) = await UniTask.WhenAll(uniTask1, uniTask2, uniTask3);
            Debug.Log((r1, r2, r3));
        }
    }
}
