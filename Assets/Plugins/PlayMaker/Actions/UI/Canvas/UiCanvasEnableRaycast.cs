// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Enable or disable Canvas Raycasting. Optionally reset on state exit")]
	public class UiCanvasEnableRaycast: ComponentAction<PlayMakerCanvasRaycastFilterProxy>
	{
		[RequiredField]
		//[CheckForComponent(typeof(PlayMakerCanvasRaycastFilterProxy))]
		[Tooltip("The GameObject to enable or disable Canvas Raycasting on.")]
		public FsmOwnerDefault gameObject;

		public FsmBool enableRaycasting;

		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		public bool everyFrame;

        [SerializeField]
		private PlayMakerCanvasRaycastFilterProxy raycastFilterProxy;

	    private bool originalValue;

		public override void Reset()
		{
			gameObject = null;
			enableRaycasting = false;
			resetOnExit = null;
			everyFrame = false;
		}

	    public override void OnPreprocess()
	    {
            //Debug.Log("OnPreprocess");
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
	        if (UpdateCacheAddComponent(go))
	        {
	            raycastFilterProxy = cachedComponent;
	        }
	    }

	    public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (UpdateCacheAddComponent(go))
			{
				raycastFilterProxy = cachedComponent;
				originalValue = raycastFilterProxy.RayCastingEnabled;
			}

			DoAction();

			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoAction();
		}

	    private void DoAction()
		{
			if (raycastFilterProxy != null)
			{
				raycastFilterProxy.RayCastingEnabled = enableRaycasting.Value;
			}
		}
		
		public override void OnExit()
		{
		    if (raycastFilterProxy == null) return;
			
			if (resetOnExit.Value)
			{
				raycastFilterProxy.RayCastingEnabled = originalValue;
			}
		}
		
	}
}