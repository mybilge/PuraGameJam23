using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform[] patrolPointsTf;
    NavMeshAgent agent;
    Vector3? destination;

    [SerializeField] float normalSpeed;
    [SerializeField] float chaseSpeed;

    [SerializeField] float radius;
    [SerializeField] float stunTime = 0.5f;
    float temp = 0;
    float lastSpeed;

    bool chasing;

    Transform player;
    Animator animator;
    Rigidbody rb;

    bool green = false;

    float tempGreen = 0f;

    private void Awake() {

        GetComponent<SphereCollider>().radius = radius;
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        agent.speed = normalSpeed;
        lastSpeed = normalSpeed;
        animator = GetComponent<Animator>();
        animator.SetFloat("Horizontal", 1f);
    }

    private void Update() {

        if(temp>0)
        {
            temp -= Time.deltaTime;
            agent.speed = 0;
            return;            
        }

        if(green)
        {
            tempGreen -= Time.deltaTime;
            if(tempGreen<0)
            {
                green = false;
            }
            return;
        }
        rb.isKinematic = true;

        if (agent.velocity.x != 0)
        {
            animator.SetFloat("Horizontal", Mathf.Clamp(agent.velocity.x,-1f,1f));
        }
        animator.SetFloat("Speed", agent.velocity.sqrMagnitude);

        if(chasing)
        {
            agent.speed = chaseSpeed;
            agent.SetDestination(player.position);
            return;
        }       

        if(destination == null)
        {
            destination = patrolPointsTf[Random.Range(0,patrolPointsTf.Length)].position;
           
            agent.SetDestination(destination.Value);
            agent.speed = normalSpeed;
        }
        else{
            if(Vector3.Distance(transform.position,destination.Value)<0.1f)
            {
                destination = null;
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.TryGetComponent<Player>(out Player p))
        {
            chasing = true;
            player = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player p))
        {
            chasing = false;
            destination = null;
        }
    }

    private void OnCollisionEnter(Collision other) {
    
        if (other.gameObject.TryGetComponent<Player>(out Player p))
        {
            p.HasarAl();
            temp = stunTime;
        }
    }

    public IEnumerator GeriSek(Vector3 impactPoint, float force, float waitTime)
    {
        agent.speed =0;
        rb.isKinematic = false;
        green = true;
        Vector3 dir = transform.position - impactPoint;
        dir.y = 0;
        rb.AddForce(force*dir.normalized,ForceMode.Impulse);
        tempGreen = waitTime;
        yield return new WaitForSeconds(waitTime);
        rb.isKinematic = true;
        green = false;
        
        if(player == null)
        {
            destination = patrolPointsTf[Random.Range(0, patrolPointsTf.Length)].position;
            agent.SetDestination(destination.Value);
            agent.speed = normalSpeed;
            chasing = false;
        }
        else{
            if(Vector3.Distance(player.position, transform.position)<radius)
            {
                chasing = true;
                destination = null;
                agent.SetDestination(player.position);
                agent.speed = chaseSpeed;
            }

            else{
                destination = patrolPointsTf[Random.Range(0, patrolPointsTf.Length)].position;
                agent.SetDestination(destination.Value);
                agent.speed = normalSpeed;
                chasing = false;
            }
        }
    }
}