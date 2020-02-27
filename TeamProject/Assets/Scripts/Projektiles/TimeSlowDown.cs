using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSlowDown : MonoBehaviour
{
    public float timeSlowDown, slowDownTime;
    public Color timeStopColor;
    Camera mainCam;
    Color normalColor;
    PlayerGun PG;
    PlayerMovement PM;
    // Start is called before the first frame update
    void Start()
    {
        PG = FindObjectOfType<PlayerGun>();
        PM = FindObjectOfType<PlayerMovement>();

        StartCoroutine(SlowDown());
        StartCoroutine(ScreenChange());
    }

    IEnumerator SlowDown()
    {
        Time.timeScale = timeSlowDown;
        PG.timeMultiplier = PG.timeMultiplier / timeSlowDown;
        PM.timeMultiplier = PM.timeMultiplier / timeSlowDown;
        PG.TimeSlowSlider(slowDownTime);

        yield return new WaitForSeconds(slowDownTime);
        Time.timeScale = 1f;
        PG.timeMultiplier = 1f;
        PG.inTimeSlowDown = false;
        
        PM.timeMultiplier = 1f;
    }

    IEnumerator ScreenChange()
    {
        Camera[] cams = FindObjectsOfType<Camera>();
        foreach (Camera cam in cams)
        {
            if (cam.tag == "MainCamera")
                mainCam = cam;
        }
        normalColor = mainCam.backgroundColor;
        mainCam.backgroundColor = timeStopColor;

        yield return new WaitForSeconds(slowDownTime);
        mainCam.backgroundColor = normalColor;
        Destroy(gameObject,1);
    }

    public void SetSlider()
    {

    }
}
