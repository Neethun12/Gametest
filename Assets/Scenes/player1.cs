using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class player1 : MonoBehaviour
{
    [SerializeField] private float movementspeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    [SerializeField] private float a = 10f;
    // Update is called once per frame
    private void Update()
    {
        Vector2 inputvector = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.W)&& Input.GetKey(KeyCode.LeftControl))
        {
            inputvector.y = 1;
            movementspeed = movementspeed * 2;
            
        }
        
        if (Input.GetKey(KeyCode.W))
        {
            inputvector.y = -1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputvector.y = 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputvector.x = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputvector.x = 1;
        }
        float rotateSpeed = 10f;
        float MoveDistance = movementspeed * Time.deltaTime;
        float playerradius= 0.7f;
        float playerheight = 2f;
        Vector3 direction = new Vector3(inputvector.x, 0f, inputvector.y);      
        inputvector = inputvector.normalized;
        bool CanMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerheight, playerradius, direction, MoveDistance);
         if (!CanMove)
         {
             Vector3 DirectionX = new Vector3(direction.x, 0, 0).normalized;
             CanMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerheight, playerradius, DirectionX, MoveDistance);
             if (CanMove)
             {
                direction = DirectionX;
                transform.forward = Vector3.Slerp(transform.forward, direction, Time.deltaTime * rotateSpeed);
            }
            else
            {
                Vector3 DirectionY = new Vector3(0, direction.y, 0).normalized;
                CanMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerheight, playerradius, DirectionY, MoveDistance);
                if (CanMove)
                {
                    direction = DirectionY;
                    transform.forward = Vector3.Slerp(transform.forward, direction, Time.deltaTime * rotateSpeed);
                }
            }
        }
        if (CanMove)
        {
            transform.position += direction * MoveDistance;
            transform.forward = Vector3.Slerp(transform.forward, direction, Time.deltaTime * rotateSpeed);
        }
        movementspeed = a;
    }
}
