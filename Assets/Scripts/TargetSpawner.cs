using UnityEngine;
using System.Collections.Generic; // Needed for List

public class TargetSpawner : MonoBehaviour
{
    public GameObject targetPrefab; // assign prefab of dummy
    public GameObject arrowPrefab;  // assign prefab of arrow
    public int numberOfTargets = 5; // amount targets
    
    // for testing purposes
    // public float arrowSpawnDelay = 5f; 

// this is the boxcollider of my cube where the targets are spawned, 
// is a trigger
    private BoxCollider boxCollider;
    private List<Vector3> targetPositions = new List<Vector3>(); // store positions

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();

        // Ensure the boxCollider is a trigger
        if (boxCollider == null)
        {
            //Debug.LogError("Cube Collider is null");
            return;
        } else if (!boxCollider.isTrigger){
            boxCollider.isTrigger = true;
        }

        SpawnTargets();

        // testing: schedule arrow spawner
        // Invoke(nameof(SpawnArrowsAtTargetPositions), arrowSpawnDelay);
    }

    void SpawnTargets()
    {
        for (int i = 0; i < numberOfTargets; i++)
        {
            // Generate a random position inside the collider bounds
            Vector3 randomPosition = GetRandomPositionInsideCollider();

            // Define the rotation so the targets face the player (-90° on Y-axis)
            Quaternion targetRotation = Quaternion.Euler(0, -90, 0);

            // Instantiate the target at the random position with the specified rotation
            GameObject target = Instantiate(targetPrefab, randomPosition, targetRotation);

            // Ensure the target is activated
            target.SetActive(true);

            // Store the position for spawning arrows later
            targetPositions.Add(randomPosition);
        }
    }
/**

    void SpawnArrowsAtTargetPositions()
{
    Vector3 arrowOffset = new Vector3(0.86f, 1f, -0.3f); // Offset values

    foreach (Vector3 position in targetPositions)
    {
        // Apply the offset to the arrow's position
        Vector3 arrowPosition = position + arrowOffset;

        // Define a default arrow rotation
        Quaternion arrowRotation = Quaternion.identity;

        // Instantiate an arrow at the adjusted position
        GameObject arrow = Instantiate(arrowPrefab, arrowPosition, arrowRotation);

        // Ensure the arrow is activated
        arrow.SetActive(true);
    }

    Debug.Log("Arrows spawned with offset at target positions.");
}
*/

    // get random positions for spawning the targets
    Vector3 GetRandomPositionInsideCollider()
    {
        // Get local size of the collider
        Vector3 size = boxCollider.size;
        Vector3 center = transform.position + boxCollider.center;

        // generate random positions within bounds
        float x = Random.Range(center.x - size.x / 2 * transform.localScale.x, center.x + size.x / 2 * transform.localScale.x);
        float y = Random.Range(center.y - size.y / 2 * transform.localScale.y, center.y + size.y / 2 * transform.localScale.y);
        float z = Random.Range(center.z - size.z / 2 * transform.localScale.z, center.z + size.z / 2 * transform.localScale.z);

        return new Vector3(x, y, z);
    }
}
