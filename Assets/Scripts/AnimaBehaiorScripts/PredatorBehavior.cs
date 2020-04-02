using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredatorBehavior : AnimalBehavior
{
    // a list of all species this animal can eat (these are the tags of the animals)
    public List<string> preyList;
    // the distance this predator must be to hunt prey
    public float huntRadius;
    public float huntSpeed;
    private GameObject currentPrey;

    private Hunger hunger;

    private void Start()
    {
        base.Start();
        hunger = GetComponent<Hunger>(); 
    }

    private void FixedUpdate()
    {
        Roam();
        Hunt();
    }

    // when prey is within a certain distance from a predator, it will hunt it
    public void Hunt()
    {
        //Only hunt if the animal is hungry
        if (!currentPrey && hunger.isHungry())
        {
            // check for prey in a certain radius
            Collider[] colliders = Physics.OverlapSphere(animalBody.position, huntRadius);
            foreach (Collider c in colliders)
            {
                // if there is viable prey in the radius, hunt it
                if (preyList.Contains(c.gameObject.tag))
                {
                    currentPrey = c.gameObject;
                    break;
                }
            }
        }

        if (currentPrey)
        {
            // chase down the prey
            animalBody.position = Vector3.MoveTowards(animalBody.position, currentPrey.transform.position, huntSpeed * Time.fixedDeltaTime);
        }
        
    }

    // When this predator collides with a prey, it should eat it (destroy the instance of prey)
    private void OnCollisionEnter(Collision collision)
    {
        if (preyList.Contains(collision.gameObject.tag))
        {
            hunger.Eat(); // Increments hunger points by 1 
            Destroy(collision.gameObject);
            currentPrey = null;
        }
    }
}
