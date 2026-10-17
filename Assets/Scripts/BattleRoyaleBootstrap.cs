
using UnityEngine;

public class BattleRoyaleBootstrap : MonoBehaviour
{
    void Start()
    {
        CreateGround();
        CreatePlayer();
        CreateBots(5);
        CreateLighting();
    }

    void CreateGround()
    {
        GameObject ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
        ground.name = "BattleArena";
        ground.transform.localScale = new Vector3(5f, 1f, 5f);
        ground.GetComponent<Renderer>().material.color =
            new Color(0.25f, 0.45f, 0.25f);
    }

    void CreatePlayer()
    {
        GameObject player = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        player.name = "Player";
        player.tag = "Player";
        player.transform.position = new Vector3(0f, 1f, 0f);

        Destroy(player.GetComponent<Collider>());
        player.AddComponent<CharacterController>();
        player.AddComponent<PlayerController>();

        GameObject cam = new GameObject("Main Camera");
        cam.tag = "MainCamera";
        cam.transform.position =
            player.transform.position + new Vector3(0f, 5f, -7f);

        Camera camera = cam.AddComponent<Camera>();
        camera.fieldOfView = 65f;
        cam.AddComponent<AudioListener>();

        FollowCamera follow = cam.AddComponent<FollowCamera>();
        follow.target = player.transform;
    }

    void CreateBots(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject bot = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            bot.name = "Bot_" + (i + 1);
            bot.transform.position = new Vector3(
                Random.Range(-15f, 15f), 1f, Random.Range(5f, 20f));

            bot.GetComponent<Renderer>().material.color = Color.red;
            Destroy(bot.GetComponent<Collider>());
            bot.AddComponent<CharacterController>();
            bot.AddComponent<BotAI>();
        }
    }

    void CreateLighting()
    {
        GameObject lightObject = new GameObject("Arena Light");
        Light light = lightObject.AddComponent<Light>();
        light.type = LightType.Directional;
        light.intensity = 1.2f;
        lightObject.transform.rotation =
            Quaternion.Euler(50f, -30f, 0f);
    }
}
