﻿#region using directives

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PoGo.NecroBot.Logic.Logging;
using PoGo.NecroBot.Logic.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#endregion

namespace PoGo.NecroBot.Logic.Common
{
    public interface ITranslation
    {
        string GetTranslation(TranslationString translationString, params object[] data);

        string GetTranslation(TranslationString translationString);

        string GetPokemonTranslation(POGOProtos.Enums.PokemonId id);
    }

    public enum TranslationString
    {
        Pokeball,
        GreatPokeball,
        UltraPokeball,
        MasterPokeball,
        LogLevelDebug,
        LogLevelPokestop,
        WrongAuthType,
        LoginInvalid,
        FarmPokestopsOutsideRadius,
        FarmPokestopsNoUsableFound,
        EventFortUsed,
        EventFortFailed,
        EventFortTargeted,
        EventProfileLogin,
        EventUsedLuckyEgg,
        EventPokemonEvolvedSuccess,
        EventPokemonEvolvedFailed,
        EventPokemonTransferred,
        EventItemRecycled,
        EventPokemonCapture,
        EventNoPokeballs,
        WaitingForMorePokemonToEvolve,
        UseLuckyEggsMinPokemonAmountTooHigh,
        CatchMorePokemonToUseLuckyEgg,
        EventUseBerry,
        ItemRazzBerry,
        CatchStatusAttempt,
        CatchStatus,
        Candies,
        UnhandledGpxData,
        DisplayHighestsHeader,
        CommonWordPerfect,
        CommonWordName,
        DisplayHighestsCpHeader,
        DisplayHighestsPerfectHeader,
        WelcomeWarning,
        IncubatorPuttingEgg,
        IncubatorStatusUpdate,
        DisplayHighestsLevelHeader,
        LogEntryError,
        LogEntryAttention,
        LogEntryInfo,
        LogEntryPokestop,
        LogEntryFarming,
        LogEntrySniper,
        LogEntryRecycling,
        LogEntryPkmn,
        LogEntryTransfered,
        LogEntryEvolved,
        LogEntryBerry,
        LogEntryEgg,
        LogEntryDebug,
        LogEntryUpdate,
        LoggingIn,
        PtcOffline,
        TryingAgainIn,
        AccountNotVerified,
        CommonWordUnknown,
        OpeningGoogleDevicePage,
        CouldntCopyToClipboard,
        CouldntCopyToClipboard2,
        RealisticTravelDetected,
        NotRealisticTravel,
        CoordinatesAreInvalid,
        GotUpToDateVersion,
        AutoUpdaterDisabled,
        DownloadingUpdate,
        FinishedDownloadingRelease,
        FinishedUnpackingFiles,
        FinishedTransferringConfig,
        UpdateFinished,
        LookingForIncensePokemon,
        PokemonSkipped,
        ZeroPokeballInv,
        CurrentPokeballInv,
        MaxItemsCombinedOverMaxItemStorage,
        RecyclingQuietly,
        InvFullTransferring,
        InvFullTransferManually,
        InvFullPokestopLooting,
        IncubatorEggHatched,
        EncounterProblem,
        EncounterProblemLurePokemon,
        LookingForPokemon,
        LookingForLurePokemon,
        DesiredDestTooFar,
        PokemonRename,
        PokemonFavorite,
        PokemonIgnoreFilter,
        CatchStatusError,
        CatchStatusEscape,
        CatchStatusFlee,
        CatchStatusMissed,
        CatchStatusSuccess,
        CatchTypeNormal,
        CatchTypeLure,
        CatchTypeIncense,
        WebSocketFailStart,
        StatsTemplateString,
        StatsXpTemplateString,
        RequireInputText,
        GoogleTwoFactorAuth,
        GoogleTwoFactorAuthExplanation,
        GoogleError,
        MissingCredentialsGoogle,
        MissingCredentialsPtc,
        SnipeScan,
        SnipeScanEx,
        NoPokemonToSnipe,
        NotEnoughPokeballsToSnipe,
        DisplayHighestMove1Header,
        DisplayHighestMove2Header,
        DisplayHighestCandy,
        IPBannedError,
        NoEggsAvailable,
        UseLuckyEggActive,
        UsedLuckyEgg,
        UseLuckyEggAmount,
        NoIncenseAvailable,
        UseIncenseActive,
        UseIncenseAmount,
        UsedIncense,
        AmountPkmSeenCaught,
        PkmPotentialEvolveCount,
        PkmNotEnoughRessources,
        EventUsedIncense,
        SnipeServerOffline,
    }

