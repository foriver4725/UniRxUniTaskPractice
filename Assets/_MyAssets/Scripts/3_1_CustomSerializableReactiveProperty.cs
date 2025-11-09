using System;
using UnityEngine;
using UniRx;

namespace Section3_1
{
    public sealed class CustomSerializableReactiveProperty : MonoBehaviour
    {
        [SerializeField] private FruitReactiveProperty fruit = new(Fruit.Banana);
    }

    public enum Fruit : byte
    {
        Apple,
        Banana,
        Grape,
    }

    [Serializable]
    public sealed class FruitReactiveProperty : ReactiveProperty<Fruit>
    {
        public FruitReactiveProperty()
        {
        }

        public FruitReactiveProperty(Fruit init) : base(init)
        {
        }
    }

    [UnityEditor.CustomPropertyDrawer(typeof(FruitReactiveProperty))]
    public sealed class ExtendInspectorDisplayDrawer : InspectorDisplayDrawer
    {
    }
}
