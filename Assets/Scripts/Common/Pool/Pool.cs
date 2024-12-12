using System.Collections.Generic;
using UnityEngine;

namespace Common.Pool
{
    public class Pool <T> where T : IPoolable
    {
        private LinkedList<T> usedObjects;
        private Stack<T> unusedObjects;

        public LinkedList<T> UsedObjects => usedObjects;
        
        public Pool(IEnumerable<T> objects)
        {
            usedObjects = new LinkedList<T>();
            unusedObjects = new Stack<T>();
            
            foreach (var obj in objects)
            {
                FreeObject(obj);
            }
        }

        public T GetObject()
        {
            if (!unusedObjects.TryPop(out var result))
            {
                if (TryGetUsedObject(out result))
                {
                    FreeObject(result);
                }
                else
                {
                    Debug.LogError("Empty pool");
                    return default;
                }
            }
            
            result.OnTakenFromPool();
            usedObjects.AddFirst(result);
            
            return result;
        }

        private bool TryGetUsedObject(out T obj)
        {
            if (usedObjects.Count > 0)
            {
                obj = usedObjects.Last.Value;
                return true;
            }

            obj = default;
            return false;
        }

        public void FreeObject(T poolable)
        {
            poolable.OnTakenBackToPool();
            unusedObjects.Push(poolable);

            if (usedObjects.Contains(poolable))
            {
                usedObjects.Remove(poolable);
            }
        }
    }
}