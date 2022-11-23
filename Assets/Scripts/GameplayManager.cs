using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
    public BikeController bikeController;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI recordText;
    public Button startButton;

    private float initialTime;
    private float recordTime;
    private int secondsToStart = 3;

    private void Start()
    {
        timeText.text = recordText.text = string.Empty;

        bikeController.onKilled += RestartLevel;
        bikeController.onReachedEndOfLevel += EndGame;
        bikeController.enabled = false;

        recordTime = PlayerPrefs.GetFloat("recordLevel" + SceneManager.GetActiveScene().buildIndex, 0);

        if (recordTime > 0)
            recordText.text = "Record: " + recordTime.ToString("00.00") + "s";

    }
    
    private void Update()
    {
        if (bikeController.enabled)
        {
            timeText.text = "Time: " + (Time.time - initialTime).ToString("00.00") + "s";
        }
    }

    public void StartGame()
    {
        startButton.gameObject.SetActive(false);
        timeText.text = secondsToStart.ToString();
        InvokeRepeating(nameof(Countdown), 1, 1);
    }

    private void Countdown()
    {
        secondsToStart--;
        if (secondsToStart <= 0)
        {
            CancelInvoke();
            OnGameStarted();
        }
        else
            timeText.text = secondsToStart.ToString();
    }


    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnGameStarted()
    {
        bikeController.enabled = true;
        initialTime = Time.time;
        timeText.text = string.Empty;
    }

    private void EndGame()
    {
        startButton.gameObject.SetActive(true);
        bikeController.enabled = false;
        timeText.text = "FINAL! " + (Time.time - initialTime).ToString("00.00") + "s";

        if ((Time.time - initialTime) < recordTime)
        {
            PlayerPrefs.SetFloat("recordLevel" + SceneManager.GetActiveScene().buildIndex, (Time.time - initialTime));
            timeText.text = "NEW RECORD! " + (Time.time - initialTime).ToString("00.00") + "s";
        }
        else
        {
            timeText.text = "FINAL! " + (Time.time - initialTime).ToString("00.00") + "s";
        }

    }

}
