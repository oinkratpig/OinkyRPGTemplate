#if TOOLS
using Godot;
using System;
using System.Collections.Generic;

[Tool]
public partial class OinkyRPGCustomNodes : EditorPlugin
{
    /// <summary>
    /// Stores data for custom nodes.
    /// </summary>
    private class CustomNodeData
    {
        public string NodeName { get; private set; }
        public string ParentNodeName { get; private set; }
        public Script Script { get; private set; }
        public Texture2D Icon { get; private set; }

        /* Constructor */
        public CustomNodeData(ref List<CustomNodeData> customNodesList, string nodeName,
                string parentNodeName, string scriptName, string iconFilename)
        {
            NodeName = nodeName;
            ParentNodeName = parentNodeName;

            Script = GD.Load<Script>($"{OinkyRPG.PATH_SCRIPTS}rpgnodes/{scriptName}");
            Icon = GD.Load<Texture2D>($"{OinkyRPG.PATH_CUSTOM_NODE_ICONS}{iconFilename}");

            customNodesList.Add(this);

        } // end constructor

    } // end class CustomNodeData

    // Fields
    private List<CustomNodeData> _customNodes;

    /* Constructor */
    public OinkyRPGCustomNodes()
    {
        _customNodes = new List<CustomNodeData>();

    } // end constructor

    /* Create custom nodes */
    public override void _EnterTree()
	{
        if (!Engine.IsEditorHint()) return;

        // Custom nodes
        new CustomNodeData(ref _customNodes, "RPGNode", "Node", "RPGNode.cs", "icon_node.png");
        new CustomNodeData(ref _customNodes, "RPGMoveable", "Node", "RPGMoveable.cs", "icon_moveable.png");
        new CustomNodeData(ref _customNodes, "RPGCharacter", "Node", "RPGCharacter.cs", "icon_character.png");
        new CustomNodeData(ref _customNodes, "RPGPlayer", "Node", "RPGPlayer.cs", "icon_player.png");

        // Add all nodes
        foreach(CustomNodeData customNode in _customNodes)
            AddCustomType(customNode.NodeName, customNode.ParentNodeName, customNode.Script, customNode.Icon);

    } // end _EnterTree

    /* Cleanup */
	public override void _ExitTree()
	{
        // Remove all nodes
        foreach(CustomNodeData customNode in _customNodes)
            RemoveCustomType(customNode.NodeName);
        _customNodes.Clear();
        
    } // end _ExitTree

} // end class OinkyRPGCustomNodes
#endif