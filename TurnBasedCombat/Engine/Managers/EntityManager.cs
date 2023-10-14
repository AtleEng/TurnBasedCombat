using System.Collections.Generic;
using System.Numerics;
using CoreEngine;
using Engine;

namespace Engine
{
    //handles all entities for the components
    public static class EntityManager
    {
        public static void SpawnEntity(GameEntity entity, Vector2 position)
        {
            entity.transform.position = position;
            entity.OnInnit();
            foreach (Component component in entity.components.Values)
            {
                component.Start();
            }

            Core.entitiesToAdd.Add(entity);
        }

        public static void DestroyEntity(GameEntity gameEntity)
        {
            foreach (Component component in gameEntity.components.Values)
            {
                component.OnDestroy();
            }
            Core.entitiesToRemove.Add(gameEntity);
        }

    }
}