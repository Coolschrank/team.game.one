using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SPSlider : MonoBehaviour
{
    Slider slider;
    Text text;
    PlayerGun player;

    private void Start()
    {
        slider = GetComponent<Slider>();
        text = GetComponentInChildren<Text>();
        player = FindObjectOfType<PlayerGun>();
    }

    public void Update()
    {
        slider.value =(float)player.SP / player.maxSP;

        if(player.SP>=10)
        text.text = player.SP + "/" + player.maxSP;
        else if(player.maxSP<10)
        text.text = "0" + player.SP + "/"+ "0" + player.maxSP;
        else
        text.text = "0"+ player.SP + "/" + player.maxSP;


    }
}
