using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]

public class Grabbable : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Collider _collider;
    private Transform _originalParent;

    public Transform OriginalParent
    {
        get { return _originalParent; }
        protected set { _originalParent = value; }
    }    
    // Use this for initialization
    void Start ()
    {
        _collider = GetComponent<Collider>();
        _rigidbody = GetComponent<Rigidbody>();
        _originalParent = transform.parent;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Grab(GameObject grabber)
    {
        _collider.enabled = false;
        _rigidbody.isKinematic = true;
    }

    public void Drop()
    {
        _rigidbody.isKinematic = false;
        _rigidbody.useGravity = true;
        _rigidbody.AddForce(Vector3.forward * 50, ForceMode.Impulse);
        StartCoroutine(WaitEnableCollider());
        //_collider.enabled = true;
    }

    public IEnumerator WaitEnableCollider()
    {
        yield return new WaitForSeconds(0.5f);
        _collider.enabled = true;
    }
}
