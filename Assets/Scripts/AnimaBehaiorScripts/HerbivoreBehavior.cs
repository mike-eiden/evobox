using UnityEngine;
using System.Collections.Generic; 

public class HerbivoreBehavior : AnimalBehavior
{
    // a list of all plants this animal can eat (these are the tags of the plants)
    public List<string> foodList;
    // the distance this herbivore must be to eat a plant
    public float huntRadius;
    public float huntSpeed;
    private GameObject currentPlant;

    private Hunger hunger;

    private void Start()
    {
        base.Start();
        hunger = GetComponent<Hunger>(); 
    }

    private void FixedUpdate()
    {
        Debug.Log(hunger.hungerPoints);
        Roam();
        Hunt();
    }

    // when prey is within a certain distance from a predator, it will hunt it
    public void Hunt()
    {
        //Only hunt if the animal is hungry
        if (!currentPlant && hunger.isHungry())
        {
            // check for prey in a certain radius
            Collider[] colliders = Physics.OverlapSphere(animalBody.position, huntRadius);
            foreach (Collider c in colliders)
            {
                // if there is viable prey in the radius, hunt it
                if (foodList.Contains(c.gameObject.tag))
                {
                    // Check that this bush is not being fed on by a different animal
                    if (!c.gameObject.GetComponent<BerryManager>().BerriesLock)
                    {
                        // This lock needs to be set so that only 1 animal can feed from a bush at a time
                        c.gameObject.GetComponent<BerryManager>().BerriesLock = true; 
                        currentPlant = c.gameObject;
                        break;
                    }
                }
            }
        }

        if (currentPlant)
        {
            // chase down the plant
            animalBody.position = Vector3.MoveTowards(animalBody.position, currentPlant.transform.position, huntSpeed * Time.fixedDeltaTime);
        }
        
    }

    // When this predator collides with a prey, it should eat it (destroy the instance of prey)
    private void OnCollisionEnter(Collision collision)
    {
        if (foodList.Contains(collision.gameObject.tag))
        {
            hunger.Eat(); // Increments hunger points by 1 
            if (collision.gameObject.GetComponent<BerryManager>())
            {
                collision.gameObject.GetComponent<BerryManager>().EatBerries();
            }
            currentPlant = null;
        }
    }
}
