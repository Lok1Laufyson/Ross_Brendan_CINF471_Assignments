using UnityEngine;

public class Collectible : MonoBehaviour
{
    public float rotationSpeed;
    public GameObject onCollectEffect;

    private Rigidbody rb; // Reference to player's Rigidbody.

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Access player's Rigidbody. 
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotationSpeed, 0);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {

            // Destroy the collectible
            Destroy(gameObject);

        

        }

    }

}
