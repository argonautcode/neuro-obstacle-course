[System.Serializable]
public struct Synapse
{
    public int source;
    public int sink;
    public double weight;

    public Synapse(int source, int sink, double weight)
    {
        this.source = source;
        this.sink = sink;
        this.weight = weight;
    }
}