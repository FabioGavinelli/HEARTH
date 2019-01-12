using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine;

public class TriggerAnimation : MonoBehaviour
{
    private bool state = false;
    private Animator animator;
    public GameObject FPSCharacter;
    public Transform gameplayTransform;
    public Transform animationTransform;

    // Animations Hashes
    int watchHash = Animator.StringToHash("Watch");
    int liftHash = Animator.StringToHash("Lift");


    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            state = !state;
            animator.SetBool("Watch", state);
            StartCoroutine(SwitchCameraPosition(2f));
        }
       
    }

    public void TriggerAnAnimation(int n)
    {
        
    }

    private IEnumerator ResetCameraPosition(Transform endPosition)
    {
        if (endPosition == null) yield break;

        FPSCharacter.transform.DOLocalMove(new Vector3(0, 0, endPosition.localPosition.z), 1f);
        FPSCharacter.transform.DOLocalRotate(new Vector3(endPosition.localPosition.x, 0, 0), 1f);

        yield return new WaitForSeconds(1.5f);


    }

    private void StartAnimation(int n)
    {

    }

    private IEnumerator SwitchCameraPosition(float time)
    {
        //Attacco FPSCHARACTER a HEAD BONE
        FPSCharacter.transform.SetParent(animationTransform);
        FPSCharacter.transform.localRotation = Quaternion.identity;
        //Sposta Camera da GAMEPLAY POS a ANIMATION POS
       // FPSCharacter.transform.DOLocalRotate(new Vector3(animationTransform.localRotation.x, 0, 0), 1f);
       // FPSCharacter.transform.DOLocalMove(new Vector3(0, 0, animationTransform.localPosition.z), 1f);

        yield return new WaitForSeconds(time);

        //Attacco FPSCHARACTER a BANDIT
        FPSCharacter.transform.SetParent(gameplayTransform);
        //Sposta Camera da ANIMATION POS a GAMEPLAY POS
       // FPSCharacter.transform.DOLocalRotate(new Vector3(gameplayTransform.localRotation.x, 0, 0), 1f);
       // FPSCharacter.transform.DOLocalMove(new Vector3(0, 0, gameplayTransform.localPosition.z), 1f);

        //Resetto posizione mouse
        this.GetComponent<My_FPSController>().ResetView();

    }
}
