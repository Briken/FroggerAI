using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour {


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision Occured with goal");
        collision.gameObject.GetComponent<Frog>().fitness *= 10;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision Occured with goal");
        collision.gameObject.GetComponent<Frog>().fitness *= 10;
    }
}
