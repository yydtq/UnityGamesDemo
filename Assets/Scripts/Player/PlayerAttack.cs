using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Weapon weapon;
    public Sprite weaponIcon;

    private void Update()
    {
        if (weapon != null && Input.GetKeyDown(KeyCode.Space))
        {
            weapon.Attack();
        }
    }
    public void LoadWeapon(Weapon weapon)
    {
        this.weapon = weapon;
    }
    public void LoadWeapon(ItemSO itemSO)
    {
        if (weapon != null)
        {
            Destroy(weapon.gameObject);
            weapon = null;
        }
        
        string prefabName = itemSO.prefab.name;
        Transform weaponParent = transform.Find(prefabName + "Position");
        GameObject weaponGO = GameObject.Instantiate(itemSO.prefab);
        weaponGO.transform.parent = weaponParent;
        weaponGO.transform.localPosition = Vector3.zero;
        weaponGO.transform.localRotation = Quaternion.identity;

        this.weapon = weaponGO.GetComponent<Weapon>();
        this.weaponIcon = itemSO.icon;
        PlayerPropertyUI.Instance.UpdatePlayerPropertyUI();
    }

    public void UnLoadWeapon()
    {
        weapon = null;
    }
}
