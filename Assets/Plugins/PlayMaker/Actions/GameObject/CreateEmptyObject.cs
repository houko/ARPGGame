// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Creates a Game Object at a spawn point.\nUse a Game Object and/or Position/Rotation for the Spawn Point. If you specify a Game Object, Position is used as a local offset, and Rotation will override the object's rotation.")]
	public class CreateEmptyObject : FsmStateAction
	{

		[Tooltip("Optional GameObject to create. Usually a Prefab.")]
		public FsmGameObject gameObject;

		[Tooltip("Optional Spawn Point.")]
		public FsmGameObject spawnPoint;

		[Tooltip("Position. If a Spawn Point is defined, this is used as a local offset from the Spawn Point position.")]
		public FsmVector3 position;

		[Tooltip("Rotation. NOTE: Overrides the rotation of the Spawn Point.")]
		public FsmVector3 rotation;

		[UIHint(UIHint.Variable)]
		[Tooltip("Optionally store the created object.")]
		public FsmGameObject storeObject;

		public override void Reset()
		{
			gameObject = null;
			spawnPoint = null;
			position = new FsmVector3 { UseVariable = true };
			rotation = new FsmVector3 { UseVariable = true };
			storeObject = null;
		}

		public override void OnEnter()
		{
			var go = gameObject.Value;

			Vector3 spawnPosition = Vector3.zero;
			Vector3 spawnRotation = Vector3.zero;
			
			if (spawnPoint.Value != null)
			{
				spawnPosition = spawnPoint.Value.transform.position;
				if (!position.IsNone)
					spawnPosition += position.Value;
				
				if (!rotation.IsNone)
					spawnRotation = rotation.Value;
				else
					spawnRotation = spawnPoint.Value.transform.eulerAngles;
			}
			else
			{
				if (!position.IsNone)
					spawnPosition = position.Value;
				
				if (!rotation.IsNone)
					spawnRotation = rotation.Value;
			}
				
			var newObject = storeObject.Value;
			
			if (go != null)
			{
				newObject = (GameObject)Object.Instantiate(go);
				storeObject.Value = newObject;
			}
			else
			{
				newObject = new GameObject( "EmptyObjectFromNull" );
				storeObject.Value = newObject;
			}
			if (newObject != null)			
			{
				newObject.transform.position = spawnPosition;
				newObject.transform.eulerAngles = spawnRotation;
			}
			Finish();
		}

	}
}