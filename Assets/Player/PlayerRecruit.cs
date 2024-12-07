using System;
using UnityEngine;

public class PlayerRecruit : MonoBehaviour,IPlayerRecruit
{
    public static Action OnCrewMemberRecruited;

    private bool canRecruit;
    
    public bool CanRecruit 
    { 
        get => canRecruit;
        set => canRecruit = value; 
    }

    public void RecruitCrewMember()
    {
        if (canRecruit)
        {
            OnCrewMemberRecruited?.Invoke();
        }
    }
}
