using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace UTGM
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

        public TriggerType Type= TriggerType.Tags;
        public string[] Tags;
        public LayerMask Layers;

        public UnityEvent OnEnter= new UnityEvent();
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (Type == TriggerType.Tags || Type == TriggerType.TagsOrLayers)
            {
                if (Tags.Any(tag => collision.tag == tag))
                {
                    OnEnter?.Invoke();
                }
            }
            else if (Type == TriggerType.Layers || Type == TriggerType.TagsOrLayers)
            {
                if (LayerMaskUtility.Contains(Layers,collision.gameObject.layer))
                {
                    OnEnter?.Invoke();
                }
            }
            else if (Type == TriggerType.TagsAndLayers)
            {
                if (Tags.Any(tag => collision.tag == tag) && LayerMaskUtility.Contains(Layers, collision.gameObject.layer))
                {
                    OnEnter?.Invoke();
                }
            }
        }
    }

}
