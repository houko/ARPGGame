// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics2D)]
	[Tooltip("Iterate through a list of all colliders that overlap a point in space." +
	         "The colliders iterated are sorted in order of increasing Z coordinate. No iteration will take place if there are no colliders overlap this point.")]
	public class GetNextOverlapPoint2d : FsmStateAction
	{
		[ActionSection("Setup")]
		
		[Tooltip("Point using the gameObject position. \nOr use From Position parameter.")]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("Point as a world position. \nOr use gameObject parameter. If both define, will add position to the gameObject position")]
		public FsmVector2 position;
		
		[Tooltip("Only include objects with a Z coordinate (depth) greater than this value. leave to none for no effect")]
		public FsmInt minDepth;
		
		[Tooltip("Only include objects with a Z coordinate (depth) less than this value. leave to none")]
		public FsmInt maxDepth;

        [Tooltip("If you want to reset the iteration, raise this flag to true when you enter the state, it will indicate you want to start from the beginning again")]
        [UIHint(UIHint.Variable)]
        public FsmBool resetFlag;

        [ActionSection("Filter")] 
		
		[UIHint(UIHint.Layer)]
		[Tooltip("Pick only from these layers.")]
		public FsmInt[] layerMask;
		
		[Tooltip("Invert the mask, so you pick from all layers except those defined above.")]
		public FsmBool invertMask;
		
		
		[ActionSection("Result")] 
		
		[Tooltip("Store the number of colliders found for this overlap.")]
		[UIHint(UIHint.Variable)]
		public FsmInt collidersCount;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the next collider in a GameObject variable.")]
		public FsmGameObject storeNextCollider;
		
		[Tooltip("Event to send to get the next collider.")]
		public FsmEvent loopEvent;
		
		[Tooltip("Event to send when there are no more colliders to iterate.")]
		public FsmEvent finishedEvent;
		
		
		private Collider2D[] colliders;
		
		private int colliderCount;
		
		// increment an index as we loop through children
		private int nextColliderIndex;
		
		public override void Reset()
		{
			gameObject = null;
			position = new FsmVector2 { UseVariable = true };
			
			minDepth = new FsmInt { UseVariable = true };
			maxDepth = new FsmInt { UseVariable = true };
			
			layerMask = new FsmInt[0];
			invertMask = false;
            resetFlag = null;
            collidersCount = null;
			storeNextCollider = null;
			loopEvent = null;
			finishedEvent = null;
		}
		
		public override void OnEnter()
		{
			if (colliders == null || resetFlag.Value)
			{
                nextColliderIndex = 0;
                colliders = GetOverlapPointAll();
				colliderCount = colliders.Length;
				collidersCount.Value = colliderCount;
                resetFlag.Value = false;
            }
			
			DoGetNextCollider();
			
			Finish();
			
		}

	    private void DoGetNextCollider()
		{
			
			// no more colliders?
			// check first to avoid errors.
			
			if (nextColliderIndex >= colliderCount)
			{
				nextColliderIndex = 0;
				Fsm.Event(finishedEvent);
				return;
			}
			
			// get next collider
			
			storeNextCollider.Value = colliders[nextColliderIndex].gameObject;
			
			
			// no more colliders?
			// check a second time to avoid process lock and possible infinite loop if the action is called again.
			// Practically, this enabled calling again this state and it will start again iterating from the first child.
			
			if (nextColliderIndex >= colliderCount)
			{
                colliders = null;
                nextColliderIndex = 0;
				Fsm.Event(finishedEvent);
				return;
			}
			
			// iterate the next collider
			nextColliderIndex++;
			
			if (loopEvent != null)
			{
				Fsm.Event(loopEvent);
			}
		}


	    private Collider2D[] GetOverlapPointAll()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			
			var pos = position.Value;
			
			if (go!=null)
			{
				pos.x += go.transform.position.x;
				pos.y += go.transform.position.y;
			}
			
			
			if (minDepth.IsNone && maxDepth.IsNone)
			{
				return Physics2D.OverlapPointAll(pos,ActionHelpers.LayerArrayToLayerMask(layerMask, invertMask.Value));
			}else{
				var _minDepth = minDepth.IsNone? Mathf.NegativeInfinity:minDepth.Value;
				var _maxDepth = maxDepth.IsNone? Mathf.Infinity:maxDepth.Value;
				return Physics2D.OverlapPointAll(pos,ActionHelpers.LayerArrayToLayerMask(layerMask, invertMask.Value),_minDepth,_maxDepth);
			}
		}
		
	}
}

