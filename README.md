# CloverPitExampleMod
Example Mod for CloverPit

## BEFORE YOU START
Make sure the libraries are up-to-date.  
I'll update this with new game and API versions regularly, but you will need to pull these (or manually update them) yourself.  
To manually update the API, copy the latest release of CloverAPI into `lib` (only the DLL, the rest is only needed for runtime).  
To manually update the game's assembly, use a tool like [publicize](https://github.com/jacobEAdamson/publicize/releases/tag/v1.0.0) or [NStrip](https://github.com/bbepis/NStrip) to strip all contents from `Assembly-CSharp.dll` and make all members public, then copy it into `lib`.  
DO NOT USE THE UNSTRIPPED ASSEMBLY! Sharing the unstripped assembly is ILLEGAL and you WILL BE SENT TO JAIL.  
Even if you don't share it, it's still not a good idea to use it when modding because it won't give you access to private members.

## Features
- Basic mod structure, including patching, logging, and config handling
- Adds three example charms, which include custom strings for the third one
That's it for now, more features will be added in the future.

## How to use
1. Clone the repository
2. Open the solution in Visual Studio, Rider, or your IDE of choice
3. Restore NuGet packages (`dotnet restore` in the terminal should work). Your IDE might do this automatically.
4. Do some modding
5. Build the project (Release mode is recommended)
6. Copy the DLL from `bin` to `{CloverPitInstallDir}/BepInEx/plugins` ("Browse local files" in Steam)
7. Put any data files (like images and asset bundles) in the data folders defined inside `Plugin.cs`

## How do I do modding?
- See the [BepInEx](https://docs.bepinex.dev/api/index.html) and [Harmony](https://harmony.pardeike.net/articles/intro.html) docs for the basics of modding.
- See the [CloverAPI documentation](https://ingoh.net/cloverapi/) for more information on how to use the API.
- Ask around in the [CloverPit Discord](https://discord.gg/zTAZ9erd5g)'s modding channel if you need help.
- If you're desperate, shout into the void. It won't hear you, but it's fun.

## License
This is an example mod. Do whatever you want with it. No rights reserved, no warranty, CC0, public domain, etc.