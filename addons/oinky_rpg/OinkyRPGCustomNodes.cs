#if TOOLS
using Godot;
using System;

[Tool]
public partial class OinkyRPGCustomNodes : EditorPlugin
{
	public override void _EnterTree()
	{
        Script script = GD.Load<Script>($"{OinkyRPG.PATH_SCRIPTS}rpg_nodes");
        Texture2D texture = GD.Load<Texture2D>($"{OinkyRPG.PATH_CUSTOM_NODE_ICONS}icon_node.png");
        AddCustomType("RPGNode", "Node2D", script, texture);
    }

	public override void _ExitTree()
	{
        RemoveCustomType("RPGNode");
    }

} // end class OinkyRPGCustomNodes
#endif