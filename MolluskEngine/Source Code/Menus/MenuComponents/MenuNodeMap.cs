using System;
using System.Collections.Generic;
using System.Linq;

namespace MolluskEngine.Menus;

public class MenuNodeMap
{
    public List<Node> Nodes;
    public Node activeNode {get {return Nodes[activeNodeIndex];}}
    public bool inspectable {get {return Nodes.Any(node => node is INodeInspectable);}}
    public bool inspectActive{get;set;}
    public int activeNodeIndex {get;set;}
}
