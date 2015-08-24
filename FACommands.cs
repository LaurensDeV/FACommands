using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;
using TShockAPI.DB;

namespace FACommands
{
	[ApiVersion(1, 21)]
	public class FACommands : TerrariaPlugin
	{

		public override string Name
		{
			get
			{
				return "FACommands";
			}
		}

		public override string Author
		{
			get
			{
				return "Hiarni & Zaicon";
			}
		}

		public override string Description
		{
			get
			{
				return "FACommands";
			}
		}

		public override Version Version
		{
			get
			{
				return new Version(1, 1, 8);
			}
		}

		public FACommands(Main game)
            : base(game)
        {
            base.Order = 1;
        }

        public override void Initialize()
		{
            ServerApi.Hooks.GameInitialize.Register(this, OnInitialize);
        }

		protected override void Dispose(bool Disposing)
		{
			if (Disposing)
			{
                ServerApi.Hooks.GameInitialize.Deregister(this, OnInitialize);
            }
			base.Dispose(Disposing);
		}

		private void OnInitialize(EventArgs args)
		{
            Commands.ChatCommands.Add(new Command("facommands.staff", FACHistory, "h") { AllowServer = false, HelpText = "Short command for /history" });
            Commands.ChatCommands.Add(new Command("facommands.staff", FACClear, "ca") { HelpText = "Short command for clearing up items and projectiles." });
            Commands.ChatCommands.Add(new Command("worldedit.selection.point", FACP1, "p1") { AllowServer = false, HelpText = "Short command for WorldEdit //point1" });
            Commands.ChatCommands.Add(new Command("worldedit.selection.point", FACP2, "p2") { AllowServer = false, HelpText = "Short command for WorldEdit //point2" });
            Commands.ChatCommands.Add(new Command("facommands.more", FACMore, "more") { AllowServer = false, HelpText = "Fill up all your items to max stack." });
            Commands.ChatCommands.Add(new Command("facommands.npc", FACNPC, "npcr") { AllowServer = false, HelpText = "Respawns all Town NPC's at your location." });
            Commands.ChatCommands.Add(new Command("facommands.obc", FACOBC, "obc") { HelpText = "Owner Broadcast." });
            Commands.ChatCommands.Add(new Command("facommands.slay", FACSlay, "slay") { HelpText = "Slay them DOWN! ALL!" });
            Commands.ChatCommands.Add(new Command("facommands.fun", FACFart, "fart") { HelpText = "Woah! That fart will blow you away!" });
            Commands.ChatCommands.Add(new Command("facommands.fun", FACTickle, "tickle") { HelpText = "Tickle them down!" });
            Commands.ChatCommands.Add(new Command("facommands.fun", FACPoke, "poke") { HelpText = "Give someone a lovely poke." });
            Commands.ChatCommands.Add(new Command("facommands.spoke", FACSPoke, "spoke") { HelpText = "You shouldn't do that..." });
            Commands.ChatCommands.Add(new Command("facommands.stab", FACStab, "stab") { HelpText = "Well, you should better run now!" });
            Commands.ChatCommands.Add(new Command("facommands.fun", FACHug, "hug") { HelpText = "Awwwhhh how lovely!" });
            Commands.ChatCommands.Add(new Command("facommands.fun", FACLick, "lick") { HelpText = "Ugh... are you serious?!" });
            Commands.ChatCommands.Add(new Command("facommands.disturb", FACDisturb, "disturb") { HelpText = "They will catch you with theyr dusty sticks!" });
            Commands.ChatCommands.Add(new Command("facommands.fun", FACPalm, "facepalm") { HelpText = "Perform a facepalm." });
            Commands.ChatCommands.Add(new Command("facommands.fun", FACPlant, "faceplant") { HelpText = "Are you crazy?!" });
            Commands.ChatCommands.Add(new Command("facommands.fun", FACLove, "love") { HelpText = "hum... this must be true love..." });
            Commands.ChatCommands.Add(new Command("facommands.fun", FACBaby, "baby") { HelpText = "uhh wat?! woah wait! Dude! You will pay forever!" });
            Commands.ChatCommands.Add(new Command("facommands.fun", FACKiss, "kiss") { HelpText = "RAAWWWRRRR! What next?!" });
            Commands.ChatCommands.Add(new Command("facommands.fun", FACSlap, "slapall") { HelpText = "Dusty sticks incomming!" });
            Commands.ChatCommands.Add(new Command("facommands.gift", FACGift, "gift") { HelpText = "If they were good!" });
            Commands.ChatCommands.Add(new Command("facommands.staff", FACUI, "uinfo") { HelpText = "Lists detailed informations about players." });
            Commands.ChatCommands.Add(new Command("facommands.staff", FACBI, "binfo") { HelpText = "Lists detailed informations about banned players." });
        }

