using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class EnemyAuthoring : MonoBehaviour {
    [SerializeField] private Vector2[] waypoints = new Vector2[3];
    [SerializeField] private int enemyHealth = 100;
    [SerializeField] private float enemySpeed = 10f;

    public class EnemyAuthoringBaker : Baker<EnemyAuthoring> {
        public override void Bake(EnemyAuthoring authoring) 
        {
            var ent = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent<EnemyTag>(ent);
            float2x3 temp = new float2x3();
            for (int i = 0; i < authoring.waypoints.Length-1; i++) {
                temp[i] = new float2(
                    authoring.waypoints[i].x,
                    authoring.waypoints[i].y); 
            }
            AddComponent(ent, new EnemyMovePoints {
                points = temp
            });
            AddComponent(ent, new EnemyHealth {
                Value = authoring.enemyHealth
            });
            AddComponent(ent, new EnemySpeed {
                Value = authoring.enemySpeed
            });
            AddComponent(ent, new EnemyEntity {
                Prefab = GetEntity(authoring, TransformUsageFlags.Dynamic)
            });
        }
    }
}