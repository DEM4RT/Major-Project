using System.Collections.Generic;
using UnityEngine;

public class Sequence : Node
{
    private readonly List<Node> children;

    public Sequence(List<Node> children)
    {
        this.children = children;
    }

    public override NodeState Tick()
    {
        foreach (Node child in children)
        {
            NodeState result = child.Tick();

            if (result != NodeState.Success)
            {
                return result;
            }
        }

        return NodeState.Success;
    }

}
