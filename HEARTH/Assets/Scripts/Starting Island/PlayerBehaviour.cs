using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;
using DG.Tweening;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {

    [SerializeField] private GameObject FPSCharacter;
    [SerializeField] private Transform headBonePosition;
    [SerializeField] private Transform gameplayPosition;
    [SerializeField] private Transform animationPosition;
    private My_MouseLook mouseLook;
    private Camera fpsCamera;
    private Animator animator;
    private Transform camPosition;
    public float lifePoints;

    private bool menuState = false;
    private bool grabbed = false;

    private bool keyboardInput = true;
    private bool activePlayer = false;

    public enum animations { Watch, Lift, Walk, Run, Jump, StandUp};
    private int watchAnimHash = Animator.StringToHash("Watch");
    private int liftAnimHash = Animator.StringToHash("Lift");
    private int speedAnimHash = Animator.StringToHash("Motion Speed");
    private int objAnimHash = Animator.StringToHash("Obj");
    private int wakeAnimHash = Animator.StringToHash("Wake");
    private int jumpStateHash = Animator.StringToHash("Base Layer.Jump");


    private void Start()
    {
        lifePoints = 100f;
        fpsCamera = Camera.main;
        animator = GetComponentInChildren<Animator>();
        mouseLook = this.GetComponent<My_FPSController>().getMouseLook();
    }

    private void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        bool running = Input.GetKey(KeyCode.LeftShift);
        float speed = ((running) ? 1 : 0.5f);
        bool jumping = Input.GetKeyDown(KeyCode.Space);
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        //if (!activePlayer) return;

        CameraOffset(headBonePosition);

        // Movement
        if (moveHorizontal != 0 || moveVertical != 0)
        {
            animator.SetFloat(speedAnimHash, speed);
        }
        else
        {
            animator.SetFloat(speedAnimHash, 0f);
        }

        /*
        if (jumping && stateInfo.fullPathHash != jumpStateHash)
        {
            anim.SetTrigger("Jump");
            Debug.Log("Jumping");
        }
        */

        // Open menu
        if (Input.GetKeyDown(KeyCode.Tab) && stateInfo.fullPathHash != jumpStateHash && IsKeyboardActive())
        {
            menuState = !menuState;
            //menuCanvas.setActive(menuState);
            Cursor.visible = menuState;
            TriggerAnimation((int)animations.Watch);

            /*
            if (menuState)
            {
                SetCameraToHead(menuState);
                DisablePlayerController(!menuState);
            }
            else
            {
                StartCoroutine(DisablePlayerControlsForTime(2f));
                StartCoroutine(SetCameraToAnimPosition(1f));
            }
                
            GetComponentInChildren<Animator>().SetBool("Watch", menuState);
            */

        }

    }

    /* ---- LIFE FUNCTIONS ---- */

    public float getLifePoints()
    {
        return lifePoints;
    }

    public void setLifePoints(float lp)
    {
        lifePoints = lp;
    }

    public void Damage(float damage)
    {
        lifePoints -= damage;
    }

    public void Heal(float health)
    {
        lifePoints += health;
        if (lifePoints >= 100)
        {
            lifePoints = 100;
        }
    }


    public void TriggerAnimation(int animationIndex)
    {
        float animationTime = 0;
        bool onlyDisable = false;
        Vector3 finalHeadPosition;

        SwitchCameraParent(true);

        switch (animationIndex)
        {
            case (int)animations.Watch:

                animationTime = 2f;
                finalHeadPosition = ((menuState) ? new Vector3(45f, 0f, 0f) : new Vector3(0f, 0f, 0f));

                animator.SetBool(watchAnimHash, menuState);
                fpsCamera.transform.DOLocalRotate(finalHeadPosition, animationTime);
                onlyDisable = menuState;

                break;

            case (int)animations.Lift:

                animationTime = 7f;
                finalHeadPosition = new Vector3(0f, 0f, 0f);

                animator.SetTrigger(liftAnimHash);
                fpsCamera.transform.DOLocalRotate(finalHeadPosition, animationTime);
                onlyDisable = false;
                
                break;

            default:
                break;
        }

        
        StartCoroutine(DisableControlsDuringAnimation(animationTime, false));
        StartCoroutine(ResetParentAfterAnimation(animationTime));

    }

    private void SetPlayerControls(bool state)
    {
        this.GetComponent<My_FPSController>().enabled = state;
        this.GetComponent<CharacterController>().enabled = state;
    }

    private IEnumerator DisableControlsDuringAnimation(float time, bool onlyDisable)
    {
        SetPlayerControls(false);

        if (!onlyDisable)
        {
            yield return new WaitForSeconds(time);

            mouseLook.Init(this.transform, fpsCamera.transform);
            SetPlayerControls(true);
        }
        
    }

    private IEnumerator ResetParentAfterAnimation(float time)
    {
        yield return new WaitForSeconds(time);
        SwitchCameraParent(false);
    }


    private void CameraOffset(Transform headBoneTransform)
    {
        gameplayPosition.transform.localPosition = new Vector3(0, headBoneTransform.transform.localPosition.y + 0.00096f, headBoneTransform.transform.localPosition.z - 0.0015f);  
    }
    
    private void SwitchCameraParent(bool parentToHead)
    {
        if (parentToHead)
        {
            Debug.Log("TESTA");
            FPSCharacter.transform.SetParent(animationPosition);
        }
        else
        {
            Debug.Log("BANDIIIIIT");
            FPSCharacter.transform.SetParent(gameplayPosition);
        }
            
            
    }











































    /* ---- CONTROLS FUNCTIONS ---- */

    public IEnumerator DisablePlayerControlsForTime(float time)
    {
        GetComponent<CharacterController>().enabled = false;
        GetComponent<My_FPSController>().enabled = false;
        activePlayer = false;
        yield return new WaitForSeconds(time);
        GetComponent<CharacterController>().enabled = true;
        GetComponent<My_FPSController>().enabled = true;
        activePlayer = true;
    }

    public void DisablePlayerController(bool state)
    {
        GetComponent<CharacterController>().enabled = state;
        GetComponent<My_FPSController>().enabled = state;
    }

    public void SetKeyboardInput(bool state) //impedisce la pressione di alcuni tasti
    {
        keyboardInput = state;
    }

    public bool IsKeyboardActive()
    {
        return keyboardInput;
    }

    public void SetPlayerToActive(bool state) //impedisce personaggio di fare cose
    {
        activePlayer = state;
    }

    public bool IsPlayerActive()
    {
        return activePlayer;
    }

    /* ---- CAMERA FUNCTIONS ---- */

    public IEnumerator SetCameraToAnimPosition(float animationTime)
    {
        //Set camera child of the head
        FPSCharacter.transform.localRotation = Quaternion.identity; // SOSTITUIRE SEMPLICE RESET CON DOTWEEN
        FPSCharacter.transform.SetParent(gameplayPosition);
        FPSCharacter.transform.localPosition = Vector3.zero;
       
        //Wait till animation is end
        yield return new WaitForSeconds(animationTime);
       
        //Set camera child of the character
        FPSCharacter.transform.SetParent(headBonePosition);
        FPSCharacter.transform.localPosition = Vector3.zero;
    }

    public void SetCameraToHead(bool set)
    {
        if (set)
        {
            FPSCharacter.transform.localRotation = Quaternion.identity; // SOSTITUIRE SEMPLICE RESET CON DOTWEEN
            FPSCharacter.transform.SetParent(gameplayPosition);
            FPSCharacter.transform.localPosition = Vector3.zero;
        }
        else
        {
            FPSCharacter.transform.SetParent(headBonePosition);
            FPSCharacter.transform.localPosition = Vector3.zero;
        }
    }

    public void setGrabbedState(bool state)
    {
        animator.SetBool("Obj", state);
        grabbed = state;
    }
}
