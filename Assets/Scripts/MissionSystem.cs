using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionSystem : MonoBehaviour
{
    public List<MissionProfil> AllMission = new List<MissionProfil>();


    public void Update()
    {
        Exacute();
    }

    public void Exacute()
    {
        for (int i = 0; i < AllMission.Count; i++)
        {
               MissionProfil mission=AllMission[i];
              if(!mission.IsStarted)
            {
                mission.OnStarted?.Invoke();
                mission.IsStarted = true;
            }
              else
            {
                if(mission.OnCompltedRule.Invoke())
                {
                    mission.OnCompleted?.Invoke();
                    AllMission.Remove(mission);
                }
                else
                {
                    mission.OnContinous?.Invoke();
                }
             
            }
        }
    }



    public class MissionProfil
    {
        public bool IsStarted = false;
        public System.Action OnStarted;
        public System.Action OnContinous;
        public System.Action OnCompleted;
        public System.Func<bool> OnCompltedRule;
    }
}
