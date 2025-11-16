using System;
using System.Collections;
using UniRx;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Section1_1
{
    // 指定のURIの画像をダウンロードする。
    // ダウンロードに成功した場合は画像を UnityEngine.UI.RawImage で表示する。
    // ダウンロードに失敗した場合は計3回まで試行する。
    // 3回失敗した場合はエラーログを Debug.LogError に出力する。
    // 実行中の GameObject が破棄されたら上記の処理をキャンセルする。
    public class DownloadTexture : MonoBehaviour
    {
        // uGUI の RawImage
        [SerializeField] private RawImage _rawImage;

        private void Start()
        {
            var uri = "address/to/your/image.png";

            // テクスチャを取得する
            // ただし例外発生時は計3回まで試行する
            GetTextureAsync(uri)
                .OnErrorRetry(
                    onError: static (Exception _) => { },
                    retryCount: 3
                )
                .Subscribe(
                    result => { _rawImage.texture = result; },
                    static error => { Debug.LogError(error); }
                )
                .AddTo(this);
        }

        // コルーチンを起動して、その結果を Observable で返す
        private IObservable<Texture> GetTextureAsync(string uri)
            => Observable
                .FromCoroutine<Texture>(observer => GetTextureCoroutine(observer, uri));

        // コルーチンでテクスチャをダウンロードする
        private IEnumerator GetTextureCoroutine(IObserver<Texture> observer, string uri)
        {
            using var uwr = UnityWebRequestTexture.GetTexture(uri);

            yield return uwr.SendWebRequest();
            if (uwr.result == UnityWebRequest.Result.ConnectionError | uwr.result == UnityWebRequest.Result.ProtocolError)
            {
                // エラーが起きたら OnError メッセージを発行する
                observer.OnError(new Exception(uwr.error));
                yield break;
            }

            var result = (uwr.downloadHandler as DownloadHandlerTexture).texture;
            // 成功したら OnNext/OnCompleted メッセージを発行する
            observer.OnNext(result);
            observer.OnCompleted();
        }
    }
}
