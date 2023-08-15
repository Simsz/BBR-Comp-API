﻿using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace BattleBitAPI.Common
{
    public struct PlayerWearings
    {
        public string Head;
        public string Chest;
        public string Belt;
        public string Backbag;
        public string Eye;
        public string Face;
        public string Hair;
        public string Skin;
        public string Uniform;
        public string Camo;

        public void Write(Common.Serialization.Stream ser)
        {
            ser.WriteStringItem(this.Head);
            ser.WriteStringItem(this.Chest);
            ser.WriteStringItem(this.Belt);
            ser.WriteStringItem(this.Backbag);
            ser.WriteStringItem(this.Eye);
            ser.WriteStringItem(this.Face);
            ser.WriteStringItem(this.Hair);
            ser.WriteStringItem(this.Skin);
            ser.WriteStringItem(this.Uniform);
            ser.WriteStringItem(this.Camo);
        }
        public void Read(Common.Serialization.Stream ser)
        {
            ser.TryReadString(out this.Head);
            ser.TryReadString(out this.Chest);
            ser.TryReadString(out this.Belt);
            ser.TryReadString(out this.Backbag);
            ser.TryReadString(out this.Eye);
            ser.TryReadString(out this.Face);
            ser.TryReadString(out this.Hair);
            ser.TryReadString(out this.Skin);
            ser.TryReadString(out this.Uniform);
            ser.TryReadString(out this.Camo);
        }

        // muj features

        public override bool Equals([NotNullWhen(true)] object obj)
        {
            if (obj == null || !(obj is PlayerWearings))
            {
                return false;
            }

            PlayerWearings playerWearings = (PlayerWearings)obj;

            return Head == playerWearings.Head &&
                Chest == playerWearings.Chest &&
                Belt == playerWearings.Belt &&
                Backbag == playerWearings.Backbag &&
                Eye == playerWearings.Eye &&
                Face == playerWearings.Face &&
                Hair == playerWearings.Hair &&
                Skin == playerWearings.Skin &&
                Uniform == playerWearings.Uniform &&
                Camo == playerWearings.Camo;
        }

        public override int GetHashCode()
        {
            return (Head?.GetHashCode() ?? 0) ^
                   (Chest?.GetHashCode() ?? 0) ^
                   (Belt?.GetHashCode() ?? 0) ^
                   (Backbag?.GetHashCode() ?? 0) ^
                   (Eye?.GetHashCode() ?? 0) ^
                   (Face?.GetHashCode() ?? 0) ^
                   (Hair?.GetHashCode() ?? 0) ^
                   (Skin?.GetHashCode() ?? 0) ^
                   (Uniform?.GetHashCode() ?? 0) ^
                   (Camo?.GetHashCode() ?? 0);
        }
    }

    public static class PlayerWearingsList
    {
        private static Dictionary<string, PlayerWearings> mWearings;

        public readonly static PlayerWearings EmptyArmor = new PlayerWearings
        {
            Head = " ",
            Chest = " ",
			Belt = " ",
			Backbag = " ",
			Eye = " ",
			Face = " ",
			Hair = " ",
			Skin = " ",
			Uniform = " ",
			Camo = " "
        };

        public readonly static PlayerWearings ExoArmor = new PlayerWearings
        {
            Head = " ",
            Chest = "Exo Armor",
			Belt = " ",
			Backbag = " ",
			Eye = " ",
			Face = " ",
			Hair = " ",
			Skin = " ",
			Uniform = " ",
			Camo = " "
        };

        public readonly static PlayerWearings HeavyArmor = new PlayerWearings
        {
            Head = " ",
            Chest = "Heavy Armor",
			Belt = " ",
			Backbag = " ",
			Eye = " ",
			Face = " ",
			Hair = " ",
			Skin = " ",
			Uniform = " ",
			Camo = " "
        };

		public readonly static PlayerWearings HeavyHelmet = new PlayerWearings
        {
            Head = "Exo Helmet",
            Chest = " ",
			Belt = " ",
			Backbag = " ",
			Eye = " ",
			Face = " ",
			Hair = " ",
			Skin = " ",
			Uniform = " ",
			Camo = " "
        };

		public readonly static PlayerWearings ExoHelmet = new PlayerWearings
        {
            Head = "Exo Helmet",
            Chest = " ",
			Belt = " ",
			Backbag = " ",
			Eye = " ",
			Face = " ",
			Hair = " ",
			Skin = " ",
			Uniform = " ",
			Camo = " "
        };

		public readonly static PlayerWearings LightHelmet = new PlayerWearings
        {
            Head = "Light Helmet",
            Chest = " ",
			Belt = " ",
			Backbag = " ",
			Eye = " ",
			Face = " ",
			Hair = " ",
			Skin = " ",
			Uniform = " ",
			Camo = " "
        };

        // Add more wearings in future...

        static PlayerWearingsList()
        {
            mWearings = new Dictionary<string, PlayerWearings>
            {
                { EmptyArmor.Chest, EmptyArmor },
                { ExoArmor.Chest, ExoArmor },
                { HeavyHelmet.Head, HeavyHelmet },
				{ ExoHelmet.Head, ExoHelmet },
				{ LightHelmet.Head, LightHelmet },
                // Add more wearings to the dictionary...
            };
        }

        public static bool TryFind(string chest, out PlayerWearings wearing)
        {
            return mWearings.TryGetValue(chest, out wearing);
        }
    }
}
