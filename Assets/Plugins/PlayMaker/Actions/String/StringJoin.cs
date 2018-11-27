namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.String)]
    [Tooltip("Join an array of strings into a single string.")]
    public class StringJoin : FsmStateAction
    {
        [UIHint(UIHint.Variable)]
        [ArrayEditor(VariableType.String)]
        [Tooltip("Array of string to join into a single string.")]
        public FsmArray stringArray;

        [Tooltip("Separator to add between each string.")]
        public FsmString separator;

        [UIHint(UIHint.Variable)]
        [Tooltip("Store the joined string in string variable.")]
        public FsmString storeResult;

        public override void OnEnter()
	    {
            if (!stringArray.IsNone && !storeResult.IsNone)
            {
                storeResult.Value = string.Join(separator.Value, stringArray.stringValues);
            }

            Finish();
	    }
    }
}
