using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject Player;
    public float speed;
    private float distance;
    public float distanceBetween;
    public int damage = 1;
    Rigidbody2D rb;
    public Collider2D collider2d;

    Animator animator;
    public ExperienceManager experienceManager;
    

    public float Health{
        set{
            health = value;
            if(health <= 0){
                Defeated();
                experienceManager.AddExperience(5);
            }
        }
        get{
            return health;
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            PlayerController Player = other.GetComponent<PlayerController>();

            if(Player != null){
                Player.Health -= damage;
                Defeated();
            }
        }
        
        
    }

    public float health = 1;

    public void Defeated(){
        animator.SetTrigger("Defeated");
        collider2d.enabled = false;
    }

    public void RemoveEnemy(){
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        collider2d.enabled = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 direction = (Player.GetComponent<Rigidbody2D>().position - rb.position).normalized;

        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }
}
