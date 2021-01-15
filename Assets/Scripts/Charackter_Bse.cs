using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Charackter_Bse : MonoBehaviour
{
    public NavMeshAgent agent;
    public bool Islive = true;
    public System.Action OnDead;
    public float distance = 5;



    public virtual void Start()
    {
        Debug.Log(transform.right);
        OnDead = () =>
        {

            Islive = false;
            int randpart = Random.Range(10, 30);
            for (int i = 0; i < randpart; i++)
            {
                
                Vector3 ps = new Vector3(Random.Range(-distance,distance), Random.Range(-distance,distance), Random.Range(-distance,distance));
                ps = ps + transform.position;
              
                GameObject createdpart = Instantiate(GameManager.instance.bodypartprefab,ps, Quaternion.identity);
                createdpart.GetComponent<Rigidbody>().AddExplosionForce(Random.Range(10f,20f),transform.position, 50f);
                createdpart.GetComponent<Renderer>().material.color = CurrentColor;
            }
            transform.GetComponent<MeshRenderer>().enabled = false;
            GameManager.instance.AllCharackter.Remove(this);
            GameManager.instance.NotImposter.Remove(this);

        };
    }


    public void GotoPos(Vector3 pos)
    {
        agent.SetDestination(pos);
    }

    public Color32 CurrentColor
    {
        get
        {
            return transform.GetComponent<Renderer>().material.color;
        }
        set
        {
            transform.GetComponent<Renderer>().material.color = value;
        }
    }

   
}
