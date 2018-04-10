using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA : MonoBehaviour {
    public int[] genes;
   
    

    void Start()
    {
        
    }
    // Update is called once per frame
    void Update() {

    }

    public void FillGenes(int[] newGenes, int size)
    {
        if (newGenes != null)
        {
            Debug.Log("Sucessful new genes");
            genes = newGenes;
        }

        else
        {
            genes = new int[size];
            for (int i = 0; i < genes.Length; i++)
            {
                genes[i] = (int)Mathf.Round(Random.Range(0, 5));
            }
        }
    }

    public int[] CrossOver(int[] parentBGenes)
    {
        int[] childGenes = new int[genes.Length];
        int midPoint = (int)Mathf.Floor(genes.Length / 2);
        for (int i = 0; i < genes.Length; i++)
        {
            if (i < midPoint)
            {
                childGenes[i] = genes[i];
            }
            else
            {
                childGenes[i] = parentBGenes[i];
            }
        }
        return MutateGenes(childGenes);
    }

    public int[] MutateGenes(int[] initGenes)
    {
        for (int i = 0; i < initGenes.Length; i++)
        {
            if (Random.Range(0, 1) < 0.001)
            {
                initGenes[i] = (int)Mathf.Round(Random.Range(0, 5));
            }
        }
        return initGenes;
    }
}
