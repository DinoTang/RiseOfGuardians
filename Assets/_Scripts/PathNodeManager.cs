using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNodeManager : DinoMonoBehaviour
{
    private static PathNodeManager instance;
    public static PathNodeManager Instance => instance;
    [SerializeField] protected Transform mergePathNodes;
    [SerializeField] protected Transform pathNode1;
    [SerializeField] protected Transform pathNode2;
    [SerializeField] protected List<PathNode> pathNodes;
    public List<PathNode> PathNodes => pathNodes;
    protected override void Awake()
    {
        if (PathNodeManager.instance != null)
            Debug.LogWarning("Only 1 PathNodeManager allows to exist");
        instance = this;
    }
    protected override void LoadComponent()
    {
        this.LoadMergePN();
        this.LoadPN1();
        this.LoadPN2();
        this.LoadPathNodes();
    }
    protected void LoadMergePN()
    {
        if (this.mergePathNodes != null) return;
        this.mergePathNodes = transform.Find("MergePathNode");
        Debug.Log(this.transform.name + ": LoadMergePN");
    }
    protected void LoadPN1()
    {
        if (this.pathNode1 != null) return;
        this.pathNode1 = transform.Find("PathNode1");
        Debug.Log(this.transform.name + ": LoadMergePN");
    }
    protected void LoadPN2()
    {
        if (this.pathNode2 != null) return;
        this.pathNode2 = transform.Find("PathNode2");
        Debug.Log(this.transform.name + ": LoadMergePN");
    }

    protected void LoadPathNodes()
    {
        
        this.AddPathNodeFromChild(this.pathNode1);
        this.AddPathNodeFromChild(this.pathNode2);
        this.AddPathNodeFromChild(this.mergePathNodes);
    }

    protected void AddPathNodeFromChild(Transform pathNodes)
    {
        if (this.pathNodes == null) return;
        foreach (Transform child in pathNodes)
        {
            PathNode pathNode = new PathNode();
            pathNode.trans = child;
            pathNode.type = pathNodes.name;
            this.pathNodes.Add(pathNode);
        }
    }
}