    public class Translation : ITranslation
    {
        [JsonProperty("TranslationStrings",
            ItemTypeNameHandling = TypeNameHandling.Arrays,
            ItemConverterType = typeof(KeyValuePairConverter),
            ObjectCreationHandling = ObjectCreationHandling.Replace,
            DefaultValueHandling = DefaultValueHandling.Populate)]
        //Default Translations (ENGLISH)        
        private readonly List<KeyValuePair<TranslationString, string>> _translationStrings = new List
            <KeyValuePair<TranslationString, string>>
        {
            new KeyValuePair<TranslationString, string>(TranslationString.Pokeball, "PokeBall"),
            new KeyValuePair<TranslationString, string>(TranslationString.GreatPokeball, "GreatBall"),
            new KeyValuePair<TranslationString, string>(TranslationString.UltraPokeball, "UltraBall"),
            new KeyValuePair<TranslationString, string>(TranslationString.MasterPokeball, "MasterBall"),
            new KeyValuePair<TranslationString, string>(TranslationString.WrongAuthType,
                "Unknown AuthType in config.json"),
            new KeyValuePair<TranslationString, string>(TranslationString.LoginInvalid, "User credentials are invalid and login failed."),
            new KeyValuePair<TranslationString, string>(TranslationString.FarmPokestopsOutsideRadius,
                "You're outside of your defined radius! Walking to start ({0}m away) in 5 seconds. Is your LastPos.ini file correct?"),
            new KeyValuePair<TranslationString, string>(TranslationString.FarmPokestopsNoUsableFound,
                "No usable PokeStops found in your area. Is your maximum distance too small?"),
            new KeyValuePair<TranslationString, string>(TranslationString.EventFortUsed,
                "Name: {0} XP: {1}, Gems: {2}, Items: {3}, Lat: {4}, Long: {5}"),
            new KeyValuePair<TranslationString, string>(TranslationString.EventFortFailed,
                "Name: {0} INFO: Looting failed, possible softban. Unban in: {1}/{2}"),
            new KeyValuePair<TranslationString, string>(TranslationString.EventFortTargeted,
                "Traveling to Pokestop: {0} ({1}m)({2}seconds)"),
            new KeyValuePair<TranslationString, string>(TranslationString.EventProfileLogin, "Playing as {0}"),
            new KeyValuePair<TranslationString, string>(TranslationString.EventUsedIncense,
                "Used Incense, remaining: {0}"),
            new KeyValuePair<TranslationString, string>(TranslationString.EventUsedLuckyEgg,
                "Used Lucky Egg, remaining: {0}"),
            new KeyValuePair<TranslationString, string>(TranslationString.EventPokemonEvolvedSuccess,
                "{0} successfully for {1}xp"),
            new KeyValuePair<TranslationString, string>(TranslationString.EventPokemonEvolvedFailed,
                "Failed {0}. Result was {1}, stopping evolving {2}"),
            new KeyValuePair<TranslationString, string>(TranslationString.EventPokemonTransferred,
                "{0}\t- CP: {1}  IV: {2}%   [Best CP: {3}  IV: {4}%] (Candies: {5})"),
            new KeyValuePair<TranslationString, string>(TranslationString.EventItemRecycled, "{0}x {1}"),
            new KeyValuePair<TranslationString, string>(TranslationString.EventPokemonCapture,
                "({0}) | ({1}) {2} Lvl: {3} CP: ({4}/{5}) IV: {6}% | Chance: {7}% | {8}m dist | with a {9} ({10} left). | {11} | lat: {12} long: {13}"),
            new KeyValuePair<TranslationString, string>(TranslationString.EventNoPokeballs,
                "No Pokeballs - We missed a {0} with CP {1}"),
            new KeyValuePair<TranslationString, string>(TranslationString.WaitingForMorePokemonToEvolve,
                "Waiting to evolve {0} Pokemon once {1} more are caught! ({2}/{3} for {4}% inventory)"),
            new KeyValuePair<TranslationString, string>(TranslationString.UseLuckyEggsMinPokemonAmountTooHigh,
                "Lucky eggs will never be used with UseLuckyEggsMinPokemonAmount set to {0}, use <= {1} instead"),
            new KeyValuePair<TranslationString, string>(TranslationString.CatchMorePokemonToUseLuckyEgg,
                "Catch {0} more Pokemon to use a Lucky Egg!"),
            new KeyValuePair<TranslationString, string>(TranslationString.EventUseBerry, "Used {0} | {1} remaining"),
            new KeyValuePair<TranslationString, string>(TranslationString.ItemRazzBerry, "Razz Berry"),
            new KeyValuePair<TranslationString, string>(TranslationString.CatchStatusAttempt, "{0} Attempt #{1}"),
            new KeyValuePair<TranslationString, string>(TranslationString.CatchStatus, "{0}"),
            new KeyValuePair<TranslationString, string>(TranslationString.Candies, "Candies: {0}"),
            new KeyValuePair<TranslationString, string>(TranslationString.UnhandledGpxData,
                "Unhandled data in GPX file, attempting to skip."),
            new KeyValuePair<TranslationString, string>(TranslationString.DisplayHighestsHeader, "Pokemons"),
            new KeyValuePair<TranslationString, string>(TranslationString.CommonWordPerfect, "perfect"),
            new KeyValuePair<TranslationString, string>(TranslationString.CommonWordName, "name"),
            new KeyValuePair<TranslationString, string>(TranslationString.CommonWordUnknown, "Unknown"),
            new KeyValuePair<TranslationString, string>(TranslationString.DisplayHighestsCpHeader, "DisplayHighestsCP"),
            new KeyValuePair<TranslationString, string>(TranslationString.DisplayHighestsPerfectHeader,
                "DisplayHighestsPerfect"),
            new KeyValuePair<TranslationString, string>(TranslationString.DisplayHighestsLevelHeader,
                "DisplayHighestsLevel"),
            new KeyValuePair<TranslationString, string>(TranslationString.WelcomeWarning,
                "Make sure Lat & Lng are right. Exit Program if not! Lat: {0} Lng: {1}"),
            new KeyValuePair<TranslationString, string>(TranslationString.IncubatorPuttingEgg,
                "Putting egg in incubator: {0:0.00}km left"),
            new KeyValuePair<TranslationString, string>(TranslationString.IncubatorStatusUpdate,
                "Incubator status update: {0:0.00}km left"),
            new KeyValuePair<TranslationString, string>(TranslationString.IncubatorEggHatched,
                "Incubated egg has hatched: {0} | Lvl: {1} CP: ({2}/{3}) IV: {4}%"),
            new KeyValuePair<TranslationString, string>(TranslationString.LogEntryError, "ERROR"),
            new KeyValuePair<TranslationString, string>(TranslationString.LogEntryAttention, "ATTENTION"),
            new KeyValuePair<TranslationString, string>(TranslationString.LogEntryInfo, "INFO"),
            new KeyValuePair<TranslationString, string>(TranslationString.LogEntryPokestop, "POKESTOP"),
            new KeyValuePair<TranslationString, string>(TranslationString.LogEntryFarming, "FARMING"),
            new KeyValuePair<TranslationString, string>(TranslationString.LogEntrySniper, "SNIPER"),
            new KeyValuePair<TranslationString, string>(TranslationString.LogEntryRecycling, "RECYCLING"),
            new KeyValuePair<TranslationString, string>(TranslationString.LogEntryPkmn, "PKMN"),
            new KeyValuePair<TranslationString, string>(TranslationString.LogEntryTransfered, "TRANSFERED"),
            new KeyValuePair<TranslationString, string>(TranslationString.LogEntryEvolved, "EVOLVED"),
            new KeyValuePair<TranslationString, string>(TranslationString.LogEntryBerry, "BERRY"),
            new KeyValuePair<TranslationString, string>(TranslationString.LogEntryEgg, "EGG"),
            new KeyValuePair<TranslationString, string>(TranslationString.LogEntryDebug, "DEBUG"),
            new KeyValuePair<TranslationString, string>(TranslationString.LogEntryUpdate, "UPDATE"),
            new KeyValuePair<TranslationString, string>(TranslationString.LoggingIn, "Logging in using {0}"),
            new KeyValuePair<TranslationString, string>(TranslationString.PtcOffline,
                "PTC Servers are probably down OR your credentials are wrong. Try google"),
            new KeyValuePair<TranslationString, string>(TranslationString.TryingAgainIn,
                "Trying again in {0} seconds..."),
            new KeyValuePair<TranslationString, string>(TranslationString.AccountNotVerified,
                "Account not verified! Exiting..."),
            new KeyValuePair<TranslationString, string>(TranslationString.OpeningGoogleDevicePage,
                "Opening Google Device page. Please paste the code using CTRL+V"),
            new KeyValuePair<TranslationString, string>(TranslationString.CouldntCopyToClipboard,
                "Couldnt copy to clipboard, do it manually"),
            new KeyValuePair<TranslationString, string>(TranslationString.CouldntCopyToClipboard2,
                "Goto: {0} & enter {1}"),
            new KeyValuePair<TranslationString, string>(TranslationString.RealisticTravelDetected,
                "Detected realistic Traveling , using Default Settings inside config.json"),
            new KeyValuePair<TranslationString, string>(TranslationString.NotRealisticTravel,
                "Not realistic Traveling at {0}, using last saved LastPos.ini"),
            new KeyValuePair<TranslationString, string>(TranslationString.CoordinatesAreInvalid,
                "Coordinates in \"LastPos.ini\" file are invalid, using the default coordinates"),
            new KeyValuePair<TranslationString, string>(TranslationString.GotUpToDateVersion,
                "Perfect! You already have the newest Version {0}"),
            new KeyValuePair<TranslationString, string>(TranslationString.AutoUpdaterDisabled,
                "AutoUpdater is disabled. Get the latest release from: {0}\n "),
            new KeyValuePair<TranslationString, string>(TranslationString.DownloadingUpdate,
                "Downloading and apply Update..."),
            new KeyValuePair<TranslationString, string>(TranslationString.FinishedDownloadingRelease,
                "Finished downloading newest Release..."),
            new KeyValuePair<TranslationString, string>(TranslationString.FinishedUnpackingFiles,
                "Finished unpacking files..."),
            new KeyValuePair<TranslationString, string>(TranslationString.FinishedTransferringConfig,
                "Finished transferring your config to the new version..."),
            new KeyValuePair<TranslationString, string>(TranslationString.UpdateFinished,
                "Update finished, you can close this window now."),
            new KeyValuePair<TranslationString, string>(TranslationString.LookingForIncensePokemon,
                "Looking for incense Pokemon..."),
            new KeyValuePair<TranslationString, string>(TranslationString.LookingForPokemon, "Looking for Pokemon..."),
            new KeyValuePair<TranslationString, string>(TranslationString.LookingForLurePokemon,
                "Looking for lure Pokemon..."),
            new KeyValuePair<TranslationString, string>(TranslationString.PokemonSkipped, "Skipped {0}"),
            new KeyValuePair<TranslationString, string>(TranslationString.ZeroPokeballInv,
                "You have no pokeballs in your inventory, no more Pokemon can be caught!"),
            new KeyValuePair<TranslationString, string>(TranslationString.CurrentPokeballInv,
                "[Current Inventory] Pokeballs: {0} | Greatballs: {1} | Ultraballs: {2} | Masterballs: {3}"),
            new KeyValuePair<TranslationString, string>(TranslationString.MaxItemsCombinedOverMaxItemStorage,
                "[Configuration Invalid] Your maximum items combined (balls+potions+revives={0}) is over your max item storage ({1})"),
            new KeyValuePair<TranslationString, string>(TranslationString.RecyclingQuietly, "Recycling Quietly..."),
            new KeyValuePair<TranslationString, string>(TranslationString.InvFullTransferring,
                "Pokemon Inventory is full, transferring Pokemon..."),
            new KeyValuePair<TranslationString, string>(TranslationString.InvFullTransferManually,
                "Pokemon Inventory is full! Please transfer Pokemon manually or set TransferDuplicatePokemon to true in settings..."),
            new KeyValuePair<TranslationString, string>(TranslationString.InvFullPokestopLooting,
                "Inventory is full, no items looted!"),
            new KeyValuePair<TranslationString, string>(TranslationString.EncounterProblem, "Encounter problem: {0}"),
            new KeyValuePair<TranslationString, string>(TranslationString.EncounterProblemLurePokemon,
                "Encounter problem: Lure Pokemon {0}"),
            new KeyValuePair<TranslationString, string>(TranslationString.DesiredDestTooFar,
                "Your desired destination of {0}, {1} is too far from your current position of {2}, {3}"),
            new KeyValuePair<TranslationString, string>(TranslationString.PokemonRename,
                "Pokemon {0} ({1}) renamed from {2} to {3}."),
            new KeyValuePair<TranslationString, string>(TranslationString.PokemonFavorite,
                "{0}% perfect {1} (CP {2}) *favorited*."),
            new KeyValuePair<TranslationString, string>(TranslationString.PokemonIgnoreFilter,
                "[Pokemon ignore filter] - Ignoring {0} as defined in settings"),
            new KeyValuePair<TranslationString, string>(TranslationString.CatchStatusAttempt, "CatchAttempt"),
            new KeyValuePair<TranslationString, string>(TranslationString.CatchStatusError, "CatchError"),
            new KeyValuePair<TranslationString, string>(TranslationString.CatchStatusEscape, "CatchEscape"),
            new KeyValuePair<TranslationString, string>(TranslationString.CatchStatusFlee, "CatchFlee"),
            new KeyValuePair<TranslationString, string>(TranslationString.CatchStatusMissed, "CatchMissed"),
            new KeyValuePair<TranslationString, string>(TranslationString.CatchStatusSuccess, "CatchSuccess"),
            new KeyValuePair<TranslationString, string>(TranslationString.CatchTypeNormal, "Normal"),
            new KeyValuePair<TranslationString, string>(TranslationString.CatchTypeLure, "Lure"),
            new KeyValuePair<TranslationString, string>(TranslationString.CatchTypeIncense, "Incense"),
            new KeyValuePair<TranslationString, string>(TranslationString.WebSocketFailStart,
                "Failed to start WebSocketServer on port : {0}"),
            new KeyValuePair<TranslationString, string>(TranslationString.StatsTemplateString,
                "{0} - Runtime {1} - Lvl: {2} | EXP/H: {3:n0} | P/H: {4:n0} | Stardust: {5:n0} | Transfered: {6:n0} | Recycled: {7:n0}"),
            new KeyValuePair<TranslationString, string>(TranslationString.StatsXpTemplateString,
                "{0} (Advance in {1}h {2}m | {3:n0}/{4:n0} XP)"),
            new KeyValuePair<TranslationString, string>(TranslationString.RequireInputText,
                "Program will continue after the key press..."),
            new KeyValuePair<TranslationString, string>(TranslationString.GoogleTwoFactorAuth,
                "As you have Google Two Factor Auth enabled, you will need to insert an App Specific Password into the auth.json"),
            new KeyValuePair<TranslationString, string>(TranslationString.GoogleTwoFactorAuthExplanation,
                "Opening Google App-Passwords. Please make a new App Password (use Other as Device)"),
            new KeyValuePair<TranslationString, string>(TranslationString.GoogleError,
                "Make sure you have entered the right Email & Password."),
            new KeyValuePair<TranslationString, string>(TranslationString.MissingCredentialsGoogle,
                "You need to fill out GoogleUsername and GooglePassword in auth.json!"),
            new KeyValuePair<TranslationString, string>(TranslationString.MissingCredentialsPtc,
                "You need to fill out PtcUsername and PtcPassword in auth.json!"),
            new KeyValuePair<TranslationString, string>(TranslationString.SnipeScan,
                "Scanning for Snipeable Pokemon at {0}..."),
            new KeyValuePair<TranslationString, string>(TranslationString.SnipeScanEx,
                "Sniping a {0} with {1} IV at {2}..."),
            new KeyValuePair<TranslationString, string>(TranslationString.NoPokemonToSnipe,
                "No Pokemon found to snipe!"),
            new KeyValuePair<TranslationString, string>(TranslationString.NotEnoughPokeballsToSnipe,
                "Not enough Pokeballs to start sniping! ({0}/{1})"),
            new KeyValuePair<TranslationString, string>(TranslationString.DisplayHighestMove1Header, "MOVE1"),
            new KeyValuePair<TranslationString, string>(TranslationString.DisplayHighestMove2Header, "MOVE2"),
            new KeyValuePair<TranslationString, string>(TranslationString.DisplayHighestCandy, "Candy"),
            new KeyValuePair<TranslationString, string>(TranslationString.IPBannedError,
                "Connection refused. Your IP might have been Blacklisted by Niantic. Exiting.."),
            new KeyValuePair<TranslationString, string>(TranslationString.NoEggsAvailable, "No Eggs Available"),
            new KeyValuePair<TranslationString, string>(TranslationString.UseLuckyEggActive, "Lucky Egg Already Active"),
            new KeyValuePair<TranslationString, string>(TranslationString.UsedLuckyEgg, "Used Lucky Egg"),
            new KeyValuePair<TranslationString, string>(TranslationString.UseLuckyEggAmount, "Lucky Eggs in Inventory: {0}"),
            new KeyValuePair<TranslationString, string>(TranslationString.NoIncenseAvailable, "No Incense Available"),
            new KeyValuePair<TranslationString, string>(TranslationString.UseIncenseActive, "Incense Already Active"),
            new KeyValuePair<TranslationString, string>(TranslationString.UseIncenseAmount, "Incense in Inventory: {0}"),
            new KeyValuePair<TranslationString, string>(TranslationString.UsedIncense, "Used an Incense"),
            new KeyValuePair<TranslationString, string>(TranslationString.AmountPkmSeenCaught,
                "Amount of Pokemon Seen: {0}/151, Amount of Pokemon Caught: {1}/151"),
            new KeyValuePair<TranslationString, string>(TranslationString.PkmPotentialEvolveCount,
                "[Evolves] Potential Evolves: {0}"),
            new KeyValuePair<TranslationString, string>(TranslationString.PkmNotEnoughRessources,
                "Pokemon Upgrade Failed Not Enough Resources"),
            new KeyValuePair<TranslationString, string>(TranslationString.SnipeServerOffline, "Sniping server is offline. Skipping...")
        };

