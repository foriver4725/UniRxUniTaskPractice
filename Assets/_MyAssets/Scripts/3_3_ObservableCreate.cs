using System;
using System.Threading.Tasks;
using UnityEngine;
using UniRx;

namespace Section3_3
{
    public sealed class ObservableCreate : MonoBehaviour
    {
        private void Start()
        {
            Observable.Create<char>(static observer =>
                      {
                          var disposable = new CancellationDisposable();

                          Task.Run(async () =>
                          {
                              for (var i = 0; i < 26; i++)
                              {
                                  await Task.Delay(TimeSpan.FromSeconds(1), disposable.Token);
                                  observer.OnNext((char)('A' + i));
                              }

                              observer.OnCompleted();
                          }, disposable.Token);

                          return disposable;
                      })
                      .Subscribe(
                          x => Debug.Log("OnNext: " + x),
                          ex => Debug.LogError("OnError: " + ex.Message),
                          () => Debug.Log("OnCompleted")
                      ).AddTo(this);
        }
    }
}
