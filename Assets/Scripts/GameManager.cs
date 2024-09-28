using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;

    private void Start()
    {
        InitializeTargets();
    }

    void InitializeTargets()
    {
        var targetsContainer = GameObject.Find("--Targets--");
        if (targetsContainer != null)
        {
            foreach (Transform target in targetsContainer.transform)
            {
                targets.Add(target.gameObject);
            }
        }
    }
}
