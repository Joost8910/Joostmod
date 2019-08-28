using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Dusts
{
	public class LunarRing : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.noGravity = true;
            dust.frame = new Rectangle(0, 0, 10, 10);
        }
        public override bool MidUpdate(Dust dust)
        {
            dust.scale -= 0.01f;
            dust.rotation = dust.velocity.ToRotation() + 1.57f;
            dust.frame.Y = (dust.frame.Y >= 30 ? 0 : dust.frame.Y + 10);
            if (!dust.noLight)
			{
				float strength = dust.scale;
				if (strength > 1f)
				{
					strength = 1f;
				}
				Lighting.AddLight(dust.position, 0.36f * strength, 0.9f * strength, 0.64f * strength);
			}
			return false;
		}
		public override Color? GetAlpha(Dust dust, Color lightColor)
		{
			return new Color(lightColor.R, lightColor.G, lightColor.B, 25);
		}
	}
}