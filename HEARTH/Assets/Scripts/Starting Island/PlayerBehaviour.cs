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
    public GameObject menuCanvas;

    private bool keyboardInput = true;
    private bool activePlayer = true;

    [SerializeField] private int seedCount = 0;

    public enum animations { Watch, Lift, Walk, Run, Jump, StandUp};
    private int watchAnimHash = Animator.StringToHash("Watch");
    private int liftAnimHash = Animator.StringToHash("Lift");
    private int speedAnimHash = Animator.StringToHash("Motion Speed");
    private int objAnimHash = Animator.StringToHash("Obj");
    private int wakeAnimHash = Animator.StringToHash("Wake");
    private int jumpStateHash = Animator.StringToHash("Base Layer.Jump");

    private Vector3 standardHeadPosition = new Vector3 (-0.001649857f, 1.694621f, 0.2f);
    private Vector3 runHeadPostion = new Vector3 (-0.0016f, 1.572f, 0.38f);

    private void Start()
    {
        lifePoints = 100f;
        fpsCamera = Camera.main;
        animator = GetComponentInChildren<Animator>();
        mouseLook = this.GetComponent<My_FPSController>().getMouseLook();
        //Se sono su prima isola metto activePlayer a false all'inizio
    }

    private void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        bool running = Input.GetKey(KeyCode.LeftShift);
        float speed = ((running) ? 1f : 0.5f);
        Vector3 offset = standardHeadPosition; /* = ((running) ? runHeadPostion : standardHeadPosition);*/
        bool jumping = Input.GetKeyDown(KeyCode.Space);
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (!activePlayer && !menuState) return;

        // Movement
        if (moveHorizontal != 0 || moveVertical != 0)
        {
            offset = ((running) ? runHeadPostion : standardHeadPosition);
            animator.SetFloat(speedAnimHash, speed);
        }
        else
        {
            offset = standardHeadPosition;
            animator.SetFloat(speedAnimHash, 0f);
        }

        gameplayPosition.localPosition = offset;

        // Open menu
        if (Input.GetKeyDown(KeyCode.Tab) && !grabbed && this.GetComponent<CharacterController>().isGrounded)
        {
            menuState = !menuState;
            menuCanvas.SetActive(menuState);
            Cursor.visible = menuState;
            TriggerAnimation((int)animations.Watch);
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

    /* ---- SEEDS FUNCTIONS ---- */

    public void AddSeed()
    {
        seedCount++;
    }

    public void RemoveSeed()
    {
        seedCount--;
    }

    public int GetSeedCount()
    {
        return seedCount;
    }

    /* ---- ANIMATIONS ---- */

    public void TriggerAnimation(int animationIndex)
    {
        float animationTime = 0;
        bool onlyDisable = false;
        bool changeParent = false;
        Vector3 finalHeadPosition;
        Vector3 finalHeadRotation;

        switch (animationIndex)
        {
            case (int)animations.Watch:
                animationTime = 2f;
                finalHeadRotation = ((menuState) ? new Vector3(45f, 0f, 0f) : new Vector3(0f, 0f, 0f));
                onlyDisable = menuState;
                animator.SetBool(watchAnimHash, menuState);
                fpsCamera.transform.DOLocalRotate(finalHeadRotation, animationTime);
                break;

            case (int)animations.Lift:
                animationTime = 5f;
                finalHeadPosition = new Vector3(-0.03f, -1f, 0.45f);
                finalHeadRotation = new Vector3(25f, 0f, 0f);
                onlyDisable = false;
                animator.SetTrigger(liftAnimHash);
                FPSCharacter.transform.DOPunchPosition(finalHeadPosition, animationTime, 0, 1);
                FPSCharacter.transform.DOPunchRotation(finalHeadRotation, animationTime, 0 , 1); 
                break;

            case (int)animations.StandUp:
                animationTime = 11f;
                changeParent = activePlayer;
                onlyDisable = true;
                animator.SetTrigger(wakeAnimHash);
                break;


            default:
                break;
        }
        
        StartCoroutine(DisableControlsDuringAnimation(animationTime, onlyDisable));
        if(changeParent)
            StartCoroutine(SwitchCameraParentInAnimations(animationTime));
    }

    private void SetPlayerControls(bool state)
    {
        this.GetComponent<My_FPSController>().enabled = state;
        this.GetComponent<CharacterController>().enabled = state;
        SetPlayerToActive(state);
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

    public IEnumerator SwitchCameraParentInAnimations(float time)
    {
        SetPlayerToActive(false);
        SetCameraToAnimation(true);

        yield return new WaitForSeconds(time);

        SetPlayerToActive(true);
        SetCameraToAnimation(false);
    }
    
    public void SetCameraToAnimation(bool parentToHead)
    {
        if (parentToHead)
        {
            FPSCharacter.transform.SetParent(animationPosition);
        }
        else
        {
            FPSCharacter.transform.SetParent(gameplayPosition);
        }

        FPSCharacter.transform.localPosition = Vector3.zero;
    }

    /* ---- CONTROLS FUNCTIONS ---- */

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

    public void setGrabbedState(bool state)
    {
        animator.SetBool("Obj", state);
        grabbed = state;
    }
}
