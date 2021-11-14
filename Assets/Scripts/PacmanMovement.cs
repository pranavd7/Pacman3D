using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float rotateSpeed;
    [SerializeField] float rayDistance;
    [SerializeField] LayerMask layerMask;

    Rigidbody rb;
    Animator animator;
    float vInput;
    float hInput;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        vInput = Input.GetAxis("Vertical");
        hInput = Input.GetAxis("Horizontal");
        if (vInput > 0)
        {
            if (!CheckCollision(Vector3.forward))
            {
                transform.rotation = Quaternion.Euler(Vector3.up * 0f);
                rb.velocity = Vector3.forward * moveSpeed;
            }
        }
        else if (vInput < 0)
        {
            if (!CheckCollision(Vector3.back))
            {
                transform.rotation = Quaternion.Euler(Vector3.up * 180f);
                rb.velocity = Vector3.back * moveSpeed;
            }
        }
        if (hInput > 0)
        {
            if (!CheckCollision(Vector3.right))
            {
                transform.rotation = Quaternion.Euler(Vector3.up * 90f);
                rb.velocity = Vector3.right * moveSpeed;
            }
        }
        else if (hInput < 0)
        {
            if (!CheckCollision(Vector3.left))
            {
                transform.rotation = Quaternion.Euler(Vector3.up * -90f);
                rb.velocity = Vector3.left * moveSpeed;
            }
        }
    }

    public void Die()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = new Vector3(0, rotateSpeed, 0);
        GetComponent<SphereCollider>().enabled = false;
        this.enabled = false;
    }

    bool CheckCollision(Vector3 direction)
    {
        return (Physics.BoxCast(transform.position, new Vector3(0.4f, 0.2f, .4f), direction, Quaternion.identity, rayDistance, layerMask));
    }
}
