using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Genetics : MonoBehaviour {
    public GameObject[] population;
    List<GameObject> matingPool;

    public GameObject templateFrog;
    int currentMember;

    public int tick;
    public int lifeSpan;
    bool popInitialised = false;

	// Use this for initialization
	void Start ()
    {
        tick = 0;
        InitialisePopulation();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (popInitialised)
        {
            if (tick < lifeSpan)
            {
                
               // for (int i = 0; i < lifeSpan; i++)
              //  {
                    Debug.Log("Current Population member: " + currentMember + "/" + population.Length);
                Debug.Log("population member " + currentMember + " name is" + population[currentMember].name);
                
                    switch (population[currentMember].GetComponent<Frog>().myDNA.genes[tick])
                    {
                        case 0:
                            population[currentMember].GetComponent<Frog>().MoveUp();
                            break;
                        case 1:
                            population[currentMember].GetComponent<Frog>().MoveLeft();
                            break;
                        case 2:
                            population[currentMember].GetComponent<Frog>().MoveRight();
                            break;
                        case 3:
                            population[currentMember].GetComponent<Frog>().MoveDown();
                            break;
                        case 4:
                            population[currentMember].GetComponent<Frog>().Wait();
                            break;
                    }
              //  }
                
                tick++;
            }
            if (tick == lifeSpan)
            {
                
                if (currentMember == population.Length - 1)
                {
                    currentMember = 0;
                    popInitialised = false;
                    EvaluatePopulation();
                    ProducePopulation(Selection());
                }
                else
                {
                    currentMember++;
                    
                    tick = 0;
                }
            }
        }
	}

    void InitialisePopulation()
    {
        
            for (int i = 0; i < population.Length; i++)
            {
                GameObject g = Instantiate(templateFrog);
                population[i] = g;
                population[i].GetComponent<Frog>().myDNA = population[i].GetComponent<DNA>();
                population[i].GetComponent<Frog>().myDNA.FillGenes(null, lifeSpan);
               

            }
        popInitialised = true;
    }

    GameObject[] Selection()
    {
        GameObject[] newPop = new GameObject[population.Length];
        for (int i = 0; i < population.Length; i++)
        {
            Frog parentA = matingPool[(int)Mathf.Round(Random.Range(0, matingPool.Count))].GetComponent<Frog>();
            Frog parentB = matingPool[(int)Mathf.Round(Random.Range(0, matingPool.Count))].GetComponent<Frog>();
            GameObject child = Instantiate(templateFrog);
            newPop[i] = child;

            int[] childGenes = parentA.myDNA.CrossOver(parentB.myDNA.genes);

          //  newPop[i].GetComponent<Frog>().myDNA = population[i].GetComponent<DNA>();
            child.GetComponent<Frog>().myDNA.FillGenes(childGenes, lifeSpan);
            
        }
        return newPop;
    }

    void ProducePopulation(GameObject[] pop)
    {
        foreach (GameObject n in population)
        {
            Destroy(n);
        }
        population = pop;
        popInitialised = true;
    }

    void EvaluatePopulation()
    {
        float maxFitness = 0;
        matingPool = new List<GameObject>();
        float avgFit = 0;
        for (int i = 0; i < population.Length; i++)
        {
            population[i].GetComponent<Frog>().CalculateFitness();

            if (population[i].GetComponent<Frog>().fitness > maxFitness) { maxFitness = population[i].GetComponent<Frog>().fitness; }

            population[i].GetComponent<Frog>().fitness /= maxFitness;
            avgFit += population[i].GetComponent<Frog>().fitness;
            float n = population[i].GetComponent<Frog>().fitness * 100;
            for (int j = 0; j < n; j++)
            {
                matingPool.Add(population[i]);
            }
        }
        Debug.Log("Average fitness: " + avgFit/population.Length);
    }
}
