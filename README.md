# SimpleSteamServerQuery
This is a C# project that will produce a command line executable to get basic server information.

When run from the command line, you will need to include the correct parameters
Example:
    SteamServerQueryPing.exe [Server IP Address] [Steam Server Query Port] [Optional Times To Loop]
Definitions:
    [Server IP Address] must be a valid string representation for the IPv4 address of the server."
    [Steam Server Query Port] must be a valid string representation for the Steam Server Query Port of the server (usually game port + 1).
    [Times To Loop] is optional, will be used as the number of times to ping the server. Will ping indefinately if omitted or a negitive integer is given.
    
Console Output:
    Date Time: Server Name, Game/Mission Name, Map, Game Server IP, Game Server Port, Current Players/Max Players, Game Server Version, Ping Time (Milliseconds)
    