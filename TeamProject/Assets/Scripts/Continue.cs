using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Continue : MonoBehaviour
{
    public int sceneJump; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void NextScene()
    {
        Time.timeScale = 1;
        FindObjectOfType<HighScore>().NextScene(sceneJump);
    }
    
}
