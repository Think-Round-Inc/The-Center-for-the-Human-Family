using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeEnum { Default, BackYards, Mothers, Fathers, GirlsGroups, BoysGroups, Adolescents, Girls, Boys, Infants, FrontYards }

public static class NodeInfo
{
    public static Dictionary<NodeEnum, Color> nodeColorDictionary = new Dictionary<NodeEnum, Color>()
    {
        { NodeEnum.BackYards,           new Color( 1f, 1f, 1f) },
        { NodeEnum.Mothers,             new Color(.33f, .33f, .33f) },
        { NodeEnum.Fathers,             new Color(.66f, .66f, .66f) },
        { NodeEnum.GirlsGroups,         new Color( 1f, 0f,  0f) },
        { NodeEnum.BoysGroups,          new Color( 0f, 0f, 1f) },
        { NodeEnum.Adolescents,         new Color( 1f, 1f, 0f) },
        { NodeEnum.Girls,               new Color( 1f, .5f, 0f) },
        { NodeEnum.Boys,                new Color( 0f, 1f, 0f) },
        { NodeEnum.Infants,             new Color( 1f, 0f, 1f) },
        { NodeEnum.FrontYards,          new Color( 0.9f, 0.9f,  0.9f) }
    };

    public static Color GetColor(NodeEnum nodeEnum)
    {
        if(nodeColorDictionary.ContainsKey(nodeEnum))
        {
            return nodeColorDictionary[nodeEnum];
        }
        return Color.white;
    }
}
