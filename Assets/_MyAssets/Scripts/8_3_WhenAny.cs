using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Section8_3
{
    public sealed class WhenAny : MonoBehaviour
    {
        private async UniTaskVoid Start()
        {
            int i = await UniTask.WhenAny(
                LogAsync(2),
                LogAsync(1),
                LogAsync(3)
            );
            Debug.Log($"First completed task index: {i}");
        }

        private async UniTask LogAsync(int delaySeconds)
        {
            await UniTask.Delay(delaySeconds * 1000);
            Debug.Log($"Delay {delaySeconds} seconds");
        }
    }
}
