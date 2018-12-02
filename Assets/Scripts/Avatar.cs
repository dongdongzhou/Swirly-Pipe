using UnityEngine;

public class Avatar : MonoBehaviour
{
    public ParticleSystem shape, trail, burst;
    public float deathCountDown = -1f;
    private Player player;

    private void Awake()
    {
        player = transform.root.GetComponent<Player>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (deathCountDown < 0f)
        {
            ParticleSystem.EmissionModule shapeEmission = shape.emission;
            shapeEmission.enabled = false;
            ParticleSystem.EmissionModule trailEmission = trail.emission;
            trailEmission.enabled = false;
            burst.Emit(burst.main.maxParticles);
            deathCountDown = burst.main.startLifetime.constant;
        }
    }

    void Update()
    {
        if (0f <= deathCountDown)
        {
            deathCountDown -= Time.deltaTime;
            if (deathCountDown <= 0f)
            {
                deathCountDown = -1f;
                ParticleSystem.EmissionModule shapeEmission = shape.emission;
                shapeEmission.enabled = true;
                ParticleSystem.EmissionModule trailEmission = trail.emission;
                trailEmission.enabled = true;
                player.Die();
            }
        }
    }

}