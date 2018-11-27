// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
// http://j.mp/1U86Q5d

using UnityEngine;
using UnityEngine.EventSystems;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Returns the EventSystem's currently select GameObject.")]
	public class UiGetSelectedGameObject : FsmStateAction
	{
		[UIHint(UIHint.Variable)]
        [Tooltip("The currently selected GameObject")]
		public FsmGameObject StoreGameObject;

        [UIHint(UIHint.Variable)]
        [Tooltip("Event when the selected GameObject changes")]
        public FsmEvent ObjectChangedEvent;

        [UIHint(UIHint.Variable)]
        [Tooltip("If true, each frame will check the currently selected GameObject")]
        public bool everyFrame;

        private GameObject lastGameObject;

        public override void Reset()
        {
            StoreGameObject = null;
            everyFrame = false;
        }

        public override void OnEnter()
        {
            GetCurrentSelectedGameObject();
            lastGameObject = StoreGameObject.Value;
        }

        public override void OnUpdate()
        {
            GetCurrentSelectedGameObject();

            if (StoreGameObject.Value != lastGameObject && ObjectChangedEvent != null)
            {
                Fsm.Event(ObjectChangedEvent);
            }

            if(!everyFrame)
                Finish();
        }

        private void GetCurrentSelectedGameObject()
        {
            StoreGameObject.Value = EventSystem.current.currentSelectedGameObject;
        }
	}
}