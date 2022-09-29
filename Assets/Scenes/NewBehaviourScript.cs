using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Rigidbody2D Rigidbody2D;
    public BoxCollider2D CircleCollider2D;
    public float Offset;
    RaycastHit2D[] RaycastHit2D = new RaycastHit2D[1];
    public ContactFilter2D ContactFilter2D;

    private void FixedUpdate()
    {
        int raycastHit2DUp = Physics2D.Raycast(transform.position, transform.up, ContactFilter2D, RaycastHit2D, transform.up.magnitude);
        //RaycastHit2D raycastHit2DDown = Physics2D.Raycast(transform.position, -1f * transform.up, transform.up.magnitude);

        //RaycastHit2D raycastHit2DRight = Physics2D.Raycast(transform.position, transform.right, transform.right.magnitude);
        //RaycastHit2D raycastHit2DLeft = Physics2D.Raycast(transform.position, -1f * transform.right, transform.right.magnitude);


        if (raycastHit2DUp == 1)
        {
            Debug.Log("Up");
        }
        else if (raycastHit2DUp == 0)
        {
            Debug.Log("Null");
        }

        //if (raycastHit2DDown.collider != null)
        //{
        //    Debug.Log("Down");
        //}

        //if (raycastHit2DRight.collider != null)
        //{
        //    Debug.Log("Right");
        //}

        //if (raycastHit2DLeft.collider != null)
        //{
        //    Debug.Log("Left");
        //}

        Debug.DrawRay(transform.position, transform.up, Color.red);
    //    Debug.DrawRay(transform.position, -1f * transform.up, Color.red);

    //    Debug.DrawRay(transform.position, transform.right, Color.red);
    //    Debug.DrawRay(transform.position, -1f * transform.right, Color.red);
    }


}