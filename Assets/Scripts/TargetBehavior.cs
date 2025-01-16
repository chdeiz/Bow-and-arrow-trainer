using UnityEngine;

public class TargetBehavior : MonoBehaviour
{
    public float maxScore = 100f; // Max score
    public float timePenaltyRate = 3f; // point penalty per second
    public int baseScore = 0; // min score

    private float spawnTime; // target spawn time
    private bool isHit = false; // only one hit otherwise the collider counts multiple hits at once

    private void Start()
    {
        spawnTime = Time.time;
    }

    private void OnTriggerEnter(Collider other)
    {
        // check if arrow entered the collider
        if (other.CompareTag("Arrow") && !isHit)
        {
            isHit = true; // target hit

            // calc score
            float timeSinceSpawned = Time.time - spawnTime;
            int score = CalculateScore(timeSinceSpawned);
            ScoreManager.Instance.AddScore(score);

            // deactivate target
            gameObject.SetActive(false);

        }else Debug.Log("Something other than arrow entered the collider!");
    }

    private int CalculateScore(float timeSinceSpawned)
    {
        float timePenalty = timeSinceSpawned * timePenaltyRate;
        int finalScore = Mathf.Max(baseScore, Mathf.RoundToInt(maxScore - timePenalty));

        return finalScore;
    }
}
