// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Gets info on the last RaycastAll and store in array variables.")]
	public class GetRaycastAllInfo : FsmStateAction
	{

		[Tooltip("Store the GameObjects hit in an array variable.")]
		[UIHint(UIHint.Variable)]
		[ArrayEditor(VariableType.GameObject)]
		public FsmArray storeHitObjects;
		
		[Tooltip("Get the world position of all ray hit point and store them in an array variable.")]
		[UIHint(UIHint.Variable)]
		[ArrayEditor(VariableType.Vector3)]
		public FsmArray points;
		
		[Tooltip("Get the normal at all hit points and store them in an array variable.")]
		[UIHint(UIHint.Variable)]
		[ArrayEditor(VariableType.Vector3)]
		public FsmArray normals;

		[Tooltip("Get the distance along the ray to all hit points and store them in an array variable.")]
		[UIHint(UIHint.Variable)]
		[ArrayEditor(VariableType.Float)]
		public FsmArray distances;

        [Tooltip("Repeat every frame. Warning, this could be affecting performances")]
	    public bool everyFrame;

		public override void Reset()
		{
			storeHitObjects = null;
			points = null;
			normals = null;
			distances = null;
		    everyFrame = false;
		}

		void StoreRaycastAllInfo()
		{
			if (RaycastAll.RaycastAllHitInfo == null)
			{
				return;
			}

			storeHitObjects.Resize(RaycastAll.RaycastAllHitInfo.Length);
			points.Resize(RaycastAll.RaycastAllHitInfo.Length);
			normals.Resize(RaycastAll.RaycastAllHitInfo.Length);
			distances.Resize(RaycastAll.RaycastAllHitInfo.Length);

			for (int i = 0; i < RaycastAll.RaycastAllHitInfo.Length; i++)
			{
				storeHitObjects.Values[i] = RaycastAll.RaycastAllHitInfo[i].collider.gameObject;

				points.Values[i] =  RaycastAll.RaycastAllHitInfo[i].point;
				normals.Values[i] =  RaycastAll.RaycastAllHitInfo[i].normal;
				distances.Values[i] =  RaycastAll.RaycastAllHitInfo[i].distance;
			}
		}

		public override void OnEnter()
		{
			StoreRaycastAllInfo();
			
            if (!everyFrame)
            {
                Finish();
            }
		}

        public override void OnUpdate()
        {
			StoreRaycastAllInfo();
        }
	}
}