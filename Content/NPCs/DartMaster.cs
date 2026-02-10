using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlentyODarts.Content.Projectiles;
using PlentyODarts.World;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.Personalities;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace PlentyODarts.Content.NPCs
{
    [AutoloadHead]
    public class DartMaster : ModNPC
    {
        public const string ShopName = "Dart Emporium";
        public int InteractionCounter = 0;

        private static readonly Profiles.StackedNPCProfile NPCProfile;

        public static LocalizedText UpgradedText { get; private set; }

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 25;

            NPCID.Sets.ExtraFramesCount[Type] = 9;
            NPCID.Sets.AttackFrameCount[Type] = 4;
            NPCID.Sets.DangerDetectRange[Type] = 500;
            NPCID.Sets.AttackType[Type] = 0;
            NPCID.Sets.AttackTime[Type] = 30;
            NPCID.Sets.AttackAverageChance[Type] = 20;
            NPCID.Sets.HasNoPartyText[Type] = true; // WIP
            // NPCID.Sets.HatOffsetY[Type] = ; // WIP
            NPCID.Sets.ShimmerTownTransform[Type] = false;

            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers =
                new()
                {
                    Velocity = 1f,
                    Direction = 1,
                };

            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);

            NPC.Happiness.SetBiomeAffection<ForestBiome>(AffectionLevel.Love)
                .SetBiomeAffection<DesertBiome>(AffectionLevel.Hate)
                .SetNPCAffection(NPCID.Nurse, AffectionLevel.Like)
                .SetNPCAffection(NPCID.Guide, AffectionLevel.Love)
                .SetNPCAffection(NPCID.ArmsDealer, AffectionLevel.Dislike);
        }

        public override void SetDefaults()
        {
            NPC.townNPC = true;
            NPC.friendly = true;
            NPC.width = 28;
            NPC.height = 46;
            NPC.aiStyle = 7;
            NPC.damage = 25;
            NPC.defense = 20;
            NPC.lifeMax = 1000;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath39;
            NPC.knockBackResist = 0.3f;

            AnimationType = NPCID.Golfer;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(
                [BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface]
            );
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (
                NPCID.Sets.NPCBestiaryDrawOffset.TryGetValue(
                    Type,
                    out NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers
                )
            )
            {
                drawModifiers.Rotation += 0.001f;

                NPCID.Sets.NPCBestiaryDrawOffset.Remove(Type);
                NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
            }

            return true;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            int num = NPC.life > 0 ? 1 : 5;

            for (int i = 0; i < num; i++)
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.PurpleCrystalShard);

            if (Main.netMode != NetmodeID.Server && NPC.life <= 0)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity / .7f, 99);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * .7f, 99);
            }
        }

        public override void AI() =>
            PoDWorld.arrivedDartMaster = !PoDWorld.arrivedDartMaster || PoDWorld.arrivedDartMaster;

        public override bool CanTownNPCSpawn(int numTownNPCs) =>
            PoDWorld.arrivedDartMaster;

        public override List<String> SetNPCNameList() =>
            [
                "Luke H.",
                "Luke L.",
                "Michael",
                "Peter",
                "Gerwyn",
                "Rob",
                "Gary",
                "Phil",
                "Adrian",
                "John",
                "Raymond",
            ];

        public override string GetChat()
        {
            WeightedRandom<string> dialogue = new();
            dialogue.Add("Sup");

            return dialogue;
        }

        public override bool CanGoToStatue(bool toKingStatue) => true;

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            if (!Main.hardMode)
            {
                damage = !Main.dayTime || Main.bloodMoon || Main.invasionType != 0 ? 70 : 50;

                knockback = 1f;
            }
            else
            {
                damage =
                    !Main.dayTime || Main.bloodMoon || Main.eclipse || Main.invasionType != 0
                        ? 100
                        : 70;

                knockback = 2f;
            }
        }

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            projType = ModContent.ProjectileType<WiseGoldDartProj>();
            attackDelay = 1;
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 60;
            randExtraCooldown = 30;
        }

        public override void TownNPCAttackProjSpeed(
            ref float multiplier,
            ref float gravityCorrection,
            ref float randomOffset
        )
        {
            multiplier = Main.hardMode ? 20f : 15f;
        }
    }
}
