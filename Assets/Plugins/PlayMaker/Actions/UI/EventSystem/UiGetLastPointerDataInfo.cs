// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

using UnityEngine.EventSystems;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Gets pointer data on the last System event.")]
	public class UiGetLastPointerDataInfo : FsmStateAction
	{
		public static PointerEventData lastPointerEventData;

		[UIHint(UIHint.Variable)]
		public FsmInt clickCount;

		[UIHint(UIHint.Variable)]
		public FsmFloat clickTime;

		[UIHint(UIHint.Variable)]
		public FsmVector2 delta;

		[UIHint(UIHint.Variable)]
		public FsmBool dragging;

		[UIHint(UIHint.Variable)]
		[ObjectType(typeof(PointerEventData.InputButton))]
		public FsmEnum inputButton;

		[UIHint(UIHint.Variable)]
		public FsmBool eligibleForClick;

		[UIHint(UIHint.Variable)]
		public FsmGameObject enterEventCamera;

		[UIHint(UIHint.Variable)]
		public FsmGameObject pressEventCamera;

		[UIHint(UIHint.Variable)]
		public FsmBool isPointerMoving;

		[UIHint(UIHint.Variable)]
		public FsmBool isScrolling;

		[UIHint(UIHint.Variable)]
		public FsmGameObject lastPress;

		[UIHint(UIHint.Variable)]
		public FsmGameObject pointerDrag;

		[UIHint(UIHint.Variable)]
		public FsmGameObject pointerEnter;

		[UIHint(UIHint.Variable)]
		public FsmInt pointerId;

		[UIHint(UIHint.Variable)]
		public FsmGameObject pointerPress;

		[UIHint(UIHint.Variable)]
		public FsmVector2 position;

		[UIHint(UIHint.Variable)]
		public FsmVector2 pressPosition;

		[UIHint(UIHint.Variable)]
		public FsmGameObject rawPointerPress;

		[UIHint(UIHint.Variable)]
		public FsmVector2 scrollDelta;

		[UIHint(UIHint.Variable)]
		public FsmBool used;

		[UIHint(UIHint.Variable)]
		public FsmBool useDragThreshold;

		[UIHint(UIHint.Variable)]
		public FsmVector3 worldNormal;

		[UIHint(UIHint.Variable)]
		public FsmVector3 worldPosition;


		public override void Reset()
		{
			clickCount = null;
			clickTime = null;
			delta = null;
			dragging = null;
			inputButton = PointerEventData.InputButton.Left;

			eligibleForClick = null;
			enterEventCamera = null;
			pressEventCamera = null;
			isPointerMoving= null;
			isScrolling = null;
			lastPress = null;
			pointerDrag = null;
			pointerEnter = null;
			pointerId = null;
			pointerPress = null;
			position = null;
			pressPosition = null;
			rawPointerPress = null;
			scrollDelta = null;
			used = null;
			useDragThreshold = null;
			worldNormal = null;
			worldPosition = null;
		}
		
		public override void OnEnter()
		{

			if (lastPointerEventData==null)
			{
				Finish();
				return;
			}


			if (!clickCount.IsNone)
			{
				clickCount.Value =  lastPointerEventData.clickCount;
			}

			if (!clickTime.IsNone)
			{
				clickTime.Value =  lastPointerEventData.clickTime;
			}

			if (!delta.IsNone)
			{
				delta.Value =  lastPointerEventData.delta;
			}

			if (!dragging.IsNone)
			{
				dragging.Value =  lastPointerEventData.dragging;
			}

			if (!inputButton.IsNone)
			{
				inputButton.Value = lastPointerEventData.button;
			}

			if (!eligibleForClick.IsNone)
			{
				eligibleForClick.Value =  lastPointerEventData.eligibleForClick;
			}

			if (!enterEventCamera.IsNone)
			{
				enterEventCamera.Value =  lastPointerEventData.enterEventCamera.gameObject;
			}

			if (!isPointerMoving.IsNone)
			{
				isPointerMoving.Value =  lastPointerEventData.IsPointerMoving();
			}

			if (!isScrolling.IsNone)
			{
				isScrolling.Value =  lastPointerEventData.IsScrolling();
			}

			if (!lastPress.IsNone)
			{
				lastPress.Value =  lastPointerEventData.lastPress;
			}

			if (!pointerDrag.IsNone)
			{
				pointerDrag.Value =  lastPointerEventData.pointerDrag;
			}

			if (!pointerEnter.IsNone)
			{
				pointerEnter.Value =  lastPointerEventData.pointerEnter;
			}

			if (!pointerId.IsNone)
			{
				pointerId.Value =  lastPointerEventData.pointerId;
			}

			if (!pointerPress.IsNone)
			{
				pointerPress.Value =  lastPointerEventData.pointerPress;
			}

			if (!position.IsNone)
			{
				position.Value =  lastPointerEventData.position;
			}

			if (!pressEventCamera.IsNone)
			{
				pressEventCamera.Value =  lastPointerEventData.pressEventCamera.gameObject;
			}

			if (!pressPosition.IsNone)
			{
				pressPosition.Value =  lastPointerEventData.pressPosition;
			}

			if (!rawPointerPress.IsNone)
			{
				rawPointerPress.Value =  lastPointerEventData.rawPointerPress;
			}

			if (!scrollDelta.IsNone)
			{
				scrollDelta.Value =  lastPointerEventData.scrollDelta;
			}

			if (!used.IsNone)
			{
				used.Value =  lastPointerEventData.used;
			}

			if (!useDragThreshold.IsNone)
			{
				useDragThreshold.Value =  lastPointerEventData.useDragThreshold;
			}

			if (!worldNormal.IsNone)
			{
				worldNormal.Value =  lastPointerEventData.pointerCurrentRaycast.worldNormal;
			}

			if (!worldPosition.IsNone)
			{
				worldPosition.Value =  lastPointerEventData.pointerCurrentRaycast.worldPosition;
			}


			Finish();
		}
	}
}