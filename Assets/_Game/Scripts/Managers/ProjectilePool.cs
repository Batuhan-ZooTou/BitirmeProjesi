using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using _Game.Scripts.Managers;
using _Game.Scripts.Extensions;
using System;
using _Game.Scripts.Objects;

[Serializable]
public class ProjectilePoolObject
{
    public string id;
    public IObjectPool<Projectile> projectilePool;
    public Projectile projectilePrefab;
    public Transform projectileHolder;
    [HideInInspector]public int index;
}
public class ProjectilePool : Singleton<ProjectilePool>
{
    
}
