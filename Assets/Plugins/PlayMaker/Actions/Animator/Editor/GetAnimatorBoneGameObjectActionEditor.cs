// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;
using UnityEditor;
using UnityEngine;

namespace HutongGames.PlayMakerEditor
{
	[CustomActionEditor(typeof(GetAnimatorBoneGameObject))]
	public class GetAnimatorBoneGameObjectActionEditor : CustomActionEditor
	{
		#region implemented abstract members of CustomActionEditor
		public override bool OnGUI ()
		{
			return DrawDefaultInspector();
		}
		#endregion
		

		
	}
}