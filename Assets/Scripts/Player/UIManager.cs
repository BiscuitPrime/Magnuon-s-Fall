using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/**
 *  Class used to handle the Player's UI
 *  Component of the Player prefab
 */
public class UIManager : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] private Image crosshair; //crosshair, only displayed during ADS
    [SerializeField] private TextMeshProUGUI ammoCounter;
    private int maxAmmo=0; //max ammo of the currently used weapon, changed at "changeGun" method of GunManager
    #endregion

    //updates the ammo conter, called by other scripts
    public void updateAmmoCounter(int curAmmo)
    {
        ammoCounter.text = curAmmo + "/" + maxAmmo;
    }

    public void setMaxAmmo(int max)
    {
        maxAmmo = max;
    }

    //method called by GunManager, that handles the display of crosshair
    public IEnumerator displayCrosshairs(bool status)
    {
        if(status)
        {
            //we show the cross
            yield return new WaitForSeconds(0.1f);
            crosshair.gameObject.SetActive(true);
        }
        else
        {
            //we hide the cross
            yield return new WaitForSeconds(0f);
            crosshair.gameObject.SetActive(false);
        }
    }

}
