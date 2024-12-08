using UnityEngine;

public class VictoryZone : MonoBehaviour
{
    [Header("Player Settings")]
    public string playerTag = "Player";

    [Header("Message Settings")]
    public string victoryMessage = "You Win! Thank you for playing.";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            Debug.Log(victoryMessage);

            Time.timeScale = 0f;
        }
    }
}
