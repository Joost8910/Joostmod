using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace JoostMod.Items.Weapons.Melee
{
    public class Deathbringer : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Deathbringer");
            Tooltip.SetDefault("Critical hits have a chance to instantly kill enemies\n" + "Enemies with over 5000 life are immune to this effect");
        }
        public override void SetDefaults()
        {
            Item.damage = 86;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 44;
            Item.height = 52;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 4.5f;
            Item.value = 500000;
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.Next(6) == 0)
            {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 109);
            }
        }
        public override void ModifyHitNPC(Player player, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            if (target.life <= 5000 && crit && Main.rand.Next(5) <= 1)
            {
                damage += target.life + target.defense;
                knockback = 55;
                int shootNum = 24;
                float spread = 360 * 0.0174f;
                float baseSpeed = 5;
                double startAngle = Math.Atan2(3f, 4f) - spread / shootNum;
                double deltaAngle = spread / shootNum;
                double offsetAngle;
                for (int i = 0; i < shootNum; i++)
                {
                    offsetAngle = startAngle + deltaAngle * i;
                    Dust.NewDust(target.Center, 8, 8, DustID.Asphalt, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), 0, default, 1f);
                }
                SoundEngine.PlaySound(SoundID.Item100, player.position);
            }
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            if (knockback == 55 && crit && damage > target.life)
            {
                bool flag = false;
                for (int i = 0; i < 100; i++)
                {
                    if (Main.combatText[i].active && Main.combatText[i].text == damage.ToString() && player.Distance(Main.combatText[i].position) < 250)
                    {
                        Main.combatText[i].active = false;
                        flag = true;
                    }
                }
                if (flag)
                {
                    string text = "KO";
                    switch (Main.rand.Next(16))
                    {
                        case 1:
                            text = "DESTROYED";
                            break;
                        case 2:
                            text = "DEMOLISHED";
                            break;
                        case 3:
                            text = "VAPORIZED";
                            break;
                        case 4:
                            text = "PULVERIZED";
                            break;
                        case 5:
                            text = "POUNDED";
                            break;
                        case 6:
                            text = "CLAPPED";
                            break;
                        case 7:
                            text = "CRUSHED";
                            break;
                        case 8:
                            text = "CLEAVED";
                            break;
                        case 9:
                            text = "SHATTERED";
                            break;
                        case 10:
                            text = "SMAAAASH!!";
                            break;
                        case 11:
                            text = "SLAMMED";
                            break;
                        case 12:
                            text = "JAMMED";
                            break;
                        case 13:
                            text = "DUNKED";
                            break;
                        case 14:
                            text = "SPLATTERED";
                            break;
                        case 15:
                            text = "KILLTACULAR";
                            break;
                        default:
                            text = "KO";
                            break;
                    }
                    CombatText.NewText(new Rectangle((int)target.position.X, (int)target.position.Y, target.width, target.height), Color.Black, text, true, false);
                }
            }
        }
    }
}

