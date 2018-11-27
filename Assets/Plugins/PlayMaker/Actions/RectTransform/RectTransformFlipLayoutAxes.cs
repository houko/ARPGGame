// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
 
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("RectTransform")]
	[Tooltip("Flips the horizontal and vertical axes of the RectTransform size and alignment, and optionally its children as well.")]
	public class RectTransformFlipLayoutAxis : FsmStateAction
	{

		public enum RectTransformFlipOptions {Horizontal,Vertical,Both};


		[RequiredField]
		[CheckForComponent(typeof(RectTransform))]
		[Tooltip("The GameObject target.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("The axis to flip")]
		public RectTransformFlipOptions axis;

		[Tooltip("Flips around the pivot if true. Flips within the parent rect if false.")]
		public FsmBool keepPositioning;

		[Tooltip("Flip the children as well?")]
		public FsmBool recursive;


		public override void Reset()
		{
			gameObject = null;
			axis = RectTransformFlipOptions.Both;
			keepPositioning = null;
			recursive = null;
		}
		
		public override void OnEnter()
		{

			DoFlip();

			Finish();
				
		}

		void DoFlip()
		{

			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go != null)
			{
				RectTransform _rt = go.GetComponent<RectTransform>();
				if (_rt!=null)
				{
					if (axis== RectTransformFlipOptions.Both)
					{
					RectTransformUtility.FlipLayoutAxes(_rt,keepPositioning.Value,recursive.Value);
					}else if (axis== RectTransformFlipOptions.Horizontal)
					{
						RectTransformUtility.FlipLayoutOnAxis(_rt,0,keepPositioning.Value,recursive.Value);
					}else if (axis== RectTransformFlipOptions.Vertical)
					{
						RectTransformUtility.FlipLayoutOnAxis(_rt,1,keepPositioning.Value,recursive.Value);
					}
				}
			}

		}
		
		
	}
}