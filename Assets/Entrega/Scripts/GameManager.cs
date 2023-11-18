using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<Node> _allNodes = new List<Node>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        AddAllNodes();
    }

    void AddAllNodes()
    {
        Node[] allNodes = FindObjectsOfType<Node>();
        _allNodes.AddRange(allNodes);
    }
}
