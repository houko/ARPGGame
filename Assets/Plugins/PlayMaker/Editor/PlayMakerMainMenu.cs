// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using System.ComponentModel;
using HutongGames.PlayMakerEditor;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[Localizable(false)]
internal static class PlayMakerMainMenu
{
    // Change MenuRoot to move the Playmaker Menu
    // E.g., MenuRoot = "Plugins/PlayMaker/"
    private const string MenuRoot = "PlayMaker/"; 

	[MenuItem(MenuRoot + "PlayMaker Editor", false, 0)]
	public static void OpenFsmEditor()
	{
		FsmEditorWindow.OpenWindow();
	    if (EditorStartupPrefs.ShowWelcomeScreen)
	    {
	        PlayMakerWelcomeWindow.Open();
	    }
	}

	#region EDITOR WINDOWS 

    private const string editorsRoot = MenuRoot + "Editor Windows/";
    private const int iEditors = 1; //10;

    [MenuItem(editorsRoot + "Action Browser", true)]
    public static bool ValidateOpenActionWindow()
    {
        return FsmEditorWindow.IsOpen();
    }

    [MenuItem(editorsRoot + "Action Browser", false, iEditors)]
    public static void OpenActionWindow()
    {
        FsmEditor.OpenActionWindow();
    }

    [MenuItem(editorsRoot + "State Browser", true)]
    public static bool ValidateOpenStateSelectorWindow()
    {
        return FsmEditorWindow.IsOpen();
    }

    [MenuItem(editorsRoot + "State Browser", false, iEditors + 1)]
    public static void OpenStateSelectorWindow()
    {
        FsmEditor.OpenStateSelectorWindow();
    }

    [MenuItem(editorsRoot + "FSM Browser", true)]
	public static bool ValidateOpenFsmSelectorWindow()
	{
		return FsmEditorWindow.IsOpen();
	}

	[MenuItem(editorsRoot + "FSM Browser", false, iEditors + 2)]
	public static void OpenFsmSelectorWindow()
	{
		FsmEditor.OpenFsmSelectorWindow();
	}

	[MenuItem(editorsRoot + "Templates Browser", true)]
	public static bool ValidateOpenFsmTemplateWindow()
	{
		return FsmEditorWindow.IsOpen();
	}

    [MenuItem(editorsRoot + "Templates Browser", false, iEditors + 3)]
	public static void OpenFsmTemplateWindow()
	{
		FsmEditor.OpenFsmTemplateWindow();
	}

    [MenuItem(editorsRoot + "Event Browser", true)]
    public static bool ValidateOpenGlobalEventsWindow()
    {
        return FsmEditorWindow.IsOpen();
    }

    [MenuItem(editorsRoot + "Event Browser", false, iEditors + 4)]
    public static void OpenGlobalEventsWindow()
    {
        FsmEditor.OpenGlobalEventsWindow();
    }

	[MenuItem(editorsRoot + "Global Variables", true)]
	public static bool ValidateOpenGlobalVariablesWindow()
	{
		return FsmEditorWindow.IsOpen();
	}

    [MenuItem(editorsRoot + "Global Variables", false, iEditors + 5)]
	public static void OpenGlobalVariablesWindow()
	{
		FsmEditor.OpenGlobalVariablesWindow();
	}

    [MenuItem(editorsRoot + "Edit Tools", true)]
    public static bool ValidateOpenToolWindow()
    {
        return FsmEditorWindow.IsOpen();
    }

    [MenuItem(editorsRoot + "Edit Tools", false, iEditors + 6)]
    public static void OpenToolWindow()
    {
        FsmEditor.OpenToolWindow();
    }

    // -----------------------------------------

    [MenuItem(editorsRoot + "Timeline Log", true)]
    public static bool ValidateOpenTimelineWindow()
    {
        return FsmEditorWindow.IsOpen();
    }

    [MenuItem(editorsRoot + "Timeline Log", false, iEditors + 17)]
    public static void OpenTimelineWindow()
    {
        FsmEditor.OpenTimelineWindow();
    }

	[MenuItem(editorsRoot + "FSM Log", true)]
	public static bool ValidateOpenFsmLogWindow()
	{
		return FsmEditorWindow.IsOpen();
	}

    [MenuItem(editorsRoot + "FSM Log", false, iEditors + 18)]
	public static void OpenFsmLogWindow()
	{
		FsmEditor.OpenFsmLogWindow();
	}

	[MenuItem(editorsRoot + "Editor Log", true)]
	public static bool ValidateOpenReportWindow()
	{
		return FsmEditorWindow.IsOpen();
	}

    [MenuItem(editorsRoot + "Editor Log", false, iEditors + 29)]
	public static void OpenReportWindow()
	{
		FsmEditor.OpenReportWindow();
	}

/* Enable when window is implemeneted
    [MenuItem(editorsRoot + "Search", true)]
    public static bool ValidateOpenSearchWindow()
    {
        return FsmEditorWindow.IsOpen();
    }

    [MenuItem(editorsRoot + "Search", false, 19)]
    public static void OpenSearchWindow()
    {
        FsmEditor.OpenSearchWindow();
    }
*/

	#endregion

	#region COMPONENTS

    private const int iComponents = 1;  // iEditors + 10;

	[MenuItem(MenuRoot + "Components/Add FSM To Selected Objects", true)]
	public static bool ValidateAddFsmToSelected()
	{
		return Selection.activeGameObject != null;
	}

