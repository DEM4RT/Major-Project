using System;
using UnityEngine;

public class ActionNode : Node
{
    private readonly Func<NodeState> action;
    
    public ActionNode(Func<NodeState> action)
    {
        this.action = action;
    }

    public override NodeState Tick()
    {
        return action();
    }
}
