using UnityEngine;

public class Agent : MonoBehaviour
{
    [SerializeField] public Genome genome;
    [SerializeField] private Brain brain;
    [SerializeField] private double[] input;
    [SerializeField] private double[] output;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private bool reachedGoal;
    private float timer;

    void Start()
    {
        genome = GenomeUtil.CreateGenome();
        brain = GenomeUtil.CreateBrain(genome);
        input = new double[GenomeUtil.numInput];
        output = new double[GenomeUtil.numOutput];
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        transform.localScale.Set(1, 1.5f, 1);
        transform.Rotate(0, 0, -90);
        rb.drag = 0.5f;
        rb.angularDrag = 1f;
        timer = 60;
    }

    void FixedUpdate()
    {
        if (!reachedGoal && timer > 0)
        {
            timer -= Time.deltaTime;
        }

        for (int i = 0; i < 3; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(Mathf.Cos((transform.eulerAngles.z + 45 + i * 45) * Mathf.Deg2Rad), Mathf.Sin((transform.eulerAngles.z + 45 + i * 45) * Mathf.Deg2Rad)), 5);
            if (hit.collider != null)
            {
                input[i] = (5 - hit.distance) / 5f;
            }
            else
            {
                input[i] = 0;
            }
        }
        input[3] = Mathf.Abs(transform.eulerAngles.z < 90 ? transform.eulerAngles.z + 90 : transform.eulerAngles.z - 270) / 180f;

        output = brain.Run(input);
        rb.AddRelativeForce(new Vector2(0, (float) output[0] * 8f));
        rb.AddTorque((float) output[1] * 0.5f);

        if (transform.position.x > 97)
        {
            reachedGoal = true;
        }
    }

    public float GetScore()
    {
        return (reachedGoal) ? 1000 - timer : transform.position.x;
    }

    public void Reset(Genome g)
    {
        genome = new Genome(g.Code);
        brain = GenomeUtil.CreateBrain(g);
        transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        transform.Rotate(0, 0, -90);
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        timer = 60;
    }

    public void Toggle(bool show)
    {
        sr.enabled = show;
    }
}
