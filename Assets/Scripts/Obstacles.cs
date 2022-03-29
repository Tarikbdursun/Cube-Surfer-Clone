using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BaseCube"))
        {
            Player.Instance.Animations(false);
            GameManager.Instance.GameEnd(false);
        }
    }
}
