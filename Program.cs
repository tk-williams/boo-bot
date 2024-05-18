using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace boo_bot
{
    public class Program
    { 
        private DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services;

        //This is the program's entry point. It will create the program and then call the MainAsync method and wait for it to process.
        public static void Main(String[] args) => new Program().MainAsync().GetAwaiter().GetResult();

        private Program() {
            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                // How much logging do you want to see?
                LogLevel = LogSeverity.Info,
            
                // If you or another service needs to do anything with messages
                // (eg. checking Reactions, checking the content of edited/deleted messages),
                // you must set the MessageCacheSize. You may adjust the number as needed.
                //MessageCacheSize = 50,
            });
        
            _commands = new CommandService(new CommandServiceConfig
            {
                // Again, log level:
                LogLevel = LogSeverity.Info,
            
                // There's a few more properties you can set,
                // for example, case-insensitive commands.
                CaseSensitiveCommands = false,
            });

            _client.Log += Log;
            _commands.Log += Log;

            //Set up DI container
            //_services = ConfigureServices();
        }

        public async Task MainAsync() {

            await InitCommands();
            var token = File.ReadAllText("key.bbt");
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            await Task.Delay(-1);
        }

        private async Task InitCommands() {
            //await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
            await _commands.AddModuleAsync<SayModule>(_services);
            await _commands.AddModuleAsync<ReminderModule>(_services);
            await _commands.AddModuleAsync<PlayModule>(_services);
            await _commands.AddModuleAsync<HelpModule>(_services);
            await _commands.AddModuleAsync<StreamModule>(_services);

            _client.MessageReceived += HandleCommandAsync;
        }

        private async Task HandleCommandAsync(SocketMessage arg) {
            //Check if it is a system message and discard.
            var message = arg as SocketUserMessage;
            if (message == null) return;

            //Check if it is handling a message from itself or another bot and discard.
            if (message.Author.Id == _client.CurrentUser.Id || message.Author.IsBot) return;

            // Create a number to track where the command begins following the prefix '!'
            int pos = 0;
            if (message.HasCharPrefix('!', ref pos)) {
                //Create a Command Context
                var context = new SocketCommandContext(_client, message);

                //Execute the command
                var result = await _commands.ExecuteAsync(context, pos, _services);

                //Notify if failed
                if (!result.IsSuccess && result.Error != CommandError.UnknownCommand) {
                    await message.Channel.SendMessageAsync(result.ErrorReason);
                }
            }
        }

        private Task Log(LogMessage message) {
            Console.WriteLine(message.ToString());
            return Task.CompletedTask;
        }
    }
}
