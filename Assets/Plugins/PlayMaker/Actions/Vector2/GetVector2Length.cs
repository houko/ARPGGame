// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Vector2)]
	[Tooltip("Get Vector2 Length.")]
	public class GetVector2Length : FsmStateAction
	{
		[Tooltip("The Vector2 to get the length from")]
		public FsmVector2 vector2;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Vector2 the length")]
		public FsmFloat storeLength;
		
		[Tooltip("Repeat every frame")]
		public bool everyFrame;
		
		
		public override void Reset()
		{
			vector2 = null;
			storeLength = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoVectorLength();
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			DoVectorLength();
		}
		
		void DoVectorLength()
		{
			if (vector2 == null) return;
			if (storeLength == null) return;
			storeLength.Value = vector2.Value.magnitude;
		}
	}
}