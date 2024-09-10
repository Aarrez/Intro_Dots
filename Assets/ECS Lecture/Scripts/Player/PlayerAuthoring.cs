using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace ECS_Lecture.Scripts.Player {
    public class PlayerAuthoring : MonoBehaviour {
        [SerializeField] private float moveSpeed;
        [SerializeField] GameObject projectilePrefab;

        class PlayerAuthoringBaker : Baker<PlayerAuthoring> {
            public override void Bake(PlayerAuthoring authoring) {
                Entity playerEntity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent<PlayerTag>(playerEntity);
                AddComponent<PlayerMoveInput>(playerEntity);

                AddComponent(playerEntity, new PlayerMoveSpeed {
                    Value = authoring.moveSpeed
                });

                AddComponent<FireProjectileTag>(playerEntity);
                SetComponentEnabled<FireProjectileTag>(playerEntity, false);

                AddComponent(playerEntity, new ProjectilePrefab {
                    Value = GetEntity(authoring.projectilePrefab,
                        TransformUsageFlags.Dynamic)
                });
            }
        }
    }

    public struct PlayerMoveInput : IComponentData {
        public float2 Value;
    }

    public struct PlayerMoveSpeed :
        IComponentData {
        public float Value;
    }

    public struct PlayerTag :
        IComponentData { }

    public struct ProjectilePrefab :
        IComponentData {
        public Entity Value;
    }

    public struct ProjectileMoveSpeed :
        IComponentData {
        public float Value;
    }

    public struct FireProjectileTag :
        IComponentData,
        IEnableableComponent { }
}