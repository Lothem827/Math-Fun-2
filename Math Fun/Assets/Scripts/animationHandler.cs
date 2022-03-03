using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class animationHandler : MonoBehaviour
{

    public Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void runTrigger(string trigger)
    {
       anim.SetTrigger(trigger);
    }
}
