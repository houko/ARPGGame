// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using System;
using PlayMaker.ConditionalExpression;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Debug)]
	[Tooltip("Checks if the conditional expression Is True or Is False. Breaks the execution of the game if the assertion fails.\n" +
	         "This is a useful way to check your assumptions. If you expect a certain value use an Assert to make sure!\n" +
	         "Only runs in Editor.")]
	public class Assert : FsmStateAction, IEvaluatorContext 
    {
        public enum AssertType
        {
            IsTrue,
            IsFalse
        }

		#region Inputs
		
		public CompiledAst Ast { get; set; }
		public string LastErrorMessage { get; set; }

        [UIHint(UIHint.TextArea)]
        [Tooltip("Enter an expression to evaluate.\nExample: (a < b) && c\n$(variable name with spaces)")]
		public FsmString expression;

        [Tooltip("Expected result of the expression.")]
        public AssertType assert;

        [Tooltip("Repeat every frame while the state is active.")]
		public bool everyFrame;

		#endregion

	    private string cachedExpression;

		#region IEvaluatorContext implementation
		
		/// <inheritdoc/>
		FsmVar IEvaluatorContext.GetVariable(string name) 
        {
			var v = Fsm.Variables.GetVariable(name);
			if (v != null)
			{
			    return new FsmVar(v);
			}
			
			throw new VariableNotFoundException(name);
		}

        #endregion

#if UNITY_EDITOR

        public override void Reset() 
        {
		    expression = null;
			
			Ast = null;
			
			everyFrame = false;
		}
		
		public override void Awake() 
        {
			try 
            {
				LastErrorMessage = "";			
				CompileExpressionIfNeeded();
			}
			catch (Exception ex) 
            {
				LastErrorMessage = ex.Message;
			}
		}
		
		public override void OnEnter()
        {
			DoAction();
			
			if (!everyFrame)
			{
			    Finish();
			}
		}
		
		public override void OnUpdate() 
        {
			DoAction();
		}
		
		private void DoAction()
		{
		    if (expression == null || string.IsNullOrEmpty(expression.Value)) return;

			try 
            {
				LastErrorMessage = "";				
				CompileExpressionIfNeeded();
				
				var result = Ast.Evaluate();
                var failed = false;

                if (assert == AssertType.IsTrue)
                {
                    if (!result) failed = true;
                }
                else if (assert == AssertType.IsFalse)
                {
                    if (result) failed = true;
                }

                if (failed)
                {
                    LogError("Failed: " + expression.Value);
                    //Fsm.DoBreakError("Assert Failed: " + expression.Value);
                }
            }
			catch (Exception ex) 
            {
				LastErrorMessage = ex.Message;
			}
		}


	    private void CompileExpressionIfNeeded()
	    {
            if (Ast == null || cachedExpression != expression.Value)
            {
                Ast = CompiledAst.Compile(expression.Value, this);
                cachedExpression = expression.Value;
            }
	    }
#endif
	    
	}

}