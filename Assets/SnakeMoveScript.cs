using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
using UnityEngine.UIElements;

public class SnakeMoveScript : MonoBehaviour
{

    public bool Alive;
    public Rigidbody2D Rigidbody;
    public float moveSpeed = 1;
    public float timer;
    public GameObject snake;
    public string direction;
    public LogicScript logic;
    Queue<string> directions = new Queue<string>();
    public List<Transform> bodySegments = new List<Transform>();
    public GameObject Body;
    GameObject newBody;
    Vector3 prevHead;

    // Start is called before the first frame update
    void Start()
    {
        Alive = true;
        Physics.IgnoreLayerCollision(0, 6);
        logic = GameObject.FindGameObjectWithTag("logic").GetComponent<LogicScript>();

    }

    // Update is called once per frame
    void Update()
    {
        timer += 10*Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (!direction.Equals("left") && !direction.Equals("right"))
            {
                directions.Enqueue("right");
                direction = "right";
            }
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (!direction.Equals("right") && !direction.Equals("left"))
            {
                directions.Enqueue("left");
                direction = "left";
            }
        }
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (!direction.Equals("down") && !direction.Equals("up"))
            {
                directions.Enqueue("up");
                direction = "up";
            }
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (!direction.Equals("up") && !direction.Equals("down"))
            {
                directions.Enqueue("down");
                direction = "down";
            }
        }


        if (timer > moveSpeed)
        {
            moveInDirection(direction, directions);
            timer = 0;
        }

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "wall")
        {
            Destroy(gameObject);
            Alive = false;
        }

        if(collision.gameObject.tag == "apple")
        {
            Destroy(collision.gameObject);
            snake.transform.position = new Vector3((float) Math.Round(snake.transform.position.x), (float) Math.Round(snake.transform.position.y), 0);
            snake.transform.rotation = Quaternion.Euler(0, 0, 0);

            logic.CreateApple();
            grow();
            logic.addScore();

        }

    }

    public void grow()
    {

        if (direction.Equals("left"))
        {
            newBody = Instantiate(Body, new Vector3(snake.transform.position.x + 1, snake.transform.position.y, 0), transform.rotation);
            bodySegments.Add(newBody.transform);
        }
        else if (direction.Equals("right"))
        {
            newBody = Instantiate(Body, new Vector3(snake.transform.position.x - 1, snake.transform.position.y, 0), transform.rotation);
            bodySegments.Add(newBody.transform);
        }
        else if (direction.Equals("up"))
        {
            newBody = Instantiate(Body, new Vector3(snake.transform.position.x, snake.transform.position.y - 1, 0), transform.rotation);
            bodySegments.Add(newBody.transform);
        }
        else if (direction.Equals("down"))
        {
            newBody = Instantiate(Body, new Vector3(snake.transform.position.x, snake.transform.position.y + 1, 0), transform.rotation);
            bodySegments.Add(newBody.transform);
        }


    }
    
    public void moveInDirection(string direction, Queue<string> directions)
    {
        prevHead = new Vector3(snake.transform.position.x, snake.transform.position.y, 0);


        if (direction.Equals("left"))
        {
            snake.transform.position = new Vector3((snake.transform.position.x - 1), (snake.transform.position.y), 0);
        }
        else if (direction.Equals("right"))
        {
            snake.transform.position = new Vector3((snake.transform.position.x + 1), (snake.transform.position.y), 0);
        }
        else if (direction.Equals("up"))
        {
            snake.transform.position = new Vector3((snake.transform.position.x), (snake.transform.position.y + 1), 0);
        }
        else if (direction.Equals("down"))
        {
            snake.transform.position = new Vector3((snake.transform.position.x), (snake.transform.position.y - 1), 0);
        }

        if(bodySegments.Count > 0)
        {

            for (int i = bodySegments.Count - 1; i > 0; i--)
            {
                bodySegments[i].position = bodySegments[i-1].position;
            }
            bodySegments[0].position = prevHead;
        }
    }


}
/*
 * public class SnakeController : MonoBehaviour
{
public GameObject bodySegmentPrefab;
public List<Transform> bodySegments = new List<Transform>();
public float moveSpeed = 1.0f;

void Update()
{
    HandleInput();
    Move();
}

void HandleInput()
{
    // Handle input to change the direction of the snake
}

void Move()
{
    // Move the head of the snake based on input

    for (int i = bodySegments.Count - 1; i > 0; i--)
    {
        // Update the position of each body segment to follow the previous one
        bodySegments[i].position = bodySegments[i - 1].position;
    }

    // Move the first body segment to the head's position
    bodySegments[0].position = headPosition;
}

void Grow()
{
    // Instantiate a new body segment at the tail
    GameObject newSegment = Instantiate(bodySegmentPrefab, tailPosition, Quaternion.identity);

    // Add the new segment to the list
    bodySegments.Add(newSegment.transform);
}
}

 */
