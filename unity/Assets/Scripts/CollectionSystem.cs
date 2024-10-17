using UnityEngine;

public class CollectionSystem : MonoBehaviour
{
    public static CollectionSystem Instance { get; private set; }

    private int score = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CollectItem(int value)
    {
        score += value;
        Debug.Log($"Collected item! Score: {score}");
        // You can add events or UI updates here
    }

    public int GetScore()
    {
        return score;
    }
}