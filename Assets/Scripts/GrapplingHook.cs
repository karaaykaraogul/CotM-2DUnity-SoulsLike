using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    public LineRenderer lr;
    public DistanceJoint2D dj;
    public Animator animator;
    public Vector2 target;
    public float distance = 4f;
    public Transform HookFirePoint;
    public GameObject HookAimPoint;
    public Rigidbody2D rb;
    bool swinging=false;
    bool facingRight = true;

    void Start()
    {
        dj.enabled = false;
    }

    public bool isSwinging()
    {
        return swinging;
    }

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(HookFirePoint.position,HookFirePoint.up, distance);
        float horizontalMove = Input.GetAxisRaw("Horizontal");
        //sets active a game object that lets player know
        //where he is aiming at
        if(hit.collider != null){
            HookAimPoint.SetActive(true);
            HookAimPoint.transform.position = hit.point;
            if (Input.GetMouseButtonDown(1)){
                rb.gravityScale = 5;
                animator.SetBool("IsGrappling", true);
                target = hit.point;
                swinging = true;
                HookOn();
            }
        }
        else{
            HookAimPoint.SetActive(false);
        }

        if (Input.GetMouseButtonUp(1) && swinging)
        {
            rb.gravityScale = 2;
            if(facingRight){
                transform.rotation = Quaternion.Euler(new Vector3(0f,0f,0f));
            }
            else{
                transform.rotation = Quaternion.Euler(new Vector3(0f,180f,0f));
            }
            animator.SetBool("IsGrappling", false);
            swinging = false;
            HookOff();
        }

        if (horizontalMove > 0 && !facingRight && !swinging)
        {
            // ... flip the player.
            facingRight = !facingRight;
        }
        else if (horizontalMove < 0 && facingRight && !swinging)
        {
            // ... flip the player.
            facingRight = !facingRight;
        }
        


        if(swinging){
            
            lr.positionCount = 2;
            lr.SetPosition(0, target);
            lr.SetPosition(1, new Vector3(transform.position.x,transform.position.y+0.1f,transform.position.z));

            Vector3 rotateTarget = new Vector3(target.x, target.y, 0);

            rotateTarget.x = rotateTarget.x-transform.position.x;
            rotateTarget.y = rotateTarget.y-transform.position.y;

            float angle = Mathf.Atan2(rotateTarget.y,rotateTarget.x) * Mathf.Rad2Deg;

            if(facingRight)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0.0f,0.0f, angle));
            }
            else if(!facingRight)
            {
                transform.rotation = Quaternion.Euler(new Vector3(180.0f,0.0f, 360.0f-angle));
            }
            
            if (horizontalMove > 0 && !facingRight)
			{
				// ... flip the player.
				facingRight = !facingRight;
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (horizontalMove < 0 && facingRight)
			{
				// ... flip the player.
				facingRight = !facingRight;
			}
        }
    }

    public void HookOn()
    {
        dj.enabled = true;
        dj.connectedAnchor = target;
                
    }

    public void HookOff()
    {
        dj.enabled = false;
        lr.positionCount = 0;
    }
}
