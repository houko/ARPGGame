// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Vector2)]
	[Tooltip("Performs most possible operations on 2 Vector2: Dot product, Distance, Angle, Add, Subtract, Multiply, Divide, Min, Max")]
	public class Vector2Operator : FsmStateAction
	{
		
		public enum Vector2Operation
		{
			DotProduct,
			Distance,
			Angle,
			Add,
			Subtract,
			Multiply,
			Divide,
			Min,
			Max
		}

		[RequiredField]
		[Tooltip("The first vector")]
		public FsmVector2 vector1;
		
		[RequiredField]
		[Tooltip("The second vector")]
		public FsmVector2 vector2;
		
		[Tooltip("The operation")]
		public Vector2Operation operation = Vector2Operation.Add;

		[UIHint(UIHint.Variable)]
		[Tooltip("The Vector2 result when it applies.")]
		public FsmVector2 storeVector2Result;

		[UIHint(UIHint.Variable)]
		[Tooltip("The float result when it applies")]
		public FsmFloat storeFloatResult;
		
		[Tooltip("Repeat every frame")]
		public bool everyFrame;

		public override void Reset()
		{
			vector1 = null;
			vector2 = null;
			operation = Vector2Operation.Add;
			storeVector2Result = null;
			storeFloatResult = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoVector2Operator();

			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoVector2Operator();
		}

		void DoVector2Operator()
		{
			var v1 = vector1.Value;
			var v2 = vector2.Value;

			switch (operation)
			{
				case Vector2Operation.DotProduct:
					storeFloatResult.Value = Vector2.Dot(v1, v2);
					break;
				
				case Vector2Operation.Distance:
					storeFloatResult.Value = Vector2.Distance(v1, v2);
					break;

				case Vector2Operation.Angle:
					storeFloatResult.Value = Vector2.Angle(v1, v2);
					break;

				case Vector2Operation.Add:
					storeVector2Result.Value = v1 + v2;
					break;

				case Vector2Operation.Subtract:
					storeVector2Result.Value = v1 - v2;
					break;

				case Vector2Operation.Multiply:
					// I know... this is a far reach and not useful in 99% of cases. 
					// I do use it when I use vector2 as arrays recipients holding something else than a position in space.
					var multResult = Vector2.zero;
					multResult.x = v1.x * v2.x;
					multResult.y = v1.y * v2.y;
					storeVector2Result.Value = multResult;
					break;

				case Vector2Operation.Divide: // I know... this is a far reach and not useful in 99% of cases.
					// I do use it when I use vector2 as arrays recipients holding something else than a position in space.
					var divResult = Vector2.zero;
					divResult.x = v1.x / v2.x;
					divResult.y = v1.y / v2.y;
					storeVector2Result.Value = divResult;
					break;

				case Vector2Operation.Min:
					storeVector2Result.Value = Vector2.Min(v1, v2);
					break;

				case Vector2Operation.Max:
					storeVector2Result.Value = Vector2.Max(v1, v2);
					break;
			}
		}
	}
}