using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolableObject 
{
    public void OnTakeFromPool();
    public void OnReturnedToPool();
}
