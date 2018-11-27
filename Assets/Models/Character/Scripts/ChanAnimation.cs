/**
* author: xiaomo
* github: https://github.com/xiaomoinfo
* email: xiaomo@xiamoo.info
* QQ_NO: 83387856
* Desc: 
*/

using UnityEngine;

public class ChanAnimation : MonoBehaviour
{
    private Animator anim;

    public float animSpeed = 1.5f;


    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void LateUpdate()
    {
        var vertical = ETCInput.GetAxis("Vertical");
        var horizontal = ETCInput.GetAxis("Horizontal");
        anim.SetFloat("Speed", vertical);
        anim.SetFloat("Direction", horizontal);
        anim.speed = animSpeed;

        if (ETCInput.GetButtonDown("Jump"))
        {
            anim.SetTrigger("Jump");
        }
    }
}
