﻿namespace UB3RB0T.Commands
{
    using System.Threading.Tasks;
    using Discord;

    [BotPermissions(ChannelPermission.AddReactions, "RequireReactionAdd")]
    public class QuickPollCommand : IDiscordCommand
    {
        public async Task<CommandResponse> Process(IDiscordBotContext context)
        {
            var guildChannel = context.GuildChannel;
            if (guildChannel == null)
            {
                return null;
            }

            var parts = context.Message.Content.Split(new[] { ' ' }, 2);
            if (parts.Length == 1)
            {
                return new CommandResponse { Text = "Ask a question for your poll, jeez" };
            }

            var currentPermissions = (guildChannel as ITextChannel).GetCurrentUserPermissions();

            if (!currentPermissions.AddReactions)
            {
                return new CommandResponse { Text = "oy barfbag I don't have permissions to add reactions in here." };
            }

            if (currentPermissions.UseExternalEmojis)
            {
                await context.Message.AddReactionAsync(Emote.Parse("<:check:363764608969867274>"));
                await context.Message.AddReactionAsync(Emote.Parse("<:xmark:363764632160043008>"));
            }
            else
            {
                await context.Message.AddReactionAsync(new Emoji("✅"));
                await context.Message.AddReactionAsync(new Emoji("❌"));
            }

            return null;
        }
    }
}
