using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharackter : Charackter_Bse
{
    public LayerMask groundmask;

    public override void Start()
    {
        base.Start();
    }
    public void Update()
    {
        ClickDetection();
    }
    public void ClickDetection()
    {
        if (!GameManager.instance.ISStartGame) return;

        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray.origin,ray.direction,out hit,200f,groundmask))
            {
                GotoPos(hit.point);
            }
        }
    }
}
