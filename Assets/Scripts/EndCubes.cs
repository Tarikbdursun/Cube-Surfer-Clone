using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class EndCubes : MonoBehaviour
{
    private string cubeName;
    private int x;

    private void Start()
    {
        cubeName = this.gameObject.name;
        x = Convert.ToInt32(cubeName) - 1;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BaseCube"))
        {
            Player.Instance.end = true;
            Player.Instance.diamond *= x;
            Player.Instance.Animations(true);
            Player.Instance.PlayerWin();
            Invoke("GameEnding", 1f);
        }
    }
    private void GameEnding()
    {
        GameManager.Instance.GameEnd(true);
        UIManager.Instance.totalText.text = Player.Instance.diamond.ToString();
    }
}
