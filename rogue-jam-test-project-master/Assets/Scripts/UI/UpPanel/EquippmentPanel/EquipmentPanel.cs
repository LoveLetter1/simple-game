using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentPanel : PanelBase
{
    private WeaponPanel[] weaponPanels;

    protected override void Awake()
    {
        base.Awake();
        Initialzation();
    }

    private void Initialzation()
    {
        weaponPanels = GetComponentsInChildren<WeaponPanel>();
    }
    
    #region Public API

    public void SetWeaponPanel(KeyCode keyCode, Weapon weapon)
    {
        int idx = keyCode == KeyCode.J ? 0 : keyCode == KeyCode.K ? 1 : -1;
        if (idx == -1)
            return;
        weaponPanels[idx].SetWeaponInfo(weapon);
    }
    #endregion
}
