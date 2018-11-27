// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Debug)]
	[Tooltip("Logs the value of a Vector2 Variable in the PlayMaker Log Window.")]
	public class DebugVector2 : FsmStateAction
	{
        [Tooltip("Info, Warning, or Error.")]
		public LogLevel logLevel;

		[UIHint(UIHint.Variable)]
        [Tooltip("Prints the value of a Vector2 variable in the PlayMaker log window.")]
		public FsmVector2 vector2Variable;

		public override void Reset()
		{
			logLevel = LogLevel.Info;
			vector2Variable = null;
		}

		public override void OnEnter()
		{
			string text = "None";
			
			if (!vector2Variable.IsNone)
			{
				text = vector2Variable.Name + ": " + vector2Variable.Value;
			}

			ActionHelpers.DebugLog(Fsm, logLevel, text);

			Finish();
		}
	}
}