using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {

    [SerializeField] private GameObject FPSCharacter;
    [SerializeField] private Transform cameraGameplayPosition;
    [SerializeField] private Transform cameraAnimationPosition;
    private Animator anim;
    private Transform camPosition;
    private int jumpStateHash = Animator.StringToHash("Base Layer.Jump");
    private bool grabbed = false;

    public float lifePoints;

    private bool menuState = false;

    private bool keyboardInput = true;
    private bool activePlayer = false;

    private void Start()
    {
        lifePoints = 100f;
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        bool running = Input.GetKey(KeyCode.LeftShift);
        float speed = ((running) ? 1 : 0.5f);
        bool jumping = Input.GetKeyDown(KeyCode.Space);
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

         if (!activePlayer) return;

        // Movement
        if (moveHorizontal != 0 || moveVertical != 0)
        {
            if (grabbed)
                anim.SetFloat("Motion Speed", speed);
            else
                anim.SetFloat("Motion Speed", speed);
        }
        else
        {
            anim.SetFloat("Motion Speed", 0f);
        }

        /*
        if (jumping && stateInfo.fullPathHash != jumpStateHash)
        {
            anim.SetTrigger("Jump");
            Debug.Log("Jumping");
        }
        */

        // Open menu
        if (Input.GetKeyDown(KeyCode.Tab) && stateInfo.fullPathHash != jumpStateHash)
        {
            menuState = !menuState;
            //menuCanvas.setActive(menuState);
            Cursor.visible = menuState;

            if (menuState)
                SetCameraToHead(menuState);
            else
                StartCoroutine(SetCameraToAnimPosition(1f));

            GetComponentInChildren<Animator>().SetBool("Watch", menuState);
            DisablePlayerController(!menuState);

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

    /* ---- CONTROLS FUNCTIONS ---- */

    public IEnumerator DisablePlayerControlsForTime(float time)
    {
        GetComponent<CharacterController>().enabled = false;
        GetComponent<My_FPSController>().enabled = false;
        keyboardInput = false;
        activePlayer = false;
        yield return new WaitForSeconds(time);
        GetComponent<CharacterController>().enabled = true;
        GetComponent<My_FPSController>().enabled = true;
        keyboardInput = true;
        activePlayer = true;
    }

    public void DisablePlayerController(bool state)
    {
        GetComponent<CharacterController>().enabled = state;
        GetComponent<My_FPSController>().enabled = state;
        keyboardInput = state;
    }

    public void SetKeyboardInput(bool state)
    {
        keyboardInput = state;
    }

    public bool IsKeyboardActive()
    {
        return keyboardInput;
    }

    public void SetPlayerToActive(bool state)
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
        FPSCharacter.transform.localRotation = Quaternion.identity;
        FPSCharacter.transform.SetParent(cameraAnimationPosition);
        FPSCharacter.transform.localPosition = Vector3.zero;
        
        //Wait till animation is end
        yield return new WaitForSeconds(animationTime);
        //Set camera child of the character
        FPSCharacter.transform.SetParent(cameraGameplayPosition);
        FPSCharacter.transform.localPosition = Vector3.zero;
    }

    public void SetCameraToHead(bool set)
    {
        if (set)
        {
            FPSCharacter.transform.SetParent(cameraAnimationPosition);
            FPSCharacter.transform.localPosition = Vector3.zero;
        }
        else
        {
            FPSCharacter.transform.SetParent(cameraGameplayPosition);
            FPSCharacter.transform.localPosition = Vector3.zero;
        }
    }

    public void setGrabbedState(bool state)
    {
        anim.SetBool("Obj", state);
        grabbed = state;
    }
}
