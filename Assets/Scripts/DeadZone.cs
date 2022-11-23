using UnityEngine;

public class DeadZone : MonoBehaviour
{
    public GameplayManager gameplayManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameplayManager.RestartLevel();
        }
    }
}