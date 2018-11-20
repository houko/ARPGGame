using UnityEngine;

public class characterRotate : MonoBehaviour
{
    public GameObject frog;

    private Rect FpsRect;
    private string frpString;

    private GameObject instanceObj;
    public GameObject[] gameObjArray = new GameObject[10];
    public AnimationClip[] AniList = new AnimationClip[4];

    private float minimum = 2.0f;
    private float maximum = 50.0f;
    private float touchNum = 0f;
    string touchDirection = "forward";
    private GameObject toad;

    private void OnGUI()
    {
        if (GUI.Button(new Rect(20, 20, 100, 60), "Idle"))
        {
            frog.GetComponent<Animation>().wrapMode = WrapMode.Loop;
            frog.GetComponent<Animation>().CrossFade("Idle");
        }

        if (GUI.Button(new Rect(130, 20, 100, 60), "Walk"))
        {
            frog.GetComponent<Animation>().wrapMode = WrapMode.Loop;
            frog.GetComponent<Animation>().CrossFade("Walk");
        }

        if (GUI.Button(new Rect(240, 20, 100, 60), "Talk"))
        {
            frog.GetComponent<Animation>().wrapMode = WrapMode.Loop;
            frog.GetComponent<Animation>().CrossFade("Talk");
        }

        if (GUI.Button(new Rect(350, 20, 100, 60), "Attack01"))
        {
            frog.GetComponent<Animation>().wrapMode = WrapMode.Loop;
            frog.GetComponent<Animation>().CrossFade("Attack01");
        }

        if (GUI.Button(new Rect(460, 20, 100, 60), "Damage"))
        {
            frog.GetComponent<Animation>().wrapMode = WrapMode.Loop;
            frog.GetComponent<Animation>().CrossFade("Damage");
        }

        if (GUI.Button(new Rect(570, 20, 100, 60), "Dead"))
        {
            frog.GetComponent<Animation>().wrapMode = WrapMode.Loop;
            frog.GetComponent<Animation>().CrossFade("Dead");
        }
    }
}
