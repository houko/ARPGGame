// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics2D)]
	[Tooltip("Sets the various properties of a HingeJoint2d component")]
	public class SetHingeJoint2dProperties : FsmStateAction
	{
		
		[RequiredField]
		[Tooltip("The HingeJoint2d target")]
		[CheckForComponent(typeof(HingeJoint2D))]
		public FsmOwnerDefault gameObject;

		[ActionSection("Limits")]

		[Tooltip("Should limits be placed on the range of rotation?")]
		public FsmBool useLimits;

		[Tooltip("Lower angular limit of rotation.")]
		public FsmFloat min;

		[Tooltip("Upper angular limit of rotation")]
		public FsmFloat max;


		[ActionSection("Motor")]
		
		[Tooltip("Should a motor force be applied automatically to the Rigidbody2D?")]
		public FsmBool useMotor;
		
		[Tooltip("The desired speed for the Rigidbody2D to reach as it moves with the joint.")]
		public FsmFloat motorSpeed;
		
		[Tooltip("The maximum force that can be applied to the Rigidbody2D at the joint to attain the target speed.")]
		public FsmFloat maxMotorTorque;
		

		[Tooltip("Repeat every frame while the state is active.")]
		public bool everyFrame;
		
		HingeJoint2D _joint;
		JointMotor2D _motor;
		JointAngleLimits2D _limits;

		public override void Reset()
		{

			useLimits = new FsmBool() {UseVariable=true};			
			min = new FsmFloat() {UseVariable=true};
			max = new FsmFloat() {UseVariable=true};

			useMotor = new FsmBool() {UseVariable=true};			
			motorSpeed = new FsmFloat() {UseVariable=true};
			maxMotorTorque = new FsmFloat() {UseVariable=true};

			
			everyFrame = false;
			
		}
		
		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			
			if (go != null)
			{
				_joint = go.GetComponent<HingeJoint2D>();
				
				if(_joint!=null)
				{
					_motor = _joint.motor;
					_limits = _joint.limits;
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
			if(_joint==null)
			{
				return;
			}

			if (!useMotor.IsNone)
			{
				_joint.useMotor = useMotor.Value;
			}
			
			if (!motorSpeed.IsNone)
			{
				_motor.motorSpeed = motorSpeed.Value;
				_joint.motor = _motor;
			}
			if (!maxMotorTorque.IsNone)
			{
				_motor.maxMotorTorque = maxMotorTorque.Value;
				_joint.motor = _motor;
			}

			if (!useLimits.IsNone)
			{
				_joint.useLimits = useLimits.Value;
			}
			
			if (!min.IsNone)
			{
				_limits.min = min.Value;
				_joint.limits = _limits;
			}
			if (!max.IsNone)
			{
				_limits.max = max.Value;
				_joint.limits = _limits;
			}

		}
		
	}
}