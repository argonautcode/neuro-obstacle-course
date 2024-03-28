using System;
using UnityEngine;

[System.Serializable]
public class Brain
{
    private double[] hidden;
    private double[] output;
    [SerializeField] private Synapse[] ihEdges;
    [SerializeField] private Synapse[] hoEdges;

    public Brain(int numHidden, int numOutput, Synapse[] ihEdges, Synapse[] hoEdges)
    {
        hidden = new double[numHidden];
        output = new double[numOutput];

        this.ihEdges = ihEdges;
        this.hoEdges = hoEdges;
    }

    public double[] Run(double[] input)
    {
        Array.Clear(hidden, 0, hidden.Length);
        RunSynapses(ihEdges, input, hidden);
        Activate(hidden);
        Array.Clear(output, 0, output.Length);
        RunSynapses(hoEdges, hidden, output);
        Activate(output);
        return output;
    }

    public void RunSynapses(Synapse[] edges, double[] sources, double[] sinks)
    {
        for (int i = 0; i < edges.Length; i++)
        {
            double sourceVal = sources[edges[i].source % sources.Length];
            sinks[edges[i].sink % sinks.Length] += sourceVal * edges[i].weight;
        }
    }

    public void Activate(double[] neurons)
    {
        for (int i = 0; i < neurons.Length; i++)
        {
            neurons[i] = BrainUtil.FastTanh(neurons[i]);
        }
    }
}
