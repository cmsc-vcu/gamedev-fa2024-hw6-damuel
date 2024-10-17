using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int value = 1;
    public float rotationSpeed = 50f;

    private void Update()
    {
        // Rotate the collectible
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CollectionSystem.Instance.CollectItem(value);
            Destroy(gameObject);
        }
    }
}