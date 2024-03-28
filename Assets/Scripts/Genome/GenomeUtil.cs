using System;

public static class GenomeUtil
{
    // These are set specifically for the current task
    public static int numInput = 4;
    public static int numHidden = 6;
    public static int numOutput = 2;

    public static Genome CreateGenome()
    {
        return new Genome(8 * (numInput * numHidden + numHidden * numOutput));
    }

    public static Brain CreateBrain(Genome g)
    {
        if (g.Code.Length < 8 * (numInput * numHidden + numHidden * numOutput))
        {
            throw new System.Exception("Genome is too short");
        }

        Synapse[] ihEdges = new Synapse[numInput * numHidden];
        Synapse[] hoEdges = new Synapse[numHidden * numOutput];

        for (int i = 0; i < numInput; i++)
        {
            for (int j = 0; j < numHidden; j++)
            {
                ihEdges[i * numHidden + j] = new Synapse(i, j, Convert.ToInt32(g.Code.Substring(8 * (i * numHidden + j), 8), 2) / 63.75 - 2);
            }
        }

        for (int i = 0; i < numHidden; i++)
        {
            for (int j = 0; j < numOutput; j++)
            {
                hoEdges[i * numOutput + j] = new Synapse(i, j, Convert.ToInt32(g.Code.Substring(8 * (numInput * numHidden + i * numOutput + j), 8), 2) / 63.75 - 2);
            }
        }

        Brain b = new Brain(numHidden, numOutput, ihEdges, hoEdges);
        return b;
    }
}
