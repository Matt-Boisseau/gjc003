using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcMovement : MonoBehaviour
{
    public float speed;
    private Animator animator;
    public Vector2 moveDirection;
 
    // Use this for initialization
    void Start()
    {
        animator = this.GetComponent<Animator>();
    }
 
    // Update is called once per frame
    void Update()
    {
       animator.SetInteger("walkInt", 0); // drop to neutral 

        moveDirection = new Vector2(0,0);
        var vertical = Input.GetAxis("Vertical");
        var horizontal = Input.GetAxis("Horizontal");
     
        if (horizontal > 0)
        {
            moveDirection.x = 1;
            animator.SetInteger("walkInt", 1);
            //animator.SetInteger("Direction", 2);
        }
        else if (horizontal < 0)
        {
            moveDirection.x = -1;
            animator.SetInteger("walkInt", -1);
            //animator.SetInteger("Direction", 0);
        }
        else if (vertical > 0)
        {
            moveDirection.y = 1;
            animator.SetInteger("walkInt", 2);
            //animator.SetInteger("Direction", 1);
        }
        else if (vertical < 0)
        {
            moveDirection.y = -1;
            animator.SetInteger("walkInt", -2);
            //animator.SetInteger("Direction", 3);
        }
        transform.Translate(moveDirection * speed* Time.deltaTime, Space.World);
    }
}