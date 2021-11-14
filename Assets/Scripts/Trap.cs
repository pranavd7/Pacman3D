using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] int points;
    [SerializeField] float waitTime;
    [SerializeField] float vulnerableTime;
    [SerializeField] float moveSpeed;
    [SerializeField] Transform[] wayPoints;

    Rigidbody rb;
    MeshRenderer meshRenderer;

    bool canDie;
    float moveTime; //time after trap starts moving
    float canDieTime; //time after vulnerability is cancelled
    int currentWaypointIndex;
    Vector3 moveDirection;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if (Time.time > moveTime)
        {
            if (wayPoints.Length != 0)
            {
                moveDirection = wayPoints[currentWaypointIndex].position - transform.position;
                rb.velocity = moveDirection.normalized * moveSpeed;
                if (moveDirection.magnitude < 0.05)
                {
                    rb.velocity = Vector3.zero;
                    moveTime = Time.time + waitTime;
                    currentWaypointIndex++;
                    currentWaypointIndex %= wayPoints.Length;
                }
            }
        }

        if (Time.time > canDieTime)
        {
            EnableDeath(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            if (canDie)
            {
                FindObjectOfType<GameManager>().AddPoints(points, true);
                Destroy(gameObject);
            }
            else
            {
                FindObjectOfType<GameManager>().EndGame();
            }
        }
    }

    /// <summary>
    /// Enable or disable death of trap and set time till vulnerability exists and change mesh renderer color
    /// </summary>
    /// <param name="canDie"></param>
    public void EnableDeath(bool canDie)
    {
        this.canDie = canDie;
        if (canDie)
        {
            canDieTime = Time.time + vulnerableTime;
            meshRenderer.material.color = Color.white;
        }
        else
        {
            meshRenderer.material.color = Color.red;
        }
    }
}
