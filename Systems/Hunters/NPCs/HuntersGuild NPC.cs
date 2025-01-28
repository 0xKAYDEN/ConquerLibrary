[NpcAttribute(NpcID.HuntersGuild)]
public static void HuntersGuild(Client.GameClient client, ServerSockets.Packet stream, byte Option, string Input, uint id)
{
    DateTime Now64 = DateTime.Now;
    Dialog data = new Dialog(client, stream);
    switch (Option)
    {
        case 0:
            {
                if (DeadRabbitProject.Hunters.HuntersRank.IsHunter(client.Player.UID))
                {
                    data.AddText("--------------------------------------------------------------------------------------------------------\n");
                    data.AddText("Hello Hunter " + client.Player.Name + ", Welcome to [" + Program.ServerConfig.ServerName + " Server] I will be glad to tell yo about this Event.\n");
                    data.AddText("This Event This our way to test our player's strength we hope you will enjoy it\n");
                    data.AddText("The rules are very easy hunt or be hunted\n");
                    data.AddText("The Story started when the war started between the realms, you hope you can protect our realm from outside monsters\n");
                    data.AddText("--------------------------------------------------------------------------------------------------------\n");
                    data.AddOption("Global Rank", 1);
                    data.AddOption("Give me Teleportation Item", 2);
                    data.AddOption("Just passing by..", 255);
                    data.AddAvatar(265).FinalizeDialog();
                }
                else
                {
                    data.AddText("--------------------------------------------------------------------------------------------------------\n");
                    data.AddText("Hello Dear [ " + client.Player.Name + " ], Welcome to [ " + Program.ServerConfig.ServerName + " Server] I will be glad to tell yo about this Event.\n");
                    data.AddText("This Event This our way to test our player's strength we hope you will enjoy it\n");
                    data.AddText("The rules are very easy hunt or be hunted\n");
                    data.AddText("The Story started when the war started between the realms, you hope you can protect our realm from outside monsters\n");
                    data.AddText("--------------------------------------------------------------------------------------------------------\n");
                    data.AddOption("I want to become a hunter", 3);
                    data.AddOption("Just passing by..", 255);
                    data.AddAvatar(265).FinalizeDialog();
                }
               
                break;
            }
        case 1:
            {
                // Combine the data into a list of objects for easier sorting
                var hunters = DeadRabbitProject.Hunters.HuntersRank.HunterName.Select((name, index) => new
                {
                    Name = name,
                    Rank = DeadRabbitProject.Hunters.HuntersRank.HunterRank[index],
                    Score = DeadRabbitProject.Hunters.HuntersRank.HunterScore[index],
                    Guild = DeadRabbitProject.Hunters.HuntersRank.HunterGuild[index]
                }).ToList();

                // Sort the hunters by score in descending order
                var sortedHunters = hunters.OrderByDescending(h => h.Score).Take(10).ToList();

                // Display the sorted hunters from 1 to 10
                data.AddText("--------------------------------------------------------------------------------------------------------\n");
                data.AddText(" Rank Number            Hunter Name          Hunter Score             HunterRank           Hunter Guild \n");
                data.AddText("--------------------------------------------------------------------------------------------------------\n");
                for (int i = 0; i < sortedHunters.Count; i++)
                {
                    var hunter = sortedHunters[i];
                    data.AddText($"{i + 1}\t{hunter.Name}\t\t{hunter.Score}\t{hunter.Rank}\t\t{hunter.Guild}");

                    break;
                }
                break;
            }       
        case 2:
            {
                if(client.Inventory.HaveSpace(1))
                {
                    client.Inventory.Add(stream, 720828, 1); // you give MemoryAgate to the player
                    client.CreateBoxDialog("Congratulation  " + client.Player.Name + ", check your Inventory \n");
                }
                else
                {
                    client.CreateBoxDialog("You Need at least one place in your Inventory");
                }       
                break;
            }
        case 3:
            {
                if(client.Inventory.Contain(112211,3)) //change item id As you like
                {
                    client.Inventory.Remove(112211, 3,stream); //change item id As you like
                    DeadRabbitProject.Hunters.HuntersRank.RegisterHunters(client.Player.UID, client.Player.Name, client.Player.BattlePower, client.Player.MyGuild.GuildName);
                    data.AddText("--------------------------------------------------------------------------------------------------------\n");
                    data.AddText("Congratulation  " + client.Player.Name + ", You have became a hunter now your rank is [ E ] to upgrade your rank you should kill any monster appears when you gates opens \n");
                    data.AddText("--------------------------------------------------------------------------------------------------------\n");
                    data.AddOption("Thank you very much", 255);
                    data.AddAvatar(265).FinalizeDialog();
                }
                else
                {
                    data.AddText("--------------------------------------------------------------------------------------------------------\n");
                    data.AddText("Dear  " + client.Player.Name + ", to became a Hunter you should have collected the 3 fragments from the monsters\n");
                    data.AddText("--------------------------------------------------------------------------------------------------------\n");
                    data.AddOption("I will think about that...", 255);
                    data.AddAvatar(265).FinalizeDialog();
                }                       
                break;
            }
    }