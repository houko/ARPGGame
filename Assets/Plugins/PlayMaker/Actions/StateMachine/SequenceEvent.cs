// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.StateMachine)]
	[Tooltip("Sends the next event on the state each time the state is entered.")]
	public class SequenceEvent : FsmStateAction
	{
		[HasFloatSlider(0, 10)]
		public FsmFloat delay;

        [UIHint(UIHint.Variable)]
        [Tooltip("Assign a variable to control reset. Set it to True to reset the sequence. Value is set to False after resetting.")]
        public FsmBool reset;

		DelayedEvent delayedEvent;
		int eventIndex;

		public override void Reset()
		{
			delay = null;
		}

		public override void OnEnter()
		{
		    if (reset.Value)
		    {
		        eventIndex = 0;
		        reset.Value = false;
		    }

			var eventCount = State.Transitions.Length;

			if (eventCount > 0)
			{
				var fsmEvent = State.Transitions[eventIndex].FsmEvent;
				
				if (delay.Value < 0.001f)
				{
					Fsm.Event(fsmEvent);
					Finish();
				}
				else
				{
					delayedEvent = Fsm.DelayedEvent(fsmEvent, delay.Value);
				}
				
				eventIndex++;
				if (eventIndex == eventCount)
				{
					eventIndex = 0;
				}
			}
		}

		public override void OnUpdate()
		{
			if (DelayedEvent.WasSent(delayedEvent))
			{
				Finish();
			}
		}
	}
}