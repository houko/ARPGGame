// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Vector2)]
	[Tooltip("Use a low pass filter to reduce the influence of sudden changes in a Vector2 Variable.")]
	public class Vector2LowPassFilter : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Vector2 Variable to filter. Should generally come from some constantly updated input")]
		public FsmVector2 vector2Variable;
		[Tooltip("Determines how much influence new changes have. E.g., 0.1 keeps 10 percent of the unfiltered vector and 90 percent of the previously filtered value")]
		public FsmFloat filteringFactor;		
		
		Vector2 filteredVector;
		
		public override void Reset()
		{
			vector2Variable = null;
			filteringFactor = 0.1f;
		}

		public override void OnEnter()
		{
			filteredVector = new Vector2(vector2Variable.Value.x, vector2Variable.Value.y);
		}

		public override void OnUpdate()
		{
			filteredVector.x = (vector2Variable.Value.x * filteringFactor.Value) + (filteredVector.x * (1.0f - filteringFactor.Value));
			filteredVector.y = (vector2Variable.Value.y * filteringFactor.Value) + (filteredVector.y * (1.0f - filteringFactor.Value));

			vector2Variable.Value = new Vector2(filteredVector.x, filteredVector.y);
		}
	}
}

