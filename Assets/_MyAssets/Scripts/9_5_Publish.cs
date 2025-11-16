using UnityEngine;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;

namespace Section9_5
{
    public sealed class Publish : MonoBehaviour
    {
        private async UniTaskVoid Start()
        {
            var connectable = UniTaskAsyncEnumerable.Range(0, 10)
                .Publish();

            connectable.ForEachAsync(value =>
            {
                Debug.Log($"Subscriber 1 received: {value}");
            }).Forget();

            connectable.ForEachAsync(value =>
            {
                Debug.Log($"Subscriber 2 received: {value}");
            }).Forget();

            connectable.ForEachAsync(value =>
            {
                Debug.Log($"Subscriber 3 received: {value}");
            }).Forget();

            await UniTask.Delay(2000);

            Debug.Log("Connecting...");
            var disposable = connectable.Connect();

            Debug.Log("Disposing...");
            disposable.Dispose();
        }
    }
}
