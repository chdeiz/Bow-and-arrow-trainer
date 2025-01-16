using UnityEngine;

/**
    Simple Score manager, no UI, just console
*/
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance; // Singleton for ease of use
    private int totalScore = 0;

    private void Awake()
    {
        // Set up singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int score)
    {
        totalScore += score;
        Debug.Log("Score added: " + score + " | Total Score: " + totalScore);
    }

    public int GetTotalScore()
    {
        return totalScore;
    }
}
