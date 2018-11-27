using HutongGames.Editor;

namespace HutongGames.PlayMakerEditor
{
    /// <summary>
    /// Guided Tour of the various PlayMaker editor windows and UI.
    /// NOTE: You can extend BaseGuidedTourWindow to make your own Guided Tour windows!
    /// </summary>
    public class PlayMakerGuidedTour : BaseGuidedTourWindow
    {
        private static PlayMakerGuidedTour instance;

        /// <summary>
        /// Open as Utility Window
        /// </summary>
        public static void Open()
        {
            FsmEditorWindow.OpenWindow(); // make sure main editor is open
            if (instance == null)
            {
                var window = CreateInstance<PlayMakerGuidedTour>();
                window.ShowUtility(); // stays on top in OSX
            }
            else
            {
                GetWindow<PlayMakerGuidedTour>();
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            instance = this;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            instance = null;
        }

        public override void Initialize()
        {
            base.Initialize();
            needsMainEditor = true;
            SetTitle("PlayMaker");
            BuildGuide();
        }

        public override void DoGUI()
        {
            FsmEditorStyles.Init();
            FsmEditorGUILayout.ToolWindowLargeTitle(this, "Guided Tour");
            FsmEditorGUILayout.LabelWidth(200);

            //EditorGUILayout.HelpBox("NOTE: This is a BETA feature, please give feedback on the Playmaker Forums. Thanks!", MessageType.Info);

            DoGuidedTourGUI();
        }

        public void OnFocus()
        {
            // Repaint all to make sure highlight ids are refreshed
            FsmEditor.RepaintAll();
            Repaint();
        }

        private void BuildGuide()
        {
            guidedTour.Reset();
            BuildMainEditorGuide();
            BuildActionBrowserGuide();
            BuildGlobalVariablesGuide();
            BuildFsmBrowserGuide();
            BuildStateBrowserGuide();
            BuildTemplatesBrowserGuide();
            BuildEventBrowserGuide();
            BuildFsmTimelineGuide();
            BuildFsmLogGuide();
        }

        private void BuildMainEditorGuide()
        {
            var root = AddWindow(typeof(FsmEditorWindow), "Main Editor", 
                "The main PlayMaker Editor used to make FSMs.", 
                "PlayMaker also uses various tool windows found in " +
                "<i>Main Menu > PlayMaker > Editor Windows</i>. " +
                "\n\nTool windows need the main PlayMaker Editor to be open.\n\n" +
                "NOTE: You can arrange PlayMaker windows and save custom layouts. " +
                "See: <a href=\"https://docs.unity3d.com/Manual/CustomizingYourWorkspace.html\">Customizing Your Workspace</a>.", 
                "Window", GetUrl(WikiPages.MainEditorWindow));

            var selectionToolbar = AddTopic(root, "Selection Toolbar", "The Selection Toolbar lets you quickly select FSMs in the scene.", "", "", GetUrl(WikiPages.SelectionToolbar));
            AddTopic(selectionToolbar, "Back", "Move Back in the selection history.",
                "TIP: Use this to quickly go back to a parent FSM after selecting a sub FSM.");
            AddTopic(selectionToolbar, "Forward", "Move forward in the selection history.");
            AddTopic(selectionToolbar, "History", "Recently selected FSMs.", 
                "The popup menu shows recent FSMs, letting you quickly reselect one.");
            AddTopic(selectionToolbar, "Selected GameObject", "Select GameObjects that have FSMs",
                "The button shows the currently selected GameObject. The dropdown menu lets you select other GameObjects that have FSMs.");
            AddTopic(selectionToolbar, "Selected FSM", "Select an FSM on the GameObject.", "NOTE: A GameObject can have multiple FSMs.");
            AddTopic(selectionToolbar, "Lock Selection", "Lock the current selection.", 
                "Keeps the FSM selected so it doesn't switch as you select other GameObjects." +
                "\n\nTIP: Use Lock to drag and drop components from other GameObjects into the selected FSM.");
            AddTopic(selectionToolbar, "Select Owner", "Select the GameObject that owns this FSM.");
            AddTopic(selectionToolbar, "Minimap Toggle", "Toggle the MiniMap on/off.", 
                "The MiniMap shows an overview of the selected FSM." +
                "\n\nSee <a href=\"W123\">Graph View</a> for more info.");

            AddTopic(root, "Graph View", "The canvas where you build FSMs.", 
                "The Graph View shows a zoom-able canvas where you can add States and connect them with Transitions. " +
                "This defines the overall structure of an FSM. Most editing is done with right-click context menus." +
                "\n\nSelect a State to edit it further in the Inspector Panel", "", GetUrl(WikiPages.GraphView));

            var inspector = AddTopic(root, "Inspector Panel", "Context editing of properties.", 
                "<a href=\"W960\">FSM Inspector</a>\nEdit the high level behaviour of the FSM.\n\n" +
                "<a href=\"W151\">State Inspector</a>\nEdit the Actions run by the selected State.\n\n" +
                "<a href=\"W148\">Events Manager</a>\nManage the Events used by the FSM.\n\n" +
                "<a href=\"W50\">Variables Manager</a>\nManage the Variables used by the FSM.\n\n", 
                "", GetUrl(WikiPages.InspectorPanel));

            var fsmInspector = AddTopic(inspector, "FSM Inspector", 
                "Edit the high level behaviour of the FSM.", 
                "See also: <a href=\"W960\">FSM Inspector</a>", "", GetUrl(WikiPages.FsmInspector));
            fsmInspector.OnClick = () => FsmEditor.Inspector.SetMode(InspectorMode.FsmInspector);
            fsmInspector.Validate = () => FsmEditor.Inspector.Mode == InspectorMode.FsmInspector;

            var stateInspector = AddTopic(inspector, "State Inspector", 
                "Edit the Actions run by the selected State.", 
                "See also: <a href=\"W151\">State Inspector</a>", "", GetUrl(WikiPages.StateInspector));
            stateInspector.OnClick = () => FsmEditor.Inspector.SetMode(InspectorMode.StateInspector);
            stateInspector.Validate = () => FsmEditor.Inspector.Mode == InspectorMode.StateInspector;        
            AddTopic(stateInspector, "Settings Menu");

            var eventManager = AddTopic(inspector, "Event Manager", 
                "Manage the Events used by the FSM.", 
                "See also: <a href=\"W148\">Events Manager</a>", "", GetUrl(WikiPages.EventManager));
            eventManager.OnClick = () => FsmEditor.Inspector.SetMode(InspectorMode.EventManager);
            eventManager.Validate = () => FsmEditor.Inspector.Mode == InspectorMode.EventManager;

            var variablesManager = AddTopic(inspector, "Variable Manager", 
                "Manage the Variables used by the FSM.", 
                "See also: <a href=\"W50\">Variables Manager</a>", "", GetUrl(WikiPages.VariableManager));
            variablesManager.OnClick = () => FsmEditor.Inspector.SetMode(InspectorMode.VariableManager);
            variablesManager.Validate = () => FsmEditor.Inspector.Mode == InspectorMode.VariableManager;

            var debugToolbar = AddTopic(root, "Debug Toolbar", "The Debug Toolbar collects tools for debugging FSMs in the editor.", "", "", GetUrl(WikiPages.DebuggingToolbar));
            AddTopic(debugToolbar, "Error Count", "Shows any errors in the project.",
                "Click to open the <a href=\"W156\">Error Check Window</a>\n\n" +
                "NOTE: Realtime error checking is controlled in <i>Preferences > Error Checking</i>");
            AddTopic(debugToolbar, "Debug Menu", "Opens the Debug Menu", "See <a href=\"W27\">Debug Menu</a>.");
            AddTopic(debugToolbar, "Play Controls", "Play, Pause, and Step.", 
                "You can see the behaviour of PlayMaker FSMs as the game runs in the Editor. " +
                "This 'visual debugging' of behaviours is one of the most powerful features of PlayMaker.\n\n" +
                "NOTE: Step behaviour is controlled in the Debug Menu.");

            var preferences = AddTopic(root, "Preferences", "", 
                "Preferences allow you to customize how PlayMaker behaves.", 
                "Preferences Toolbar", GetUrl(WikiPages.Preferences));
            preferences.OnClick = () => FsmEditor.Inspector.SetMode(InspectorMode.Preferences);
            preferences.Validate = () => FsmEditor.Inspector.Mode == InspectorMode.Preferences;
            AddTopic(preferences, "Toggle Hints (F1)", "Quickly toggle all Hints off/on.", 
                "Hints are in-line help boxes in PlayMaker editor windows. " +
                "They are useful while learning PlayMaker, " +
                "but you can save some space by turning them off.", "Hints");
            AddTopic(preferences, "Open Preferences", 
                "Opens Preferences in the Inspector Panel.");
            AddTopic(preferences, "Preferences Category", 
                "Preferences are grouped into categories.", "See <a href=\"W117\">Online Help</a> for more info.");
        }

        private void BuildActionBrowserGuide()
        {
            var root = AddWindow(typeof(FsmActionWindow), "Action Browser", "Browse for actions to add to a state.", 
                "Actions run while a state is active. For an overview of how actions work, " +
                "check out <a href=\"W174\">Core Concepts > Actions</a> in the online manual.", "Window", GetUrl(WikiPages.ActionBrowser));
            AddTopic(root, "Settings Menu", "Settings for this window.",
                "<b>Show Preview</b>\nToggle the Preview of the selected action.\n\n" +
                "<b>Hide Obsolete Actions</b>\nHide actions that have been marked as Obsolete.\n\n" +
                "<b>Close Window After Adding Action</b>\nTreat the window like a popup dialog.\n\n" +
                "<b>Auto Refresh Action Usage</b>\nTrack action usage as you edit. In larger projects this might effect performance.\n\n" +
                "<b></b>" );
            AddTopic(root, "Search", "Search for actions.", 
                "Filters the Action List as you type. Use the Search Mode dropdown to control the search.\n\n" +
                "NOTE: The Action List is also filtered by the Settings Menu.");
            AddTopic(root, "Search Mode", "Controls how to search for actions.",
                "<b>Name</b>\nSearch action names only.\n\n" +
                "<b>Description</b>\nSearch action description also.\n");
            AddTopic(root, "Action List", "A list of all actions available in the project.", 
                "NOTE: The list can be filtered by Search and the Settings Menu.");
            AddTopic(root, "Action Description", "A brief description of how the action works.",
                "A more detailed description can be found in the online help.");
            AddTopic(root, "Action Online Help", "Get more info on this action.", 
                "Opens the relevant page in the online <a href=\"W2\">Action Reference</a>.", "Online Help");
            AddTopic(root, "Action Preview", "Preview the GUI for the selected action.");
            AddTopic(root, "Preview Toggle", "Toggle the preview of the selected action.");
            AddTopic(root, "Add Action To State", "Add selected action to the selected state.", 
                "NOTE: You can also double click the action; Or drag and drop actions from the Action Browser into the State Inspector or Graph View.", "Add Action");
        }
        
        private void BuildGlobalVariablesGuide()
        {
            var root = AddWindow(typeof(FsmGlobalsWindow), "Global Variables", 
                "Manage the global variables used in the project.", 
                "NOTE: Global variables are stored in:" +
                "\n<a href=\"Assets/PlayMaker/Resources/PlayMakerGlobals.asset\">" +
                "Assets/PlayMaker/Resources/PlayMakerGlobals.asset</a>\n\n" +
                "<b>DO NOT DELETE OR OVERWRITE THIS ASSET!</b>\n\n" +
                "Use PlayMaker/Tools/Export Globals and Import Globals to transfer and merge globals between projects.", 
                "Window", GetUrl(WikiPages.GlobalVariablesBrowser));
            AddTopic(root, "Settings Menu", "Settings for this window.");
            AddTopic(root, "Search", "Interactively search as you type.");
            AddTopic(root, "Search Mode", "Controls how to perform the search.");
            AddTopic(root, "Variables List", "A list of all global variables used in the project.", 
                "Add or select a global variable to edit.\n\nClick column headers to sort the list.");
            AddTopic(root, "Variable Editor", "Add/Edit a variable.");
            AddTopic(root, "Refresh Used Count", "Update the used count for this scene.");
        }

        private void BuildFsmBrowserGuide()
        {
            var root = AddWindow(typeof(FsmSelectorWindow), "FSM Browser", "Shows all loaded FSMs.", 
                "Use this window to:\n\n" +
                "- Quickly select FSMs in your project.\n" +
                "- Track state of FSMs when the game is running.\n" +
                "- Quickly enable/disable FSMs as you test.\n" +
                "- Edit an FSM\'s description and help link.\n", 
                "Window", GetUrl(WikiPages.FsmBrowser));
            AddTopic(root, "Settings Menu", "Settings for this window.",
                "<b>Show Full FSM Path</b>\n" +
                "Show the GameObject and full path to the FSM.\n\n" +
                "<b>Show Disabled FSMs</b>\n" +
                "It can be useful at runtime to filter out disabled FSMs.\n\n" +
                "<b>Show FSMs in Prefabs</b>\n" +
                "Show FSMs used in Prefabs. Uncheck to see only FSMs in the scene.\n\n" +
                "<b>Hide Prefabs when Playing</b>\n" +
                "Most of the time you don't want to see prefabs when playing, just the running FSMs.\n");
            AddTopic(root, "FSM Filter", "Filters the FSMs shown in the list.",
                "<b>All</b>\nShow all loaded FSMs.\n\n" +
                "<b>On Selected Object</b>\nOnly show FSMs on the selected GameObject/s.\n\n" +
                "<b>Recently Selected</b>\nOrders FSMs by their selection history, most recently selected at the top.\n\n" +
                "<b>With Errors</b>\nOnly show FSMs that have errors.\n");
            AddTopic(root, "FSM List", "", 
                "For each FSM shows: Enabled, Name, Current State.\n\n" +
                "Click an FSM to select it in the Main Editor and edit its properties.\n");
            AddTopic(root, "Description", "A user defined description of what the FSM does.", 
                "Use this field to help document your project.");
            AddTopic(root, "Help Url", "User defined link to online help.", 
                "Use this field to help document your project. You can make a web page that explains how the FSM works in more detail.\n\n" +
                "HINT: You can right click in the Graph View and Save Screenshot to upload it to a web page.\n");
        }

        private void BuildStateBrowserGuide()
        {
            var root = AddWindow(typeof(FsmStateWindow), "State Browser", 
                "Shows all States in the selected FSM", "", "Window", GetUrl(WikiPages.StateBrowser));
            AddTopic(root, "FSM Selection", "The selected FSM.", 
                "The dropdown lets you select another FSM.\n" +
                "The selection is synced across editor windows.");
            AddTopic(root, "State List", "States in the selected FSM.", 
                "Click a State to select it in the Main Editor.");
        }

        private void BuildTemplatesBrowserGuide()
        {
            var root = AddWindow(typeof(FsmTemplateWindow), "Templates Browser", "Shows all saved FSM Templates.", 
                "Templates are re-usable FSMs. You can paste a Template as a new FSM, paste it into an existing FSM, " +
                "run it in a State using the <a href=\"W1138\">Run FSM Action</a> etc.\n\n" +
                "This window lets you manage the Templates in your project.", "Window", GetUrl(WikiPages.TemplatesBrowser));
            AddTopic(root, "Settings Menu", "Settings for this window.");
            AddTopic(root, "Search", "Interactively search as you type.");
            AddTopic(root, "Search Mode", "Controls how to perform the search.",
                "<b>Name</b>\nSearch template names only.\n\n" +
                "<b>Description</b>\nSearch template description also.\n");
            AddTopic(root, "Templates List", "", 
                "Shows all the Templates in your project sorted by the categories you've defined.");
            AddTopic(root, "Category", "User defined category", 
                "Use this field to edit the Template's Category. You can make as many Categories as you need for a project.");
            AddTopic(root, "Description", "User defined description of what the Template does.", 
                "Use this to document your project. Since Templates can be re-used a good description is important.");
            AddTopic(root, "New Template", "Save a new Template asset", 
                "NOTE: Template assets must be saved under the projects Assets folder. " +
                "To transfer templates to another project export it in a unitypackage.");
            AddTopic(root, "Load Add Templates", "Refresh the list of Templates.");
        }

        private void BuildEventBrowserGuide()
        {
            var root = AddWindow(typeof(FsmEventsWindow), "Events Browser", "Shows Events used by FSMs", 
                "", "Window", GetUrl(WikiPages.EventBrowser));
            AddTopic(root, "Settings Menu", "Settings for this window.");
            AddTopic(root, "Search", "Interactively search as you type.");
            AddTopic(root, "Search Mode", "Controls how to perform the search.");
            AddTopic(root, "Event List", "Events used by FSMs.", 
                "The first column is the Global flag. Global Events can be sent between FSMs.\n" +
                "The Used column shows how many loaded FSMs use the event.\n" +
                "HINT: Right click on an event to see the FSMs that use it.\n\n" +
                "NOTE: Events are created in the Events tab in the Main Editor.");
        }

        private void BuildFsmTimelineGuide()
        {
            var root = AddWindow(typeof(FsmTimelineWindow), "Timeline Log", "A visual log of FSM state changes.", 
                "NOTE: This is not connected to the new Unity Timeline feature!", "Window", GetUrl(WikiPages.FsmTimeline));
            AddTopic(root, "FSM Filter", "Filters the FSMs to show.", 
                "<b>All FSMs</b>\nShows all FSMs.\n\n" +
                "<b>FSMs in Scene:</b>\nShows only the FSMs in the current scene.\n\n" +
                "<b>On Selected Objects:</b>\nShows only the FSMs on the currently selected GameObject/s.\n\n" +
                "<b>Recently Selected:</b>\nOrders FSMs by their selection history, most recently selected at the top.\n");
            AddTopic(root, "Timeline", "Time bar for the visual log.",
                "Panning and zooming works in a similar way to the Unity Animation Window.\n\n" +
                "Click the bar to pause the game and scrub time. Time scrubbing is synchronized across all Log Windows. " +
                "HINT: If Debug Flow is enabled the Main Editor will show variable values at that time.\n");
            AddTopic(root, "Visual Log", "Visual log of FSM states.",
                "Bars represent the State of each FSM over time. The bars use the State's color to help visually organize state changes.");
            AddTopic(root, "Refresh", "Refresh FSM List", 
                "Sometimes GameObjects are added/removed while playing. Use Refresh to update the FSM list.");
        }

        private void BuildFsmLogGuide()
        {
            var root = AddWindow(typeof(FsmLogWindow), "FSM Log", "A log of all FSM Events", 
                "The log lets you examine events and state changes in an FSM. " +
                "You can see how long an FSM was in a state, the event that triggered the state change, etc.\n\n" +
                "HINT: Click <b>SentBy</b> to jump to the GameObject/FSM that sent an event.", "Window", GetUrl(WikiPages.FsmLog));
            AddTopic(root, "Settings Menu", "Settings for this window.",
                "<b>Show TimeCode</b>\nDisplay a time code next to each log event. " +
                "Time codes can make it easier to figure out what else is happening on other FSMs at the same time.\n\n" +
                "<b>Show Sent By</b>\nShow the FSM that sent an event. Click on the sender to select it.\n\n" +
                "<b>Show State Exit</b>\nShow state Exit events in log. Hiding Exit events can make the log more concise; " +
                "the Exit is implied in the Entry to the next state.");
            AddTopic(root, "FSM Selection", "The selected FSM.", 
                "The dropdown lets you select another FSM.\n" +
                "The selection is synced across editor windows.");
            AddTopic(root, "Log Entries", "Log entries for the selected FSM",
                "Entries follow these conventions:\n\n" +
                "<b>START</b> - Logged when the FSM is enabled.\n" +
                "<b>EVENT</b> [EventName] - Logged when the FSM receives an event.\n" +
                "<b>EXIT</b> [StateName] - Logged when a state is exited.\n" +
                "<b>ENTER</b> [StateName] - Logged when a state is entered.\n" +
                "<b>STOP</b> - Logged when the FSM is disabled.\n\n" +
                "Additionally, Actions and scripts can log 3 types of entries:\n\n" +
                "<b>INFO</b> Plain text log.\n" +
                "<b>WARNING</b> Warning icon and message.\n" +
                "<b>ERROR</b> Error icon and message. Also pauses execution.\n\n" +
                "HINT: Click <b>SentBy</b> to jump to the GameObject/FSM that sent an event.\n\n" +
                "See <a href=\"W28\">Online Docs</a> for more info.\n");
            AddTopic(root, "Clear", "Delete all log entries.");
        }

        /* You can cleanup here
        protected override void OnDisable()
        {
            base.OnDisable();
        }*/

    }

}

