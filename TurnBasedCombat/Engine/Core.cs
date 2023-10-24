using System.Collections.Generic;
using System.Numerics;
using System.IO;
using Raylib_cs;
using CoreEngine;
using Engine;
using CoreAnimation;


namespace CoreEngine
{
    public static class Core
    {
        public static GameEntity currentScene = new Scene();
        public static bool shouldClose;

        static public List<GameEntity> gameEntities = new();
        static public List<GameEntity> activeGameEntities = new();

        static public Dictionary<Type, GameSystem> systems = new();

        static public List<GameEntity> entitiesToAdd = new();
        static public List<GameEntity> entitiesToRemove = new();

        //deltaTime variabler
        static float oldTime = 0;
        static float newTime = 0;
        public static void Start()
        {
            AddSystem(new ScriptSystem());
            AddSystem(new AnimationSystem());
            AddSystem(new SpriteSystem());

            // Innit all the systems in the right order
            foreach (var system in systems.Values)
            {
                system.Start();
            }
            //Console.Clear();
            currentScene.OnInnit();

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
            activeGameEntities.Clear();
            foreach (GameEntity gameEntity in gameEntities)
            {
                if (gameEntity.isActive == true)
                {
                    activeGameEntities.Add(gameEntity);
                }
            }
            // Uppdate all the systems in the right order
            foreach (var system in systems.Values)
            {
                system.Update(delta);
            }

            UppdateChildren(currentScene);

            // Add and remove games entities
            foreach (var entity in entitiesToAdd)
            {
                gameEntities.Add(entity);
            }
            foreach (var entity in entitiesToRemove)
            {
                if (entity.parent != null)
                {
                    entity.parent.children.Remove(entity);
                }
                gameEntities.Remove(entity);
            }
            //clear the lists
            entitiesToAdd.Clear();
            entitiesToRemove.Clear();
            if (Raylib.IsKeyDown(KeyboardKey.KEY_F3))
            {
                Console.Clear();
                PrintEntityTree(currentScene, "", "");
            }
        }

        public static void UppdateChildren(GameEntity parent)
        {
            foreach (var child in parent.children)
            {
                child.worldTransform.position = child.localTransform.position + parent.worldTransform.position;
                child.worldTransform.size = child.localTransform.size * parent.worldTransform.size;

                UppdateChildren(child);
            }
        }

        public static void AddSystem<T>(T system) where T : GameSystem
        {
            systems.Add(typeof(T), system);
        }
        public static void RemoveSystem<T>() where T : GameSystem
        {
            systems.Remove(typeof(T));
        }

        public static void PrintEntityTree(GameEntity entity, string layer = "", string space = "")
        {
            Console.WriteLine($"{space}{layer}{entity.name} [{entity.PrintStats()}]");

            // Components
            if (entity.components.Count > 0)
            {
                foreach (Component component in entity.components.Values)
                {
                    Console.WriteLine($"   {space}{component.GetType().Name} [{component.PrintStats()}]");
                }
            }
            // Entities
            if (entity.children.Count > 0)
            {
                foreach (var child in entity.children)
                {
                    PrintEntityTree(child, $"{layer}>", $"{space} ");
                }

            }
        }
    }
}

namespace Engine
{
    public class Scene : GameEntity
    {
        public override void OnInnit()
        {
            name = "Scene";

            EntityManager.SpawnEntity(new GameManager(), Vector2.Zero);
        }
    }
}