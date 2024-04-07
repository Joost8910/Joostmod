using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Utilities;
using Microsoft.Xna.Framework.Graphics;
using JoostMod.Items.Legendaries;
using JoostMod.Projectiles.Fishhooks;
using JoostMod.Projectiles.Ranged;
using Terraria.GameContent.Events;

namespace JoostMod.Items.Legendaries.Weps
{
    public class UncleCariusPole : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Uncle Carius's Fishing Pole");
            Tooltip.SetDefault("'This is the pole of ol' Uncle Carius\n" +
            "Deals more damage and throws more hooks as you kill bosses throughout the game\n" +
            "Fishing power is equivelent to damage\n" +
            "Right click to throw some fish, velocity increases with boss progression");
        }
        public override void SetDefaults()
        {
            Item.damage = 100;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 46;
            Item.height = 40;
            Item.useTime = 8;
            Item.useAnimation = 8;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.value = 300000;
            Item.rare = ItemRarityID.Cyan;
            Item.knockBack = 6;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = false;
            Item.shoot = ModContent.ProjectileType<UncleCariusHook>();
            Item.shootSpeed = 17f;
            Item.fishingPole = 100; 
            Item.GetGlobalItem<JoostGlobalItem>().glowmaskTex = (Texture2D)ModContent.Request<Texture2D>($"{Texture}_String");
        }
        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Item.GetGlobalItem<JoostGlobalItem>().glowmaskColor = new Color(90, 255, (int)(51 + Main.DiscoG * 0.75f));
        }
        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Item.GetGlobalItem<JoostGlobalItem>().glowmaskColor = new Color(90, 255, (int)(51 + Main.DiscoG * 0.75f));
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.FiberglassFishingPole)
            .AddIngredient<SeaStoneWest>()
            .AddIngredient<SeaStoneEast>()
            .AddIngredient<SeaStoneHigh>()
            .AddIngredient<SeaStoneDeep>()
            .AddTile<Tiles.ShrineOfLegends>()
            .Register();
        }
        public override void UpdateInventory(Player player)
        {
            player.GetModPlayer<JoostPlayer>().eastStone = true;
            player.GetModPlayer<JoostPlayer>().westStone = true;
            player.GetModPlayer<JoostPlayer>().highStone = true;
            player.GetModPlayer<JoostPlayer>().deepStone = true;
        }
        //Was this even being used? Commenting it out for now
        /*
        private int getDamage()
        {
            float dmg = Item.damage * JoostGlobalItem.LegendaryDamage() * 0.06f;

            if (JoostMod.instance.battleRodsLoaded)
            {
                dmg *= BattleRodsFishingDamage;
            }

            return (int)Math.Round(dmg);
        }
        */
        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            float mult = 1f;
            if (JoostMod.instance.battleRodsLoaded)//TODO: Test if the math checks out given player.GetDamage change
            {
                //mult *= JoostGlobalItem.LegendaryDamage() * 0.08f * BattleRodsFishingDamage / player.GetDamage(DamageClass.Ranged).Multiplicative;
            }
            else
            {
                mult *= JoostGlobalItem.LegendaryDamage() * 0.08f;
            }
            damage *= mult;
            Item.fishingPole = (int)damage.ApplyTo(100);
        }
        public override bool? PrefixChance(int pre, UnifiedRandom rand)
        {
            return false;
        }

        //TODO: Method deprecated, find new one to update fishing power
        /*
        public override void GetWeaponDamage(Player player, ref int damage)
        {
            Item.fishingPole = damage;
        }
        */
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(0, 255, (int)(51 + Main.DiscoG * 0.75f));
                }
            }
            if (JoostMod.instance.battleRodsLoaded)
            {
                Player player = Main.player[Main.myPlayer];
                int dmg = list.FindIndex(x => x.Name == "Damage");
                list.RemoveAt(dmg);
                list.Insert(dmg, new TooltipLine(Mod, "Damage", player.GetWeaponDamage(Item) + " Fishing damage"));
            }
        }
        //TODO: Figure out how to work UnuBattleRods compatability with current version
        /*
        public float BattleRodsFishingDamage
        {
            get { Player player = Main.player[Main.myPlayer]; return player.GetModPlayer<UnuBattleRods.FishPlayer>().bobberDamage; }
        }
        public int BattleRodsCrit
        {
            get { Player player = Main.player[Main.myPlayer]; return player.GetModPlayer<UnuBattleRods.FishPlayer>().bobberCrit; }
        }
        
        public override void ModifyWeaponCrit(Player player, ref float crit)
        {
            if (JoostMod.instance.battleRodsLoaded)
            {
                crit += BattleRodsCrit - player.GetCritChance(DamageClass.Ranged);
            }
        }
        */
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.reuseDelay = 40;
            }
            else
            {
                Item.reuseDelay = 0;
            }
            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            //TODO: Update this to include 1.4 bosses
            /*
            int num = 3 +                                   //3
                (NPC.downedSlimeKing ? 1 : 0) +             //4
                (NPC.downedBoss1 ? 1 : 0) +                 //5
                (NPC.downedBoss2 ? 1 : 0) +                 //6
                (NPC.downedQueenBee ? 1 : 0) +              //7
                (NPC.downedBoss3 ? 1 : 0) +                 //8
                (JoostWorld.downedCactusWorm ? 1 : 0) +     //9
                (Main.hardMode ? 1 : 0) +                   //10
                (NPC.downedMechBoss1 ? 1 : 0) +             //11
                (NPC.downedMechBoss2 ? 1 : 0) +             //12
                (NPC.downedMechBoss3 ? 1 : 0) +             //13
                (NPC.downedPlantBoss ? 1 : 0) +             //14
                (NPC.downedGolemBoss ? 1 : 0) +             //15
                (NPC.downedFishron ? 1 : 0) +               //16
                (NPC.downedAncientCultist ? 1 : 0) +        //17
                (NPC.downedTowerNebula ? 1 : 0) +           //18
                (NPC.downedTowerSolar ? 1 : 0) +            //19
                (NPC.downedTowerVortex ? 1 : 0) +           //20
                (NPC.downedTowerStardust ? 1 : 0) +         //21
                (NPC.downedMoonlord ? 4 : 0) +              //25
                (JoostWorld.downedJumboCactuar ? 5 : 0) +   //30
                (JoostWorld.downedSAX ? 5 : 0) +            //35
                (JoostWorld.downedGilgamesh ? 5 : 0);       //40
            */

            float huntValue = 0.2f;
            float eventValue = 0.5f;
            float fNum = 1 +                                            //1
            (JoostWorld.downedPinkzor ? huntValue : 0f) +               //1.2
            (JoostWorld.downedRogueTomato ? huntValue : 0f) +           //1.4
            (JoostWorld.downedWoodGuardian ? huntValue : 0f) +          //1.6
                    (NPC.downedSlimeKing ? 1 : 0) +                     //2.6
            (JoostWorld.downedFloweringCactoid ? huntValue : 0f) +      //2.8
                (NPC.downedGoblins ? eventValue : 0f) +                 //3.3
                    (NPC.downedBoss1 ? 1 : 0) +                         //4.3
            (JoostWorld.downedICU ? huntValue : 0f) +                   //4.5
                    (NPC.downedBoss2 ? 1 : 0) +                         //5.5
            (JoostWorld.downedSporeSpawn ? huntValue : 0f) +            //5.7
                (DD2Event.DownedInvasionT1 ? eventValue : 0f) +         //6.2
                    (NPC.downedQueenBee ? 1 : 0) +                      //7.2
            (JoostWorld.downedRoc ? huntValue : 0f) +                   //7.4
                    (NPC.downedBoss3 ? 1 : 0) +                         //8.4
            (JoostWorld.downedSkeletonDemoman ? huntValue : 0f) +       //7.6
                    (NPC.downedDeerclops ? 1 : 0f) +                    //8.6
                    (JoostWorld.downedCactusWorm ? 1 : 0) +             //9.6
            (JoostWorld.downedImpLord ? huntValue : 0f) +               //9.8
                    (Main.hardMode ? 1 : 0) +                           //10.8
                (NPC.downedPirates ? eventValue : 0f) +                 //11.3
            (JoostWorld.downedStormWyvern ? huntValue : 0f) +           //11.5
                    (NPC.downedQueenSlime ? 1 : 0f) +                   //12.5
                    (NPC.downedMechBoss1 ? 1 : 0) +                     //13.5
                    (NPC.downedMechBoss2 ? 1 : 0) +                     //14.5
                    (NPC.downedMechBoss3 ? 1 : 0) +                     //15.5                                         
                (DD2Event.DownedInvasionT2 ? eventValue : 0f) +         //16
                    (NPC.downedPlantBoss ? 1 : 0) +                     //17
                    (NPC.downedGolemBoss ? 1 : 0) +                     //18
                    (NPC.downedFishron ? 1 : 0) +                       //19
                (NPC.downedMartians ? eventValue : 0f) +                //19.5
            (NPC.downedHalloweenTree ? 0.2f : 0f) +                     //19.7
            (NPC.downedHalloweenKing ? 0.2f : 0f) +                     //19.9
            (NPC.downedChristmasTree ? 0.2f : 0f) +                     //21.1
            (NPC.downedChristmasSantank ? 0.2f : 0f) +                  //21.3
            (NPC.downedChristmasIceQueen ? 0.2f : 0f) +                 //21.5
                    (NPC.downedEmpressOfLight ? 1 : 0f) +               //22.5
                (DD2Event.DownedInvasionT3 ? 1 : 0f) +                  //23
                    (NPC.downedAncientCultist ? 1 : 0) +                //24
            (NPC.downedTowerNebula ? 0.25f : 0) +                       //24.25
            (NPC.downedTowerSolar ? 0.25f : 0) +                        //24.5
            (NPC.downedTowerVortex ? 0.25f : 0) +                       //24.75
            (NPC.downedTowerStardust ? 0.25f : 0) +                     //25
                    (NPC.downedMoonlord ? 3 : 0) +                      //28
                    (JoostWorld.downedJumboCactuar ? 3 : 0) +           //31
                    (JoostWorld.downedSAX ? 4 : 0) +                    //35
                    (JoostWorld.downedGilgamesh ? 5 : 0);               //40
            int num = (int)fNum;
            Main.NewText(fNum, Color.Violet);
            Main.NewText(num, Color.LightSeaGreen);
            if (player.altFunctionUse == 2)
            {
                int numberProjectiles = 4 + Main.rand.Next(3);
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(12));
                    float scale = 1f - Main.rand.NextFloat() * .3f;
                    perturbedSpeed = perturbedSpeed * scale * 0.04f * num;
                    Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<UncleCariusFish>(), (int)(damage * 1.5f), knockback, player.whoAmI);
                }
            }
            else
            {
                float spread = (float)num * 2 * 0.0174f;
                float baseSpeed = (float)Math.Sqrt(velocity.X * velocity.X + velocity.Y * velocity.Y);
                double startAngle = Math.Atan2(velocity.X, velocity.Y) - spread / 2;
                double deltaAngle = spread / num;
                double offsetAngle;
                int i;
                for (i = 0; i < num; i++)
                {
                    offsetAngle = startAngle + deltaAngle * i;
                    Projectile.NewProjectile(source, position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), type, damage, 0, player.whoAmI);
                }
            }
            return false;
        }
    }
}

