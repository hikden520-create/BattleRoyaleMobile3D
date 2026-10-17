
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class BotAI : MonoBehaviour
{
    public float moveSpeed = 2.5f;
    public float detectionRange = 15f;
    public float attackRange = 2f;
    public int damage = 10;
    public float attackCooldown = 1.5f;

    private Transform target;
    private CharacterController controller;
    private float nextAttackTime;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
            target = player.transform;
    }

    void Update()
    {
        if (target == null) return;

        float distance = Vector3.Distance(
            transform.position, target.position
        );

        if (distance > detectionRange) return;

        Vector3 direction = target.position - transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude > 0.01f)
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(direction),
                5f * Time.deltaTime
            );
        }

        if (distance > attackRange)
        {
            controller.SimpleMove(
                direction.normalized * moveSpeed
            );
        }
        else if (Time.time >= nextAttackTime)
        {
            PlayerController player =
                target.GetComponent<PlayerController>();

            if (player != null)
                player.TakeDamage(damage);

            nextAttackTime = Time.time + attackCooldown;
        }
    }
}
