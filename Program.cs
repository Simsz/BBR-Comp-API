using BattleBitAPI;
using BattleBitAPI.Common;
using BattleBitAPI.Server;
using System.Threading.Channels;
using System.Xml;
using BBRC.Common.GameRules;
class Program {
    static void Main(string[] args) {
        var listener = new ServerListener < MyPlayer,MyGameServer > ();
        listener.Start(27099);

        Thread.Sleep(-1);
    }
}
class MyPlayer: Player < MyPlayer > {
  public override async Task OnConnected() {
    
  }
}


class MyGameServer: GameServer < MyPlayer > {

    public static BBRCGameRules Rules = new BBRCGameRules();

  public override async Task OnConnected() {
    serverBanListSet();
    ForceStartGame();
    MapRotation.SetRotation("TensaTown"); // Specify your map name here.
    GamemodeRotation.SetRotation("CONQ");
  }

  public override async Task OnPlayerConnected(MyPlayer player) {
    await Console.Out.WriteLineAsync("Connected: " + player);
  }
  public override async Task OnPlayerSpawned(MyPlayer player) {
    await Console.Out.WriteLineAsync("Spawned: " + player);
  }
  public override async Task OnAPlayerDownedAnotherPlayer(OnPlayerKillArguments < MyPlayer > args) {
    await Console.Out.WriteLineAsync("Downed: " + args.Victim);
  }
  public override async Task OnPlayerGivenUp(MyPlayer player) {
    await Console.Out.WriteLineAsync("Giveup: " + player);
  }
  public override async Task OnPlayerDied(MyPlayer player) {
    await Console.Out.WriteLineAsync("Died: " + player);
  }
  public override async Task OnAPlayerRevivedAnotherPlayer(MyPlayer from, MyPlayer to) {
    await Console.Out.WriteLineAsync(from + " revived " + to);
  }
  public override async Task OnPlayerDisconnected(MyPlayer player) {
    await Console.Out.WriteLineAsync("Disconnected: " + player);
  }

  // player spawning
  public override async Task<OnPlayerSpawnArguments> OnPlayerSpawning(MyPlayer player, OnPlayerSpawnArguments request) {
    
    Weapon WeaponPrimary = request.Loadout.PrimaryWeapon.Tool;
    Weapon WeaponSecondary = request.Loadout.SecondaryWeapon.Tool;
    Gadget HeavyGadget = request.Loadout.HeavyGadget;
    Gadget LightGadget = request.Loadout.LightGadget;
    PlayerWearings Wearings = request.Wearings;

    // TODO: make sure that this works
    if (Rules.weaponBans.IsBanned(WeaponPrimary)) {
      player.Message($"{WeaponPrimary} is banned");
      WeaponPrimary = null;
      player.SetPrimaryWeapon(default,0);
    }
    if (Rules.weaponBans.IsBanned(WeaponSecondary)) {
      player.Message($"{WeaponSecondary} is banned");
      WeaponSecondary = null;
      player.SetSecondaryWeapon(default,0);
    }
    if (Rules.gadgetBans.IsBanned(HeavyGadget)) {
      player.Message($"{HeavyGadget} is banned");
      HeavyGadget = null;
      player.SetHeavyGadget(default,0);
    }
    if (Rules.gadgetBans.IsBanned(LightGadget)) {
      player.Message($"{LightGadget} is banned");
      LightGadget = null;
      player.SetLightGadget(default,0);
    }
    if (Rules.gadgetBans.IsBanned(Gadgets.ImpactGrenade)) {
      player.Message($"{LightGadget} is banned");
      LightGadget = null;
      player.SetThrowable(default,0);
    }
    
    var (isBanned, bannedItems) = await Rules.wearingsBans.IsBanned(Wearings);
    if (isBanned) {
      if (bannedItems.Count == 0) {
        player.Message("Bro ur entire fucking outfit is banned");
        foreach(var field in typeof (PlayerWearings).GetFields()) {
          field.SetValue(player, null);
        }
      } else {
        foreach(var item in bannedItems) {
          switch (item) {
          case "Head":
            Wearings.Head = null;
            break;
          case "Chest":
            Wearings.Chest = null;
            break;
          case "Belt":
            Wearings.Belt = null;
            break;
          case "Backbag":
            Wearings.Backbag = null;
            break;
          case "Eye":
            Wearings.Eye = null;
            break;
          case "Face":
            Wearings.Face = null;
            break;
          case "Hair":
            Wearings.Hair = null;
            break;
          case "Skin":
            Wearings.Skin = null;
            break;
          case "Uniform":
            Wearings.Uniform = null;
            break;
          case "Camo":
            Wearings.Hair = null;
            break;
          }
        }
        player.Message($"The Following items are banned: |{string.Join(" | ", bannedItems)}|");
      }
    }

    return request;
  }

  public static void serverBanListSet(){

    //Weapon Bans
    Rules.weaponBans.BanWeapon(Weapons.ScorpionEVO);
    Rules.weaponBans.BanWeapon(Weapons.FAL);
    Rules.weaponBans.BanWeapon(Weapons.KrissVector);
    //Gadget Bans
    Rules.gadgetBans.BanGadget(Gadgets.C4);
    Rules.gadgetBans.BanGadget(Gadgets.SuicideC4);
    Rules.gadgetBans.BanGadget(Gadgets.AntiPersonnelMine);
    Rules.gadgetBans.BanGadget(Gadgets.AntiVehicleMine);
    Rules.gadgetBans.BanGadget(Gadgets.Claymore);
    Rules.gadgetBans.BanGadget(Gadgets.Rpg7Fragmentation);
    Rules.gadgetBans.BanGadget(Gadgets.Rpg7HeatExplosive);
    Rules.gadgetBans.BanGadget(Gadgets.Rpg7Pgo7Fragmentation);
    Rules.gadgetBans.BanGadget(Gadgets.Rpg7Pgo7HeatExplosive);
    Rules.gadgetBans.BanGadget(Gadgets.Rpg7Pgo7Tandem);
    Rules.gadgetBans.BanGadget(Gadgets.ImpactGrenade);
    //Clothing Bans
    Rules.wearingsBans.BanWearings(PlayerWearingsList.ExoArmor);
    Rules.wearingsBans.BanWearings(PlayerWearingsList.HeavyArmor);
    Rules.wearingsBans.BanWearings(PlayerWearingsList.HeavyHelmet);
    Rules.wearingsBans.BanWearings(PlayerWearingsList.ExoHelmet);
    Rules.wearingsBans.BanWearings(PlayerWearingsList.LightHelmet);

  }
}



