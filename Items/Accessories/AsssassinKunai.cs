using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace JoostMod.Items.Accessories
{    
    public class AssassinKunai : ModItem
    {
        public static LocalizedText KunaiMaxText { get; private set; }
        public override void SetStaticDefaults()
        {
            KunaiMaxText = this.GetLocalization("KunaiMax");
        }
        public override void SetDefaults()
		{
			Item.width = 34;
			Item.height = 24;
            Item.value = 75000;
            Item.rare = ItemRarityID.Green;
			Item.accessory = true;
            Item.damage = 20;
            Item.knockBack = 7;
            Item.DamageType = DamageClass.Throwing;
        }
        public override void ModifyWeaponCrit(Player player, ref float crit)
        {
            crit = 100;
        }
        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            int a = 0;
            switch (player.meleeEnchant)
            {
                case 1: //Venom
                    a = 20;
                    break;
                case 2: //Cursed Flames
                    a = 15;
                    break;
                case 3: //Fire
                    a = 10;
                    break;
                case 4: //Gold
                    a = 10;
                    break;
                case 5: //Ichor
                    a = 10;
                    break;
                case 6: //Nanites
                    a = 20;
                    break;
                case 7: //Party
                    a = 5;
                    break;
                case 8: //Poison
                    a = 5;
                    break;
                default:
                    break;
            }
            damage.Base += a;
        }
        public override bool WeaponPrefix()
        {
            return false;
        }
        public override bool RangedPrefix()
        {
            return false;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
		{
            player.GetModPlayer<JoostPlayer>().assKunaiItem = Item;
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria")
                {
                    if (line2.Name == "ItemName")
                    {
                        line2.OverrideColor = new Color(230, 204, 128);
                    }
                    if (line2.Name == "Damage" || line2.Name == "CritChance" || line2.Name == "Knockback")
                    {
                        line2.OverrideColor = Color.DarkGray;
                    }
                }
            }

            list.Add(new TooltipLine(Mod, "MaxKunai", KunaiMaxText.Format(Main.LocalPlayer.GetModPlayer<JoostPlayer>().assKunaiMax)));

        }
    }
}