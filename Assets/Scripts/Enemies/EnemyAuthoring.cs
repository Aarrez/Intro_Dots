using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
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
            AddComponent<EnemyCurrentPoint>(ent);
            FixedList128Bytes<float3> temp = new FixedList128Bytes<float3>();
            for (int i = 0; i < authoring.waypoints.Length; i++) {
                temp.Add(new float3(authoring.waypoints[i].x, 
                    authoring.waypoints[i].y, 0f));
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