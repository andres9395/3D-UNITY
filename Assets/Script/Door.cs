using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    private Animator animator;
    private bool isOpen = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OpenDoor(bool willOpen = true)
    {
        if (willOpen != isOpen)
        {
            isOpen = willOpen;
            animator.SetBool("opened", isOpen);
        }
    }
}
