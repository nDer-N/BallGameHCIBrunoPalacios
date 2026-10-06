using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject WinScreen;
    public GameObject ScoreScreen;
    public BallController ball;
    public TMP_Text scoreText;
    public Transform SpawnPoint;
    public GameObject Platform;
    public float rotateAmount  = 0.001f;
    public float rotateFreq = 1f;

    private float _rotateTimer;
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

    public void RestartScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    // Update is called once per frame
    void Update()
    {
        _rotateTimer += Time.deltaTime;

        if (_rotateTimer >= rotateFreq)
    {
        _rotateTimer = 0; 
        Platform.transform.Rotate(rotateAmount, 0f, 0f, Space.Self);
    }
    }
}
