using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] int points;
    [SerializeField] bool specialFood;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<GameManager>().AddPoints(points, false); ;
            if (specialFood)
            {
                //make traps vulnerable/ can be eaten by player for points
                Trap[] traps = FindObjectsOfType<Trap>();
                foreach (Trap trap in traps)
                {
                    trap.EnableDeath(true);
                }
            }
            Destroy(gameObject);
        }
    }
}
