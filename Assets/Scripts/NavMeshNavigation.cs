using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshNavigation : MonoBehaviour
{
    public static int GetNavMeshAgentID(string name)
    {

        for (int i = 0; i < NavMesh.GetSettingsCount(); i++)
        {
            NavMeshBuildSettings settings = NavMesh.GetSettingsByIndex(index: i);
            if (name == NavMesh.GetSettingsNameFromID(agentTypeID: settings.agentTypeID))
            {
                return settings.agentTypeID;
            }
        }
        return -1;
    }

    //Less efficient, better to cache NavMeshAgentID
    public static NavMeshPath CalculatePathTo(Transform who, Vector3 where, string agentType)
    {
        int agentID = GetNavMeshAgentID("Enemy");
        return CalculatePathTo(who, where, agentID);
    }

    public static NavMeshPath CalculatePathTo(Transform who, Vector3 where, int agentTypeId)
    {
        int agentID = GetNavMeshAgentID("Enemy");

        NavMeshQueryFilter filter = new NavMeshQueryFilter();
        filter.agentTypeID = agentID;
        NavMeshPath path = new NavMeshPath();
        if (NavMesh.CalculatePath(who.position, where, filter, path))
        {
            return path;
        }
        return null;
    }

}
