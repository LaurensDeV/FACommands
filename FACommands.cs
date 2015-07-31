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
	[ApiVersion(1, 20)]
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
				return "Zaicon & Hiarni";
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
				return new Version(1, 1, 2, 0);
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
            Commands.ChatCommands.Add(new Command("facommands.staff", FACHistory, "h"));
            Commands.ChatCommands.Add(new Command("facommands.staff", FACClear, "ca"));
            Commands.ChatCommands.Add(new Command("worldedit.selection.point", FACP1, "p1"));
            Commands.ChatCommands.Add(new Command("worldedit.selection.point", FACP2, "p2"));
            Commands.ChatCommands.Add(new Command("facommands.slay", FACSlay, "slay"));
            Commands.ChatCommands.Add(new Command("facommands.fun", FACPoke, "poke"));
            Commands.ChatCommands.Add(new Command("facommands.spoke", FACSPoke, "spoke"));
            Commands.ChatCommands.Add(new Command("facommands.stab", FACStab, "stab"));
            Commands.ChatCommands.Add(new Command("facommands.fun", FACHug, "hug"));
            Commands.ChatCommands.Add(new Command("facommands.fun", FACLick, "lick"));
            Commands.ChatCommands.Add(new Command("facommands.disturb", FACDisturb, "disturb"));
            Commands.ChatCommands.Add(new Command("facommands.fun", FACPalm, "facepalm"));
            Commands.ChatCommands.Add(new Command("facommands.fun", FACPlant, "faceplant"));
            Commands.ChatCommands.Add(new Command("facommands.fun", FACLove, "love"));
            Commands.ChatCommands.Add(new Command("facommands.fun", FACKiss, "kiss"));
            Commands.ChatCommands.Add(new Command("facommands.fun", FACSlap, "slapall"));
            Commands.ChatCommands.Add(new Command("facommands.gift", FACGift, "gift"));
            Commands.ChatCommands.Add(new Command("facommands.staff", FACUI, "uinfo"));
            Commands.ChatCommands.Add(new Command("facommands.staff", FACBI, "binfo"));
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

        private void FACSlay(CommandArgs args)
        {
            if (args.Parameters.Count != 2)
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
					args.Player.SendInfoMessage("You poked {0}.", new object[]
					{
						tSPlayer.Name
					});
					TSPlayer.All.SendSuccessMessage("{0} poked {1}.", new object[]
					{
						args.Player.Name,
						tSPlayer.Name
					});
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
					args.Player.SendInfoMessage("You poked {0}. BOOM!", new object[]
					{
						tSPlayer.Name
					});
					TSPlayer.All.SendSuccessMessage("{0} poked {1}. BOOM!", new object[]
					{
						args.Player.Name,
						tSPlayer.Name
					});
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
					args.Player.SendInfoMessage("You hugged your invisible friend {0}!", new object[]
					{
						text
					});
					TSPlayer.All.SendSuccessMessage("{0} hugged " + (args.Player.TPlayer.Male ? "his" : "her") + " invisible friend {1}!", new object[]
					{
						args.Player.Name,
						text
					});
				}
				else if (list.Count > 1)
				{
					TShock.Utils.SendMultipleMatchError(args.Player, from p in list
					select p.Name);
				}
				else
				{
					TSPlayer tSPlayer = list[0];
					args.Player.SendInfoMessage("You hugged {0}!", new object[]
					{
						tSPlayer.Name
					});
					TSPlayer.All.SendSuccessMessage("{0} hugged {1}!", new object[]
					{
						args.Player.Name,
						tSPlayer.Name
					});
					TShock.Log.Info("{0} hugged {1}!", new object[]
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
						args.Player.SendInfoMessage("You licked the air! {0} was not found...", new object[]
						{
							text
						});
						TSPlayer.All.SendSuccessMessage("{0} licked the air!", new object[]
						{
							args.Player.Name
						});
					}
					else
					{
						args.Player.SendInfoMessage("You licked the air!", new object[]
						{
							text
						});
						TSPlayer.All.SendSuccessMessage("{0} licked the air!", new object[]
						{
							args.Player.Name
						});
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
					args.Player.SendInfoMessage("You licked {0}!", new object[]
					{
						tSPlayer.Name
					});
					TSPlayer.All.SendSuccessMessage("{0} licked {1}!", new object[]
					{
						args.Player.Name,
						tSPlayer.Name
					});
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
			TSPlayer.All.SendSuccessMessage("{0} facepalmed.", new object[]
			{
				args.Player.Name
			});
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
						args.Player.SendInfoMessage("You kissed the air! {0} was not found...", new object[]
						{
							text
						});
						TSPlayer.All.SendSuccessMessage("{0} kissed the air!", new object[]
						{
							args.Player.Name
						});
					}
					else
					{
						args.Player.SendInfoMessage("You kissed the air!", new object[]
						{
							text
						});
						TSPlayer.All.SendSuccessMessage("{0} kissed the air!", new object[]
						{
							args.Player.Name
						});
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
					args.Player.SendInfoMessage("You kissed {0}!", new object[]
					{
						tSPlayer.Name
					});
					TSPlayer.All.SendSuccessMessage("{0} kisses {1}!", new object[]
					{
						args.Player.Name,
						tSPlayer.Name
					});
                    TShock.Log.Info("{0} kisses {1}!", new object[]
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
					args.Player.SendInfoMessage("You stabbed your invisible friend {0}!", new object[]
					{
						text
					});
					TSPlayer.All.SendSuccessMessage("{0} stabbed " + (args.Player.TPlayer.Male ? "his" : "her") + " invisible friend {1}!", new object[]
					{
						args.Player.Name,
						text
					});
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
					args.Player.SendInfoMessage("You stabbed {0} for OVER 9000 damage!", new object[]
					{
						tSPlayer.Name
					});
					TSPlayer.All.SendSuccessMessage("{0} stabbed {1} mercilessly!", new object[]
					{
						args.Player.Name,
						tSPlayer.Name
					});
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
						args.Player.SendInfoMessage("You love the air! {0} was not found...", new object[]
						{
							text
						});
						TSPlayer.All.SendSuccessMessage("{0} loves the air!", new object[]
						{
							args.Player.Name
						});
					}
					else
					{
						args.Player.SendInfoMessage("You love the air!", new object[]
						{
							text
						});
						TSPlayer.All.SendSuccessMessage("{0} loves the air!", new object[]
						{
							args.Player.Name
						});
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
					args.Player.SendInfoMessage("You love {0}!", new object[]
					{
						tSPlayer.Name
					});
					TSPlayer.All.SendSuccessMessage("{0} loves {1}!", new object[]
					{
						args.Player.Name,
						tSPlayer.Name
					});
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
				args.Player.SendInfoMessage("You planted your face on the ground.");
			}
			else
			{
				args.Player.DamagePlayer(1000);
			}
			TSPlayer.All.SendSuccessMessage("{0} planted " + (args.Player.TPlayer.Male ? "his" : "her") + " face on the ground.", new object[]
			{
				args.Player.Name
			});
		}

		private void FACSlap(CommandArgs args)
		{
			if (!args.Player.RealPlayer)
			{
				args.Player.SendInfoMessage("You slapped everyone! That stings!");
			}
			TSPlayer.All.SendInfoMessage("{0} slapped you (along with everyone else)!", new object[]
			{
				args.Player.Name
			});
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
				else if (list[0].Group.Name == "superadmin" | list[0].Group.Name == "owner")
                {
					args.Player.SendErrorMessage("You cannot disturb this player!");
					TSPlayer.All.SendSuccessMessage("{0} tried to disturb {1}! {1} dodged the attack!", new object[]
					{
						args.Player.Name,
						list[0].Name
					});
				}
				else
				{
					TSPlayer tSPlayer = list[0];
					tSPlayer.SetBuff(26, 3600, false);
					tSPlayer.SetBuff(30, 3600, false);
					tSPlayer.SetBuff(31, 3600, false);
					tSPlayer.SetBuff(32, 3600, false);
					tSPlayer.SetBuff(103, 3600, false);
					tSPlayer.SetBuff(115, 3600, false);
					tSPlayer.SetBuff(120, 3600, false);
					args.Player.SendInfoMessage("You disturbed {0}! You feel slightly better...", new object[]
					{
						tSPlayer.Name
					});
					TSPlayer.All.SendSuccessMessage("{0} disturbed {1}! Is he angry now?", new object[]
					{
						args.Player.Name,
						tSPlayer.Name
					});
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