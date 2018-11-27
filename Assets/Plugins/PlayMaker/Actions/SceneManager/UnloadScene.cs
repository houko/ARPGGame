// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

#if UNITY_5_3 || UNITY_5_3_OR_NEWER

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HutongGames.PlayMaker.Actions
{

	#pragma warning disable 162

	#if UNITY_5_5_OR_NEWER
	[Obsolete("Use UnloadSceneAsynch Instead")]
	#endif
	[ActionCategory(ActionCategory.Scene)]
	[Tooltip("Unload Scene. Note that assets are currently not unloaded, in order to free up asset memory call Resources.UnloadUnusedAssets.")]
	public class UnloadScene : FsmStateAction
	{
		public enum SceneReferenceOptions {ActiveScene,SceneAtBuildIndex,SceneAtIndex,SceneByName,SceneByPath,SceneByGameObject};

		[Tooltip("The reference options of the Scene")]
		public SceneReferenceOptions sceneReference;

		[Tooltip("The name of the scene to load. The given sceneName can either be the last part of the path, without .unity extension or the full path still without the .unity extension")]
		public FsmString sceneByName;

		[Tooltip("The build index of the scene to unload.")]
		public FsmInt sceneAtBuildIndex;

		[Tooltip("The index of the scene to unload.")]
		public FsmInt sceneAtIndex;

		[Tooltip("The scene Path.")]
		public FsmString sceneByPath;

		[Tooltip("The GameObject unload scene of")]
		public FsmOwnerDefault sceneByGameObject;


		[ActionSection("Result")]

		[Tooltip("True if scene was unloaded")]
		[UIHint(UIHint.Variable)]
		public FsmBool unloaded;

		[Tooltip("Event sent if scene was unloaded ")]
		public FsmEvent unloadedEvent;

		[Tooltip("Event sent scene was not unloaded")]
		[UIHint(UIHint.Variable)]
		public FsmEvent failureEvent;


		public override void Reset()
		{
			sceneReference = SceneReferenceOptions.SceneAtBuildIndex;
			sceneByName = null;
			sceneAtBuildIndex = null;
			sceneAtIndex = null;
			sceneByPath = null;
			sceneByGameObject = null;

			unloaded = null;
			unloadedEvent = null;
			failureEvent = null;
		}

		public override void OnEnter()
		{
			bool _unloaded = false;

			try{
				switch (sceneReference) {
				case SceneReferenceOptions.ActiveScene:
					#if UNITY_5_4_OR_NEWER
						_unloaded = SceneManager.UnloadScene(SceneManager.GetActiveScene());
					#else
						LogError("SceneReferenceOptions.ActiveScene not available in this version of Unity");
					#endif
					break;
				case SceneReferenceOptions.SceneAtBuildIndex:
					_unloaded = SceneManager.UnloadScene(sceneAtBuildIndex.Value);
					break;
				case SceneReferenceOptions.SceneAtIndex:
					#if UNITY_5_4_OR_NEWER
						_unloaded = SceneManager.UnloadScene(SceneManager.GetSceneAt(sceneAtIndex.Value));
					#else
						LogError("SceneReferenceOptions.SceneAtIndex not available in this version of Unity");
					#endif
					break;
				case SceneReferenceOptions.SceneByName:
					_unloaded = SceneManager.UnloadScene (sceneByName.Value);
					break;
				case SceneReferenceOptions.SceneByPath:
					#if UNITY_5_4_OR_NEWER
						_unloaded = SceneManager.UnloadScene(SceneManager.GetSceneByPath(sceneByPath.Value));
					#else
						LogError("SceneReferenceOptions.SceneByPath not available in this version of Unity");
					#endif	
					break;
				case SceneReferenceOptions.SceneByGameObject:
					#if UNITY_5_4_OR_NEWER
						GameObject _go = Fsm.GetOwnerDefaultTarget (sceneByGameObject);
						if (_go==null)
						{
							throw new  Exception ("Null GameObject");
						}else{
							_unloaded = SceneManager.UnloadScene(_go.scene);
						}
					#else
						LogError("SceneReferenceOptions.SceneByGameObject not available in this version of Unity");
					#endif	

					break;
				}
			}catch(Exception e)
			{
				LogError(e.Message);			
			}
							
			if (!unloaded.IsNone)
				unloaded.Value = _unloaded;

			if (_unloaded) {
				Fsm.Event (unloadedEvent);
			} else {
				Fsm.Event (failureEvent);
			}

			Finish();
		}

		public override string ErrorCheck()
		{
			switch (sceneReference) {
			case SceneReferenceOptions.ActiveScene:
				#if ! UNITY_5_4_OR_NEWER			
					return "ActiveScene not available in this version of Unity";
				#endif
				break;
			case SceneReferenceOptions.SceneAtIndex:
				#if ! UNITY_5_4_OR_NEWER			
					return "SceneAtIndex not available in this version of Unity";
				#endif
				break;
			case SceneReferenceOptions.SceneByPath:
				#if ! UNITY_5_4_OR_NEWER			
					return "SceneByPath not available in this version of Unity";
				#endif
				break;
			case SceneReferenceOptions.SceneByGameObject:
				#if ! UNITY_5_4_OR_NEWER			
					return "SceneByGameObject not available in this version of Unity";
				#endif
				break;
			}

			return string.Empty;
		}
	}
}

#endif