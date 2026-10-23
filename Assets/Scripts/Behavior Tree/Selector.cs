using System.Collections.Generic;
using UnityEngine;

public class Selector : Node
{
    private readonly List<Node> children;

    public Selector(List<Node> children)
    {
        this.children = children;
    }
    public override NodeState Tick()
    {
        foreach (Node child in children)
        {
            NodeState result = child.Tick();

            if (result != NodeState.Failure)
            {
                return result;
            }
        }
        return NodeState.Failure;
    }
}
