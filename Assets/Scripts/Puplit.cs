using UnityEngine;

public class Pulpit : MonoBehaviour
{
    float destroyTime;

    public void Initialize()
    {
        var data = GameConfigLoader.Config.pulpit_data;
        destroyTime = Random.Range(
            data.min_pulpit_destroy_time,
            data.max_pulpit_destroy_time
        );

        Invoke(nameof(DestroySelf), destroyTime);
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
