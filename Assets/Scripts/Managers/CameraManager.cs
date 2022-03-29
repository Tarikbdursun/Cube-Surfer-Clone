using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Vector3 camPos;
    private Vector3 camFirstPos;

    private static CameraManager instance;
    public static CameraManager Instance => instance ??= FindObjectOfType<CameraManager>();
    private void Start()
    {
        camFirstPos = transform.position;
    }
    private void LateUpdate()
    {
        if (Player.Instance.levelEnd)
        {
            camPos = transform.position;
            transform.position = Vector3.Lerp(camPos,target.position,Time.deltaTime*2f);
        }
        else
        {
            transform.position = new Vector3(target.position.x, 0, target.position.z);
        }
    }

    public void ResetCamera() 
    {
        transform.position = camFirstPos;
    }
}
