using UnityEngine.Events;
using UnityEngine;
using System.Linq;

namespace Blue
{
    public class OnTriggerStay2DEvent : MonoBehaviour
    {
        public enum TriggerType
        {
            Tags,
            Layers,
            TagsAndLayers,
            TagsOrLayers,
            NotTagsAndLayers
        }

        public TriggerType Type = TriggerType.Tags;
        public string[] Tags;
        public LayerMask Layers;
        public UnityEvent OnStay = new UnityEvent();
        public UnityEvent<Collider2D> OnStayWithCollider = new UnityEvent<Collider2D>();

        private void OnTriggerStay2D(Collider2D other)
        {
            if (Type == TriggerType.Tags || Type == TriggerType.TagsOrLayers)
            {
                if (Tags.Any(tag => other.CompareTag(tag)))
                {
                    OnStay?.Invoke();
                    OnStayWithCollider?.Invoke(other);
                }
            }

            if (Type == TriggerType.Layers || Type == TriggerType.TagsOrLayers)
            {
                if (LayerMaskUtility.Contains(Layers, other.gameObject.layer))
                {
                    OnStay?.Invoke();
                    OnStayWithCollider?.Invoke(other);
                }
            }

            if (Type == TriggerType.TagsAndLayers)
            {
                if (LayerMaskUtility.Contains(Layers, other.gameObject.layer) && Tags.Any(tag => other.CompareTag(tag)))
                {
                    OnStay?.Invoke();
                    OnStayWithCollider?.Invoke(other);
                }
            }

            if (Type == TriggerType.NotTagsAndLayers)
            {
                if (LayerMaskUtility.Contains(Layers, other.gameObject.layer) && !Tags.Any(tag => other.CompareTag(tag)))
                {
                    OnStay?.Invoke();
                    OnStayWithCollider?.Invoke(other);
                }
            }
        }
    }
}