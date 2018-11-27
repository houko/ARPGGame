// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

#if (UNITY_4_3 || UNITY_4_5 || UNITY_4_6 || UNITY_4_7 || UNITY_5_0) 
#define UNITY_PRE_5_1
#endif

using System;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace HutongGames.PlayMakerEditor
{
    /// <summary>
    /// Welcome Window with getting started shortcuts
    /// </summary>
    [InitializeOnLoad]
    public class PlayMakerWelcomeWindow : EditorWindow
    {
        // Remember to update version info since it's used by export scripts!
        public const string InstallCurrentVersion = "1.9.0";
        public const string InstallAssemblyVersion = "1.9.0.f5";
        public const string InstallBetaVersion = "";
        public const string Version = InstallCurrentVersion + " " + InstallBetaVersion;

        private const string editorPrefsSavedPage = "PlayMaker.WelcomeScreenPage";
        private const string urlSamples = "http://www.hutonggames.com/samples.php";
        private const string urlTutorials = "http://www.hutonggames.com/tutorials.html";
        private const string urlDocs = "https://hutonggames.fogbugz.com/default.asp?W1";
        private const string urlForums = "http://hutonggames.com/playmakerforum/index.php";

        private const float windowWidth = 500;
        private const float windowHeight = 440;
        private const float pageTop = 70;
        private const float pagePadding = 95;

        private static string currentVersion;
        private static string currentVersionLabel;
        private static string currentVersionShort;
        private static int majorVersion; // 1.8 -> 18 for easier comparisons
        private static bool isStudentVersion;

        private enum Page
        {
            Welcome = 0,
            Install = 1,
            GettingStarted = 2,
            UpgradeGuide = 3,
            Addons = 4
        }
        private Page currentPage = Page.Welcome;
        private Page nextPage;
        private Rect currentPageRect;
        private Rect nextPageRect;
        private float currentPageMoveTo;
        private Rect headerRect;
        private Rect backButtonRect;

        private bool pageInTransition;
        private float transitionStartTime;
        private const float transitionDuration = 0.5f;

        private Vector2 scrollPosition;
        
        private static GUIStyle playMakerHeader;
        private static GUIStyle labelWithWordWrap;
        private static GUIStyle largeTitleWithLogo;
        private static GUIStyle versionLabel;
        private static Texture samplesIcon;
        private static Texture checkIcon;
        private static Texture docsIcon;
        private static Texture videosIcon;
        private static Texture forumsIcon;
        private static Texture addonsIcon;
        private static Texture backButton;

        private static bool stylesInitialized;

#if PLAYMAKER_1_9_0
        [MenuItem("PlayMaker/Welcome Screen", false, 500)]
#elif PLAYMAKER
        [MenuItem("PlayMaker/Update PlayMaker", false, 500)]
#else
        [MenuItem("PlayMaker/Install PlayMaker", false, 500)]
#endif
        public static void OpenWelcomeWindow()
        {
            var window = GetWindow<PlayMakerWelcomeWindow>(true);
            window.SetPage(Page.Welcome);
            PlayMakerAddonManager.ResetView();
        }

        public static void Open()
        {
            OpenWelcomeWindow();
        }

        public void OnEnable()
        {
#if UNITY_PRE_5_1
            title = "Welcome To PlayMaker";
#else
            titleContent = new GUIContent("Welcome To PlayMaker");            
#endif
            maxSize = new Vector2(windowWidth, windowHeight);
            minSize = maxSize;

            // Try to get current playmaker version if installed
            GetPlayMakerVersion();

            // Is this the install for the student version?
            isStudentVersion = AssetGUIDs.IsStudentVersionInstall();

            // Init add-ons manager
            PlayMakerAddonManager.Init();

            // Setup pages

            currentPageRect = new Rect(0, pageTop, windowWidth, windowHeight - pagePadding);
            nextPageRect = new Rect(0, pageTop, windowWidth, windowHeight - pagePadding);
            headerRect = new Rect(0, 0, windowWidth, 60);
            backButtonRect = new Rect(0, windowHeight-24, 123, 24);

            // Save page to survive recompile...?
            currentPage = (Page)EditorPrefs.GetInt(editorPrefsSavedPage, (int)Page.Welcome);
            
            //currentPage = Page.Welcome;

            // We want to show the Upgrade Guide after installing

            /*
            if (EditorStartupPrefs.ShowUpgradeGuide)
            {
                //currentPage = Page.UpgradeGuide; //TODO: This was problematic
                EditorStartupPrefs.ShowUpgradeGuide = false; // reset
                EditorUtility.DisplayDialog("PlayMaker",
                    "Please check the Upgrade Guide for more information on this release.", 
                    "OK");
            }*/

            SetPage(currentPage);               
            Update();
        }

        protected void OnDisable()
        {
            PlayMakerAddonManager.SaveSettings();
        }

        private static void GetPlayMakerVersion()
        {
            var versionInfo = PlayMakerEditorStartup.GetType("HutongGames.PlayMakerEditor.VersionInfo");
            if (versionInfo != null)
            {
                currentVersion = versionInfo.GetMethod("GetAssemblyInformationalVersion").Invoke(null, null) as string;
                if (currentVersion != null)
                {
                    currentVersionShort = currentVersion.Substring(0, currentVersion.LastIndexOf('.'));
                    currentVersionLabel = "version " + currentVersionShort;
                    majorVersion = int.Parse(currentVersionShort.Substring(0, 3).Replace(".", ""));
                }
                else
                {
                    currentVersionLabel = "version unknown";
                    currentVersionShort = "";
                    majorVersion = -1;
                }
            }
            else
            {
                currentVersionLabel = "Not installed";
                currentVersionShort = "";
                majorVersion = -1;
            }
        }

        private static void InitStyles()
        {
            if (!stylesInitialized)
            {
                playMakerHeader = new GUIStyle
                {
                    normal =
                    {
                        background = Resources.Load("playMakerHeader") as Texture2D,
                        textColor = Color.white
                    },
                    border = new RectOffset(253, 0, 0, 0),
                };

                largeTitleWithLogo = new GUIStyle
                {
                    normal =
                    {
                        background = Resources.Load("logoHeader") as Texture2D,
                        textColor = Color.white
                    },
                    border = new RectOffset(60, 0, 0, 0),
                    padding = new RectOffset(60, 0, 0, 0),
                    margin = new RectOffset(0, 0, 0, 0),
                    contentOffset = new Vector2(0, 0),
                    alignment = TextAnchor.MiddleLeft,
                    fixedHeight = 60,
                    fontSize = 36,
                    fontStyle = FontStyle.Bold,
                };

                labelWithWordWrap = new GUIStyle(EditorStyles.label) { wordWrap = true };
                versionLabel = new GUIStyle(EditorStyles.label) { alignment = TextAnchor.LowerRight};

                samplesIcon = (Texture) Resources.Load("linkSamples");
                checkIcon = (Texture)Resources.Load("linkCheck");
                videosIcon = (Texture)Resources.Load("linkVideos");
                docsIcon = (Texture) Resources.Load("linkDocs");
                forumsIcon = (Texture) Resources.Load("linkForums");
                addonsIcon = (Texture) Resources.Load("linkAddons");
                backButton = (Texture) Resources.Load("backButton");
            }
            stylesInitialized = true;
        }

        public void OnGUI()
        {
            InitStyles();

            GUILayout.BeginVertical();
            
            DoHeader();

            GUILayout.BeginVertical();

            DoPage(currentPage, currentPageRect);
            
            if (pageInTransition)
            {
                DoPage(nextPage, nextPageRect);
            }

            // Bottom line

            GUILayout.FlexibleSpace();
            GUILayout.BeginHorizontal();                
            GUILayout.FlexibleSpace();

            EditorStartupPrefs.ShowWelcomeScreen = GUILayout.Toggle(EditorStartupPrefs.ShowWelcomeScreen, "Show At Startup");

            GUILayout.Space(10);
            GUILayout.EndHorizontal();

            GUILayout.EndVertical();
            GUILayout.EndVertical();

            if (currentPage != Page.Welcome && !pageInTransition)
            {
                DoBackButton(Page.Welcome);
            }
        }

        private void DoHeader()
        {
            switch (nextPage)
            {
                case Page.Welcome:
                    GUI.Box(headerRect, "", playMakerHeader);
                    break;

                case Page.Install:
                    GUI.Box(headerRect, "Installation", largeTitleWithLogo);
                    break;

                case Page.GettingStarted:
                    GUI.Box(headerRect, "Getting Started", largeTitleWithLogo);
                    break;

                case Page.UpgradeGuide:
                    GUI.Box(headerRect, "Upgrade Guide", largeTitleWithLogo);
                    break;

                case Page.Addons:
                    GUI.Box(headerRect, "Add-Ons", largeTitleWithLogo);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            // Version
            if (!string.IsNullOrEmpty(currentVersion) && majorVersion > 17)
            {
                GUI.Box(headerRect, currentVersionLabel, versionLabel);
            }

            // reserve space in layout
            GUILayoutUtility.GetRect(position.width, 60);
        }

        private void DoPage(Page page, Rect pageRect)
        {
            pageRect.height = position.height - pagePadding;
            GUILayout.BeginArea(pageRect);

            switch (page)
            {
                case Page.Welcome:
                    DoWelcomePage();
                    break;
                case Page.Install:
                    DoInstallPage();
                    break;
                case Page.GettingStarted:
                    DoGettingStartedPage();
                    break;
                case Page.UpgradeGuide:
                    DoUpgradeGuidePage();
                    break;
                case Page.Addons:
                    DoAddonsPage();
                    break;
            }

            GUILayout.EndArea();
        }

        private void DoWelcomePage()
        {
            GUILayout.BeginVertical();
            GUILayout.FlexibleSpace();

            if (isStudentVersion)
            {
                DrawLink(samplesIcon,
                    "Install PlayMaker Student Version",
                    "Import the latest student version of PlayMaker.",
                    GotoPage, Page.Install);
            }
            else
            {
                DrawLink(samplesIcon,
                    "Install PlayMaker",
                    "Import the latest version of PlayMaker.",
                    GotoPage, Page.Install);
            }

            DrawLink(docsIcon,
                     "Upgrade Guide",
                     "Guide to upgrading Unity/PlayMaker.",
                     GotoPage, Page.UpgradeGuide);

            DrawLink(videosIcon,
                     "Getting Started",
                     "Links to samples, tutorials, forums etc.",
                     GotoPage, Page.GettingStarted);

            DrawLink(addonsIcon,
                 "Add-Ons",
                 "Extend PlayMaker with these powerful add-ons.",
                 GotoPage, Page.Addons);

            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
        }

        private static void DoInstallPage()
        {
            ShowBackupHelpBox();

            GUILayout.BeginVertical();
            GUILayout.Space(30);

            DrawLink(checkIcon,
                     "Pre-Update Check",
                     "Check for potential update issues.",
                     PreUpdateCheck, null);

            if (isStudentVersion)
            {
                DrawLink(samplesIcon,
                    "Install PlayMaker Student Version " + InstallCurrentVersion,
                    "The current official release.",
                    InstallLatestStudent, null);
            }
            else
            {
                DrawLink(samplesIcon,
                    "Install PlayMaker " + InstallCurrentVersion,
                    "The current official release.",
                    InstallLatest, null);
            }

            if (!string.IsNullOrEmpty(InstallBetaVersion))
            {
                DrawLink(samplesIcon,
                         "Install PlayMaker " + InstallBetaVersion,
                         "The latest public beta version.",
                         InstallBeta, null);
            }

            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
        }

        private static void DoGettingStartedPage()
        {
            GUILayout.BeginVertical();
            GUILayout.FlexibleSpace();

            DrawLink(samplesIcon,
                 "Samples",
                 "Download sample scenes and complete projects.",
                 OpenUrl, urlSamples);

            DrawLink(videosIcon,
                 "Tutorials",
                 "Watch tutorials on the PlayMaker YouTube channel.",
                 OpenUrl, urlTutorials);

            DrawLink(docsIcon,
                 "Docs",
                 "Browse the online manual.",
                 OpenUrl, urlDocs);

            DrawLink(forumsIcon,
                 "Forums",
                 "Join the PlayMaker community!",
                 OpenUrl, urlForums);

            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
        }

        private void DoUpgradeGuidePage()
        {
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

            ShowBackupHelpBox();

            GUILayout.Label("Version 1.8+", EditorStyles.boldLabel);
            EditorGUILayout.HelpBox(
                "FSMs saved with 1.8+ cannot be opened in earlier versions of PlayMaker! Please BACKUP projects!",
                MessageType.Warning);

            GUILayout.Label("Version 1.8.6", EditorStyles.boldLabel);
            EditorGUILayout.HelpBox(
                "\nPlayMaker 1.8.6 is more strict about changes allowed in Prefab Instances: "+
                "If a Prefab Instance is modified in a way that is incompatible with the Prefab Parent it will be disconnected. " +
                "You can reconnect Instances using Apply or Revert." +
                "\n",
                MessageType.Warning);

            GUILayout.Label("Version 1.8.5", EditorStyles.boldLabel);
            EditorGUILayout.HelpBox(
                "\nPlayMaker 1.8.5 moved LateUpdate handling to an optional component automatically added as needed.\n" +
                "\nIf you have custom actions that use LateUpdate you must add this to OnPreprocess:\n" +
                "\nFsm.HandleLateUpdate = true;\n" +
                "\nSee Rotate.cs for an example." +
                "\n",
                MessageType.Warning);

            GUILayout.Label("Version 1.8.2", EditorStyles.boldLabel);
            EditorGUILayout.HelpBox("\nPlayMaker 1.8.2 added the following system events:\n" +
                                    "\nMOUSE UP AS BUTTON, JOINT BREAK, JOINT BREAK 2D, PARTICLE COLLISION." +
                                    "\n\nPlease remove any custom proxy components you used before to send these events." +
                                    "\n",
                                    MessageType.Warning);

            GUILayout.Label("Version 1.8.1", EditorStyles.boldLabel);
            EditorGUILayout.HelpBox("\nPlayMaker 1.8.1 integrated the following add-ons and actions:\n"+
                                    "\n- Physics2D Add-on" +
                                    "\n- Mecanim Animator Add-on" +
                                    "\n- Vector2, Quaternion, and Trigonometry actions"+
                                    "\n\nThe new versions of these files are under \"Assets/PlayMaker/Actions\""+
                                    "\n\nIf you imported these add-ons, the old versions are likely under \"Assets/PlayMaker Custom Actions\". "+
                                    "If you get errors from duplicate files after updating please delete the old files!" +
                                    "\n", 
                                    MessageType.Warning);

            GUILayout.Label("Unity 5 Upgrade Notes", EditorStyles.boldLabel);
            EditorGUILayout.HelpBox(
                "\nIf you run into problems updating a Unity 4.x project please check the Troubleshooting guide on the PlayMaker Wiki." +
                "\n",
                MessageType.Warning);
            EditorGUILayout.HelpBox("\nUnity 5 removed component property shortcuts from GameObject. " +
                                    "\n\nThe Unity auto update process replaces these properties with GetComponent calls. " +
                                    "In many cases this is fine, but some third party actions and addons might need manual updating! " +
                                    "Please post on the PlayMaker forums and contact the original authors for help." +
                                    "\n\nIf you used these GameObject properties in Get Property or Set Property actions " +
                                    "they are no longer valid, and you need to instead point to the Component directly. " +
                                    "E.g., Drag the Component (NOT the GameObject) into the Target Object field." +
                                    "\n", MessageType.Warning);

            GUILayout.Label("Unity 4.6 Upgrade Notes", EditorStyles.boldLabel);
            EditorGUILayout.HelpBox("\nFind support for the new Unity GUI online in our Addons page.\n", MessageType.Info);
            EditorGUILayout.HelpBox("\nPlayMakerGUI is only needed if you use OnGUI Actions. " +
                                    "If you don't use OnGUI actions un-check Auto-Add PlayMakerGUI in PlayMaker Preferences.\n", MessageType.Info);

            EditorGUILayout.EndScrollView();
            //FsmEditorGUILayout.Divider();
        }

        private static void DoAddonsPage()
        {
            PlayMakerAddonManager.OnGUI();
        }

        private static void ShowBackupHelpBox()
        {
            HelpBox("Always BACKUP projects before updating!\nUse Version Control to manage changes!", MessageType.Error);
            //HelpBox("Unity 5.3: Upgrade to 1.8.0 to address compatibility issues!", MessageType.Error);
            //HelpBox("Unity 5.4 Beta: Use 1.8.1 Beta to address compatibility issues!", MessageType.Error);
        }

        private static void HelpBox(string text, MessageType messageType)
        {
            GUILayout.BeginHorizontal();
            EditorGUILayout.HelpBox(text, messageType);
            GUILayout.Space(5);
            GUILayout.EndHorizontal();
        }

        private static void DrawLink(Texture texture, string heading, string body, LinkFunction func, object userData)
        {
            GUILayout.BeginHorizontal();

            GUILayout.Space(64);
            GUILayout.Box(texture, GUIStyle.none, GUILayout.MaxWidth(48));
            GUILayout.Space(10);

            GUILayout.BeginVertical();
            GUILayout.Space(1);
            GUILayout.Label(heading, EditorStyles.boldLabel);
            GUILayout.Label(body, labelWithWordWrap);
            GUILayout.EndVertical();

            GUILayout.EndHorizontal();

            var rect = GUILayoutUtility.GetLastRect();
            EditorGUIUtility.AddCursorRect(rect, MouseCursor.Link);

            if (Event.current.type == EventType.MouseDown && rect.Contains(Event.current.mousePosition))
            {
                func(userData);
                GUIUtility.ExitGUI();
            }

            GUILayout.Space(10);
        }

        private void DoBackButton(Page toPage)
        {
            GUI.Box(backButtonRect, backButton, GUIStyle.none);
            EditorGUIUtility.AddCursorRect(backButtonRect, MouseCursor.Link);

            if (Event.current.type == EventType.MouseDown && backButtonRect.Contains(Event.current.mousePosition))
            {
                GotoPage(toPage);
                GUIUtility.ExitGUI();
            }
        }

        private void Update()
        {
            if (pageInTransition)
            {
                DoPageTransition();
            }
        }

        private void DoPageTransition()
        {
            var t = (Time.realtimeSinceStartup - transitionStartTime) / transitionDuration;
            if (t > 1f)
            {
                SetPage(nextPage);
                return;
            }

            var nextPageX = Mathf.SmoothStep(nextPageRect.x, 0, t);
            var currentPageX = Mathf.SmoothStep(currentPageRect.x, currentPageMoveTo, t);
            currentPageRect.Set(currentPageX, pageTop, windowWidth, position.height);
            nextPageRect.Set(nextPageX, pageTop, windowWidth, position.height);

            Repaint();
        }

        private static bool DisplayInstallDialog(string versionInfo, string notes)
        {
            return EditorUtility.DisplayDialog("PlayMaker", "Install PlayMaker " + versionInfo + "\n" + 
                notes + "\n\nAlways backup projects before updating Unity or PlayMaker!",
                "I Made a Backup. Go Ahead!", "Cancel");
        }

        // Button actions:

        public delegate void LinkFunction(object userData);

        private static void PreUpdateCheck(object userData)
        {
            PreUpdateChecker.Open();
        }

        private static void InstallLatest(object userData)
        {
            if (DisplayInstallDialog(InstallCurrentVersion, "The latest release version of PlayMaker." +
                                                        "\n\nNOTE: Projects saved with PlayMaker 1.8+ cannot be opened in older versions of PlayMaker!"))
            {
                EditorStartupPrefs.ShowUpgradeGuide = true; // show upgrade guide after importing
                ImportPackage(AssetDatabase.GUIDToAssetPath(AssetGUIDs.LatestInstall));
            }
        }

        private static void InstallLatestStudent(object userData)
        {
            if (DisplayInstallDialog("Student Version " + InstallCurrentVersion, "The latest student version of PlayMaker." +
                                                            "\n\nNOTE: The Student Version is limited to built in actions only."))
            {
                EditorStartupPrefs.ShowUpgradeGuide = true; // show upgrade guide after importing
                ImportPackage(AssetDatabase.GUIDToAssetPath(AssetGUIDs.LatestStudentInstall));
            }
        }

        private static void InstallBeta(object userData)
        {
            /*
            if (DisplayInstallDialog(InstallBetaVersion, "The latest BETA version of PlayMaker." +
                                                        "\n\nNOTE: Projects saved with PlayMaker 1.8+ cannot be opened in older versions of PlayMaker!"))
            {
                EditorStartupPrefs.ShowUpgradeGuide = true; // show upgrade guide after importing
                ImportPackage(AssetDatabase.GUIDToAssetPath(AssetGUIDs.PlayMakerUnitypackage181));
            }*/
        }

        private static void ImportPackage(string package)
        {
            try
            {
                AssetDatabase.ImportPackage(package, true);
            }
            catch (Exception)
            {
                Debug.LogError("Failed to import package: " + package);
                throw;
            }

            // This didn't work that well
            // Instead let the user open the upgrade guide
            //GotoPage(Page.UpgradeGuide);
        }

        private static void OpenUrl(object userData)
        {
            Application.OpenURL(userData as string);
        }

        public void OpenInAssetStore(object userData)
        {
            AssetStore.Open("content/" + userData);
        }

        private void GotoPage(object userData)
        {
            nextPage = (Page)userData;
            pageInTransition = true;
            transitionStartTime = Time.realtimeSinceStartup;

            /* Always open PreUpdateChecker...?
            if (nextPage == Page.Install)
            {
                PreUpdateChecker.Open();
            }*/

            // next page slides in from the right
            // welcome screen slides offscreen left
            // reversed if returning to the welcome screen

            if (nextPage == Page.Welcome)
            {
                nextPageRect.x = -windowWidth;
                currentPageMoveTo = windowWidth;
            }
            else
            {
                nextPageRect.x = windowWidth;
                currentPageMoveTo = -windowWidth;
            }

            GUIUtility.ExitGUI();
        }

        private void SetPage(Page page)
        {
            currentPage = page;
            nextPage = page;
            pageInTransition = false;
            currentPageRect.x = 0;
            SaveCurrentPage();
            Repaint();
        }

        private void SaveCurrentPage()
        {
            EditorPrefs.SetInt(editorPrefsSavedPage, (int)currentPage);
        }

        [Obsolete("Use PlayMakerEditorStartup.GetType instead.")]
        public static Type GetType(string typeName)
        {
            return PlayMakerEditorStartup.GetType(typeName);
        }

        [Obsolete("Use PlayMakerEditorStartup.FindTypeInLoadedAssemblies instead.")]
        public static Type FindTypeInLoadedAssemblies(string typeName)
        {
            return PlayMakerEditorStartup.FindTypeInLoadedAssemblies(typeName);
        }
    }
}