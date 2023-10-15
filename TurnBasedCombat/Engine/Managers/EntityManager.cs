using System.Collections.Generic;
using System.Numerics;
using CoreEngine;
using Engine;

namespace Engine
{
    //handles all entities for the components
    public static class EntityManager
    {
        public static void SpawnEntity(GameEntity entity) { SpawnEntity(entity, Vector2.Zero); }
        public static void SpawnEntity(GameEntity entity, Vector2 position) { SpawnEntity(entity, position, Vector2.One); }
        public static void SpawnEntity(GameEntity entity, Vector2 position, Vector2 size)
        {
            entity.transform.position = position;
            entity.transform.size = size;
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