		private void FACHistory(CommandArgs args)
		{
            Commands.HandleCommand(args.Player, "/history");
		}

		private void FACP1(CommandArgs args)
		{
			Commands.HandleCommand(args.Player, "//point1");
		}

		private void FACP2(CommandArgs args)
		{
			Commands.HandleCommand(args.Player, "//point2");
		}

		private void FACClear(CommandArgs args)
		{
			Commands.HandleCommand(args.Player, "/clear item 100000");
			Commands.HandleCommand(args.Player, "/clear projectile 100000");
		}

        private void FACMore(CommandArgs args)
        {
            if (args.Parameters.Count > 0 && args.Parameters[0].ToLower() == "all")
            {
                bool full = true;
                int i = 0;
                foreach (Item item in args.TPlayer.inventory)
                {
                    if (item == null || item.stack == 0) continue;
                    int amtToAdd = item.maxStack - item.stack;
                    if (item.stack > 0 && amtToAdd > 0 && !item.name.ToLower().Contains("coin"))
                    {
                        full = false;
                        args.Player.GiveItem(item.type, item.name, item.width, item.height, amtToAdd);
                    }
                    i++;
                }
                if (!full)
                    args.Player.SendSuccessMessage("Filled up all your items.");
                else
                    args.Player.SendErrorMessage("Your inventory is already filled up.");
            }
            else
            {
                Item holding = args.Player.TPlayer.inventory[args.TPlayer.selectedItem];
                int amtToAdd = holding.maxStack - holding.stack;
                if (holding.stack > 0 && amtToAdd > 0)
                    args.Player.GiveItem(holding.type, holding.name, holding.width, holding.height, amtToAdd);
                if (amtToAdd == 0)
                    args.Player.SendErrorMessage("Your {0} is already full.", holding.name);
                else
                    args.Player.SendSuccessMessage("Filled up your {0}.", holding.name);
            }
        }

