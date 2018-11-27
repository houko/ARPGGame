// (c) Copyright HutongGames, LLC 2010-2012. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Trigonometry)]
	[Tooltip("Get the Arc Tangent 2 as in atan2(y,x) from a vector 3, where you pick which is x and y from the vector 3. You can get the result in degrees, simply check on the RadToDeg conversion")]
	public class GetAtan2FromVector3 : FsmStateAction
	{
		
		public enum aTan2EnumAxis
		{
			x,
			y,
			z
		}
		
		[RequiredField]
		[Tooltip("The vector3 definition of the tan")]
		public FsmVector3 vector3;
		
		[RequiredField]
		[Tooltip("which axis in the vector3 to use as the x value of the tan")]
		public aTan2EnumAxis xAxis;
		
		[RequiredField]
		[Tooltip("which axis in the vector3 to use as the y value of the tan")]
		public aTan2EnumAxis yAxis;
		

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The resulting angle. Note:If you want degrees, simply check RadToDeg")]
		public FsmFloat angle;
		
		[Tooltip("Check on if you want the angle expressed in degrees.")]
		public FsmBool RadToDeg;
		
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		public override void Reset()
		{
			vector3 = null;
			xAxis = aTan2EnumAxis.x;
			yAxis = aTan2EnumAxis.y;
			
			RadToDeg = true;
			everyFrame = false;
			angle = null;
		}

		public override void OnEnter()
		{
			DoATan();
			
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoATan();
		}
		
		void DoATan()
		{
			
			float x = vector3.Value.x;
			if (xAxis == aTan2EnumAxis.y)
			{
				x = vector3.Value.y;
			}else if (xAxis == aTan2EnumAxis.z){
				x = vector3.Value.z;
			}
			
			float y = vector3.Value.y;
			if (yAxis == aTan2EnumAxis.x)
			{
				y = vector3.Value.x;
			}else if (yAxis == aTan2EnumAxis.z){
				y = vector3.Value.z;
			}
			
			
			float _angle = Mathf.Atan2(y,x);
			
			
			if (RadToDeg.Value)
			{
				_angle	 = _angle*Mathf.Rad2Deg;
			}
			angle.Value = _angle;
			
		}
	}
}