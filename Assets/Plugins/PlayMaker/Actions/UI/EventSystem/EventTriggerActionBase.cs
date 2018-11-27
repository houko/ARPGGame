// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

using UnityEngine.EventSystems;

#if UNITY_5_6_OR_NEWER   
namespace HutongGames.PlayMaker.Actions
{
    // Base action for EventTrigger actions
    // Handles boilerplate setup etc.
    public abstract class EventTriggerActionBase : ComponentAction<EventTrigger>
    {
        [DisplayOrder(0)]
        [RequiredField]
        [Tooltip("The GameObject with the UI component.")]
        public FsmOwnerDefault gameObject;

        [DisplayOrder(1)]
        [Tooltip("Where to send the event.")]
        public FsmEventTarget eventTarget;

        protected EventTrigger trigger;
        protected EventTrigger.Entry entry;

        public override void Reset()
        {
            gameObject = null;
            eventTarget = FsmEventTarget.Self;
        }

        protected void Init(EventTriggerType eventTriggerType, UnityEngine.Events.UnityAction<BaseEventData> call)
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (UpdateCacheAddComponent(go))
            {
                trigger = cachedComponent;

                if (entry == null)
                {
                    entry = new EventTrigger.Entry ();
                }

                entry.eventID = eventTriggerType;
                entry.callback.AddListener(call);

                trigger.triggers.Add(entry);
            }
        }
		       
        public override void OnExit()
        {
            entry.callback.RemoveAllListeners ();
            trigger.triggers.Remove (entry);
        }
    }
}

#endif