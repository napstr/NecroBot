<p align="center">
  <a href="http://necrobot.io">
    <img alt="Logo" src="http://necrobot.io/img/typotype-transparent.png" width="600">
  </a>
</p>

[![Discord](https://img.shields.io/badge/discord-NECROBOT.IO-blue.svg)](https://discord.gg/VXKxNFr)
[![Build status](https://ci.appveyor.com/api/projects/status/8ijvvr6b7fe2h0w2?svg=true)](https://ci.appveyor.com/project/NecronomiconCoding/necrobot)
[![Stories in Ready](https://badge.waffle.io/NECROBOTIO/NecroBot.svg?label=ready&title=Ready)](https://waffle.io/NecronomiconCoding/NecroBot)
[![Github All Releases](https://img.shields.io/github/downloads/NECROBOTIO/NecroBot/total.svg)](https://github.com/NECROBOTIO/NecroBot/releases)
[![GitHub license](https://img.shields.io/badge/license-AGPL-blue.svg)](https://raw.githubusercontent.com/NecronomiconCoding/NecroBot/master/LICENSE.md)
[![Twitter Follow](https://img.shields.io/twitter/follow/NECROBOTIO.svg?style=social&label=Follow&maxAge=1)](https://twitter.com/NECROBOTIO)

Discord (Chat & Get Help!): https://discord.gg/VXKxNFr <br/>
<br/>

## NecroBot - the original For The Community, By The Community bot
We are proud to announce the release of 0.7.6, with special thanks to Kevin and the entire crew involved at PGD!

##IMPORTANT
Add the following to your auth.json file to not overwrite your current custom device information:
"DevicePackageName": "custom",

## This release features automatic random device generation!
You can now opt to leave the device values static and your device will not be the same as everyone elses. If you prefer to use your own device, you can do that as well! 

To change the device information in /Config/auth.json to YOUR personal device, you will need android-sdk with adb (and drivers for your phone). Here are the correlating values with the command to run to grab the value off your phone:<br>
"DeviceId": "", // adb.exe shell settings get secure android_id<br>
"AndroidBoardName": "", // adb.exe shell getprop ro.product.board<br>
"AndroidBootloader": "", // adb.exe shell getprop ro.boot.bootloader<br>
"DeviceBrand": "", // adb.exe shell getprop ro.product.brand<br>
"DeviceModel": "", // adb.exe shell getprop ro.product.model<br>
"DeviceModelIdentifier": "", // adb.exe shell getprop ro.product.name<br>
"DeviceModelBoot": "qcom",<br>
"HardwareManufacturer": "", // adb.exe shell getprop ro.product.manufacturer<br>
"HardwareModel": "", // adb.exe shell getprop ro.product.model<br>
"FirmwareBrand": "", // adb.exe shell getprop ro.product.name<br>
"FirmwareTags": "", // adb.exe shell getprop ro.build.tags<br>
"FirmwareType": "", // adb.exe shell getprop ro.build.type<br>
"FirmwareFingerprint": "" // adb.exe shell getprop ro.build.fingerprint

<h2><a name="features">Features</a></h2>

 - [PTC Login / Google]
 - [Get Map Objects and Inventory]
 - [Search for gyms/pokestops/spawns]
 - [Farm pokestops]
 - [Farm all Pokemon in neighbourhood]
 - [Throw Berries/use best pokeball]
 - [Transfers duplicate pokemons]
 - [Evolve all pokemons]
 - [Throws away unneeded items]
 - [Humanlike Walking]
 - [Configurable Custom Pathing]
 - [Softban bypass]
 - [AutoUpdate / VersionCheck]
 - [Multilanguage Support]
 - [Use lucky egg while evolve]
 - [Egg Hatching Automatically]
 - [Multi bot support]
 - [Snipe pokemon]
 - [Power-Up pokemon]

<h2><a name="getting-started">Getting Started</a></h2>
Make sure you check out our [Wiki](https://github.com/NecronomiconCoding/NecroBot/wiki) to get started.
<br/>

<h2><a name="donating">Donating</a></h2>
<a name="paypal">Feel free to buy us all a beer, by using PayPal:</a><br/>
[![Donate](https://www.paypalobjects.com/en_US/i/btn/btn_donate_LG.gif)](https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business=farhaninoor1%40gmail%2ecom&lc=GB&item_name=NecroBot%20Donations&item_number=POGO&no_note=0&currency_code=USD&bn=PP%2dDonationsBF%3abtn_donateCC_LG_global%2egif%3aNonHostedGuest)<br/>

<a name="btc">or by using Bitcoin: *1LxBH4FHhwyEuL1eHbMvLiGsTiBrzjfa1C*</a><br/>

<h6><em>[ A big part of the Donations goes to the top 3 active Contributors. Other expenses as Server and License costs will be covered using the Donations. ]</em></h6><br/>

<a name="btc">Donate Bitcoins to FeroxRev (the API library developer): *1ExYxfBb5cERHyAfqtFscJW7vm2vWBbL3e*</a><br/>

<h2><a name="credits">Credits</a></h2>
A big thank you goes to Feroxs' hard work on the API & Console. Without him, this would not have been possible. <3
<br/>
Thanks to everyone who volunteered by contributing via Pull Requests!

<h2><a name="legal">Legal</a></h2>

This Website and Project is in no way affiliated with, authorized, maintained, sponsored or endorsed by Niantic, The Pokémon Company, Nintendo or any of its affiliates or subsidiaries. This is an independent and unofficial API for educational use ONLY. 
Using the Project might be against the TOS

Inquiries: contact@necrobot.io

<hr/>
