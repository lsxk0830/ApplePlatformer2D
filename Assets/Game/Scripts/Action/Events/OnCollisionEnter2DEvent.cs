using UnityEngine.Events;
using UnityEngine;
using System.Linq;

namespace Blue
{
    public class OnCollisionEnter2DEvent : MonoBehaviour
    {
        public enum CollisionType
        {
            Tags,
            Layers,
            TagsAndLayers,
            TagsOrLayers,
            NotTagsAndLayers
        }

        public CollisionType Type = CollisionType.Tags;
        public string[] Tags;
        public LayerMask Layers;
        public UnityEvent OnEnter = new UnityEvent();
        public UnityEvent<Collision2D> OnEnterWithCollision = new UnityEvent<Collision2D>();


        private void OnCollisionEnter2D(Collision2D other)
        {
            if (Type == CollisionType.Tags || Type == CollisionType.TagsOrLayers)
            {
                if (Tags.Any(tag => other.otherCollider.CompareTag(tag)))
                {
                    OnEnter?.Invoke();
                    OnEnterWithCollision?.Invoke(other);
                }
            }

            if (Type == CollisionType.Layers || Type == CollisionType.TagsOrLayers)
            {
                if (LayerMaskUtility.Contains(Layers, other.gameObject.layer))
                {
                    OnEnter?.Invoke();
                    OnEnterWithCollision?.Invoke(other);
                }
            }

            if (Type == CollisionType.TagsAndLayers)
            {
                if (LayerMaskUtility.Contains(Layers, other.gameObject.layer) && Tags.Any(tag => other.otherCollider.CompareTag(tag)))
                {
                    OnEnter?.Invoke();
                    OnEnterWithCollision?.Invoke(other);
                }
            }

            if (Type == CollisionType.NotTagsAndLayers)
            {
                if (LayerMaskUtility.Contains(Layers, other.gameObject.layer) && !Tags.Any(tag => other.otherCollider.CompareTag(tag)))
                {
                    OnEnter?.Invoke();
                    OnEnterWithCollision?.Invoke(other);
                }
            }
        }
    }
}