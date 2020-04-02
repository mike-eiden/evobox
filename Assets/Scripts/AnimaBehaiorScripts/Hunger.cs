using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunger : MonoBehaviour
{

    public int hungerPoints = 5;

    private int maxHungerPoints; 
    void Start()
    {
        maxHungerPoints = hungerPoints; 
        // Every 5 seconds there is a 1 in 10 chance to be hungry. 
        InvokeRepeating(nameof(loseHungerPoint), 1f, 5f);
    }

    // 1 in 10 chance to lose a hunger point.  This function should be called on delay
    private void loseHungerPoint()
    {
        // If hunger points is less than or equal to zero the animal dies
        if (hungerPoints > 0)
        {
            if (Random.Range(0, 10) == 5)
            {
                hungerPoints--;
            }
        }
        else
        {
            Die();
        }
    }

    // Used by animal behavior scripts to see if the animal should eat
    public bool isHungry()
    {
        return maxHungerPoints != hungerPoints; 
    }

    // Used by animal behavior scripts to increase hunger points
    public void Eat()
    {
        // Double check that the animal can't be too full. 
        if (hungerPoints < maxHungerPoints)
        {
            hungerPoints++;
        }
    }

    void Die()
    {
        //TODO Notify user of death in some way (Animation? Event Log?)
        Debug.Log(gameObject.name + " died of hunger");
        Destroy(gameObject);
    }
}
