// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics2D)]
	[Tooltip("Gets info on the last Trigger 2d event and store in variables.  See Unity and PlayMaker docs on Unity 2D physics.")]
	public class GetTrigger2dInfo : FsmStateAction
	{
		[UIHint(UIHint.Variable)]
		[Tooltip("Get the GameObject hit.")]
		public FsmGameObject gameObjectHit;

		[UIHint(UIHint.Variable)]
		[Tooltip("The number of separate shaped regions in the collider.")]
		public FsmInt shapeCount;
	
		[UIHint(UIHint.Variable)]
		[Tooltip("Useful for triggering different effects. Audio, particles...")]
		public FsmString physics2dMaterialName;
		
		public override void Reset()
		{
			gameObjectHit = null;
			shapeCount = null;
			physics2dMaterialName = null;
		}
		
		void StoreTriggerInfo()
		{
            if (Fsm.TriggerCollider2D == null) return;

            gameObjectHit.Value = Fsm.TriggerCollider2D.gameObject;
            shapeCount.Value = Fsm.TriggerCollider2D.shapeCount;
            physics2dMaterialName.Value = Fsm.TriggerCollider2D.sharedMaterial != null ? Fsm.TriggerCollider2D.sharedMaterial.name : "";
		}
		
		public override void OnEnter()
		{
			StoreTriggerInfo();
			
			Finish();
		}
	}
}