
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 4.5f;
    public int health = 100;

    private CharacterController controller;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(x, 0f, z);
        movement = Vector3.ClampMagnitude(movement, 1f);

        if (movement.sqrMagnitude > 0.01f)
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(movement),
                10f * Time.deltaTime
            );
        }

        controller.SimpleMove(movement * moveSpeed);
    }

    public void TakeDamage(int amount)
    {
        health = Mathf.Max(0, health - amount);

        if (health == 0)
        {
            Debug.Log("Player defeated!");
        }
    }
}
