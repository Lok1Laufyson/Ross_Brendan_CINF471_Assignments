using UnityEngine;



public class Enemy : MonoBehaviour
{

    public float rotationSpeed;

    int health = 2;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }

        transform.Rotate(0, rotationSpeed, 0);
    }

    private void OnTriggerEnter()
    {
        health -= 1;
    }

}
