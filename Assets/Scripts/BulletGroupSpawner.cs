using System.Collections.Generic;
using UnityEngine;

public class BulletGroupSpawner : MonoBehaviour
{
    [SerializeField] private List<StormBulletGroup> stormBulletGroups;

    public List<StormBulletGroup> BulletGroups { get => stormBulletGroups; set => stormBulletGroups = value; }

    public void SpawnBulletGroup(int bulletGroupIndex)
    {
        if (bulletGroupIndex < stormBulletGroups.Count && bulletGroupIndex >= 0)
        {
            if (stormBulletGroups[bulletGroupIndex].CreatePosition != null)
            {
                Instantiate(stormBulletGroups[bulletGroupIndex].BulletGroup, stormBulletGroups[bulletGroupIndex].CreatePosition.position, new Quaternion(0f, 0f, 0f, 0f));
            }
        }
    }
}
