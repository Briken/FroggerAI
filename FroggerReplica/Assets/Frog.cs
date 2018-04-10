using UnityEngine;
using UnityEngine.SceneManagement;

public class Frog : MonoBehaviour {

    public DNA myDNA;
	public Rigidbody2D rb;
    public Transform goal;
    public float fitness;
    public bool isAlive;
    public float initialDist;
    public float fitD;
    public Vector3 initialPos;

    private void Start()
    {
        isAlive = true;
        goal = GameObject.Find("Goal").transform;
        initialPos = this.transform.position;
        initialDist = Vector3.Distance(goal.transform.position, this.transform.position);
        fitness = 0;
    }
    void Update () {

        if (Input.GetKeyDown(KeyCode.RightArrow))
            MoveRight();
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            MoveLeft();
        else if (Input.GetKeyDown(KeyCode.UpArrow))
            MoveUp();
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            MoveDown();
        CalculateFitness();
    }

	void OnTriggerEnter2D (Collider2D col)
	{

		if (col.tag == "Car")
		{
			Debug.Log("WE LOST!");
			Score.CurrentScore = 0;
            isAlive = false;
		}
        if (col.tag == "Goal")
        {
            Debug.Log("Goal Hit");

            fitness *= 10;
        }
	}

    public void CalculateFitness()
    {
        fitD = Vector3.Distance(goal.transform.position, this.transform.position);
        fitness = initialDist / fitD;
        if (isAlive)
        {
            if (fitD > initialDist && this.transform.position.y < goal.transform.position.y)
            {
                fitness = 0;
            }
        }
        else
        {
            fitness = 0;
        }
    }

    public void MoveUp()
    {
        if (isAlive)
        {
            rb = GetComponent<Rigidbody2D>();
            rb.MovePosition(rb.position + Vector2.up);
        }
    }
    public void MoveLeft()
    {
        if (isAlive)
        {
            rb = GetComponent<Rigidbody2D>();
            rb.MovePosition(rb.position + Vector2.left);
        }
    }
    public void MoveRight()
    {
        if (isAlive)
        {
            rb = GetComponent<Rigidbody2D>();
            rb.MovePosition(rb.position + Vector2.right);
        }
    }
    public void MoveDown()
    {
        if (isAlive)
        {
            rb = GetComponent<Rigidbody2D>();
            rb.MovePosition(rb.position + Vector2.down);
        }
    }
    public void Wait()
    {

    }
}
