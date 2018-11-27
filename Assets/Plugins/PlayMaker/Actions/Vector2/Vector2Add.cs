// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Vector2)]
	[Tooltip("Adds a value to Vector2 Variable.")]
	public class Vector2Add : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The vector2 target")]
		public FsmVector2 vector2Variable;
		
		[RequiredField]
		[Tooltip("The vector2 to add")]
		public FsmVector2 addVector;
		
		[Tooltip("Repeat every frame")]
		public bool everyFrame;
		
		[Tooltip("Add the value on a per second bases.")]
		public bool perSecond;

		public override void Reset()
		{
			vector2Variable = null;
			addVector = new FsmVector2 { UseVariable = true };
			everyFrame = false;
			perSecond = false;
		}

		public override void OnEnter()
		{
			DoVector2Add();
			
			if (!everyFrame)
				Finish();		
		}

		public override void OnUpdate()
		{
			DoVector2Add();
		}
		
		void DoVector2Add()
		{
			if(perSecond)
				vector2Variable.Value = vector2Variable.Value + addVector.Value * Time.deltaTime;
			else
				vector2Variable.Value = vector2Variable.Value + addVector.Value;
				
		}
	}
}

