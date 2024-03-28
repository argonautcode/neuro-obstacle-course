using System;
using UnityEngine;

public class Simulation : MonoBehaviour
{
    [SerializeField] private Agent agentPrefab;
    [SerializeField] private int gen;
    [SerializeField] public float timer;
    [SerializeField] private float maxTime;
    [SerializeField] private bool paused;
    [SerializeField] private float speed;
    [SerializeField] private bool bestOnly;
    private bool bestToggle;
    private Agent[] agents;

    void Start()
    {
        agents = new Agent[64];
        for (int i = 0; i < 64; i++)
        {
            agents[i] = Instantiate(agentPrefab, Vector3.zero, Quaternion.identity, transform);
        }
        gen = 1;
        timer = maxTime;
        bestToggle = false;
    }

    void Update()
    {
        if (bestToggle != bestOnly)
        {
            bestToggle = bestOnly;
            for (int i = 1; i < 64; i++)
            {
                agents[i].Toggle(!bestOnly);
            }
        }
        Time.timeScale = paused ? 0 : speed;
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            // Sort by fitness
            Array.Sort(agents, (x, y) => (x.GetScore() <= y.GetScore() ? 1 : -1));

            // Select the top 50% to reproduce with crossover and mutation
            for (int i = 0; i < 16; i++)
            {
                agents[2 * i].Reset(agents[2 * i].genome);
                agents[2 * i + 1].Reset(agents[2 * i + 1].genome);
                agents[32 + 2 * i].Reset(agents[2 * i].genome.Crossover(agents[2 * i + 1].genome).Mutate());
                agents[32 + 2 * i + 1].Reset(agents[2 * i + 1].genome.Crossover(agents[2 * i].genome).Mutate());
            }

            gen++;
            timer = maxTime;
        }
    }
}
