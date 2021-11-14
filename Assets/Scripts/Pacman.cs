using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacman : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float rotateSpeed;
    [SerializeField] float rayDistance;
    [SerializeField] LayerMask layerMask;

    Rigidbody rb;
    float vInput;
    float hInput;
    Vector3 currentDirection;
    Vector3 nextDirection;
    float timer;
    float nextRotationAngle;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        vInput = Input.GetAxis("Vertical");
        hInput = Input.GetAxis("Horizontal");

        //change direction if direction is changed less than half seconds ago to minimize error in input
        if (!CheckCollision(nextDirection) && timer < .5f)
        {
            ChangeDirection(nextDirection, nextRotationAngle);
        }
        rb.velocity = currentDirection * moveSpeed;

        //change direction according to input
        if (vInput > 0)
        {
            if (!CheckCollision(Vector3.forward))
            {
                ChangeDirection(Vector3.forward, 0f);
            }
            else
            {
                //store the direction player attempted to turn towards
                nextDirection = Vector3.forward;
                nextRotationAngle = 0f;
                //reset timer 
                timer = 0;
            }
        }
        else if (vInput < 0)
        {
            if (!CheckCollision(Vector3.back))
            {
                ChangeDirection(Vector3.back, 180f);
            }
            else
            {
                nextDirection = Vector3.back;
                nextRotationAngle = 180f;
                timer = 0;
            }
        }
        if (hInput > 0)
        {
            if (!CheckCollision(Vector3.right))
            {
                ChangeDirection(Vector3.right, 90f);
            }
            else
            {
                nextDirection = Vector3.right;
                nextRotationAngle = 90f;
                timer = 0;
            }
        }
        else if (hInput < 0)
        {
            if (!CheckCollision(Vector3.left))
            {
                ChangeDirection(Vector3.left, -90f);
            }
            else
            {
                nextDirection = Vector3.left;
                nextRotationAngle = -90f;
                timer = 0;
            }
        }
    }

    /// <summary>
    /// Change player movement direction and rotation about Y axis
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="angle"></param>
    void ChangeDirection(Vector3 direction, float angle)
    {
        currentDirection = direction;
        transform.rotation = Quaternion.Euler(Vector3.up * angle);
    }

    /// <summary>
    /// Stop the player and disable movement
    /// </summary>
    public void Die()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = new Vector3(0, rotateSpeed, 0);
        GetComponent<SphereCollider>().enabled = false;
        this.enabled = false;
    }

    /// <summary>
    /// Check if there is obstacle in required direction
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    bool CheckCollision(Vector3 direction)
    {
        return (Physics.BoxCast(transform.position, new Vector3(0.4f, 0.2f, .4f), direction, Quaternion.identity, rayDistance, layerMask));
    }
}
