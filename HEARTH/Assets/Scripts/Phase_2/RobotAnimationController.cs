using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotAnimationController : MonoBehaviour
{
    private Animator robotAnimator;
    private int startWalk = Animator.StringToHash("StartWalk");
    private int walk = Animator.StringToHash("Walk");
    public enum robotAnimations { walk };

    private bool walking = false;

    // Start is called before the first frame update
    void Start()
    {
        robotAnimator = GetComponent<Animator>();
        if (robotAnimator == null) Debug.LogError("no animator was found in robot");
    }

    public void TriggerAnimation(int animationIndex)
    {
        switch (animationIndex)
        {
            case (int)robotAnimations.walk: //Start to walk
                walking = !walking;
                if (walking) robotAnimator.SetTrigger(startWalk);
                robotAnimator.SetBool(walk, walking);
                break;

            default:
                break;

        }
    }

}
