// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

using UnityEngine.EventSystems;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Checks if Pointer is over a UI object, optionally takes a pointer ID, otherwise uses the current event.")]
	public class UiIsPointerOverUiObject : FsmStateAction
	{
		[Tooltip("Optional PointerId. Leave to none to use the current event")]
		public FsmInt pointerId;

		[Tooltip("Event to send when the Pointer is over an UI object.")]
		public FsmEvent pointerOverUI;
		
		[Tooltip("Event to send when the Pointer is NOT over an UI object.")]
		public FsmEvent pointerNotOverUI;
		
		[UIHint(UIHint.Variable)]
		public FsmBool isPointerOverUI;
		
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;
		
		public override void Reset()
		{
		    pointerId = new FsmInt {UseVariable = true};
			pointerOverUI = null;
			pointerNotOverUI = null;
			isPointerOverUI = null;
			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			DoCheckPointer();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			DoCheckPointer();
		}


	    private void DoCheckPointer()
		{
			var isOver = false;

			if (pointerId.IsNone)
			{
				isOver = EventSystem.current.IsPointerOverGameObject();
			}
			else
			{

				if(EventSystem.current.currentInputModule is PointerInputModule) {
				
					var module = EventSystem.current.currentInputModule as PointerInputModule;
					isOver = module.IsPointerOverGameObject(pointerId.Value);
				}

			}

			isPointerOverUI.Value = isOver;

		    Fsm.Event(isOver ? pointerOverUI : pointerNotOverUI);
		}	
	}
}