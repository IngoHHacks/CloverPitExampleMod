# CloverPitExampleMod
An example mod for CloverPit.

## BEFORE YOU START
- Make sure the libraries are up-to-date.
  - I'll update this with new game and API versions regularly, but you will need to pull these (or manually update them) yourself.  
- To manually update the API, copy the latest release of CloverAPI into `lib` (only the DLL, the rest is only needed for runtime).  
- To manually update the game's assembly, use a tool like [publicize](https://github.com/jacobEAdamson/publicize/releases/tag/v1.0.0) or [NStrip](https://github.com/bbepis/NStrip) to strip all contents from `Assembly-CSharp.dll` and make all members public, then copy it into `lib`.  
- If you are a little more advanced, and decide to use the unstripped (normal) `Assembly-CSharp.dll` straight from the game files, make sure you do not upload it or share it. That would be **PIRACY**, which is **ILLEGAL** - you can be fined and sent to jail so **please take this warning seriously**.
  - Also, for this slightly more advanced method, make sure you use a publicizer like [BepInEx.AssemblyPublicizer.MSBuild](https://github.com/BepInEx/BepInEx.AssemblyPublicizer/blob/master/README.md#from-msbuild).

## Features
- Basic mod structure, including patching, logging, and config handling
- Adds 3 example charms, including custom strings for the third one.

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
