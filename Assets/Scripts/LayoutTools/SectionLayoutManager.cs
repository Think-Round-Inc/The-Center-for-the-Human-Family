using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionLayoutManager : MonoBehaviour
{
    [SerializeField] SectionNode backYards = null;
    [SerializeField] SectionNode mothers = null;
    [SerializeField] SectionNode fathers = null;
    [SerializeField] SectionNode boysGroups = null;
    [SerializeField] SectionNode girlsGroups = null;
    [SerializeField] SectionNode adolescentsPrinces = null;
    [SerializeField] SectionNode girls = null;
    [SerializeField] SectionNode boys = null;
    [SerializeField] SectionNode infants = null;
    [SerializeField] SectionNode frontYards = null;

    public Dictionary<NodeEnum, SectionNode> nodeDictionary = new Dictionary<NodeEnum, SectionNode>();

    private void Awake()
    {
        InitDictionary();
    }

    private void InitDictionary()
    {
        if(backYards != null && !nodeDictionary.ContainsKey(NodeEnum.BackYards))
        {
            nodeDictionary.Add(NodeEnum.BackYards, backYards);
        }
        if (mothers != null && !nodeDictionary.ContainsKey(NodeEnum.Mothers))
        {
            nodeDictionary.Add(NodeEnum.Mothers, mothers);
        }
        if (fathers != null && !nodeDictionary.ContainsKey(NodeEnum.Fathers))
        {
            nodeDictionary.Add(NodeEnum.Fathers, fathers);
        }
        if (boysGroups != null && !nodeDictionary.ContainsKey(NodeEnum.BoysGroups))
        {
            nodeDictionary.Add(NodeEnum.BoysGroups, boysGroups);
        }
        if (girlsGroups != null && !nodeDictionary.ContainsKey(NodeEnum.GirlsGroups))
        {
            nodeDictionary.Add(NodeEnum.GirlsGroups, girlsGroups);
        }
        if (adolescentsPrinces != null && !nodeDictionary.ContainsKey(NodeEnum.Adolescents))
        {
            nodeDictionary.Add(NodeEnum.Adolescents, adolescentsPrinces);
        }        
        if (girls != null && !nodeDictionary.ContainsKey(NodeEnum.Girls))
        {
            nodeDictionary.Add(NodeEnum.Girls, girls);
        }
        if (boys != null && !nodeDictionary.ContainsKey(NodeEnum.Boys))
        {
            nodeDictionary.Add(NodeEnum.Boys, boys);
        }
        if (infants != null && !nodeDictionary.ContainsKey(NodeEnum.Infants))
        {
            nodeDictionary.Add(NodeEnum.Infants, infants);
        }
        if (frontYards != null && !nodeDictionary.ContainsKey(NodeEnum.FrontYards))
        {
            nodeDictionary.Add(NodeEnum.FrontYards, frontYards);
        }
    }


    public SectionNode GetNode(NodeEnum node)
    {
        if (nodeDictionary.Count == 0)
        {
            InitDictionary();
        }

        if (nodeDictionary.ContainsKey(node))
        {
            return nodeDictionary[node];
        }
        return null;
    }
}
