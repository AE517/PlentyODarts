using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace PlentyODarts.Content.Tiles.Furniture
{
    public class DartStationBasic : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileTable[Type] = true;
            Main.tileSolid[Type] = false;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            Main.tileFrameImportant[Type] = true;
            TileID.Sets.DisableSmartCursor[Type] = true;
            TileID.Sets.IgnoredByNpcStepUp[Type] = true;

            TileObjectData.newTile.UsesCustomCanPlace = true;
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.Width = 3;
            TileObjectData.newTile.Height = 3;
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinateHeights = new[] { 16, 16, 16 };
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.AnchorBottom = new AnchorData(
                Terraria.Enums.AnchorType.SolidTile
                    | Terraria.Enums.AnchorType.SolidWithTop
                    | Terraria.Enums.AnchorType.Table
                    | Terraria.Enums.AnchorType.SolidSide,
                TileObjectData.newTile.Width,
                0
            );
            TileObjectData.newTile.Origin = new Point16(2, 2);

            TileObjectData.addTile(Type);
        }
    }
}
