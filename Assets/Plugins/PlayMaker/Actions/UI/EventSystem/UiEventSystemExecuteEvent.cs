// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;
using UnityEngine.EventSystems;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("The eventType will be executed on all components on the GameObject that can handle it.")]
	public class UiEventSystemExecuteEvent : FsmStateAction
	{
		public enum EventHandlers {
			Submit,
			beginDrag,
			cancel,
			deselectHandler,
			dragHandler,
			dropHandler,
			endDragHandler,
			initializePotentialDrag,
			pointerClickHandler,
			pointerDownHandler,
			pointerEnterHandler,
			pointerExitHandler,
			pointerUpHandler,
			scrollHandler,
			submitHandler,
			updateSelectedHandler
		}
	
		[RequiredField]
		[Tooltip("The GameObject with  an IEventSystemHandler component (a UI button for example).")]
		public FsmOwnerDefault gameObject;

		[Tooltip("The Type of handler to execute")]
		[ObjectType(typeof(EventHandlers))]
		public FsmEnum  eventHandler;

		[Tooltip("Event Sent if execution was possible on GameObject")]
		public FsmEvent success;

		[Tooltip("Event Sent if execution was NOT possible on GameObject because it can not handle the eventHandler selected")]
		public FsmEvent canNotHandleEvent;

	    private GameObject go;


		public override void Reset()
		{
			gameObject = null;
			eventHandler = EventHandlers.Submit;
			success = null;
			canNotHandleEvent = null;
		}
		
		public override void OnEnter()
		{
		    Fsm.Event(ExecuteEvent() ? success : canNotHandleEvent);

		    Finish();
		}

	    private bool ExecuteEvent()
		{
			go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null)
			{
				LogError("Missing GameObject ");
				return false;
			}

			var handlerType = (EventHandlers)eventHandler.Value;

		    switch (handlerType)
		    {
		        case EventHandlers.Submit:
		            if (!ExecuteEvents.CanHandleEvent<ISubmitHandler>(go)) return false;
		            ExecuteEvents.Execute(go, new BaseEventData(EventSystem.current), ExecuteEvents.submitHandler);
		            break;

		        case EventHandlers.beginDrag:
		            if (!ExecuteEvents.CanHandleEvent<IBeginDragHandler>(go)) return false;
		            ExecuteEvents.Execute(go, new BaseEventData(EventSystem.current), ExecuteEvents.beginDragHandler);
		            break;

		        case EventHandlers.cancel:
		            if (!ExecuteEvents.CanHandleEvent<ICancelHandler>(go)) return false;
		            ExecuteEvents.Execute(go, new BaseEventData(EventSystem.current), ExecuteEvents.cancelHandler);
		            break;

		        case EventHandlers.deselectHandler:
		            if (!ExecuteEvents.CanHandleEvent<IDeselectHandler>(go)) return false;
		            ExecuteEvents.Execute(go, new BaseEventData(EventSystem.current), ExecuteEvents.deselectHandler);
		            break;
		        
		        case EventHandlers.dragHandler:
		            if (!ExecuteEvents.CanHandleEvent<IDragHandler>(go)) return false;
		            ExecuteEvents.Execute(go, new BaseEventData(EventSystem.current), ExecuteEvents.dragHandler);
		            break;
		        
		        case EventHandlers.dropHandler:
		            if (!ExecuteEvents.CanHandleEvent<IDropHandler>(go)) return false;
		            ExecuteEvents.Execute(go, new BaseEventData(EventSystem.current), ExecuteEvents.dropHandler);
		            break;
		        
		        case EventHandlers.endDragHandler:
		            if (!ExecuteEvents.CanHandleEvent<IEndDragHandler>(go)) return false;
		            ExecuteEvents.Execute(go, new BaseEventData(EventSystem.current), ExecuteEvents.endDragHandler);
		            break;
		        
		        case EventHandlers.initializePotentialDrag:
		            if (!ExecuteEvents.CanHandleEvent<IInitializePotentialDragHandler>(go)) return false;
		            ExecuteEvents.Execute(go, new BaseEventData(EventSystem.current), ExecuteEvents.initializePotentialDrag);
		            break;
		        
		        case EventHandlers.pointerClickHandler:
		            if (!ExecuteEvents.CanHandleEvent<IPointerClickHandler>(go)) return false;
		            ExecuteEvents.Execute(go, new BaseEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
		            break;
		        
		        case EventHandlers.pointerDownHandler:
		            if (!ExecuteEvents.CanHandleEvent<IPointerDownHandler>(go)) return false;
		            ExecuteEvents.Execute(go, new BaseEventData(EventSystem.current), ExecuteEvents.pointerDownHandler);
		            break;
		        
		        case EventHandlers.pointerUpHandler:
		            if (!ExecuteEvents.CanHandleEvent<IPointerUpHandler>(go)) return false;
		            ExecuteEvents.Execute(go, new BaseEventData(EventSystem.current), ExecuteEvents.pointerUpHandler);
		            break;
		        
		        case EventHandlers.pointerEnterHandler:
		            if (!ExecuteEvents.CanHandleEvent<IPointerEnterHandler>(go)) return false;
		            ExecuteEvents.Execute(go, new BaseEventData(EventSystem.current), ExecuteEvents.pointerEnterHandler);
		            break;
		        
		        case EventHandlers.pointerExitHandler:
		            if (!ExecuteEvents.CanHandleEvent<IPointerExitHandler>(go)) return false;
		            ExecuteEvents.Execute(go, new BaseEventData(EventSystem.current), ExecuteEvents.pointerExitHandler);
		            break;
		        
		        case EventHandlers.scrollHandler:
		            if (!ExecuteEvents.CanHandleEvent<IScrollHandler>(go)) return false;
		            ExecuteEvents.Execute(go, new BaseEventData(EventSystem.current), ExecuteEvents.scrollHandler);
		            break;
		        
		        case EventHandlers.updateSelectedHandler:
		            if (!ExecuteEvents.CanHandleEvent<IUpdateSelectedHandler>(go)) return false;
		            ExecuteEvents.Execute(go, new BaseEventData(EventSystem.current), ExecuteEvents.updateSelectedHandler);
		            break;
		    }

		    return true;

		}
		
	}
}