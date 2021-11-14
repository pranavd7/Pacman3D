using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biscuit : MonoBehaviour
{
    [SerializeField] int points;
    [SerializeField] bool specialBiscuit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<GameManager>().AddPoints(points);
            if (specialBiscuit)
            {
                Trap[] traps = FindObjectsOfType<Trap>();
                foreach (Trap trap in traps)
                {

                }
            }
            Destroy(gameObject);
        }
    }
}
