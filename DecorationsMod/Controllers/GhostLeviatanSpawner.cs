using System.Collections.Generic;
using UnityEngine;

namespace DecorationsMod.Controllers
{
    public class GhostLeviatanSpawner : MonoBehaviour
    {
        public static GameObject ghostLeviathanPrefab = null;

        public bool hasEggs = false;
        
        public float spawnInterval = 20f;
        public int maxSpawns = 2;
        public float spawnDistance = 10f;
        public float minDepthForSpawn = -100f;
        
        private readonly HashSet<GameObject> spawnedCreatures = new HashSet<GameObject>();

        private float timeNextSpawn = -1f;
        
        private void Start()
        {
            // ClassID: 5ea36b37-300f-4f01-96fa-003ae47c61e5
            // Path: WorldEntities/Creatures/GhostLeviathanJuvenile
            if (GhostLeviatanSpawner.ghostLeviathanPrefab == null)
                GhostLeviatanSpawner.ghostLeviathanPrefab = Resources.Load<GameObject>("WorldEntities/Creatures/GhostLeviathanJuvenile");
            
            System.Random r = new System.Random();
            // Set spawn time ratio to 0.001f minimum, or value from Config.txt file
            float spawnTimeRatio = ((ConfigSwitcher.GhostLeviatan_spawnTimeRatio <= 0.000f) ? 0.001f : ConfigSwitcher.GhostLeviatan_spawnTimeRatio);
            // Set random spawn interval between 400 and 1800, multiplied by spawn time ratio
            this.spawnInterval = r.Next(400, 1800) * spawnTimeRatio;
            // Get max spawns value from Config.txt file
            this.maxSpawns = ConfigSwitcher.GhostLeviatan_maxSpawns;

            base.InvokeRepeating("UpdateSpawn", 0f, 2f);
        }
        
        private void UpdateSpawn()
        {
            // If eggs model changed, refresh eggs status and next spawn time
            bool flag = HasEggs();
            if (this.hasEggs != flag)
            {
                this.hasEggs = flag;
                this.timeNextSpawn = this.CalculateTimeNextSpawn();
            }

            if (this.hasEggs && this.timeNextSpawn > 0f && Time.time > this.timeNextSpawn && this.TryGetSpawnPosition(this.gameObject.transform.position, out Vector3 position))
            {
                GameObject item = UnityEngine.Object.Instantiate<GameObject>(GhostLeviatanSpawner.ghostLeviathanPrefab, position, Quaternion.identity);
                LiveMixin liveMixin = item.GetComponent<LiveMixin>();
                liveMixin.data.maxHealth = ConfigSwitcher.GhostLeviatan_health;
                liveMixin.health = ConfigSwitcher.GhostLeviatan_health;
                this.spawnedCreatures.Add(item);
                this.timeNextSpawn = this.CalculateTimeNextSpawn();
            }
        }

        private bool HasEggs()
        {
            // Get model
            GameObject model = this.gameObject.FindChild("lost_river_cove_tree_01");
            // Are eggs active
            return (model.FindChild("lost_river_cove_tree_01_eggs").activeSelf);
        }
        
        private bool TryGetSpawnPosition(Vector3 covetreePosition, out Vector3 spawnPosition)
        {
            spawnPosition = Vector3.zero;
            for (int i = 0; i < 10; i++)
            {
                spawnPosition = covetreePosition + UnityEngine.Random.onUnitSphere * this.spawnDistance;
                if (spawnPosition.y < this.minDepthForSpawn && this.HasEggs())
                    return true;
            }
            return false;
        }
        
        private float CalculateTimeNextSpawn()
        {
            if (!hasEggs)
                return -1f;
            if (this.spawnedCreatures.Count >= this.maxSpawns)
            {
                // Get model
                GameObject model = this.gameObject.FindChild("lost_river_cove_tree_01");
                // Hide eggs
                model.FindChild("lost_river_cove_tree_01_eggs").SetActive(false);
                model.FindChild("lost_river_cove_tree_01_eggs_shells").SetActive(false);
                // Clear spawned creatures
                this.spawnedCreatures.Clear();
                // Stop timer
                return -1f;
            }
            return ((this.spawnedCreatures.Count == 0) ? Time.time + this.spawnInterval + ConfigSwitcher.GhostLeviatan_timeBeforeFirstSpawn : Time.time + this.spawnInterval);
        }
        
        public void OnGhostLeviathanDestroyed(GameObject ghostLeviathanGO)
        {
            if (this.spawnedCreatures.Remove(ghostLeviathanGO) && this.hasEggs)
            {
                float num = this.CalculateTimeNextSpawn();
                this.timeNextSpawn = ((this.timeNextSpawn <= 0f) ? num : Mathf.Min(this.timeNextSpawn, num));
            }
        }
    }
}
