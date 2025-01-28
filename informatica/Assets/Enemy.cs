using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject Player;
    public float speed;
    private float distance;
    public float distanceBetween;

    Animator animator;

    public float Health{
        set{
            health = value;
            if(health <= 0){
                Defeated();
            }
        }
        get{
            return health;
        }
    }


    public float health = 1;

    public void Defeated(){
        animator.SetTrigger("Defeated");
    }

    public void RemoveEnemy(){
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, Player.transform.position);

        if (distance < distanceBetween)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, Player.transform.position, speed * Time.deltaTime);
        }
    }
}
