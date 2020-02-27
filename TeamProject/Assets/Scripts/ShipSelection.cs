using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipSelection : MonoBehaviour
{
    public GameObject[] ships;
    public int currentShip;
    public float moveSpeed, moveDistance;
    public GameObject difficultyLevel;
    public bool isOpen, yesed;



    Vector3 endPosition;
    Rigidbody rb;
    bool moved;


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        rb = GetComponent<Rigidbody>();
        FindObjectOfType<HighScore>().currentShip = ships[currentShip];
    }

    // Update is called once per frame
    void Update()
    {
        var xMove = Input.GetAxisRaw("Horizontal");
        var yes = Input.GetAxisRaw("Yes");
        if(yes == 1 && !yesed)
        {
            difficultyLevel.SetActive(true);
            if (!isOpen)
                difficultyLevel.GetComponent<UIButtons>().CursorToStart();
            isOpen = true;
        }
        else if(yes == -1 && !yesed)
        {
            yesed = true;
            if(isOpen)
            {
                difficultyLevel.SetActive(false);
                isOpen = false;
            }
            else
            { NextScene(-1); }
            
        }
        if (xMove == 0)
        {
            xMove = Input.GetAxisRaw("Horizontal2");
        }
        if (xMove == 0)
        {
            xMove = Input.GetAxisRaw("Horizontal3");
        }

        if(xMove ==1 && !moved && currentShip < ships.Length-1&& !isOpen)
        {
            GoRight();
            moved = true;
        }
        else if (xMove == -1 && !moved && currentShip >0 && !isOpen)
        {
            GoLeft();
            moved = true;
        }

       
        if(transform.position.x == endPosition.x)
        {
            moved = false;
        }

        if(yes==0)
        {
            yesed = false;
        }
    }

    private void FixedUpdate()
    {
        rb.transform.position = Vector3.MoveTowards(transform.position, endPosition, moveSpeed * Time.deltaTime);
    }

    public void GoLeft()
    {
        endPosition = new Vector3(transform.position.x + moveDistance, 0, 0);
        currentShip--;
        FindObjectOfType<HighScore>().currentShip = ships[currentShip];
    }

    public void GoRight()
    {
        endPosition = new Vector3(transform.position.x - moveDistance, 0, 0);
        currentShip++;
        FindObjectOfType<HighScore>().currentShip = ships[currentShip];
    }

    public void NextScene(int i)
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        FindObjectOfType<HighScore>().NextScene(i);
    }


}
