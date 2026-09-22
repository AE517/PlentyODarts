using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace PlentyODarts.Utils
{
	public static class DartUtils
	{
		public static int SpawnModusProjectile(
			Player player,
			Vector2 position,
			Vector2 velocity,
			int type,
			int damage,
			float knockback
		)
		{
			return Projectile.NewProjectile(
				player.GetSource_FromThis(),
				position,
				velocity,
				type,
				damage,
				knockback,
				player.whoAmI
			);
		}

		public static Vector2 Bounce(Vector2 velocity, Vector2 oldVelocity)
		{
			velocity.X =
				Math.Abs(velocity.X - oldVelocity.X) > float.Epsilon
					? -oldVelocity.X
					: velocity.X;

			velocity.Y =
				Math.Abs(velocity.Y - oldVelocity.Y) > float.Epsilon
					? -oldVelocity.Y
					: velocity.Y;

			return velocity;
		}

		public static void SoundFxVolume(SoundStyle sound, Vector2 position, float pitchOffset = 1.0f, float volumeScale = .2f)
		{
			SoundEngine.PlaySound(sound.WithPitchOffset(pitchOffset).WithVolumeScale(volumeScale), position);
		}
	}
}
