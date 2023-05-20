using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    [SerializeField] float speed = 5f;

    Rigidbody rb;
    Vector3 movement;
    Animator animator;


    private void Awake() {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        animator.SetFloat("Horizontal", 1f);
    }

    private void Update() {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.z = Input.GetAxisRaw("Vertical");
        movement.Normalize();
        rb.velocity = Vector3.zero;

        if(movement.x != 0)
        {
            animator.SetFloat("Horizontal", movement.x);
        }        
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    private void FixedUpdate() {
        rb.MovePosition(Time.fixedDeltaTime * speed * movement + rb.position);
    }
}
