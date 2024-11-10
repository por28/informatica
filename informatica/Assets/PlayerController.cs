using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float movespeed = 1f;
    public float collisionOffset = 0.05f;
    
    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    public ContactFilter2D movementFilter;
    public SwordAttack swordAttack;
    public TrailRenderer tr;
   
    
    Vector2 movementInput;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    Animator animator;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (isDashing)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
    }
    
    // Update is called once per frame
    private void FixedUpdate()
    {
        if(canMove){
            if(movementInput != Vector2.zero){
                bool success = TryMove(movementInput);
                
                if(!success){
                    success = TryMove(new Vector2(movementInput.x, 0));  
                }

                if(!success){
                    success = TryMove(new Vector2(0, movementInput.y));
                }
                animator.SetBool("isMoving", success);
            } else{
                animator.SetBool("isMoving", false);
            }
            if(movementInput.x < 0){
                spriteRenderer.flipX = true;
            } else if(movementInput.x > 0){
                spriteRenderer.flipX = false;
            }
        } 

         if (isDashing)
        {
            return;
        }
    }

       
 

    private bool TryMove(Vector2 direction){
        if(direction != Vector2.zero){
            int count = rb.Cast(
                    direction,
                    movementFilter,
                    castCollisions,
                    movespeed * Time.fixedDeltaTime + collisionOffset);

            if(count == 0){
                rb.MovePosition(rb.position + direction * movespeed * Time.fixedDeltaTime);
                return true;
            } else{
                return false;
            }
        }else{
            return false;
            }
    }

    void OnMove(InputValue movementValue) {
        movementInput = movementValue.Get<Vector2>();
    }

    void OnFire(){
        animator.SetTrigger("swordAttack");
    }

    public void SwordAttack(){
        LockMovement();
        if(spriteRenderer.flipX == true){
            swordAttack.AttackLeft();
        } else{
            swordAttack.AttackRight();
        }
    }

    public void EndSwordAttack(){
        UnlockMovement();
        swordAttack.StopAttack();
    }

    public void LockMovement(){
        canMove = false;
    }

    public void UnlockMovement(){
        canMove = true;
    }
 private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
 
}
