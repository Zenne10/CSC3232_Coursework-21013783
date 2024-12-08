using UnityEngine;

public class VectorFieldAura : MonoBehaviour
{
    public Vector3[,,] vectorField; 
    public int fieldSize = 10; 
    public float swirlStrength = 2f; 
    public float fieldFrequency = 0.1f; 
    public ParticleSystem particleSystem;

    void Start()
    {
        GenerateVectorField();
    }

    void Update()
    {
        ApplyVectorFieldToParticles();
    }

    void GenerateVectorField()
    {
        vectorField = new Vector3[fieldSize, fieldSize, fieldSize];

        for (int x = 0; x < fieldSize; x++)
        {
            for (int y = 0; y < fieldSize; y++)
            {
                for (int z = 0; z < fieldSize; z++)
                {
                    float angle = Mathf.PerlinNoise(x * fieldFrequency, z * fieldFrequency) * Mathf.PI * 2;
                    vectorField[x, y, z] = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * swirlStrength;
                }
            }
        }
    }

    void ApplyVectorFieldToParticles()
    {
        if (particleSystem == null) return;

        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[particleSystem.particleCount];
        int numParticles = particleSystem.GetParticles(particles);

        for (int i = 0; i < numParticles; i++)
        {
            Vector3 particlePos = particles[i].position;
            int x = Mathf.FloorToInt(particlePos.x + fieldSize / 2);
            int y = Mathf.FloorToInt(particlePos.y + fieldSize / 2);
            int z = Mathf.FloorToInt(particlePos.z + fieldSize / 2);

            if (x >= 0 && y >= 0 && z >= 0 && x < fieldSize && y < fieldSize && z < fieldSize)
            {
                particles[i].velocity += vectorField[x, y, z] * Time.deltaTime;
            }
        }

        particleSystem.SetParticles(particles, numParticles);
    }
}
