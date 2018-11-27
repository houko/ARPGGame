// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	#pragma warning disable 414

	public abstract class FsmStateActionAnimatorBase : FsmStateAction
	{
		public enum AnimatorFrameUpdateSelector {OnUpdate,OnAnimatorMove,OnAnimatorIK};

		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		[Tooltip("Select when to perform the action, during OnUpdate, OnAnimatorMove, OnAnimatorIK")]
		public AnimatorFrameUpdateSelector everyFrameOption;

		/// <summary>
		/// The layerIndex index passed when processing action during OnAnimatorIK
		/// </summary>
		protected int IklayerIndex;

		/// <summary>
		/// Raises the action update event. Could be fired during onUpdate or OnAnimatorMove based on the action setup.
		/// </summary>
		public abstract void OnActionUpdate();
		
		public override void Reset()
		{
			everyFrame = false;
			everyFrameOption = AnimatorFrameUpdateSelector.OnUpdate;
		}

		public override void OnPreprocess()
		{
			if (everyFrameOption == AnimatorFrameUpdateSelector.OnAnimatorMove)
			{
				Fsm.HandleAnimatorMove = true;
			}

			if (everyFrameOption == AnimatorFrameUpdateSelector.OnAnimatorIK)
			{
				Fsm.HandleAnimatorIK = true;
			}
		}

		public override void OnUpdate()
		{

			if (everyFrameOption == AnimatorFrameUpdateSelector.OnUpdate)
			{
				OnActionUpdate();
			}
			
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void DoAnimatorMove ()
		{
			if (everyFrameOption == AnimatorFrameUpdateSelector.OnAnimatorMove)
			{

				OnActionUpdate();
			}
			
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void DoAnimatorIK (int layerIndex)
		{
			IklayerIndex = layerIndex;

			if (everyFrameOption == AnimatorFrameUpdateSelector.OnAnimatorIK)
			{
				OnActionUpdate();
			}
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
	}
}