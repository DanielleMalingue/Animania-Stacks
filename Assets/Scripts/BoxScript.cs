using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    private float min_X = -2.2f, max_X = 2.2f;
    private bool canMove;
    private float move_Speed = 2f;
    private Rigidbody2D myBody;
    private bool gameOver;
    private bool ignoreCollision;
    private bool ignoreTrigger;
private bool canRotate = false;



    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        myBody.gravityScale = 0f;
    }

    void Start()
    {
        canMove = true;
        if(Random.Range(0,2) > 0)
        {
            move_Speed *= -1f;
        }
        GamePlayController.instance.currentBox = this;
      
    }

    void Update()
    {
         {
        // Check for two-finger touch to enable rotation
        if (Input.touchCount == 2)
        {
            canRotate = true;
            RotateBox();
        }
        else
        {
            canRotate = false;
        }

        // Check for one-finger touch to drop the box
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            DropBox();
        }
    }
        MoveBox();
    }

    void MoveBox()
    {
        if(canMove)
        {
            Vector3 temp = transform.position;
            temp.x += move_Speed * Time.deltaTime;

            if(temp.x > max_X)
            {
                move_Speed *= -1f;
            }
            else if(temp.x < min_X)
            {
                move_Speed *= -1f;
            }
            transform.position = temp;
        }
    }
    public void DropBox()
    {
        canMove = false;
        myBody.gravityScale = Random.Range(2, 4);
    }
    void Landed()
    {
        if(gameOver)
        {
            return;
        }
        ignoreCollision = true;
        ignoreTrigger = true;

        GamePlayController.instance.SpawnNewBox();
        GamePlayController.instance.MoveCamera();
        GamePlayController.instance.IncreaseScore(GamePlayController.instance.scoreIncreaseAmount);
    }
    void RestartGame()
    {
        GamePlayController.instance.RestartGame();
    }

 void RotateBox()
    {
        if (canRotate)
        {
            // Get the rotation angle based on two-finger touch positions
            Vector2 touch1 = Input.GetTouch(0).position;
            Vector2 touch2 = Input.GetTouch(1).position;
            float angle = Mathf.Atan2(touch2.y - touch1.y, touch2.x - touch1.x) * Mathf.Rad2Deg;

            // Apply rotation to the box
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }

    void OnCollisionEnter2D(Collision2D target)
    {
        if(ignoreCollision)
        {
            return;
        }
        if(target.gameObject.tag == "Platform")
        {
            Invoke("Landed", 2f);
            ignoreCollision = true;
        }
        if(target.gameObject.tag == "Box")
        {
            Invoke("Landed", 2f);
            ignoreCollision = true;
        }
        
    }
    void OnTriggerEnter2D(Collider2D target)
    {
        if(ignoreTrigger)
        {
            return;
        }
        if(target.tag == "GameOver")
        {
            CancelInvoke("Landed");
            gameOver = true;
            ignoreTrigger = true;
            Invoke("RestartGame", 2f);
        }
    }
}
