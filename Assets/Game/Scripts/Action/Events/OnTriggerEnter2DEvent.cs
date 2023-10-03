using UnityEngine.Events;
using UnityEngine;
using System.Linq;

namespace Blue
{
    public class OnTriggerEnter2DEvent : MonoBehaviour
    {
        public enum TriggerType
        {
            Tags,
            Layers,
            TagsAndLayers,
            TagsOrLayers
        }

        public TriggerType Type = TriggerType.Tags;
        public string[] Tags;
        public LayerMask Layers;
        public UnityEvent OnEnter = new UnityEvent();

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (Type == TriggerType.Tags || Type == TriggerType.TagsOrLayers)
            {
                if (Tags.Any(tag => other.CompareTag(tag)))
                {
                    OnEnter?.Invoke();
                }
            }

            if (Type == TriggerType.Layers || Type == TriggerType.TagsOrLayers)
            {
                if (LayerMaskUtility.Contains(Layers, other.gameObject.layer))
                {
                    OnEnter?.Invoke();
                }
            }

            if (Type == TriggerType.TagsAndLayers)
            {
                if (LayerMaskUtility.Contains(Layers, other.gameObject.layer) && Tags.Any(tag => other.CompareTag(tag)))
                {
                    OnEnter?.Invoke();
                }
            }
        }
    }
}