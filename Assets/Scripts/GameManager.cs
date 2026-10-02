using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject WinScreen;
    public GameObject ScoreScreen;
    public BallController ball;
    public TMP_Text scoreText;
    public Transform SpawnPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ball.OnCollected += checkScore;
        ball.OnFall += Respawn;
        checkScore(0, 1);
    }



    private void checkScore(int value, int value2)
    {
        if(value == value2)
        {
            WinScreen.SetActive(true);
            ScoreScreen.SetActive(false);
        }
        else
        {
            scoreText.text = value.ToString();
        }
        
    }

    private void Respawn()
    {
        ball.transform.SetPositionAndRotation(SpawnPoint.position,SpawnPoint.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
