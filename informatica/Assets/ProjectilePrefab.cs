using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5f;  
    public float lifetime = 3f;
    public int damage = 1;  

    private Rigidbody2D rb;
    public Collider2D collider2d;
    GameObject Player;

    void Start()
    {
        Player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>(); 
        rb.gravityScale = 0;
        rb.angularVelocity = 0;

        Destroy(gameObject, lifetime);
    }

    void FixedUpdate()
    {
        Vector2 direction = (Player.transform.position - transform.position).normalized;
        rb.velocity = direction * speed;
        rb.angularVelocity = 0;
    }

    void OnTriggerEnter2D(Collider2D other)  
    {
        if (other.CompareTag("Player"))  
        {
            PlayerController Player = other.GetComponent<PlayerController>();  
            if(Player != null){
                Player.Health -= damage;
            }
            collider2d.enabled = false;
            Destroy(gameObject);
        }
    }
}