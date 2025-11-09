using System;
using System.Threading;
using UniRx;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Section1_2
{
    public class DownloadTextureUniTask : MonoBehaviour
    {
        [SerializeField] private RawImage _rawImage;

        private void Start()
        {
            // このGameObjectに紐づいた CancellationToken を取得
            var token = this.GetCancellationTokenOnDestroy();

            // テクスチャのセットアップを実行
            SetUpTextureAsync(token).Forget();
        }

        private async UniTaskVoid SetUpTextureAsync(CancellationToken token)
        {
            try
            {
                var uri = "<表示したい画像へのアドレス>";

                // UniRx の Retry を使いたいので、 UniTask から Observable へ変換する
                var observable = Observable
                                 .Defer(() =>
                                 {
                                     // UniTask -> IObservable
                                     return GetTextureAsync(uri, token)
                                         .ToObservable();
                                 })
                                 .Retry(3);

                // Observable も await で待受が可能
                var texture = await observable;

                _rawImage.texture = texture;
            }
            catch (Exception e) when (!(e is OperationCanceledException))
            {
                Debug.LogError(e);
            }
        }

        // コルーチンの代わりに async/await を利用する
        private async UniTask<Texture> GetTextureAsync(
            string uri,
            CancellationToken token)
        {
            using (var uwr = UnityWebRequestTexture.GetTexture(uri))
            {
                await uwr.SendWebRequest().WithCancellation(token);
                return ((DownloadHandlerTexture)uwr.downloadHandler).texture;
            }
        }
    }
}
