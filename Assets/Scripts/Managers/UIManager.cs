using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    #region SINGLETON
    private static UIManager instance;
    public static UIManager Instance => instance ??= FindObjectOfType<UIManager>();
    #endregion
    #region Panels
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject hudPanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject failPanel;
    public TextMeshProUGUI totalText;
    #endregion

    public void GameStart() 
    {
        startPanel.SetActive(false);
        hudPanel.SetActive(true);
    }
    public void GameEnd(bool won) 
    {
        hudPanel.SetActive(false);
        if (won)
        {
            winPanel.SetActive(true);
        }
        else
        {
            failPanel.SetActive(true);
        }
    }
    public void NextGame() 
    {
        winPanel.SetActive(false);
        startPanel.SetActive(true);
    }
    public void RestartGame() 
    {
        failPanel.SetActive(false);
        startPanel.SetActive(true);
    }
}
