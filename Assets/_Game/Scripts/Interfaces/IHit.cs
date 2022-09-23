using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHit<T> where T : MonoBehaviour
{
    void OnHit(T t);
}
