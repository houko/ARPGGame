﻿using UnityEditor;
using UnityEngine;

namespace HutongGames.PlayMakerEditor
{
    /// <summary>
    /// Manages optional add-ons for Playmaker
    /// Used by PlayMakerWelcomeWindow
    /// </summary>
	public class PlayMakerAddonManager
    {
        private const string urlAssetStoreRoot = "https://www.assetstore.unity3d.com/#!/content/";
	    private const string urlPhotonAddon = "https://hutonggames.fogbugz.com/default.asp?W928";
	    private const string urlAddonsWiki = "https://hutonggames.fogbugz.com/default.asp?W714";
	    private const string urlEcosystemWiki = "https://hutonggames.fogbugz.com/default.asp?W1181";
	    private const string urlLegacyNetworkDocs = "https://hutonggames.fogbugz.com/default.asp?W852";
        private const string urlLegacyGUIDocs = "https://docs.unity3d.com/Manual/GUIReference.html";
        private const string urlITweenDocs = "https://hutonggames.fogbugz.com/default.asp?W544";

	    private static bool setupPhoton;
	    private static Texture photonIcon;

        public static void ResetView()
        {
            EditorPrefs.SetFloat("PlayMaker.AddonManagerScroll",0);
            addonsScroll.y = 0;
        }

	    public static void Init()
	    {
	        // Is PlayMakerPhotonWizard available?
	        setupPhoton = PlayMakerEditorStartup.GetType("PlayMakerPhotonWizard") != null;
	        photonIcon = (Texture) Resources.Load("photonIcon");

	        addonsScroll.y = EditorPrefs.GetFloat("PlayMaker.AddonManagerScroll",0);
	    }

	    private static Vector2 addonsScroll;

	    public static void OnGUI()
	    {
	        addonsScroll = EditorGUILayout.BeginScrollView(addonsScroll);

	        const float margin = 60;

            GUILayout.BeginHorizontal();
            GUILayout.Space(margin);

            GUILayout.BeginVertical();
            DoOfficialAddons();            
            DoThirdPartyAddons();
            DoLegacyAddons();
            GUILayout.EndVertical();

            GUILayout.Space(margin);
            GUILayout.EndHorizontal();

	        EditorGUILayout.EndScrollView();
	    }

	    private static void DoSectionTitle(string title)
	    {
	        GUILayout.Label(title, EditorStyles.boldLabel);
	    }

	    private static void DoTopSpacer()
	    {
	        GUILayout.Space(10);
	    }

	    private static void DoBottomSpacer()
	    {
	        GUILayout.Space(20);
	    }

	    private static void DoOfficialAddons()
	    {
	        DoSectionTitle("Official Add-ons");

	        //EditorGUILayout.HelpBox("These Add-ons and more are available on the PlayMaker Wiki." +
            //                        "\nThey are developed and supported by Hutong Games.", MessageType.Info);

	        DoTopSpacer();

	        AddonDownload("Ecosystem Browser",
	            "An integrated online browser for custom actions, samples and add-ons.",
	            urlEcosystemWiki);

	        AddonDownload("Add-ons Wiki",
	            "Official add-ons available online.",
	            urlAddonsWiki);

	        DoBottomSpacer();
	    }

	    private static void DoThirdPartyAddons()
	    {
	        DoSectionTitle("Third Party Assets");
	        EditorGUILayout.HelpBox("The best assets with PlayMaker support!" +
	                                "\nPlease direct any questions to the developer of the asset.", MessageType.Info);

	        DoTopSpacer();

	        const string photonTitle = "Photon Cloud";
	        const string photonTooltip = "Build scalable MMOGs, FPS or any other multiplayer game " +
	                                     "and application for PC, Mac, Browser, Mobile or Console.";
	        if (setupPhoton)
	        {
                PhotonSetupWizard(photonTitle, photonTooltip);
	        }
	        else
	        {
	            AddonDownload(photonTitle, photonTooltip, urlPhotonAddon, photonIcon);	            
	        }

	        AddonAsset("Easy Save",
	            "The Fast and Simple way to Save and Load Data.",
	            "768");

	        AddonAsset("Pro Camera 2D",
	            "Quickly set-up a camera for any kind of 2D game.",
	            "42095");

	        DoBottomSpacer();
	    }

