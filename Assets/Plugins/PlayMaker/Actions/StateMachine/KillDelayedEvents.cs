// (c) copyright Hutong Games, LLC 2010-2012. All rights reserved.

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.StateMachine)]
    [Note("Kill all queued delayed events.")]
    [Tooltip("Kill all queued delayed events. " +
             "Normally delayed events are automatically killed when the active state is exited, " +
             "but you can override this behaviour in FSM settings. " +
             "If you choose to keep delayed events you can use this action to kill them when needed.")]
    public class KillDelayedEvents : FsmStateAction
    {
        public override void OnEnter()
        {
            Fsm.KillDelayedEvents();           
            Finish();
        }
    }
}
