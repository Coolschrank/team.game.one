using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtons : MonoBehaviour
{
    public bool menuOpen,bPressed, cPressed, inGame, setCursorTrue; // b = pauseButton ; c= cursorButton;
    Button[] buttons ;
    Text[] texts;
    Button currentButton;
    public GameObject cursor;
    public string cInput, kInput;
    

    private void Start()
    {
        buttons = GetComponentsInChildren<Button>();
       
        
        texts = GetComponentsInChildren<Text>();
        if (inGame)
            CloseMenu();
        else
            CursorToStart();
    }


    private void Update()
    {
        float pause = Input.GetAxisRaw("Pause");
        float cursorMove = Input.GetAxisRaw("Vertical");
        float activate = Input.GetAxisRaw("Yes");
        if((Input.GetKeyDown(cInput)|| Input.GetKeyDown(kInput)) && menuOpen&& setCursorTrue)
        {
            currentButton.onClick.Invoke();
        }
        if (cursorMove == 0)
        {
            cursorMove = Input.GetAxisRaw("Vertical2");
        }
        if (cursorMove == 0)
        {
            cursorMove = Input.GetAxisRaw("Vertical3");
        }

        if (cursorMove == 0 && menuOpen)
        {
            cPressed = false;
        }
        else if (cursorMove == 1 && cPressed == false && menuOpen)
        {
            cPressed = true;
            MoveUp();
        }
        else if (cursorMove == -1 && cPressed == false && menuOpen)
        {
            cPressed = true;
            MoveDown();
        }

        if (pause ==0)
        {
            bPressed = false;
        }
        else if (pause == 1 && !bPressed && inGame)
        {
            bPressed = true;
            menuOpen = !menuOpen;
            if (menuOpen)
            {
                OpenMenu();
            }
            else
            {
                CloseMenu();
            }
        }
    }

    public void OpenMenu()
    {
        setCursorTrue = false;
        Time.timeScale = 0f;
        foreach (Text text in texts)
        {
            text.gameObject.SetActive(true);
        }
        CursorToStart();
        cursor.SetActive(true);
    }

    public void CloseMenu()
    {
        Time.timeScale = 1f;
        foreach (Text text in texts)
        {
            text.gameObject.SetActive(false);
        }
        foreach (Button button in buttons)
        {
            button.gameObject.SetActive(false);
        }
        cursor.SetActive(false);
    }

    public void MoveDown()
    {
        float cpy = cursor.transform.position.y;   // CursorPositionY
        float nbtY = 0;   // nextButtonPositionY
        foreach (Button button in buttons)
        {
            float btY = button.transform.position.y;
            if (nbtY == 0f && btY < cpy)
            {
                nbtY = btY;
                currentButton = button;
            }
            else if(btY > nbtY && btY < cpy)
            {
                nbtY = btY;
                currentButton = button;
            }
        }
        if (nbtY == 0)
        {
            foreach (Button button in buttons)
            {
                float btY = button.transform.position.y;
                if (btY > nbtY)
                {
                    nbtY = btY;
                    currentButton = button;
                }
            }
        }
        cursor.transform.position = new Vector3(cursor.transform.position.x, nbtY, cursor.transform.position.z);

    }
    public void MoveUp()
    {
        float cpy = cursor.transform.position.y;   // CursorPositionY
        float nbtY = 0;   // nextButtonPositionY
        foreach (Button button in buttons)
        {
            float btY = button.transform.position.y;
            if (nbtY == 0f && btY > cpy)
            {
                nbtY = btY;
                currentButton = button;
            }
            else if (btY < nbtY && btY > cpy)
            {
                nbtY = btY;
                currentButton = button;
            }
        }
        if(nbtY == 0)
        {
            foreach (Button button in buttons)
            {
                float btY = button.transform.position.y;
                if (btY < nbtY || nbtY==0)
                {
                    nbtY = btY;
                    currentButton = button;
                }
            }
        }
        cursor.transform.position = new Vector3(cursor.transform.position.x, nbtY, cursor.transform.position.z);
    }

    public void endGame()
    {
        Application.Quit();
    }

    public void CursorToStart()
    {
        float nbtY = 0;
        foreach (Button button in buttons)
        {
            button.gameObject.SetActive(true);
            if (button.gameObject.transform.position.y > nbtY)
            {
                nbtY = button.gameObject.transform.position.y;
                currentButton = button;
            }
        }
        cursor.transform.position = new Vector3(cursor.transform.position.x, nbtY, cursor.transform.position.z);
        setCursorTrue = true;
    }



}
