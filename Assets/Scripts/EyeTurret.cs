using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class EyeTurret : MonoBehaviour
{
    [SerializeField]
    Transform rayEmitter;
    [SerializeField]
    LookAtConstraint aimCon;
    Animator anim;
    [SerializeField]
    GameObject gunk;
    
    private RaycastHit hit;
    private Quaternion origRot;
    bool hasNotFired;
    bool foundPlayer;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        aimCon = GetComponent<LookAtConstraint>();
        origRot = gameObject.transform.rotation;
        hasNotFired = true;
        foundPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Physics.Raycast(rayEmitter.position, -rayEmitter.transform.forward, out hit))
        {
            FindPlayer(hit.collider);
            Debug.DrawRay(rayEmitter.position, -rayEmitter.transform.forward * 100);
        }
    }

    void FindPlayer(Collider collider)
    {
        if (hit.collider.CompareTag("Player"))
        {
            // play animation
            StartCoroutine(Wait());
            aimCon.constraintActive = true;
            anim.SetBool("HasSeen", true);

            if(foundPlayer)
            {
                ShootPlayer();
            }
            
        }
        else
        {
            aimCon.constraintActive = false;
            anim.SetBool("HasSeen", false);
            gameObject.transform.rotation = origRot;
            foundPlayer = false;
        }
    }

    void ShootPlayer()
    {
        if (hasNotFired)
        {
            GameObject gunkBullet = Instantiate(gunk, rayEmitter.position, rayEmitter.rotation, null);

            gunkBullet.GetComponent<Rigidbody>().AddForce(-rayEmitter.transform.forward * 10);
            hasNotFired = false;
            StartCoroutine(FireAgain());
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        foundPlayer = true;
    }
    IEnumerator FireAgain()
    {
        yield return new WaitForSeconds(1.5f);
        hasNotFired = true;
    }
}
