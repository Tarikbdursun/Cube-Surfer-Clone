using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region SINGLETON
    private static GameManager instance;
    public static GameManager Instance => instance ??= FindObjectOfType<GameManager>();
    #endregion
    public bool isGameStart;
    [SerializeField] private LevelList levelList;
    void Start()
    {
        LevelManager.SetLevelManager(levelList);
    }

    public void GameStart() 
    {
        isGameStart = true;
        UIManager.Instance.GameStart();
    }
    public void GameEnd(bool won) 
    {
        isGameStart = false;
        UIManager.Instance.GameEnd(won);
    }
    public void NextGame() 
    {
        LevelManager.NextLevel();
        Player.Instance.ResetPlayer();
        CameraManager.Instance.ResetCamera();
        UIManager.Instance.NextGame();
    }
    public void RestartGame() 
    {
        LevelManager.RestartLevel();
        Player.Instance.ResetPlayer();
        CameraManager.Instance.ResetCamera();
        UIManager.Instance.RestartGame();
    }
}
