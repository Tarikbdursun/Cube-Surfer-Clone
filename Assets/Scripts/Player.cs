using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    #region Variables
    [SerializeField] private Transform sideMovementRoot;
    [SerializeField] private Transform leftLimit;
    [SerializeField] private Transform rightLimit;

    [SerializeField] private GameObject confetti;

    [SerializeField] private Animator playerAnimator;

    [SerializeField] private float sideMovementSensitivity;
    [SerializeField] private float sideMovementLerpSpeed;
    [SerializeField] private float forwardSpeed;

    [SerializeField] private TextMeshProUGUI diamondText;
    [SerializeField] private TextMeshProUGUI successText;
    
    public Vector3 pos = new Vector3(0, -.75f, 0);
    public bool end;
    public bool levelEnd;

    public int diamond = 0;

    private Vector3 playerFirstPos;
    
    private Vector2 inputDrag;
    private Vector2 previousMousePosition;

    private float leftLimitX => leftLimit.localPosition.x;
    private float rightLimitX => rightLimit.localPosition.x;

    private float sideMovementTarget = 0;
    private Vector2 mousePositionCM // Providing the same experience to everyone
    {
        get
        {
            Vector2 pixels = Input.mousePosition;
            var inches = pixels / Screen.dpi;
            var centimetres = inches * 2.54f; // 1 inch = 2.54 cm

            return centimetres;
        }
    }
    #endregion
    #region SINGLETON
    private static Player instance;
    public static Player Instance => instance ??= FindObjectOfType<Player>();
    #endregion

    private void Start()
    {
        diamondText.text = diamond.ToString();
        playerFirstPos = transform.position;
    }

    void Update()
    {
        HandleInput();
        if (GameManager.Instance.isGameStart && !end)
        {
            HandleForwardMovement();
            HandleSideMovement();
        }
        diamondText.text = diamond.ToString();
    }

    private void HandleForwardMovement()
    {
        transform.position += transform.forward * Time.deltaTime * forwardSpeed;
    }

    private void HandleSideMovement()
    {
        sideMovementTarget += inputDrag.x * sideMovementSensitivity;
        sideMovementTarget = Mathf.Clamp(sideMovementTarget, leftLimitX, rightLimitX);

        var localPos = sideMovementRoot.localPosition;

        localPos.x = Mathf.Lerp(localPos.x, sideMovementTarget, Time.deltaTime * sideMovementLerpSpeed);

        sideMovementRoot.localPosition = localPos;
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            previousMousePosition = mousePositionCM;
        }
        if (Input.GetMouseButton(0))
        {
            var deltaMouse = (Vector2)mousePositionCM - previousMousePosition;
            inputDrag = deltaMouse;
            previousMousePosition = mousePositionCM;
        }
        else
        {
            inputDrag = Vector2.zero;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Diamond"))
        {
            other.gameObject.SetActive(false);
            diamond++;
            diamondText.text = diamond.ToString();
        }
        else if (other.gameObject.CompareTag("LevelEnd"))
        {
            levelEnd = true;
        }
        else if (other.gameObject.CompareTag("Success"))
        {
            other.GetComponent<Collider>().enabled = false;
            end = true;
            Debug.Log(diamond);
            diamond *= 20;
            Debug.Log(" total " + diamond);
            successText.text = diamond.ToString();
            PlayerWin();
            Invoke("GameEnding", 1);
        }
    }
    public void PlayerWin() 
    {
        Animations(true);
        confetti.SetActive(true);
    }
    public void Animations(bool win) 
    {
        if (win)
        {
            playerAnimator.SetTrigger("Win");
        }
        else
        {
            playerAnimator.SetTrigger("Fail");
        }
    }
    private void GameEnding() 
    {
        GameManager.Instance.GameEnd(true);
    }
    public void ResetPlayer() 
    {
        confetti.SetActive(false);
        transform.position = playerFirstPos;
        end = false;
        levelEnd = false;
        diamond = 0;
        playerAnimator.SetTrigger("Reset");
        pos= new Vector3(0, -.75f, 0);
    }
}
