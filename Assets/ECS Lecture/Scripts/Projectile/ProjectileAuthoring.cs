using ECS_Lecture.Scripts.Player;
using Unity.Entities;
using UnityEngine;

namespace ECS_Lecture.Scripts.Projectile {
    public class ProjectileAuthoring : MonoBehaviour
    {
        public float ProjectileSpeed;

        public class ProjectileAuthoringBaker : Baker<ProjectileAuthoring>
        {
            public override void Bake(ProjectileAuthoring authoring)
            { 
                Entity entity = GetEntity(authoring, TransformUsageFlags.Dynamic);
                AddComponent(entity, new ProjectileMoveSpeed {Value = authoring.ProjectileSpeed});
            }
        }
    }

}
