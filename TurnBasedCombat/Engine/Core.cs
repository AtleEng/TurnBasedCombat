using System.Collections.Generic;
using System.Numerics;
using Raylib_cs;
using CoreEngine;
using Engine;

namespace CoreEngine
{
    public static class Core
    {
        public static bool shouldClose;

        static public List<GameEntity> gameEntities = new();
        static public Dictionary<Type, GameSystem> systems = new();

        static public List<GameEntity> entitiesToAdd = new();
        static public List<GameEntity> entitiesToRemove = new();

        //deltaTime variabler
        static float oldTime = 0;
        static float newTime = 0;
        public static void Start()
        {
            EntityManager.SpawnEntity(new GameManager(), Vector2.Zero);

            AddSystem(new ScriptSystem());
            AddSystem(new SpriteSystem());

            // Innit all the systems in the right order
            foreach (var system in systems.Values)
            {
                system.Start();
            }
            while (shouldClose == false)
            {
                oldTime = newTime;
                newTime = (float)Raylib.GetTime();
                float deltaTime = newTime - oldTime;

                Update(deltaTime);
            }
        }
        static void Update(float delta)
        {
            // Uppdate all the systems in the right order
            foreach (var system in systems.Values)
            {
                system.Update(delta);
            }

            // Add and remove games entities
            foreach (var entity in entitiesToAdd)
            {
                gameEntities.Add(entity);
            }
            foreach (var entity in entitiesToRemove)
            {
                gameEntities.Remove(entity);
            }
            //clear the lists
            entitiesToAdd.Clear();
            entitiesToRemove.Clear();
        }

        public static void AddSystem<T>(T system) where T : GameSystem
        {
            systems.Add(typeof(T), system);
        }
        public static void RemoveSystem<T>() where T : GameSystem
        {
            systems.Remove(typeof(T));
        }
    }
}