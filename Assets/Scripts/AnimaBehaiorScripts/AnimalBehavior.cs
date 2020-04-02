using UnityEngine;

// A general class for animal behavior, to be derived by every specific animal script (DeerBehavior, WolfBehavior, etc.)
public class AnimalBehavior : MonoBehaviour
{
    public Transform animalBody;
    public float maxSpeed;

    private Vector3 spawnPosition;
    private float timeSinceDirectionChange;
    private float timeBetweenDirectionChange;
    private Vector3 roamDirection;
    private float currentSpeed;
    // the maximum distance this animal can roam from its spawn point
    private float roamDistance;

    protected void Start()
    {
        maxSpeed = 1f;
        spawnPosition = animalBody.position;
        timeSinceDirectionChange = 0f;
        timeBetweenDirectionChange = 0f;
        roamDistance = 2f;
        GetComponent<Rigidbody>().freezeRotation = true; 
    }

    private void FixedUpdate()
    {
        Roam();
    }

    // Just a function that makes the animal somewhat randomly roam around its spawn position
    public void Roam()
    {
        timeSinceDirectionChange += Time.fixedDeltaTime;

        // if the animal has roamed in the same direction long enough, time to change it
        if (timeSinceDirectionChange >= timeBetweenDirectionChange)
        {
            // The new time between when the animal should choose a new direction to roam
            timeBetweenDirectionChange = Random.Range(2f, 5f);
            // reset time since last change
            timeSinceDirectionChange = 0f;

            // Get a new roam direction
            roamDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
            roamDirection.Normalize();

            // have chance for the animal to sit still for a bit
            if (Random.value < .3 && currentSpeed != 0f)
            {
                currentSpeed = 0f;
                // dont want them still for too long
                timeBetweenDirectionChange = Random.Range(1f, 3f);
            }
            else
            {
                currentSpeed = Random.Range(.5f, maxSpeed);
            }
        }

        // move the animal
        animalBody.position += roamDirection * currentSpeed * Time.fixedDeltaTime;

        // check if the animal has roamed too far
        if (Vector3.Distance(animalBody.position, spawnPosition) > roamDistance)
        {
            // if so, turn the animal around
            currentSpeed *= -1;
        }

    }
}
