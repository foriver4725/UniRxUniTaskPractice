using UnityEngine;
using UniRx;

namespace Section3_2
{
    public sealed class ReactiveCollection : MonoBehaviour
    {
        private void Start()
        {
            var rc = new ReactiveCollection<int>().AddTo(this);

            rc.ObserveMove()
              .Subscribe(static @event =>
              {
                  Debug.Log($"Move: {@event.Value} from {@event.OldIndex} to {@event.NewIndex}");
              });

            rc.Add(1);
            rc.Add(2);
            rc.Add(3);
            rc[0] = 3;
            rc.Move(1, 2);
            foreach (var item in rc)
            {
                Debug.Log(item);
            }
        }
    }
}
