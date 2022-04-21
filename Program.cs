using System;
using System.Collections;
using System.Collections.Generic;
using GameLibrary;
using GameLibrary.Interfaces;
using GameLibrary.Factories;
using GameLibrary.ConfigExtensions;

namespace GameThoery
{
    public class Program
    {
        static void Main(string[] args)
        {
            //SceneWorld sceneWorld = new SceneWorld(new GameConfigurations());
            //sceneWorld.Play();

            GameConfigurations gameConfig = ConfigReader.ReadConfig("GameConfig");
            SceneWorld sceneWorld = new SceneWorld(gameConfig);

            IWeaponFactory weaponFactory = new WeaponFactory();

            for (int i = 0; i < 20; i++)
            {
                var q = weaponFactory.Create(WeaponType.Melee);
                Console.WriteLine(q);
            }

            //StandardPlayerChar player = new StandardPlayerChar(30, new Vector2(5, 5), "Player");
            //player.LootAndEquipItem(new Shield("Great shield", 2));

            //sceneWorld.AddGameObject(player);
            //Console.WriteLine(sceneWorld.FindObjectByName("Player"));

            //Orc Centaur = new Orc(10, new Vector2(0, 0), "Centaur");
            //Centaur.LootAndEquipItem(new Weapon("Giant axe", 4));

            //Centaur.LootItem(player);

            //player.MoveUpToTarget(Centaur.position);

            //while (!Centaur.IsDead && !player.IsDead)
            //{
            //    if (!player.InRange(Centaur.position)) break;
            //    player.DoDamage(Centaur);
            //    Centaur.DoDamage(player);
            //}

            //Weapon weapon = (Weapon)Centaur.LootItem(player); // we loot to player and save the ref
            //player.EquipItem(weapon); // then we equip it

            StandardPlayerChar player = new StandardPlayerChar(30, new Vector2(0, 0), "Pogchamp");
            sceneWorld.AddGameObject(player);

            Orc orcThief = new Orc(10, new Vector2(0, 0), "Orc thief");

            while (!player.InRange(orcThief.position))
            {
                player.MoveUpToTarget(orcThief.position);
            }

            while (!player.IsDead && !orcThief.IsDead)
            {
                player.DoDamage(orcThief);
                orcThief.DoDamage(player);
            }

            //orcThief.Revive();

            player.GetHealth();

            Potion healingPotion = new HealingPotion(14);
            healingPotion.Consume(player);

            player.GetHealth();

            Console.WriteLine(player.position);

            Potion TPPotion = new TeleportPotion();
            TPPotion.Consume(player);


            var item = orcThief.LootRandomGeneratedItem(player);
            Console.WriteLine(item);

            player.EquipItem(item);

            Console.WriteLine("Hello comrade");

        } // think of composite or strategy pattern, strategy pattern could add some sorting method
        // also need the config file, don't really now what it could contain
    }
}
