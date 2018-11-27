// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Input)]
	[Tooltip("Perform a Mouse Pick on a 2d scene and stores the results. Use Ray Distance to set how close the camera must be to pick the 2d object.")]
	public class MousePick2d : FsmStateAction
	{
		[UIHint(UIHint.Variable)]
        [Tooltip("Store if a GameObject was picked in a Bool variable. True if a GameObject was picked, otherwise false.")]
		public FsmBool storeDidPickObject;
		
        [UIHint(UIHint.Variable)]
        [Tooltip("Store the picked GameObject in a variable.")]
		public FsmGameObject storeGameObject;
		
        [UIHint(UIHint.Variable)]
        [Tooltip("Store the picked point in a variable.")]
		public FsmVector2 storePoint;
		
        [UIHint(UIHint.Layer)]
		[Tooltip("Pick only from these layers.")]
		public FsmInt[] layerMask;
		
        [Tooltip("Invert the mask, so you pick from all layers except those defined above.")]
		public FsmBool invertMask;
		
        [Tooltip("Repeat every frame.")]
        public bool everyFrame;
		
		public override void Reset()
		{
			storeDidPickObject = null;
			storeGameObject = null;
			storePoint = null;
			layerMask = new FsmInt[0];
			invertMask = false;
			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			DoMousePick2d();

		    if (!everyFrame)
		    {
		        Finish();
		    }
		}
		
		public override void OnUpdate()
		{
			DoMousePick2d();
		}
		
		void DoMousePick2d()
		{

			var hitInfo = Physics2D.GetRayIntersection(
				Camera.main.ScreenPointToRay(Input.mousePosition),
				Mathf.Infinity,
				ActionHelpers.LayerArrayToLayerMask(layerMask, invertMask.Value));

			var didPick = hitInfo.collider != null;
			storeDidPickObject.Value = didPick;
			
			if (didPick)
			{
				storeGameObject.Value = hitInfo.collider.gameObject;
				storePoint.Value = hitInfo.point;
			}
			else
			{
				// TODO: not sure if this is the right strategy...
				storeGameObject.Value = null;
				storePoint.Value = Vector3.zero;
			}
			
		}
	}
}