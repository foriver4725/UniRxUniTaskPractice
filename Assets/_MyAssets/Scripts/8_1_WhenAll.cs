using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Section8_1
{
    public sealed class WhenAll : MonoBehaviour
    {
        private async UniTaskVoid Start()
        {
            var (a, b, c) = await UniTask.WhenAll(
                UniTask.FromResult("a"),
                UniTask.FromResult("b"),
                UniTask.FromResult("c")
            );

            var array = await UniTask.WhenAll(new[]
            {
                UniTask.FromResult("a"),
                UniTask.FromResult("b"),
                UniTask.FromResult("c"),
            });

            var array2 = await UniTask.WhenAll(
                UniTask.FromResult("a"),
                UniTask.FromResult("b"),
                UniTask.FromResult("c"),
                UniTask.FromResult("d"),
                UniTask.FromResult("e"),
                UniTask.FromResult("f"),
                UniTask.FromResult("g"),
                UniTask.FromResult("h"),
                UniTask.FromResult("i"),
                UniTask.FromResult("j"),
                UniTask.FromResult("k"),
                UniTask.FromResult("l"),
                UniTask.FromResult("m"),
                UniTask.FromResult("n"),
                UniTask.FromResult("o"),
                UniTask.FromResult("p")
            );
        }
    }
}
