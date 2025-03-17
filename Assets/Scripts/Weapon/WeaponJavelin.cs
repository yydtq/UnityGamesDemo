using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponJavelin : Weapon
{
    public float bullteSpeed;
    public GameObject bulltePrefab;
    private GameObject bullteGo;


    private void Start()
    {
        SpawnBullte();
    }

    public override void Attack()
    {
        if (bullteGo != null)
        {
            bullteGo.transform.parent = null;
            bullteGo.GetComponent<Rigidbody>().velocity = transform.forward * bullteSpeed;
            bullteGo.GetComponent<Collider>().enabled = true;
            Destroy(bullteGo, 10);
            bullteGo = null;
            Invoke("SpawnBullte", 0.5f);
        }
        else
        {
            return;
        }
        
       
    }

    private void SpawnBullte()
    {
        bullteGo = GameObject.Instantiate(bulltePrefab, transform.position, transform.rotation);
        bullteGo.transform.parent = transform;
        bullteGo.GetComponent<Collider>().enabled = false;
        if (tag == Tag.INTERACTABLE)  
        {
            Destroy(bullteGo.GetComponent<JavelinBullte>());

            bullteGo.tag = Tag.INTERACTABLE;
            PickableObject po = bullteGo.AddComponent<PickableObject>();
            po.itemSO = GetComponent<PickableObject>().itemSO;

            Rigidbody rgd = bullteGo.GetComponent<Rigidbody>();
            rgd.constraints = ~RigidbodyConstraints.FreezeAll;
            bullteGo.GetComponent<Collider>().enabled = true;
            bullteGo.transform.parent = null;
            Destroy(this.gameObject);
        }
    }
}
