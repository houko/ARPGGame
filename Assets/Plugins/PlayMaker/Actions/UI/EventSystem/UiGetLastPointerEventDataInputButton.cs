// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

using UnityEngine.EventSystems;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Gets pointer data Input Button on the last System event.")]
	public class UiGetLastPointerEventDataInputButton : FsmStateAction
	{
		[UIHint(UIHint.Variable)]
		[ObjectType(typeof(PointerEventData.InputButton))]
		public FsmEnum inputButton;

		public FsmEvent leftClick;

		public FsmEvent middleClick;

		public FsmEvent rightClick;


		public override void Reset()
		{

			inputButton = PointerEventData.InputButton.Left;
			leftClick = null;
			middleClick = null;
			rightClick = null;

		}
		
		public override void OnEnter()
		{
			ExecuteAction();

			Finish();
		}

	    private void ExecuteAction()
		{
			if (UiGetLastPointerDataInfo.lastPointerEventData==null)
			{
				return;
			}

			if (!inputButton.IsNone)
			{
				inputButton.Value = UiGetLastPointerDataInfo.lastPointerEventData.button;
			}
			
			if (!string.IsNullOrEmpty(leftClick.Name) && 
			    UiGetLastPointerDataInfo.lastPointerEventData.button == PointerEventData.InputButton.Left)
			{
				Fsm.Event(leftClick);
				return;
			}

			if (!string.IsNullOrEmpty(middleClick.Name) && 
			    UiGetLastPointerDataInfo.lastPointerEventData.button == PointerEventData.InputButton.Middle)
			{
				Fsm.Event(middleClick);
				return;
			}

			if (!string.IsNullOrEmpty(rightClick.Name) && 
			    UiGetLastPointerDataInfo.lastPointerEventData.button == PointerEventData.InputButton.Right)
			{
				Fsm.Event(rightClick);
			}
		}
	}
}