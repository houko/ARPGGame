// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

#if !UNITY_4_3

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics2D)]
	[Tooltip("Sets the various properties of a WheelJoint2d component")]
	public class SetWheelJoint2dProperties : FsmStateAction
	{
		
		[RequiredField]
		[Tooltip("The WheelJoint2d target")]
		[CheckForComponent(typeof(WheelJoint2D))]
		public FsmOwnerDefault gameObject;
		
		[ActionSection("Motor")]

		[Tooltip("Should a motor force be applied automatically to the Rigidbody2D?")]
		public FsmBool useMotor;

		[Tooltip("The desired speed for the Rigidbody2D to reach as it moves with the joint.")]
		public FsmFloat motorSpeed;

		[Tooltip("The maximum force that can be applied to the Rigidbody2D at the joint to attain the target speed.")]
		public FsmFloat maxMotorTorque;

		[ActionSection("Suspension")]

		[Tooltip("The world angle along which the suspension will move. This provides 2D constrained motion similar to a SliderJoint2D. This is typically how suspension works in the real world.")]
		public FsmFloat angle;

		[Tooltip("The amount by which the suspension spring force is reduced in proportion to the movement speed.")]
		public FsmFloat dampingRatio;

		[Tooltip("The frequency at which the suspension spring oscillates.")]
		public FsmFloat frequency;

		[Tooltip("Repeat every frame while the state is active.")]
		public bool everyFrame;

		WheelJoint2D _wj2d;
		JointMotor2D _motor;
		JointSuspension2D _suspension;
		public override void Reset()
		{
			useMotor = new FsmBool() {UseVariable=true};
			
			motorSpeed = new FsmFloat() {UseVariable=true};
			maxMotorTorque = new FsmFloat() {UseVariable=true};
			angle = new FsmFloat() {UseVariable=true};
			dampingRatio = new FsmFloat() {UseVariable=true};
			frequency = new FsmFloat() {UseVariable=true};

			everyFrame = false;

		}
		
		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			
			if (go != null)
			{
				_wj2d = go.GetComponent<WheelJoint2D>();

				if(_wj2d!=null)
				{
					_motor = _wj2d.motor;
					_suspension = _wj2d.suspension;
				}
			}

			SetProperties();

			if(!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			SetProperties();
		}

		void SetProperties()
		{
			if(_wj2d==null)
			{
				return;
			}

			if (!useMotor.IsNone)
			{
				_wj2d.useMotor = useMotor.Value;
			}

			if (!motorSpeed.IsNone)
			{
				_motor.motorSpeed = motorSpeed.Value;
				_wj2d.motor = _motor;
			}
			if (!maxMotorTorque.IsNone)
			{
				_motor.maxMotorTorque = maxMotorTorque.Value;
				_wj2d.motor = _motor;
			}

			if (!angle.IsNone)
			{
				_suspension.angle = angle.Value;
				_wj2d.suspension = _suspension;
			}

			if (!dampingRatio.IsNone)
			{
				_suspension.dampingRatio = dampingRatio.Value;
				_wj2d.suspension = _suspension;
			}

			if (!frequency.IsNone)
			{
				_suspension.frequency = frequency.Value;
				_wj2d.suspension = _suspension;
			}
		}
		
	}
}
#endif