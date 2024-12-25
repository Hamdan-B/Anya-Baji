using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcAnimation : MonoBehaviour
{
    public Animator anim;

    public enum AnimNames
    {
        StandingIdle,
        Sitting1,
        Sitting2,
        SitToStand,
        Angry,
    }

    public AnimNames animations;

    public void Start()
    {
        anim.CrossFade(animations.ToString(), 0, 0);
    }

    // Update is called once per frame
    void Update() { }
}
