﻿using UnityEngine;
using UnityEngine.Scripting;
using System;

namespace HutongGames.PlayMaker
{
    [Preserve]
	public class FsmProcessor
    {
        // TODO: Add callback for custom user processing?
        //public static Action<PlayMakerFSM> onPreprocess;

	    public static void OnPreprocess(PlayMakerFSM fsm)
	    {
            //Debug.Log("OnPreprocess");

            // Most Event Handlers are added by PlayMakerFSM
	        // However, we can move them here if they need to be stripped from dll
	        // E.g., for build size or because the system becomes obsolete
            
	        // Legacy Networking

            if (fsm.Fsm.HandleLegacyNetworking)
            {
                if (!AddEventHandlerComponent(fsm, ReflectionUtils.GetGlobalType("PlayMakerLegacyNetworking")))
                {
                    Debug.LogError("Could not add PlayMakerLegacyNetworking proxy!");
                }
            }

#if !PLAYMAKER_NO_UI

            if (fsm.Fsm.HandleUiEvents != UiEvents.None)
	        {
	            HandleUiEvent<PlayMakerUiClickEvent>(fsm, UiEvents.Click);
                HandleUiEvent<PlayMakerUiDragEvents>(fsm, UiEvents.DragEvents );
	            HandleUiEvent<PlayMakerUiDropEvent>(fsm, UiEvents.Drop);
                HandleUiEvent<PlayMakerUiPointerEvents>(fsm, UiEvents.PointerEvents);
                HandleUiEvent<PlayMakerUiBoolValueChangedEvent>(fsm, UiEvents.BoolValueChanged);
                HandleUiEvent<PlayMakerUiFloatValueChangedEvent>(fsm, UiEvents.FloatValueChanged);
                HandleUiEvent<PlayMakerUiIntValueChangedEvent>(fsm, UiEvents.IntValueChanged);
                HandleUiEvent<PlayMakerUiVector2ValueChangedEvent>(fsm, UiEvents.Vector2ValueChanged);
                HandleUiEvent<PlayMakerUiEndEditEvent>(fsm, UiEvents.EndEdit);
	        }
#endif

            //if (onPreprocess != null)
            //    onPreprocess(fsm);
        }

#if !PLAYMAKER_NO_UI

        private static void HandleUiEvent<T>(PlayMakerFSM fsm, UiEvents uiEvent) where T : PlayMakerUiEventBase 
        {
            if ((fsm.Fsm.HandleUiEvents & uiEvent) != 0)
            {
                AddUiEventHandler<T>(fsm);
            }
        }

        private static void AddUiEventHandler<T>(PlayMakerFSM fsm) where T : PlayMakerUiEventBase 
        {
            var handler = fsm.GetComponent<T>();
            if (handler == null)
            {
                handler = fsm.gameObject.AddComponent<T>();
                if (!PlayMakerPrefs.ShowEventHandlerComponents)
                {
                    handler.hideFlags = HideFlags.HideInInspector;
                }
            }

            handler.AddTargetFsm(fsm);
        }

#endif

        private static bool AddEventHandlerComponent(PlayMakerFSM fsm, Type type)
	    {
	        if (type == null) return false;

	        var proxy = GetEventHandlerComponent(fsm.gameObject, type);
	        if (proxy == null) return false;

	        proxy.AddTarget(fsm);
            //proxy.PreProcess();

	        if (!PlayMakerGlobals.IsEditor)
	        {
	            // Log so we can track down cases where Preprocess is not called
	            if (PlayMakerPrefs.LogPerformanceWarnings)
	            {
	                Debug.Log("AddEventHandlerComponent: " + type.FullName);
	            }            
	        }

	        return true;
	    }

	    public static PlayMakerProxyBase GetEventHandlerComponent(GameObject go, Type type)
	    {
	        if (go == null) return null;

	        var proxy = go.GetComponent(type);
	        if (proxy == null)
	        {
	            proxy = go.AddComponent(type);
	            if (!PlayMakerPrefs.ShowEventHandlerComponents)
	            {
	                proxy.hideFlags = HideFlags.HideInInspector;
	            }
	        }
	        return proxy as PlayMakerProxyBase;
	    }
	}
}