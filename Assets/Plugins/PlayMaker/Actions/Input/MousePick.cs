// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Input)]
	[Tooltip("Perform a Mouse Pick on the scene from the Main Camera and stores the results. Use Ray Distance to set how close the camera must be to pick the object.")]
	public class MousePick : FsmStateAction
	{
		[RequiredField]
		[Tooltip("Set the length of the ray to cast from the Main Camera.")]
        public FsmFloat rayDistance = 100f;
		
        [UIHint(UIHint.Variable)]
        [Tooltip("Set Bool variable true if an object was picked, false if not.")]
		public FsmBool storeDidPickObject;
		
        [UIHint(UIHint.Variable)]
        [Tooltip("Store the picked GameObject.")]
		public FsmGameObject storeGameObject;
		
        [UIHint(UIHint.Variable)]
        [Tooltip("Store the point of contact.")]
		public FsmVector3 storePoint;
		
        [UIHint(UIHint.Variable)]
        [Tooltip("Store the normal at the point of contact.")]
		public FsmVector3 storeNormal;
		
        [UIHint(UIHint.Variable)]
        [Tooltip("Store the distance to the point of contact.")]
		public FsmFloat storeDistance;
		
        [UIHint(UIHint.Layer)]	
        [Tooltip("Pick only from these layers.")]
		public FsmInt[] layerMask;
		
        [Tooltip("Invert the mask, so you pick from all layers except those defined above.")]
		public FsmBool invertMask;
		
        [Tooltip("Repeat every frame.")]
        public bool everyFrame;
		
		public override void Reset()
		{
			rayDistance = 100f;
			storeDidPickObject = null;
			storeGameObject = null;
			storePoint = null;
			storeNormal = null;
			storeDistance = null;
			layerMask = new FsmInt[0];
			invertMask = false;
			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			DoMousePick();

		    if (!everyFrame)
		    {
		        Finish();
		    }
		}
		
		public override void OnUpdate()
		{
			DoMousePick();
		}

	    private void DoMousePick()
		{
			var hitInfo = ActionHelpers.MousePick(rayDistance.Value, ActionHelpers.LayerArrayToLayerMask(layerMask, invertMask.Value));
			
			var didPick = hitInfo.collider != null;
			storeDidPickObject.Value = didPick;
			
			if (didPick)
			{
				storeGameObject.Value = hitInfo.collider.gameObject;
				storeDistance.Value = hitInfo.distance;
				storePoint.Value = hitInfo.point;
				storeNormal.Value = hitInfo.normal;
			}
			else
			{
				// TODO: not sure if this is the right strategy...
				storeGameObject.Value = null;
				storeDistance.Value = Mathf.Infinity;
				storePoint.Value = Vector3.zero;
				storeNormal.Value = Vector3.zero;
			}
			
		}
	}
}