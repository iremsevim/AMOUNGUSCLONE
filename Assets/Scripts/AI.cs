using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AI : Charackter_Bse
{
    public MissionSystem missionsystem;
  

    public override void Start()
    {
        base.Start();
    }

    public void SetMission()
    {
        if(GameManager.instance.imposter==this)
        {
            AddImposterMission();
        }
        else
        {
            AddNotImposterMission();
        }
       
    }

    public void AddNotImposterMission()
    {
        Transform currentpoint = null;
        float timer = 2f;
        MissionSystem.MissionProfil newmission = new MissionSystem.MissionProfil();
        newmission.OnStarted = () =>
          {
            currentpoint= GameManager.instance.AllMissionPoints[Random.Range(0, GameManager.instance.AllMissionPoints.Count)];
              GotoPos(currentpoint.position);
          };
        newmission.OnContinous = () =>
          {
               
              if(Vector3.Distance(transform.position,currentpoint.position)<50f)
              {
                  timer -= Time.deltaTime;
                  if(timer<=0)
                  {
                      List<Transform> filteredpoints = GameManager.instance.AllMissionPoints.FindAll(x => x != currentpoint);
                      currentpoint = filteredpoints[Random.Range(0, filteredpoints.Count)];
                      GotoPos(currentpoint.position);
                      timer = Random.Range(2f,5f);
                  }
              }
          };
        newmission.OnCompleted = () =>
          {
              agent.isStopped = true;
          };
        newmission.OnCompltedRule = () =>
          {
              return !Islive;
          };
        missionsystem.AllMission.Add(newmission);
    }

    public void AddImposterMission()
    {
        bool IsFake = false;
        Transform currentpoint=null;
        Charackter_Bse currenttarget = null;
        float timer = 1f;
        MissionSystem.MissionProfil newmission = new MissionSystem.MissionProfil();
        newmission.OnStarted = () =>
          {
              IsFake = Random.Range(0, 10) > 8;
              if (IsFake)
              {
                  currentpoint = GameManager.instance.AllMissionPoints[Random.Range(0, GameManager.instance.AllMissionPoints.Count)];
                  GotoPos(currentpoint.position);

                  Debug.Log("Ayak çekiyor");
              }
              else
              {
                  //Following
                  currenttarget = GameManager.instance.NotImposter[Random.Range(0, GameManager.instance.NotImposter.Count)];
                 
                  GotoPos(currenttarget.transform.position);

                  Debug.Log("Düşmanı takip ediyor");
              }

          };
         
        newmission.OnContinous = () =>
          {
              if(IsFake)
              {
                  if(Vector3.Distance(transform.position,currentpoint.position)<50f)
                  {
                      timer -= Time.deltaTime;
                      if(timer<=0)
                      {
                          SwitchStateController();
                          timer = 1f;
                      }
                  }
              }
              else
              {
                  //Following
                  GotoPos(currenttarget.transform.position);
              
                  if (Vector3.Distance(transform.position,currenttarget.transform.position)<100f)
                  {
                      if(!IsThereAnyEnemy())
                      {
                          Debug.Log("Düşmana Saldır");
                          currenttarget.OnDead?.Invoke();


                      }
                     
                      SwitchStateController();
                  }
              }
          };
        newmission.OnCompleted = () =>
          {

          };
        newmission.OnCompltedRule = () =>
          {
              return false;
          };
         missionsystem.AllMission.Add(newmission);

        void SwitchStateController()
        {
            IsFake = Random.Range(0, 10) > 8;
            if(IsFake)
            {
                currentpoint = GameManager.instance.AllMissionPoints[Random.Range(0, GameManager.instance.AllMissionPoints.Count)];
                GotoPos(currentpoint.position);

                Debug.Log("State Değişti noktaya gidiyor");
            }
            else
            {
                //Following
                currenttarget = GameManager.instance.NotImposter[Random.Range(0, GameManager.instance.NotImposter.Count)];
                GotoPos(currenttarget.transform.position);

                Debug.Log("State Değişti düşman  takip ediyor");
            }
        }
   
        bool IsThereAnyEnemy()
        {
            Collider[] allhitted = Physics.OverlapSphere(transform.position, 100f);
            List<Charackter_Bse> hittedfiltered = allhitted.ToList().FindAll(x => x.GetComponent<Charackter_Bse>()).ConvertAll(x => x.GetComponent<Charackter_Bse>());
            hittedfiltered = hittedfiltered.FindAll(x => x != this && x != currenttarget);
            return hittedfiltered.Count > 0;

            //a,b,c
        }
    }

}