        [JsonProperty("PokemonStrings",
            ItemTypeNameHandling = TypeNameHandling.Arrays,
            ItemConverterType = typeof(KeyValuePairConverter),
            ObjectCreationHandling = ObjectCreationHandling.Replace,
            DefaultValueHandling = DefaultValueHandling.Populate)]
        private readonly List<KeyValuePair<POGOProtos.Enums.PokemonId, string>> _pokemonTranslationStrings = new List<KeyValuePair<POGOProtos.Enums.PokemonId, string>>()
        {
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)001,"Bulbasaur"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)002,"Ivysaur"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)003,"Venusaur"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)004,"Charmander"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)005,"Charmeleon"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)006,"Charizard"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)007,"Squirtle"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)008,"Wartortle"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)009,"Blastoise"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)010,"Caterpie"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)011,"Metapod"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)012,"Butterfree"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)013,"Weedle"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)014,"Kakuna"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)015,"Beedrill"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)016,"Pidgey"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)017,"Pidgeotto"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)018,"Pidgeot"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)019,"Rattata"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)020,"Raticate"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)021,"Spearow"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)022,"Fearow"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)023,"Ekans"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)024,"Arbok"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)025,"Pikachu"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)026,"Raichu"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)027,"Sandshrew"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)028,"Sandslash"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)029,"NidoranF"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)030,"Nidorina"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)031,"Nidoqueen"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)032,"NidoranM"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)033,"Nidorino"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)034,"Nidoking"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)035,"Clefairy"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)036,"Clefable"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)037,"Vulpix"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)038,"Ninetales"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)039,"Jigglypuff"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)040,"Wigglytuff"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)041,"Zubat"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)042,"Golbat"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)043,"Oddish"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)044,"Gloom"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)045,"Vileplume"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)046,"Paras"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)047,"Parasect"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)048,"Venonat"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)049,"Venomoth"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)050,"Diglett"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)051,"Dugtrio"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)052,"Meowth"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)053,"Persian"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)054,"Psyduck"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)055,"Golduck"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)056,"Mankey"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)057,"Primeape"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)058,"Growlithe"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)059,"Arcanine"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)060,"Poliwag"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)061,"Poliwhirl"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)062,"Poliwrath"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)063,"Abra"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)064,"Kadabra"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)065,"Alakazam"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)066,"Machop"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)067,"Machoke"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)068,"Machamp"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)069,"Bellsprout"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)070,"Weepinbell"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)071,"Victreebel"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)072,"Tentacool"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)073,"Tentacruel"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)074,"Geodude"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)075,"Graveler"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)076,"Golem"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)077,"Ponyta"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)078,"Rapidash"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)079,"Slowpoke"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)080,"Slowbro"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)081,"Magnemite"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)082,"Magneton"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)083,"Farfetchd"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)084,"Doduo"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)085,"Dodrio"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)086,"Seel"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)087,"Dewgong"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)088,"Grimer"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)089,"Muk"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)090,"Shellder"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)091,"Cloyster"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)092,"Gastly"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)093,"Haunter"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)094,"Gengar"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)095,"Onix"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)096,"Drowzee"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)097,"Hypno"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)098,"Krabby"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)099,"Kingler"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)100,"Voltorb"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)101,"Electrode"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)102,"Exeggcute"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)103,"Exeggutor"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)104,"Cubone"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)105,"Marowak"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)106,"Hitmonlee"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)107,"Hitmonchan"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)108,"Lickitung"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)109,"Koffing"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)110,"Weezing"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)111,"Rhyhorn"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)112,"Rhydon"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)113,"Chansey"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)114,"Tangela"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)115,"Kangaskhan"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)116,"Horsea"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)117,"Seadra"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)118,"Goldeen"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)119,"Seaking"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)120,"Staryu"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)121,"Starmie"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)122,"Mr. Mime"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)123,"Scyther"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)124,"Jynx"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)125,"Electabuzz"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)126,"Magmar"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)127,"Pinsir"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)128,"Tauros"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)129,"Magikarp"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)130,"Gyarados"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)131,"Lapras"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)132,"Ditto"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)133,"Eevee"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)134,"Vaporeon"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)135,"Jolteon"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)136,"Flareon"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)137,"Porygon"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)138,"Omanyte"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)139,"Omastar"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)140,"Kabuto"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)141,"Kabutops"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)142,"Aerodactyl"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)143,"Snorlax"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)144,"Articuno"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)145,"Zapdos"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)146,"Moltres"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)147,"Dratini"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)148,"Dragonair"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)149,"Dragonite"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)150,"Mewtwo"),
            new KeyValuePair<POGOProtos.Enums.PokemonId, string>((POGOProtos.Enums.PokemonId)151,"Mew"),
        };

        public string GetTranslation(TranslationString translationString, params object[] data)
        {
            var translation = _translationStrings.FirstOrDefault(t => t.Key.Equals(translationString)).Value;
            return translation != default(string)
                ? string.Format(translation, data)
                : $"Translation for {translationString} is missing";
        }

        public string GetTranslation(TranslationString translationString)
        {
            var translation = _translationStrings.FirstOrDefault(t => t.Key.Equals(translationString)).Value;
            return translation != default(string) ? translation : $"Translation for {translationString} is missing";
        }

        public string GetPokemonTranslation(POGOProtos.Enums.PokemonId id)
        {
            var translation = _pokemonTranslationStrings.FirstOrDefault(t => t.Key.Equals(id)).Value;
            return translation != default(string) ? translation : $"Translation for pokemon {id} is missing";
        }

        public static Translation Load(ILogicSettings logicSettings)
        {
            var translationsLanguageCode = logicSettings.TranslationLanguageCode;
            var translationPath = Path.Combine(logicSettings.GeneralConfigPath, "translations");
            var fullPath = Path.Combine(translationPath, "translation." + translationsLanguageCode + ".json");

            Translation translations;
            if (File.Exists(fullPath))
            {
                var input = File.ReadAllText(fullPath);

                var jsonSettings = new JsonSerializerSettings();
                jsonSettings.Converters.Add(new StringEnumConverter { CamelCaseText = true });
                jsonSettings.ObjectCreationHandling = ObjectCreationHandling.Replace;
                jsonSettings.DefaultValueHandling = DefaultValueHandling.Populate;

                try
                {
                    translations = JsonConvert.DeserializeObject<Translation>( input, jsonSettings );
                    //TODO make json to fill default values as it won't do it now
                    new Translation()._translationStrings.Where(
                        item => translations._translationStrings.All( a => a.Key != item.Key ) )
                        .ToList()
                        .ForEach( translations._translationStrings.Add );
                    new Translation()._pokemonTranslationStrings.Where(
                        item => translations._pokemonTranslationStrings.All( a => a.Key != item.Key ) )
                        .ToList()
                        .ForEach( translations._pokemonTranslationStrings.Add );
                }
                catch( JsonException )
                {
                    Logger.Write( "[ERROR] Issue loading translations", LogLevel.Error );

                    switch( translationsLanguageCode )
                    {
                        case "en":
                            Logger.Write( "[Request] Rebuild the translations folder? Y/N" );

                            string strInput = Console.ReadLine().ToLower();

                            if( strInput.Equals( "y" ) )
                            {
                                // Currently this section can only rebuild the EN translations file \\
                                // This is because default values cannot be supplied from other languages \\
                                Logger.Write( "Loading fresh translations and continuing" );
                                translations = new Translation();
                                translations.Save( Path.Combine( translationPath, "translation.en.json" ) );
                            }
                            else
                            {
                                ErrorHandler.ThrowFatalError( null, 3 );
                                return null;
                            }

                            break;
                        default:
                            ErrorHandler.ThrowFatalError( "[ERROR] No replacement translations: Check appropriate files for typos", 5 );
                            return null;
                    }
                }
            }
            else
            {
                translations = new Translation();
                translations.Save(Path.Combine(translationPath, "translation.en.json"));
            }

            return translations;
        }

        public void Save(string fullPath)
        {
            var output = JsonConvert.SerializeObject(this, Formatting.Indented,
                new StringEnumConverter { CamelCaseText = true });

            var folder = Path.GetDirectoryName(fullPath);
            if (folder != null && !Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            File.WriteAllText(fullPath, output);
        }
    }
}
