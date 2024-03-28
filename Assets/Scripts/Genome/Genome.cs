using System.Text;
using UnityEngine;

[System.Serializable]
public class Genome
{
    [SerializeField] private string code;
    public string Code { get { return code; } }

    private static float inversion = 0.01f;

    public Genome(string code)
    {
        this.code = code;
    }

    public Genome(int length)
    {
        StringBuilder sb = new StringBuilder(length);
        for (int i = 0; i < length; i++)
        {
            sb.Append((int) Random.Range(0, 2));
        }
        code = sb.ToString();
    }

    public Genome Mutate()
    {
        StringBuilder sb = new StringBuilder(code.Length);
        for (int i = 0; i < code.Length; i++)
        {
            if (Random.value < inversion)
            {
                sb.Append(code[i] == '1' ? '0' : '1');
            }
            else
            {
                sb.Append(code[i]);
            }
        }
        return new Genome(sb.ToString());
    }

    public Genome Crossover(Genome mate)
    {
        StringBuilder sb = new StringBuilder(code.Length);
        int cut = Random.Range(1, code.Length);
        sb.Append(code.Substring(0, cut));
        sb.Append(mate.Code.Substring(cut));
        return new Genome(sb.ToString());
    }
}
