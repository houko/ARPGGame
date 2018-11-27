/*
namespace HutongGames.PlayMakerEditor
{
    [CustomActionEditor(typeof (PlayMaker.Actions.SetProperty))]
    public class SetPropertyAction : CustomActionEditor
    {
        private PlayMaker.Actions.SetProperty setPropertyAction;

        public override void OnEnable()
        {
            // Temporary fix for bug in 1.8.1 with SetProperty actions created with new context menus
            setPropertyAction = target as PlayMaker.Actions.SetProperty;
            if (setPropertyAction != null) // should never be null!
            {
                setPropertyAction.targetProperty.setProperty = true;
            }
        }

        public override bool OnGUI()
        {
            return DrawDefaultInspector();
        }
    }
}*/
