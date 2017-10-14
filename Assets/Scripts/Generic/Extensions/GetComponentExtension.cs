﻿using UnityEngine;

namespace DylanJay.Extensions
{
    public static class GetComponentExtension
    {
        public static T GetRequiredComponent<T>(this GameObject obj) where T : MonoBehaviour
        {
            T component = obj.GetComponent<T>();

            if (component == null)
            {
                Debug.LogError("Expected to find component of type "
                   + typeof(T) + " but found none", obj);
            }

            return component;
        }
    }
}
