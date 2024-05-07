using UnityEngine;
using TMPro;
//My Directives

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;  // Singleton pattern to easily access the instance

    public TMP_Text scoreText;  // Reference to the TextMeshPro GUI element
    public Transform ballSpawnPoint;  // Spawn point for the ball
    public GameObject bowlingBallPrefab;  // Bowling ball prefab
    private int score = 0;  // Current score
    
    void Start() 
    {
        scoreText.text = "Score: 0";  // Initialize the score display
    }

    void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PinKnockedOver()
    {
        score++;
        Debug.Log("Pin knocked over. New score: " + score);  // Confirm method execution and score increment
        scoreText.text = "Score: " + score;  // Update the score display
        Invoke("RespawnBall", 2);  // Delay the respawn to allow the score to be updated
    }

    void RespawnBall()
    {
        Instantiate(bowlingBallPrefab, ballSpawnPoint.position, Quaternion.identity);  // Respawn the ball
    }
}
