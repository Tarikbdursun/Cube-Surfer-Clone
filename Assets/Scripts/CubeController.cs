using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    private GameObject player;
    private GameObject sideMovementRoot;
    private void Start()
    {
        player = GameObject.Find("Player Root");
        sideMovementRoot = GameObject.Find("Side Movement Root");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            transform.SetParent(other.gameObject.transform);

            transform.position = new Vector3(transform.position.x, other.transform.position.y, other.transform.position.z - .5f);
            other.gameObject.GetComponent<Collider>().enabled = false;
            Player.Instance.pos -= new Vector3(0, -.5f, 0);
            player.transform.position -= new Vector3(0, .5f, 0);
        }
        else if (other.gameObject.CompareTag("Player") && gameObject.CompareTag("Cube"))
        {
            player.transform.position += new Vector3(0, .5f, 0);
            gameObject.transform.SetParent(sideMovementRoot.transform);
            transform.localPosition = Player.Instance.pos;
            Player.Instance.pos -= new Vector3(0, .5f, 0);
            this.gameObject.tag = "Player";
        }
        else if (other.gameObject.CompareTag("End"))
        {
            other.gameObject.GetComponent<Collider>().enabled = false;
            transform.SetParent(other.gameObject.transform);
            transform.position = transform.position;
        }
    }
}
