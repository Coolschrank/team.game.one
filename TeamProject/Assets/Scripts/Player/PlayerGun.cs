using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGun : MonoBehaviour
{
    public GameObject gun, projektile, shipAbility, item, abilityUI;
    public float shotRotation, fireRate, timeMultiplier, timeStopped;
    float trigger1, trigger2, trigger3, trigger4, coolDown1, coolDown2, coolDown3, coolDown4 ;
    public int maxSP, SP, shipAbilitySP, itemSP, SPG, SPGRise; // SPG = sp gained fromItems
    public bool coolDownBool1, coolDownBool2, coolDownBool3, coolDownBool4, inTimeSlowDown, nukeInCoolDown;
    
    Text[] UIChildren;
    Slider[] UISlider;

    // for reloard Ship


    public bool NeedToReload, inReload;
    public int ammo, maxAmmo;
    public float reloadTime;

    public bool canDash , dashRecovery;
    public int dashSP;
    public float dashCoolDown, dashCoolDownR;

    void Start()
    {

        abilityUI = FindObjectOfType<AbilityUI>().gameObject;
        timeMultiplier = 1f;
        UIChildren = abilityUI.GetComponentsInChildren<Text>();
        UISlider = abilityUI.GetComponentsInChildren<Slider>();
        foreach(Slider slider in UISlider)
        {
           slider.gameObject.SetActive(false);
        }
        SetAbility();
        SP = maxSP;
        if (NeedToReload)
        {
            ammo = maxAmmo;
            SetAmmo();
        }
        else if(canDash)
        {
            SetDash();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale ==0)
        {
            return;
        }

        trigger1 = Input.GetAxisRaw("Fire1");
        trigger2 = Input.GetAxisRaw("Fire2");
        trigger3 = Input.GetAxisRaw("Fire3");
        trigger4 = Input.GetAxisRaw("Fire4");
        if (trigger1 == 1 && !coolDownBool1 && !inReload)
        {
            if (NeedToReload)
            {
                ammo--;
                SetAmmo();
            }
            GameObject shot = Instantiate(projektile, gun.transform.position, transform.rotation) as GameObject;
            StartCoroutine(CoolDown1(fireRate));
            if (NeedToReload && ammo <= 0)
            {
                StartCoroutine(Reload());
                return;
            }
        }
        else if (trigger2 == 1 && !coolDownBool2 && shipAbilitySP <= SP)
        {
            SP -= shipAbilitySP;
            SetUIColor();
            GameObject shot = Instantiate(shipAbility, gun.transform.position, transform.rotation) as GameObject;
            StartCoroutine(CoolDown2(coolDown2));
        }
        else if (trigger3 == 1 && !coolDownBool3 && itemSP <= SP)
        {
            
            if (item == null)
                return;
            if (inTimeSlowDown && item.GetComponent<TimeSlowDown>())
                return;
            if (nukeInCoolDown && item.GetComponent<BigBoom>())
                return;
            SP -= itemSP;
            SetUIColor();
            GameObject shot = Instantiate(item, gun.transform.position, transform.rotation) as GameObject;
            StartCoroutine(CoolDown3(coolDown3));
        }
        else if(trigger4 == 1 )
        {
            if(NeedToReload)
            {
                if(ammo < maxAmmo&& !inReload)
                StartCoroutine(Reload());
            }
            else if(canDash)
            {
                var pm = GetComponent<PlayerMovement>();
                if (SP >= dashSP && !dashRecovery && (pm.xMove != 0|| pm.zMove != 0))
                {
                    SP -= dashSP;
                   // StartCoroutine(CoolDown(dashCoolDown));
                    StartCoroutine(DashCoolDown(dashCoolDownR));
                    pm.Dash();
                }
            }
            SetUIColor();
        }

        if(UISlider[2].IsActive()&& inReload)
        {
            UISlider[2].value = Time.time;
        }
        else
        {
            UISlider[2].minValue = 0;
            UISlider[2].maxValue = maxAmmo;
            UISlider[2].value = ammo;
        }
        if(inTimeSlowDown )
        {
            UISlider[1].value = timeStopped - (Time.time- timeStopped);
        }
        else if(nukeInCoolDown)
        {
            UISlider[1].value = Time.time;
        }
        else { UISlider[1].gameObject.SetActive(false); }
        

    }

    IEnumerator CoolDown1(float coolDown)
    {
        coolDownBool1 = true;
        yield return new WaitForSeconds(coolDown/ timeMultiplier);
        coolDownBool1 = false;
    }
    IEnumerator CoolDown2(float coolDown)
    {
        coolDownBool2 = true;
        yield return new WaitForSeconds(coolDown / timeMultiplier);
        coolDownBool2 = false;
    }
    IEnumerator CoolDown3(float coolDown)
    {
        coolDownBool3 = true;
        yield return new WaitForSeconds(coolDown / timeMultiplier);
        coolDownBool3 = false;
    }

    IEnumerator DashCoolDown(float coolDown)
    {
        dashRecovery = true;
        yield return new WaitForSeconds(coolDown / timeMultiplier);
        dashRecovery = false;
    }

    IEnumerator Reload()
    {

        inReload = true;
        UISlider[2].GetComponentInChildren<Image>().color = Color.red;
        //UISlider[2].gameObject.SetActive(true);
        UISlider[2].minValue = Time.time;
        UISlider[2].maxValue = Time.time + (reloadTime / timeMultiplier);
        yield return new WaitForSeconds(reloadTime/ timeMultiplier);
        //UISlider[2].gameObject.SetActive(false);
        UISlider[2].GetComponentInChildren<Image>().color = Color.white;
        ammo = maxAmmo;
        SetAmmo();
        inReload = false;
    }

    public void SetUIColor()
    {
        
        if (SP < shipAbilitySP)
        {
            UIChildren[0].color = Color.red;
        }
        else
        {
            UIChildren[0].color = Color.white;
        }
        if (SP < itemSP)
        {
            UIChildren[1].color = Color.red;
        }
        else
        {
            UIChildren[1].color = Color.white;
        }
        if (SP < dashSP && canDash)
        {
            UIChildren[2].color = Color.red;
        }
        else if(canDash)
        {
            UIChildren[2].color = Color.white;
        }
    }

    public void SetAbility()
    {
        var PA = projektile.GetComponent<Ability>();
        var SA = shipAbility.GetComponent<Ability>();
        /*if (SA.needSlider)
        {
            UISlider[0].gameObject.SetActive(true);
        }*/

        shipAbilitySP = SA.SPCost;
        coolDown1 = PA.coolDown;
        coolDown2 = SA.coolDown;
        
        
        UIChildren[0].text = SA.abilityName + "\n"+ SA.SPCost.ToString()+" EP";
        if (item!= null)
        {
            
            var IA = item.GetComponent<Ability>();
             
            coolDown3 = IA.coolDown;
            itemSP = IA.SPCost;
            UIChildren[1].text = IA.abilityName + "\n" + IA.SPCost.ToString() + " EP";
        }

        if (NeedToReload)
        {
            SetAmmo();
        }
        
    }

    public void SPGain(int spGain, bool p)
    {
        SP += spGain;
        if(p)
        {
            maxSP += spGain;
            SPG += SPGRise;
        }
        if(SP>maxSP)
        {
            SP = maxSP;
        }

    }

    public void SetAmmo()
    {
        UISlider[2].gameObject.SetActive(true);
        
        UISlider[2].gameObject.GetComponentInChildren<Text>().text = ammo.ToString();
        
        if (ammo>=10 )
        UISlider[2].gameObject.GetComponentInChildren<Text>().text = ammo.ToString();
        else 
            UISlider[2].gameObject.GetComponentInChildren<Text>().text = "0"+ ammo.ToString();
        
            
        UIChildren[2].text = "Reloard \n";
    }

    public void SetDash()
    {
        UIChildren[2].text = "Dasd \n" + dashSP + " EP";
    }

    public void TimeSlowSlider(float time)
    {
        inTimeSlowDown = true;
        UISlider[1].gameObject.SetActive(true);
        UISlider[1].maxValue = Time.time;
        UISlider[1].minValue = Time.time - time;
        UISlider[1].value = timeStopped;
        timeStopped = Time.time;

    }

    public void NukeCoolDown(float nukeCoolDown)
    {
        nukeInCoolDown = true;
        UISlider[1].gameObject.SetActive(true);
        UISlider[1].maxValue = Time.time + nukeCoolDown;
        UISlider[1].minValue = Time.time;
        UISlider[1].GetComponentInChildren<Image>().color = Color.red;
        StartCoroutine(NCD(nukeCoolDown));
    }

    IEnumerator NCD(float n) // NCD = nukeCoolDown
    {
        yield return new WaitForSeconds(n);
        nukeInCoolDown = false;
        UISlider[1].GetComponentInChildren<Image>().color = Color.white;
    }
}
