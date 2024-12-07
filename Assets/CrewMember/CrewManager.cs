using System.Collections.Generic;
using UnityEngine;

public class CrewManager : MonoBehaviour
{
    [SerializeField] private List<Transform> standPointsForCrewMembers;
    [SerializeField] private float maxCrewMemberCountToRecruit = 3f;

    private List<CrewMember> memberList;
    private int memberCount = 0;

    private void OnEnable()
    {
        CrewMember.OnCrewMemberSaved += OnCrewMemberSaved;
    }

    private void OnDisable()
    {
        CrewMember.OnCrewMemberSaved -= OnCrewMemberSaved;
    }

    private void OnCrewMemberSaved(CrewMember member)
    {
        member.StopRunningAnimation();
        member.transform.position = standPointsForCrewMembers[memberCount].transform.position;
        member.transform.SetParent(standPointsForCrewMembers[memberCount].transform);
        ++memberCount;
    }
}
