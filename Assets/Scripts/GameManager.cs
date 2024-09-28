using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;

    public TMP_Text targetsHitText;

    public static GameManager Instance;

    private void Start()
    {
        Instance = this;
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

    public void UpdateUI()
    {
        int hitCount = 0;

        foreach (GameObject target in targets)
        {
            if (target.GetComponent<TargetBehavior>().isHit) hitCount++;
        }

        targetsHitText.text = $"{hitCount}/{targets.Count}";
    }
}
