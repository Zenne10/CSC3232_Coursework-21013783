using UnityEngine;
using System.Collections;

public class NPCAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private GameObject projectilePrefab; 
    [SerializeField] private Transform firePoint; 
    [SerializeField] private float projectileSpeed = 10f; 
    [SerializeField] private float shootingInterval = 2f; 
    [SerializeField] private float projectileLifetime = 5f; 

    private NPCDetection npcDetection; 
    private bool canShoot = true; 

    private void Start()
    {
        npcDetection = GetComponent<NPCDetection>();
        if (npcDetection == null)
        {
            Debug.LogError("NPCDetection script not found on NPC!");
        }
    }

    private void Update()
    {
        if (npcDetection.IsPlayerDetected && canShoot)
        {
            StartCoroutine(ShootAtPlayer());
        }
    }

    private IEnumerator ShootAtPlayer()
    {
        canShoot = false;
        FireProjectile();
        yield return new WaitForSeconds(shootingInterval);

        canShoot = true;
    }

    private void FireProjectile()
    {
        if (projectilePrefab == null || firePoint == null)
        {
            Debug.LogError("Projectile Prefab or Fire Point not assigned!");
            return;
        }

      
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 direction = (npcDetection.Player.position - firePoint.position).normalized;
            rb.velocity = direction * projectileSpeed;
        }

        ProjectileBehavior projectileBehavior = projectile.AddComponent<ProjectileBehavior>();
        projectileBehavior.Setup(projectileLifetime);
    }

    private class ProjectileBehavior : MonoBehaviour
    {
        private float lifetime;

        public void Setup(float projectileLifetime)
        {
            lifetime = projectileLifetime;
            StartCoroutine(DestroyAfterLifetime());
        }

        private IEnumerator DestroyAfterLifetime()
        {
            yield return new WaitForSeconds(lifetime);
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("You died. Game over.");
                Time.timeScale = 0f; 
                Destroy(gameObject);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            Destroy(gameObject);
        }
    }
}
