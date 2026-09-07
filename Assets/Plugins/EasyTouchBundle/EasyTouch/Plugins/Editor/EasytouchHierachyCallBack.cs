// EasyTouch library is copyright (c) of Hedgehog Team
// Please send feedback or bug reports to the.hedgehog.team@gmail.com

using UnityEngine;
using UnityEditor;
using System;
using HedgehogTeam.EasyTouch;

[InitializeOnLoad]
public class EasytouchHierachyCallBack{
	#if UNITY_6000_4_OR_NEWER
	private static readonly EditorApplication.HierarchyWindowItemByEntityIdCallback hiearchyItemCallback;
	#else
	private static readonly EditorApplication.HierarchyWindowItemCallback hiearchyItemCallback;
	#endif
	private static Texture2D hierarchyIcon;
	private static Texture2D HierarchyIcon {
		get {
			if (EasytouchHierachyCallBack.hierarchyIcon==null){
				EasytouchHierachyCallBack.hierarchyIcon = (Texture2D)Resources.Load( "EasyTouch_Icon");
			}
			return EasytouchHierachyCallBack.hierarchyIcon;
		}
	}	

	private static Texture2D hierarchyEventIcon;
	private static Texture2D HierarchyEventIcon {
		get {
			if (EasytouchHierachyCallBack.hierarchyEventIcon==null){
				EasytouchHierachyCallBack.hierarchyEventIcon = (Texture2D)Resources.Load( "EasyTouchTrigger_Icon");
			}
			return EasytouchHierachyCallBack.hierarchyEventIcon;
		}
	}


	// constructor
	static EasytouchHierachyCallBack()
	{
		#if UNITY_6000_4_OR_NEWER
		EasytouchHierachyCallBack.hiearchyItemCallback = new EditorApplication.HierarchyWindowItemByEntityIdCallback(EasytouchHierachyCallBack.DrawHierarchyIcon);
		EditorApplication.hierarchyWindowItemByEntityIdOnGUI += EasytouchHierachyCallBack.hiearchyItemCallback;
		#else
		EasytouchHierachyCallBack.hiearchyItemCallback = new EditorApplication.HierarchyWindowItemCallback(EasytouchHierachyCallBack.DrawHierarchyIcon);
		EditorApplication.hierarchyWindowItemOnGUI = (EditorApplication.HierarchyWindowItemCallback)Delegate.Combine(EditorApplication.hierarchyWindowItemOnGUI, EasytouchHierachyCallBack.hiearchyItemCallback);
		#endif
		
	}
	#if UNITY_6000_3_OR_NEWER
	private static void DrawHierarchyIcon(UnityEngine.EntityId entityId, Rect selectionRect)
	{
		GameObject gameObject = EditorUtility.EntityIdToObject(entityId) as GameObject;
	#else
	private static void DrawHierarchyIcon(int instanceID, Rect selectionRect)
	{
		GameObject gameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
	#endif

		if (gameObject != null){
			Rect rect = new Rect(selectionRect.x + selectionRect.width - 16f, selectionRect.y, 16f, 16f);
			if ( gameObject.GetComponent<EasyTouch>() != null){
				GUI.DrawTexture( rect,EasytouchHierachyCallBack.HierarchyIcon);
			}
			else if (gameObject.GetComponent<QuickBase>() != null){
				GUI.DrawTexture( rect,EasytouchHierachyCallBack.HierarchyEventIcon);
			}
#if FALSE
			else if (gameObject.GetComponent<EasyTouchSceneProxy>() != null){
				GUI.DrawTexture( rect,EasytouchHierachyCallBack.HierarchyIcon);
			}
#endif
		}
	}
		
}