        private void FACNPC(CommandArgs args)
        {
            int killCount = 0;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i].active && Main.npc[i].townNPC)
                {
                    TSPlayer.Server.StrikeNPC(i, 99999, 0f, 0);
                    killCount++;
                }
            }
            TSPlayer.All.SendInfoMessage(string.Format("{0} killed {1} friendly NPCs and spawned all town NPCs.", args.Player.Name, killCount));
            TSPlayer.Server.SpawnNPC(TShock.Utils.GetNPCById(19).type, TShock.Utils.GetNPCById(19).name, 1, args.Player.TileX, args.Player.TileY, 20, 20);
            TSPlayer.Server.SpawnNPC(TShock.Utils.GetNPCById(54).type, TShock.Utils.GetNPCById(54).name, 1, args.Player.TileX, args.Player.TileY, 20, 20);
            TSPlayer.Server.SpawnNPC(TShock.Utils.GetNPCById(209).type, TShock.Utils.GetNPCById(209).name, 1, args.Player.TileX, args.Player.TileY, 20, 20);
            TSPlayer.Server.SpawnNPC(TShock.Utils.GetNPCById(38).type, TShock.Utils.GetNPCById(38).name, 1, args.Player.TileX, args.Player.TileY, 20, 20);
            TSPlayer.Server.SpawnNPC(TShock.Utils.GetNPCById(20).type, TShock.Utils.GetNPCById(20).name, 1, args.Player.TileX, args.Player.TileY, 20, 20);
            TSPlayer.Server.SpawnNPC(TShock.Utils.GetNPCById(207).type, TShock.Utils.GetNPCById(207).name, 1, args.Player.TileX, args.Player.TileY, 20, 20);
            TSPlayer.Server.SpawnNPC(TShock.Utils.GetNPCById(107).type, TShock.Utils.GetNPCById(107).name, 1, args.Player.TileX, args.Player.TileY, 20, 20);
            TSPlayer.Server.SpawnNPC(TShock.Utils.GetNPCById(22).type, TShock.Utils.GetNPCById(22).name, 1, args.Player.TileX, args.Player.TileY, 20, 20);
            TSPlayer.Server.SpawnNPC(TShock.Utils.GetNPCById(124).type, TShock.Utils.GetNPCById(124).name, 1, args.Player.TileX, args.Player.TileY, 20, 20);
            TSPlayer.Server.SpawnNPC(TShock.Utils.GetNPCById(17).type, TShock.Utils.GetNPCById(17).name, 1, args.Player.TileX, args.Player.TileY, 20, 20);
            TSPlayer.Server.SpawnNPC(TShock.Utils.GetNPCById(18).type, TShock.Utils.GetNPCById(18).name, 1, args.Player.TileX, args.Player.TileY, 20, 20);
            TSPlayer.Server.SpawnNPC(TShock.Utils.GetNPCById(227).type, TShock.Utils.GetNPCById(227).name, 1, args.Player.TileX, args.Player.TileY, 20, 20);
            TSPlayer.Server.SpawnNPC(TShock.Utils.GetNPCById(208).type, TShock.Utils.GetNPCById(208).name, 1, args.Player.TileX, args.Player.TileY, 20, 20);
            TSPlayer.Server.SpawnNPC(TShock.Utils.GetNPCById(229).type, TShock.Utils.GetNPCById(229).name, 1, args.Player.TileX, args.Player.TileY, 20, 20);
            TSPlayer.Server.SpawnNPC(TShock.Utils.GetNPCById(178).type, TShock.Utils.GetNPCById(178).name, 1, args.Player.TileX, args.Player.TileY, 20, 20);
            TSPlayer.Server.SpawnNPC(TShock.Utils.GetNPCById(353).type, TShock.Utils.GetNPCById(353).name, 1, args.Player.TileX, args.Player.TileY, 20, 20);
            TSPlayer.Server.SpawnNPC(TShock.Utils.GetNPCById(368).type, TShock.Utils.GetNPCById(368).name, 1, args.Player.TileX, args.Player.TileY, 20, 20);
            TSPlayer.Server.SpawnNPC(TShock.Utils.GetNPCById(160).type, TShock.Utils.GetNPCById(160).name, 1, args.Player.TileX, args.Player.TileY, 20, 20);
            TSPlayer.Server.SpawnNPC(TShock.Utils.GetNPCById(228).type, TShock.Utils.GetNPCById(228).name, 1, args.Player.TileX, args.Player.TileY, 20, 20);
            TSPlayer.Server.SpawnNPC(TShock.Utils.GetNPCById(108).type, TShock.Utils.GetNPCById(108).name, 1, args.Player.TileX, args.Player.TileY, 20, 20);
            TSPlayer.Server.SpawnNPC(TShock.Utils.GetNPCById(369).type, TShock.Utils.GetNPCById(369).name, 1, args.Player.TileX, args.Player.TileY, 20, 20);
            TSPlayer.Server.SpawnNPC(TShock.Utils.GetNPCById(441).type, TShock.Utils.GetNPCById(441).name, 1, args.Player.TileX, args.Player.TileY, 20, 20);
        }

        private void FACOBC(CommandArgs args)
        {
            string message = string.Join(" ", args.Parameters);

            TShock.Utils.Broadcast(
                "(Owner Broadcast) " + message, (Color.Cyan));
        }

        private void FACSlay(CommandArgs args)
        {
            if (args.Parameters.Count < 2)
            {
                args.Player.SendErrorMessage("Invalid syntax! Proper syntax: /slay <player> <reason>");
            }
            else if (args.Parameters[0].Length == 0)
            {
                args.Player.SendErrorMessage("Invalid player!");
            }
            else
            {
                string text = args.Parameters[0];
                List<TSPlayer> list = TShock.Utils.FindPlayer(text);
                if (list.Count == 0)
                {
                    args.Player.SendErrorMessage("Invalid player!");
                }
                else if (list.Count > 1)
                {
                    TShock.Utils.SendMultipleMatchError(args.Player, from p in list
                                                                     select p.Name);
                }
                else
                {
                    TSPlayer tSPlayer = list[0];
                    string reason = " " + string.Join(" ", args.Parameters.Skip(1));
                    NetMessage.SendData((int)26, -1, -1, reason, tSPlayer.Index, 0f, 15000);
                    args.Player.SendSuccessMessage("You just slayed {0}.", tSPlayer.Name);
                }
            }
        }

        private void FACFart(CommandArgs args)
        {
            if (args.Parameters.Count != 1)
			{
				args.Player.SendErrorMessage("Invalid syntax! Proper syntax: /fart <player>");
			}
			else if (args.Parameters[0].Length == 0)
			{
				args.Player.SendErrorMessage("Invalid player!");
			}
			else
			{
				string text = args.Parameters[0];
				List<TSPlayer> list = TShock.Utils.FindPlayer(text);
				if (list.Count == 0)
				{
					args.Player.SendErrorMessage("No players matched!");
				}
				else if (list.Count > 1)
                {
                    TShock.Utils.SendMultipleMatchError(args.Player, from p in list
                    select p.Name);
                }
                else if (list[0].Group.Name == "admin" | list[0].Group.Name == "owner")
                {
                    args.Player.SendErrorMessage("You cannot fart this player!");
                }
                else
                {
                    TSPlayer tSPlayer = list[0];
                    tSPlayer.SetBuff(163, 600, true);
                    tSPlayer.SetBuff(120, 600, true);
                    args.Player.SendInfoMessage("Woah! That fart dude! You shouldn't eat so much shit!", new object[]
                {
                        tSPlayer.Name
                });
                    TSPlayer.All.SendMessage(string.Format("{0} turned around and farted {1} in the face! Woah! That fart mades you blind dude! aahh it stinks so much! Run away! o.O", args.Player.Name, tSPlayer.Name), Color.Sienna);
                    TShock.Log.Info("{0} farted {1} in the face!", new object[]
                    {
                        args.Player.Name,
                        tSPlayer.Name
                    });
                }
            }
        }

        private void FACTickle(CommandArgs args)
        {
            if (args.Parameters.Count != 1)
            {
                args.Player.SendErrorMessage("Invalid syntax! Proper syntax: /tickle <player>");
            }
            else if (args.Parameters[0].Length == 0)
            {
                args.Player.SendErrorMessage("Invalid player!");
            }
            else
            {
                string text = args.Parameters[0];
                List<TSPlayer> list = TShock.Utils.FindPlayer(text);
                if (list.Count > 1)
                {
                    TShock.Utils.SendMultipleMatchError(args.Player, from p in list
                                                                     select p.Name);
                }
                else
                {
                    TSPlayer tSPlayer = list[0];
                    tSPlayer.SetBuff(2, 3600, true);
                    args.Player.SendInfoMessage("You shouldn't tickle people to much... >.>", new object[]
                {
                        tSPlayer.Name
                });
                    TSPlayer.All.SendMessage(string.Format("{0} tickles {1}! Isn't that sweet? :P", args.Player.Name, tSPlayer.Name), Color.Thistle);
                    TShock.Log.Info("{0} tickles {1}!", new object[]
                    {
                        args.Player.Name,
                        tSPlayer.Name
                    });
                }
            }
        }

        private void FACPoke(CommandArgs args)
		{
			if (args.Parameters.Count != 1)
			{
				args.Player.SendErrorMessage("Invalid syntax! Proper syntax: /poke <player>");
			}
			else if (args.Parameters[0].Length == 0)
			{
				args.Player.SendErrorMessage("Invalid player!");
			}
			else
			{
				string text = args.Parameters[0];
				List<TSPlayer> list = TShock.Utils.FindPlayer(text);
				if (list.Count == 0)
				{
					args.Player.SendErrorMessage("Invalid player!");
				}
				else if (list.Count > 1)
				{
					TShock.Utils.SendMultipleMatchError(args.Player, from p in list
					select p.Name);
				}
				else
				{
					TSPlayer tSPlayer = list[0];
					tSPlayer.DamagePlayer(1);
					args.Player.SendInfoMessage("You poked {0}. Uh oh... should you run now?", new object[]
					{
						tSPlayer.Name
					});
                    TSPlayer.All.SendMessage(string.Format("{0} poked {1}. Well, that was lovely! :3", args.Player.Name, tSPlayer.Name), Color.LightSkyBlue);                    
					TShock.Log.Info("{0} poked {1}.", new object[]
					{
						args.Player.Name,
						tSPlayer.Name
					});
				}
			}
		}

		private void FACSPoke(CommandArgs args)
		{
			if (args.Parameters.Count != 1)
			{
				args.Player.SendErrorMessage("Invalid syntax! Proper syntax: /spoke <player>");
			}
			else if (args.Parameters[0].Length == 0)
			{
				args.Player.SendErrorMessage("Invalid player!");
			}
			else
			{
				string text = args.Parameters[0];
				List<TSPlayer> list = TShock.Utils.FindPlayer(text);
				if (list.Count == 0)
				{
					args.Player.SendErrorMessage("Invalid player!");
				}
				else if (list.Count > 1)
				{
					TShock.Utils.SendMultipleMatchError(args.Player, from p in list
					select p.Name);
				}
				else
				{
					TSPlayer tSPlayer = list[0];
					tSPlayer.DamagePlayer(9001);
					args.Player.SendInfoMessage("You poked {0}. BOOM! BANG! PAW!", new object[]
					{
						tSPlayer.Name
					});
                    TSPlayer.All.SendMessage(string.Format("{0} poked {1} in the tummy. KADUSH! Who is the next one?!", args.Player.Name, tSPlayer.Name), Color.MediumTurquoise);                    
					TShock.Log.Info("{0} super-poked {1}.", new object[]
					{
						args.Player.Name,
						tSPlayer.Name
					});
				}
			}
		}

		private void FACHug(CommandArgs args)
		{
			if (args.Parameters.Count != 1)
			{
				args.Player.SendErrorMessage("Invalid syntax! Proper syntax: /hug <player>");
			}
			else if (args.Parameters[0].Length == 0)
			{
				args.Player.SendErrorMessage("Invalid player!");
			}
			else
			{
				string text = args.Parameters[0];
				List<TSPlayer> list = TShock.Utils.FindPlayer(text);
				if (list.Count == 0)
				{
					args.Player.SendInfoMessage("You hugged your invisible friend {0}! Common! Get a life bro...", new object[]
					{
						text
					});
                    TSPlayer.All.SendMessage(string.Format("{0} hugged " + (args.Player.TPlayer.Male ? "his" : "her") + " invisible friend {1}! Common! Really? Get a life bro...", args.Player.Name, text), Color.Chartreuse);                   
				}
				else if (list.Count > 1)
				{
					TShock.Utils.SendMultipleMatchError(args.Player, from p in list
					select p.Name);
				}
				else
				{
					TSPlayer tSPlayer = list[0];
					args.Player.SendInfoMessage("You hugged {0}! You need love huh?", new object[]
					{
						tSPlayer.Name
					});
                    TSPlayer.All.SendMessage(string.Format("{0} hugged {1}! Awwwhhh... how sweet? <3", args.Player.Name, tSPlayer.Name), Color.Chartreuse);
					TShock.Log.Info("{0} hugged {1}! Awwwhhh... how sweet? <3", new object[]
					{
						args.Player.Name,
						tSPlayer.Name
					});
				}
			}
		}

		private void FACLick(CommandArgs args)
		{
			if (args.Parameters.Count != 1)
			{
				args.Player.SendErrorMessage("Invalid syntax! Proper syntax: /lick <player>");
			}
			else if (args.Parameters[0].Length == 0)
			{
				args.Player.SendErrorMessage("Invalid player!");
			}
			else
			{
				string text = args.Parameters[0];
				List<TSPlayer> list = TShock.Utils.FindPlayer(text);
				if (list.Count == 0)
				{
					if (args.Parameters[0].ToLower() != "air")
					{
						args.Player.SendInfoMessage("You licked the air! Tasted like air... hum... {0} was not found...", new object[]
						{
							text
						});
                        TSPlayer.All.SendMessage(string.Format("{0} licked the air! Tasted like air... hum...", args.Player.Name), Color.DarkOrchid);
					}
					else
					{
						args.Player.SendInfoMessage("You licked the air! Tasted like air... hum...", new object[]
						{
							text
						});
                        TSPlayer.All.SendMessage(string.Format("{0} licked the air! Tasted like air... hum...", args.Player.Name), Color.DarkOrchid);
					}
				}
				else if (list.Count > 1)
				{
					TShock.Utils.SendMultipleMatchError(args.Player, from p in list
					select p.Name);
				}
				else
				{
					TSPlayer tSPlayer = list[0];
					args.Player.SendInfoMessage("You licked {0}! Really...?", new object[]
					{
						tSPlayer.Name
					});
                    TSPlayer.All.SendMessage(string.Format("{0} licked {1}! Ugh... X.X", args.Player.Name, tSPlayer.Name), Color.DarkOrchid);                    
					TShock.Log.Info("{0} licked {1}!", new object[]
					{
						args.Player.Name,
						tSPlayer.Name
					});
				}
			}
		}

		private void FACPalm(CommandArgs args)
		{
			args.Player.SendInfoMessage("You facepalmed.");
            TSPlayer.All.SendMessage(string.Format("{0} facepalmed.", args.Player.Name), Color.Chocolate);   
            TShock.Log.Info("{0} facepalmed.", new object[]
			{
				args.Player.Name
			});
		}

		private void FACKiss(CommandArgs args)
		{
			if (args.Parameters.Count != 1)
			{
				args.Player.SendErrorMessage("Invalid syntax! Proper syntax: /kiss <player>");
			}
			else if (args.Parameters[0].Length == 0)
			{
				args.Player.SendErrorMessage("Invalid player!");
			}
			else
			{
				string text = args.Parameters[0];
				List<TSPlayer> list = TShock.Utils.FindPlayer(text);
				if (list.Count == 0)
				{
					if (args.Parameters[0].ToLower() != "air")
					{
						args.Player.SendInfoMessage("You kissed the air! {0} was not found... get a life bro...", new object[]
						{
							text
						});
                        TSPlayer.All.SendMessage(string.Format("{0} kissed the air! The hell...?!", args.Player.Name), Color.Coral);                    
					}
					else
					{
						args.Player.SendInfoMessage("You kissed the air! WTF?!", new object[]
						{
							text
						});
                        TSPlayer.All.SendMessage(string.Format("{0} kissed the air! The hell...?!", args.Player.Name), Color.Coral);
					}
				}
				else if (list.Count > 1)
				{
					TShock.Utils.SendMultipleMatchError(args.Player, from p in list
					select p.Name);
				}
				else
				{
					TSPlayer tSPlayer = list[0];
					args.Player.SendInfoMessage("You kissed {0}! RAAWWRRR! Are you horny?", new object[]
					{
						tSPlayer.Name
					});
                    TSPlayer.All.SendMessage(string.Format("{0} kisses {1}! Love is everywhere! <3", args.Player.Name, tSPlayer.Name), Color.Coral);                 
                    TShock.Log.Info("{0} kisses {1}!", new object[]
					{
						args.Player.Name,
						tSPlayer.Name
					});
				}
			}
		}

        private void FACBaby(CommandArgs args)
        {
            if (args.Parameters.Count != 1)
            {
                args.Player.SendErrorMessage("Invalid syntax! Proper syntax: /baby <player>");
            }
            else if (args.Parameters[0].Length == 0)
            {
                args.Player.SendErrorMessage("Invalid player!");
            }
            else
            {
                string text = args.Parameters[0];
                List<TSPlayer> list = TShock.Utils.FindPlayer(text);
                if (list.Count > 1)
                {
                    TShock.Utils.SendMultipleMatchError(args.Player, from p in list
                                                                     select p.Name);
                }
                else if (list[0].Group.Name == "admin" | list[0].Group.Name == "owner")
                {
                    args.Player.SendErrorMessage("You can't make a baby with this player!");
                }
                else
                {
                    TSPlayer tSPlayer = list[0];
                        tSPlayer.SetBuff(92, 3600, false);
                        tSPlayer.SetBuff(103, 3600, true);
                        args.Player.SendInfoMessage("... does it made you happy? I hope it's your true love... otherwise much fun with the alimony! :D", new object[]
                    {
                        tSPlayer.Name
                    });
                    TSPlayer.All.SendMessage(string.Format("{0} tried to make a baby (grinch) with {1}! awwhhh look! It's soooo cute! <3", args.Player.Name, tSPlayer.Name), Color.Firebrick);
                    TShock.Log.Info("{0} tried to make a baby (grinch) with {1}!", new object[]
                    {
                        args.Player.Name,
                        tSPlayer.Name
                    });
                }
            }
        }

        private void FACStab(CommandArgs args)
		{
			if (args.Parameters.Count != 1)
			{
				args.Player.SendErrorMessage("Invalid syntax! Proper syntax: /stab <player>");
			}
			else if (args.Parameters[0].Length == 0)
			{
				args.Player.SendErrorMessage("Invalid player!");
			}
			else
			{
				string text = args.Parameters[0];
				List<TSPlayer> list = TShock.Utils.FindPlayer(text);
				if (list.Count == 0)
				{
					args.Player.SendInfoMessage("You stabbed your invisible friend {0}! You should train harder!", new object[]
					{
						text
					});
                    TSPlayer.All.SendMessage(string.Format("{0} stabbed " + (args.Player.TPlayer.Male ? "his" : "her") + " invisible friend {1}! He should train harder...", args.Player.Name, text), Color.AliceBlue);
				}
				else if (list.Count > 1)
				{
					TShock.Utils.SendMultipleMatchError(args.Player, from p in list
					select p.Name);
				}
				else
				{
					TSPlayer tSPlayer = list[0];
					tSPlayer.DamagePlayer(9001);
					args.Player.SendInfoMessage("You stabbed {0} for OVER 9000 damage! That was close!", new object[]
					{
						tSPlayer.Name
					});
                    TSPlayer.All.SendMessage(string.Format("{0} stabbed {1} mercilessly! GO CATCH HIM! O.O", args.Player.Name, tSPlayer.Name), Color.AliceBlue);                   
                    TShock.Log.Info("{0} stabbed {1}.", new object[]
					{
						args.Player.Name,
						tSPlayer.Name
					});
				}
			}
		}

		private void FACLove(CommandArgs args)
		{
			if (args.Parameters.Count != 1)
			{
				args.Player.SendErrorMessage("Invalid syntax! Proper syntax: /love <player>");
			}
			else if (args.Parameters[0].Length == 0)
			{
				args.Player.SendErrorMessage("Invalid player!");
			}
			else
			{
				string text = args.Parameters[0];
				List<TSPlayer> list = TShock.Utils.FindPlayer(text);
				if (list.Count == 0)
				{
					if (args.Parameters[0].ToLower() != "air")
					{
						args.Player.SendInfoMessage("You love the air! {0} was not found... GO! Search your true love!", new object[]
						{
							text
						});
                        TSPlayer.All.SendMessage(string.Format("{0} loves the air! You should get some friends... :O", args.Player.Name), Color.Pink);                       
					}
					else
					{
						args.Player.SendInfoMessage("You love the air! Really?", new object[]
						{
							text
						});
                        TSPlayer.All.SendMessage(string.Format("{0} loves the air! Well... >.>", args.Player.Name), Color.Pink);                        
					}
				}
				else if (list.Count > 1)
				{
					TShock.Utils.SendMultipleMatchError(args.Player, from p in list
					select p.Name);
				}
				else
				{
					TSPlayer tSPlayer = list[0];
					args.Player.SendInfoMessage("You love {0}! hum... next step? ;)", new object[]
					{
						tSPlayer.Name
					});
                    TSPlayer.All.SendMessage(string.Format("{0} loves {1}! Oehlalah... ready for the next step? <3", args.Player.Name, tSPlayer.Name), Color.Pink);
                    TShock.Log.Info("{0} loves {1}!", new object[]
					{
						args.Player.Name,
						tSPlayer.Name
					});
				}
			}
		}

		private void FACPlant(CommandArgs args)
		{
			if (!args.Player.RealPlayer)
			{
				args.Player.SendInfoMessage("You planted your face on the ground. Serious man...?");
			}
			else
			{
				args.Player.DamagePlayer(1000);
			}
            TSPlayer.All.SendMessage(string.Format("{0} planted " + (args.Player.TPlayer.Male ? "his" : "her") + " face on the ground. Are you crazy?!", args.Player.Name), Color.BlanchedAlmond); 
		}

		private void FACSlap(CommandArgs args)
		{
			if (!args.Player.RealPlayer)
			{
				args.Player.SendInfoMessage("You slapped everyone! That stings!");
			}
            TSPlayer.All.SendMessage(string.Format("{0} slapped you (along with everyone else)! THE HELL! CATCH HIM! :O", args.Player.Name), Color.BlanchedAlmond);
			TSPlayer.All.DamagePlayer(15);
		}

		private void FACGift(CommandArgs args)
		{
			if (args.Parameters.Count != 1)
			{
				args.Player.SendErrorMessage("Invalid syntax: /gift <player>");
			}
			else
			{
				List<TSPlayer> list = TShock.Utils.FindPlayer(args.Parameters[0]);
				if (list.Count == 0)
				{
					args.Player.SendErrorMessage("No players found by that name.");
				}
				else if (list.Count > 1)
				{
					TShock.Utils.SendMultipleMatchError(args.Player, from p in list
					select p.Name);
				}
				else
				{
					TSPlayer tSPlayer = list[0];
					Random random = new Random();
					if (random.Next(2) == 0)
					{
						Item itemById = TShock.Utils.GetItemById(1922);
						tSPlayer.GiveItem(itemById.type, itemById.name, itemById.width, itemById.height, itemById.maxStack, 0);
						tSPlayer.SendInfoMessage("{0} gave you Coal! You were a naughty {1}...", new object[]
						{
							args.Player.Name,
							tSPlayer.TPlayer.Male ? "boy" : "girl"
						});
						args.Player.SendSuccessMessage("You gave {0} Coal! {0} was a naughty {1}...", new object[]
						{
							tSPlayer.Name,
							tSPlayer.TPlayer.Male ? "boy" : "girl"
						});
					}
					else
					{
						Item itemById = TShock.Utils.GetItemById(1869);
						tSPlayer.GiveItem(itemById.type, itemById.name, itemById.width, itemById.height, itemById.stack, 1);
						tSPlayer.SendInfoMessage("{0} gave you a Present! You were a good {1}...", new object[]
						{
							args.Player.Name,
							tSPlayer.TPlayer.Male ? "boy" : "girl"
						});
						args.Player.SendSuccessMessage("You gave {0} a Present! {0} was a good {1}...", new object[]
						{
							tSPlayer.Name,
							tSPlayer.TPlayer.Male ? "boy" : "girl"
						});
					}
				}
			}
		}

		private void FACDisturb(CommandArgs args)
		{
			if (args.Parameters.Count != 1)
			{
				args.Player.SendErrorMessage("Invalid syntax! Proper syntax: /disturb <player>");
			}
			else if (args.Parameters[0].Length == 0)
			{
				args.Player.SendErrorMessage("Invalid player!");
			}
			else
			{
				string text = args.Parameters[0];
				List<TSPlayer> list = TShock.Utils.FindPlayer(text);
				if (list.Count == 0)
				{
					args.Player.SendErrorMessage("No players matched!");
				}
				else if (list.Count > 1)
				{
					TShock.Utils.SendMultipleMatchError(args.Player, from p in list
					select p.Name);
				}
				else if (list[0].Group.Name == "admin" | list[0].Group.Name == "owner")
                {
					args.Player.SendErrorMessage("You cannot disturb this player!");
				}
				else
				{
					TSPlayer tSPlayer = list[0];
					tSPlayer.SetBuff(26, 900, true);
					tSPlayer.SetBuff(30, 900, true);
					tSPlayer.SetBuff(31, 900, true);
					tSPlayer.SetBuff(32, 900, true);
					tSPlayer.SetBuff(103, 900, true);
					tSPlayer.SetBuff(115, 900, true);
					tSPlayer.SetBuff(120, 900, true);
					args.Player.SendInfoMessage("You disturbed {0}! You feel slightly better now...", new object[]
					{
						tSPlayer.Name
					});
                    TSPlayer.All.SendMessage(string.Format("{0} disturbed {1}! Is he angry now? huh?", args.Player.Name, tSPlayer.Name), Color.Crimson);
                    TShock.Log.Info("{0} disturbed {1}.", new object[]
					{
						args.Player.Name,
						tSPlayer.Name
					});
				}
			}
		}

		private void FACUI(CommandArgs args)
		{
			if (args.Parameters.Count == 1)
			{
				string text = string.Join(" ", args.Parameters);
				if (text != null & text != "")
				{
					User userByName = TShock.Users.GetUserByName(text);
					if (userByName != null)
					{
						args.Player.SendMessage(string.Format("User {0} exists.", text), Color.DeepPink);
						try
						{
							DateTime dateTime = DateTime.Parse(userByName.Registered);
							DateTime dateTime2 = DateTime.Parse(userByName.LastAccessed);
							List<string> list = JsonConvert.DeserializeObject<List<string>>(userByName.KnownIps);
							string arg = list[list.Count - 1];
							args.Player.SendMessage(string.Format("{0}'s group is {1}.", text, userByName.Group), Color.DeepPink);
							args.Player.SendMessage(string.Format("{0}'s last known IP is {1}.", text, arg), Color.DeepPink);
							args.Player.SendMessage(string.Format("{0}'s register date is {1}.", text, dateTime.ToShortDateString()), Color.DeepPink);
							args.Player.SendMessage(string.Format("{0} was last seen {1}.", text, dateTime2.ToShortDateString(), dateTime2.ToShortTimeString()), Color.DeepPink);
						}
						catch
						{
							DateTime dateTime = DateTime.Parse(userByName.Registered);
							args.Player.SendMessage(string.Format("{0}'s group is {1}.", text, userByName.Group), Color.DeepPink);
							args.Player.SendMessage(string.Format("{0}'s register date is {1}.", text, dateTime.ToShortDateString()), Color.DeepPink);
						}
					}
					else
					{
						args.Player.SendMessage(string.Format("User {0} does not exist.", text), Color.DeepPink);
					}
				}
				else
				{
					args.Player.SendErrorMessage("Syntax: /uinfo <player name>.");
				}
			}
			else
			{
				args.Player.SendErrorMessage("Syntax: /uinfo <player name>");
			}
		}

		private void FACBI(CommandArgs args)
		{

			if (args.Parameters.Count != 1)
			{
				args.Player.SendErrorMessage("Invalid syntax: /baninfo \"Player Name\"");
			}
			else
			{
				string text = args.Parameters[0];
				Ban banByName = TShock.Bans.GetBanByName(text, true);
				if (banByName == null)
				{
					args.Player.SendErrorMessage("No bans by this name were found.");
				}
				else
				{
					args.Player.SendInfoMessage(string.Concat(new string[]
					{
						"Account name: ",
						banByName.Name,
						" (",
						banByName.IP,
						")"
					}));
					args.Player.SendInfoMessage("Date banned: " + banByName.Date);
					if (banByName.Expiration != "")
					{
						args.Player.SendInfoMessage("Expiration date: " + banByName.Expiration);
					}
					args.Player.SendInfoMessage("Banning user: " + banByName.BanningUser);
					args.Player.SendInfoMessage("Reason: " + banByName.Reason);
				}
			}
		}
    }
}