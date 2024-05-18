using System;
using System.Text.RegularExpressions;
using Discord.Commands;
using System.Threading.Tasks;

namespace boo_bot {
    public class SayModule : ModuleBase<SocketCommandContext> {
        //!say : repeats the user's statement back to them
        [Command("say")]
        [Summary("Repeats a statement back to the user.")]
        public Task SayAsync([Remainder] [Summary("The text to echo")] string echo) => ReplyAsync(echo);
    }

    public class ReminderModule : ModuleBase<SocketCommandContext> {
        //!remind : Allows you to set a reminder message that will be DM'd to you at the time of your choosing.
        [Command("reminder")]
        [Summary("Sends a text reminder to the user through their DM's at a specified time.")]
        //public Task ReminderAsync([Remainder] [Summary("Test text")] string test) => ReplyAsync(test);

        public async Task RemidnerAsync() {
            await ReplyAsync("Please enter your message.");
            GatherMessage();
            await ReplyAsync("When would you like to be reminded?");
            GatherTime();
            await ReplyAsync("Gotcha!");
            Console.WriteLine("Completed");
            return;
        }

        private void GatherMessage() {

        }

        private void GatherTime() {
            StoreTime();
        }

        private void StoreTime() {

        }
    }

    public class PlayModule : ModuleBase<SocketCommandContext> {
        //!play : Lets you stream audio from a provided YouTube link through your currently occupied voice channel.
        [Command("play")]
        [Summary("Lets you stream audio from a provided YouTube link through your currently occupied voice channel.")]
        public Task ReminderAsync([Remainder] [Summary("Test text")] string test) => ReplyAsync(test);
    }

    public class HelpModule : ModuleBase<SocketCommandContext> {
        //!help : Displays a print-out of every command and an explaination of what they do.
        [Command("help")]
        [Summary("Displays a print-out of every command and an explaination of what they do.")]
        public Task ReminderAsync([Remainder] [Summary("Test text")] string test) => ReplyAsync(test);
    }

    public class StreamModule : ModuleBase<SocketCommandContext> {
        //!stream : Notifies if there is a Giant Bomb stream currently going on and, if yes, will link to it. 
        [Command("stream")]
        [Summary("Tells the user if there is a GiantBomb stream currently live.")]
        public Task ReminderAsync([Remainder] [Summary("Test text")] string test) => ReplyAsync(test);
    }
}