	[MenuItem(MenuRoot + "Components/Add FSM To Selected Objects", false, iComponents)]
	public static void AddFsmToSelected()
	{
		FsmBuilder.AddFsmToSelected();
		//PlayMakerFSM playmakerFSM = Selection.activeGameObject.AddComponent<PlayMakerFSM>();
		//FsmEditor.SelectFsm(playmakerFSM.Fsm);
	}

	[MenuItem(MenuRoot + "Components/Add PlayMakerGUI to Scene", true)]
	public static bool ValidateAddPlayMakerGUI()
	{
		return (Object.FindObjectOfType(typeof(PlayMakerGUI)) as PlayMakerGUI) == null;
	}

    [MenuItem(MenuRoot + "Components/Add PlayMakerGUI to Scene", false, iComponents + 1)]
	public static void AddPlayMakerGUI()
	{
		PlayMakerGUI.Instance.enabled = true;
	}

	#endregion

	#region TOOLS

    private const string toolsRoot = MenuRoot + "Tools/";
    private const int iTools = iComponents;// + 10;

    [MenuItem(toolsRoot + "Export Globals", false, iTools)]
    public static void ExportGlobals()
    {
        GlobalsAsset.Export();
    }

    [MenuItem(toolsRoot + "Import Globals", false, iTools + 1)]
    public static void ImportGlobals()
    {
        GlobalsAsset.Import();
    }

    [MenuItem(toolsRoot + "Custom Action Wizard", false, iTools + 12)]
    public static void CreateWizard()
    {
        EditorWindow.GetWindow<PlayMakerCustomActionWizard>(true);
    }

    [MenuItem(toolsRoot + "Documentation Helpers", false, iTools + 13)]
    public static void DocHelpers()
    {
        EditorWindow.GetWindow<PlayMakerDocHelpers>(true);
    }

    /* In PlayMakerProjectTools.cs
    [MenuItem(toolsRoot + "Update All Loaded FSMs", false, iTools + 24)]
    public static void UpdateAllLoadedFSMs()
    {
        ProjectTools.ReSaveAllLoadedFSMs();
    }

    [MenuItem(toolsRoot + "Update All FSMs in Build", false, iTools + 25)]
    public static void UpdateAllFSMsInBuild()
    {
        ProjectTools.UpdateScenesInBuild();
    }*/

    [MenuItem(toolsRoot + "Load All PlayMaker Prefabs", false, iTools + 25)]
    public static void LoadAllPrefabsInProject()
    {
        var paths = Files.LoadAllPlaymakerPrefabs();

        if (paths.Count == 0)
        {
            EditorUtility.DisplayDialog("Loading PlayMaker Prefabs", "No PlayMaker Prefabs Found!", "OK");
        }
        else
        {
            EditorUtility.DisplayDialog("Loaded PlayMaker Prefabs", "Prefabs found: " + paths.Count + "\nCheck console for details...", "OK");
        }
    }

    [MenuItem(toolsRoot + "Submit Bug Report", false,  iTools + 86)]
    public static void SubmitBug()
    {
        EditorWindow.GetWindow<PlayMakerBugReportWindow>(true);
    }

#if UNITY_5_0 || UNITY_5
    [MenuItem(toolsRoot + "Post-Update Check", false, 67)]
    public static void RunAutoUpdater()
    {
        PlayMakerAutoUpdater.OpenAutoUpdater();
    }
#endif

	#endregion

	#region HELP

    private const string helpRoot = MenuRoot + "Help/";
    private const int iHelp = 1; // iTools + 100;

    [MenuItem(helpRoot + "Guided Tour", false, iHelp)]
    public static void GuidedTour()
    {
        PlayMakerGuidedTour.Open();
    }

    [MenuItem(helpRoot + "Online Manual", false, iHelp + 1)]
	public static void OnlineManual()
	{
		EditorCommands.OpenWikiHelp();
	}

    [MenuItem(helpRoot + "YouTube Channel", false, iHelp + 2)]
	public static void YouTubeChannel()
	{
		Application.OpenURL("http://www.youtube.com/user/HutongGamesLLC");
	}

    [MenuItem(helpRoot + "PlayMaker Forums", false, iHelp + 3)]
	public static void PlayMakerForum()
	{
		Application.OpenURL("http://hutonggames.com/playmakerforum/");
	}

    /*
    [MenuItem(helpRoot + "Check For Updates", false, iHelp + 3)]
    public static void CheckForUpdates()
    {
        AssetStore.Open("content/368");
    }*/


    [MenuItem(helpRoot + "Submit Bug Report", false,  iHelp + 20)]
    public static void SubmitBugFromHelp()
    {
        EditorWindow.GetWindow<PlayMakerBugReportWindow>(true);
    }

    [MenuItem(helpRoot + "About PlayMaker...", false, iHelp + 40)]
    public static void OpenAboutWindow()
    {
        EditorWindow.GetWindow<AboutWindow>(true);
    }

	#endregion

    // PlayMakerWelcomeWindow.cs
    //[MenuItem("PlayMaker/Welcome Screen", false, 1000)]

    #region ADDONS

    private const string addonsRoot = MenuRoot + "Addons/";
    private const int iAddons = 1000;

    [MenuItem(addonsRoot + "Download Addons", false, iAddons)]
    public static void OpenAddonsWiki()
    {
        Application.OpenURL("https://hutonggames.fogbugz.com/default.asp?W714");
    }

#if !(UNITY_5 || UNITY_5_0 || UNITY_5_3_OR_NEWER) 

    [MenuItem(addonsRoot + "Windows Phone 8 Addon", false, iAddons + 1)]
    public static void GetWindowsPhone8Addon()
    {
        AssetStore.Open("content/10602");
    }

#endif
    

    #endregion

    
}
