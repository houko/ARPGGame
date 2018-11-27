// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

namespace HutongGames.PlayMaker.Actions
{
    // Base class for actions that need to select Update, LateUpdate, or FixedUpdate
	public abstract class BaseUpdateAction : FsmStateAction
	{
	    public enum UpdateType
	    {
	        OnUpdate,
	        OnLateUpdate,
	        OnFixedUpdate
	    }

		[ActionSection("Update type")]

		[Tooltip("Repeat every frame.")]
		public bool everyFrame;
		
		public UpdateType updateType;
			
		public abstract void OnActionUpdate();
		
		public override void Reset()
		{
			everyFrame = false;
			updateType = UpdateType.OnUpdate;
		}
		
		public override void OnPreprocess()
		{
			if (updateType == UpdateType.OnFixedUpdate)
			{
				   Fsm.HandleFixedUpdate = true;
			}
		}
		
		public override void OnUpdate()
		{
			if (updateType == UpdateType.OnUpdate)
			{
				OnActionUpdate();
			}
			
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnLateUpdate()
		{
			if (updateType == UpdateType.OnLateUpdate)
			{
				OnActionUpdate();
			}

			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnFixedUpdate()
		{
			if (updateType == UpdateType.OnFixedUpdate)
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