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

        if(movement.x != 0)
        {
            animator.SetFloat("Horizontal", movement.x);
        }        
        animator.SetFloat("Speed", movement.sqrMagnitude);
        if (movement.sqrMagnitude != 0)
        {
            rb.velocity = Vector3.zero;
        }
    }

    private void FixedUpdate() {

        if(!GetComponent<Player>().isHooking)
        {
            rb.MovePosition(Time.fixedDeltaTime * speed * movement + rb.position);
        }
        else{
            rb.velocity = Vector3.zero;
        }
        
    }
}
