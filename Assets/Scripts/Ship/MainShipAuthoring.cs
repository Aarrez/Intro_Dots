﻿using Unity.Entities;
using UnityEngine;

public class MainShipAuthoring : MonoBehaviour {
    
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float MoveSpeed = 5f;
    [SerializeField] private float RotateSpeed = 90f;

    public class MainShipBaker : Baker<MainShipAuthoring> {
        public override void Bake(MainShipAuthoring authoring) {

            Entity ent = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent<MainShipTag>(ent);
            AddComponent(ent, new MainShipRotationSpeed {
                Value = authoring.RotateSpeed
            });
            AddComponent(ent, new MainShipMoveSpeed {
                Value = authoring.MoveSpeed
            });

            AddComponent<FireBulletTag>(ent);
            SetComponentEnabled<FireBulletTag>(ent , false);
            
            AddComponent(ent , new BulletPrefab {
                Value = GetEntity(authoring.bulletPrefab, TransformUsageFlags.Dynamic)
            });


        }
    }
}