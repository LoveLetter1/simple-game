using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
public class WeaponPanel : PanelBase
{
    public Image[] image;

    protected override void Awake()
    {
        base.Awake();
        image = GetComponentsInChildren<Image>();
    }
    #region Public API

    public void SetWeaponInfo(Weapon weapon)
    {
        image[0].sprite = weapon.weaponSprite;
        
        
    }

    public void Reset()
    {
        image[0].sprite = null;
    }
    #endregion
}
