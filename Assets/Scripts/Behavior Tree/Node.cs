using UnityEngine;

public enum NodeState
{
    Running,
    Success,
    Failure
}
public abstract class Node
{
    public abstract NodeState Tick();
}