	    private static void DoLegacyAddons()
	    {
	        DoSectionTitle("Legacy Systems");
	        EditorGUILayout.HelpBox("NOTE: Legacy systems might be removed by Unity in future releases!", 
	            MessageType.Warning);

	        DoTopSpacer();

            // Legacy Networking

	        GUILayout.BeginHorizontal();
	        label.text = "Legacy Networking";
	        label.tooltip = "Unity's Legacy Networking system.\nClick for more info online.";
            if (GUILayout.Button(label, EditorStyles.label))
	        {
	            Application.OpenURL(urlLegacyNetworkDocs);
	        }

            GUILayout.FlexibleSpace();

#if PLAYMAKER_LEGACY_NETWORK
            label.text = "Disable";
	        label.tooltip = "Remove PLAYMAKER_LEGACY_NETWORK symbol."+
                            "\nYou can then delete:"+
	                        "\nPlayMaker\\Actions\\Network";
	        if (GUILayout.Button(label, GUILayout.Width(100)))
	        {
                PlayMakerDefines.RemoveSymbolFromAllTargets("PLAYMAKER_LEGACY_NETWORK");
    	        EditorStartupPrefs.UseLegacyNetworking = false;

	            EditorUtility.DisplayDialog("PlayMaker Add-ons",
	                "PLAYMAKER_LEGACY_NETWORK symbol removed." +
	                "\n\nYou can now delete:" +
	                "\nPlayMaker\\Actions\\Network", 
	                "OK");
	        }
#else
	        label.text = "Enable";
	        label.tooltip = "Define PLAYMAKER_LEGACY_NETWORK symbol." +
	                        "\nImport actions for Unity's Legacy Networking:" +
	                        "\nPlayMaker\\Actions\\Network";
	        if (GUILayout.Button(label, GUILayout.Width(100)))
	        {
	            DefinesHelper.AddSymbolToAllTargets("PLAYMAKER_LEGACY_NETWORK");

	            EditorUtility.DisplayDialog("PlayMaker Add-ons",
	                "PLAYMAKER_LEGACY_NETWORK added to scripting define symbols.", 
	                "OK");

	            ImportAddon(AssetGUIDs.LegacyNetworkingPackage);
	            EditorStartupPrefs.UseLegacyNetworking = true;
	        }
#endif
            GUILayout.EndHorizontal();

            // Legacy GUI

	        GUILayout.BeginHorizontal();
	        label.text = "Legacy GUI";
	        label.tooltip = "Actions for Unity's Legacy GUI System.\nClick for more info online.";
	        if (GUILayout.Button(label, EditorStyles.label))
	        {
	            Application.OpenURL(urlLegacyGUIDocs);
	        }

	        GUILayout.FlexibleSpace();
	        label.text = "Import";
	        label.tooltip = "Import actions for Unity's Legacy GUI system:" +
	                              "\nPlayMaker\\Actions\\GUIElement";
	        if (GUILayout.Button(label, GUILayout.Width(100)))
	        {
                ImportAddon(AssetGUIDs.LegacyGUIPackage);
	        }
	        GUILayout.EndHorizontal();

            // iTween

	        GUILayout.BeginHorizontal();
	        label.text = "iTween Support";
	        label.tooltip = "Support for iTween (available on the Asset Store). " +
	                        "NOTE: We recommend using newer third party Tweening libraries." +
	                        "\nClick for more info online.";
	        if (GUILayout.Button(label, EditorStyles.label))
	        {
	            Application.OpenURL(urlITweenDocs);
	        }

	        GUILayout.FlexibleSpace();
	        label.text = "Import";
	        label.tooltip = "Import actions for iTween:" +
	                        "\nPlayMaker\\Actions\\iTween " +
	                        "\n\nNOTE: Import iTween from Asset Store first!" ;
	        if (GUILayout.Button(label, GUILayout.Width(100)))
	        {
	            if (EditorUtility.DisplayDialog("Import iTween Support",
	                "You must import iTween from the Asset Store first. Have you already imported iTween?",
	                "Yes", "No"))
	            {
	                ImportAddon(AssetGUIDs.LegacyITweenPackage);
	            }
	        }
	        GUILayout.EndHorizontal();

	       DoBottomSpacer();
	    }

	    private static void AddonDownload(string name, string tooltip, string url, Texture image = null)
	    {
	        if (Button(name, tooltip, image))
	        {
	            Application.OpenURL(url);
	        }
	    }

	    private static void AddonAsset(string name, string tooltip, string assetId, Texture image = null)
	    {
	        if (Button(name, tooltip, image))
	        {
	            Application.OpenURL(urlAssetStoreRoot + assetId + "?aid=1101lGoU");
	        }
	    }

	    private static void ImportAddon(string GUID, bool interactive = true)
	    {
	        AssetDatabase.ImportPackage(AssetDatabase.GUIDToAssetPath(GUID), interactive);
	    }

	    private static void PhotonSetupWizard(string name, string tooltip, Texture image = null)
	    {
	        if (Button(name, tooltip, image))
	        {
	            PlayMakerEditorStartup.GetType("PlayMakerPhotonWizard").GetMethod("Init").Invoke(null, null);
	        }
	    }

        private static bool Button(string name, string tooltip, Texture image = null)
        {
            InitLabel(name, tooltip); // TODO: Fix image styling 
            return GUILayout.Button(label, GUILayout.Height(25));
        }

        private static readonly GUIContent label = new GUIContent();

        private static void InitLabel(string text, string tooltip, Texture image = null)
        {
            label.text = text;
            label.tooltip = tooltip;
            label.image = image;
        }

        public static void SaveSettings()
        {
            EditorPrefs.SetFloat("PlayMaker.AddonManagerScroll", addonsScroll.y);
        }
	}
}