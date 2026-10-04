using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;
using System;
using Terraria.GameContent.Drawing;

namespace JoostMod.Projectiles.Melee
{
    public class NightEcho : HammerEcho
    {
        public override string Texture => "JoostMod/Projectiles/Melee/HammerEcho";

        public override void SetDefaults()
        {
            Projectile.width = 284;
            Projectile.height = 284;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
            Projectile.timeLeft = maxTime;
            Projectile.tileCollide = false;
            dustId = DustID.Shadowflame;
            echoColor = new Color(65, 34, 127);
        }

    }
}

